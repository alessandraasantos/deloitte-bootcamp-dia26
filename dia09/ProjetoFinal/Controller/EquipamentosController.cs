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

        // POST: api/equipamentos
        [HttpPost]
        public async Task<ActionResult<EquipamentoMinas>> Post([FromBody] EquipamentoMinas equipamento)
        {
            if (equipamento == null) return BadRequest("Dados inválidos.");

            // Garante a regra de negócio de limpar espaços no código
            equipamento.Codigo = equipamento.Codigo?.Trim();

            // Validação de unicidade antes de tentar salvar
            if (await _context.Equipamentos.AnyAsync(e => e.Codigo == equipamento.Codigo))
            {
                return Conflict(new { mensagem = "Já existe um equipamento com este código." });
            }

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = equipamento.Id }, equipamento);
        }

        // GET: api/equipamentos (com filtros e paginação)
        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] string? tipo, 
            [FromQuery] string? status, 
            [FromQuery] string? codigo,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Equipamentos.AsQueryable();

            // Filtros dinâmicos
            if (!string.IsNullOrEmpty(tipo)) 
                query = query.Where(e => e.Tipo.ToString() == tipo);
            
            if (!string.IsNullOrEmpty(status)) 
                query = query.Where(e => e.StatusOperacional.ToString() == status);
            
            if (!string.IsNullOrEmpty(codigo)) 
                query = query.Where(e => e.Codigo.Contains(codigo));

            // Paginação
            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new 
            { 
                totalItems, 
                page, 
                pageSize, 
                items 
            });
        }

        // GET: api/equipamentos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoMinas>> GetById(int id)
        {
            var eq = await _context.Equipamentos.FindAsync(id);
            if (eq == null) return NotFound(new { mensagem = "Equipamento não encontrado." });
            
            return Ok(eq);
        }

        // PUT: api/equipamentos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EquipamentoMinas equipamento)
        {
            if (id != equipamento.Id) 
                return BadRequest(new { mensagem = "O ID da URL não coincide com o ID do objeto." });
            
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

        // DELETE: api/equipamentos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eq = await _context.Equipamentos.FindAsync(id);
            if (eq == null) return NotFound();

            // Regra da Vale: Não deletar se houver manutenções concluídas
            // Aqui simulamos a checagem. Se você tiver a tabela de manutenções, 
            // faria uma consulta aqui.
            bool temManutencaoConcluida = false; 
            
            if (temManutencaoConcluida)
                return Conflict(new { mensagem = "Não é possível remover: existem manutenções concluídas associadas." });

            _context.Equipamentos.Remove(eq);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}