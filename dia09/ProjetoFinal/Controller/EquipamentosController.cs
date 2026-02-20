using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
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
            if (equipamento == null) return BadRequest("Dados inválidos.");

            
            equipamento.Codigo = (equipamento.Codigo ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(equipamento.Codigo))
                return BadRequest(new { mensagem = "O código é obrigatório e não pode ser apenas espaços." });

            if (await _context.Equipamentos.AnyAsync(e => e.Codigo == equipamento.Codigo))
            {
                return Conflict(new { mensagem = "Já existe um equipamento com este código." });
            }

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = equipamento.Id }, equipamento);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] string? tipo, 
            [FromQuery] string? status, 
            [FromQuery] string? codigo,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
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
            if (eq == null) return NotFound(new { mensagem = "Equipamento não encontrado." });
            
            return Ok(eq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EquipamentoMinas equipamento)
        {
            if (id != equipamento.Id) 
                return BadRequest(new { mensagem = "O ID da URL não coincide com o ID do objeto." });
            
            equipamento.Codigo = (equipamento.Codigo ?? string.Empty).Trim();
            _context.Entry(equipamento).State = EntityState.Modified;
            
            try 
            {
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateException) 
            {
                if (!await _context.Equipamentos.AnyAsync(e => e.Id == id))
                    return NotFound();
                
                return Conflict(new { mensagem = "Erro ao atualizar. Verifique se o código já existe." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eq = await _context.Equipamentos.FindAsync(id);
            if (eq == null) return NotFound();

           
            bool temManutencaoConcluida = false; 
            
            if (temManutencaoConcluida)
                return Conflict(new { mensagem = "Não é possível remover: existem manutenções concluídas associadas." });

            _context.Equipamentos.Remove(eq);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}