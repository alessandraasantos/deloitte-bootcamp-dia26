using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipamentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<EquipamentoMinas>> Post([FromBody] EquipamentoMinas equipamento)
    {
        equipamento.Codigo = equipamento.Codigo.Trim();

        if (await _context.Equipamentos
            .AnyAsync(e => e.Codigo == equipamento.Codigo))
        {
            return Conflict(new { mensagem = "Já existe equipamento com esse código." });
        }

        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = equipamento.Id }, equipamento);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(
        string? tipo,
        string? status,
        string? codigo,
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 100) pageSize = 10;

        var query = _context.Equipamentos.AsQueryable();

        if (!string.IsNullOrEmpty(tipo))
            query = query.Where(e => e.Tipo.ToString() == tipo);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(e => e.StatusOperacional.ToString() == status);

        if (!string.IsNullOrEmpty(codigo))
            query = query.Where(e => e.Codigo.Contains(codigo));

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { totalItems, page, pageSize, items });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipamentoMinas>> GetById(int id)
    {
        var eq = await _context.Equipamentos.FindAsync(id);
        if (eq == null) return NotFound();

        return Ok(eq);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EquipamentoMinas equipamento)
    {
        if (id != equipamento.Id)
            return BadRequest();

        equipamento.Codigo = equipamento.Codigo.Trim();

        if (await _context.Equipamentos
            .AnyAsync(e => e.Codigo == equipamento.Codigo && e.Id != id))
        {
            return Conflict(new { mensagem = "Código já existe." });
        }

        _context.Entry(equipamento).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eq = await _context.Equipamentos
            .Include(e => e.Manutencoes)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eq == null) return NotFound();

        bool temConcluida = eq.Manutencoes
            .Any(m => m.Status == StatusManutencao.Concluida);

        if (temConcluida)
        {
            return Conflict(new
            {
                mensagem = "Não é possível remover: existem manutenções concluídas."
            });
        }

        _context.Equipamentos.Remove(eq);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}