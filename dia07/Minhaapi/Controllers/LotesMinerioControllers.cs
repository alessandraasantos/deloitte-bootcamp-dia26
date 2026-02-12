using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;
using MinhaApi.Fila; 

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/LotesMinerio")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRedisPublisher _publisher; 

        public LotesMinerioController(AppDbContext db, IRedisPublisher publisher) 
        {
            _db = db;
            _publisher = publisher;
        }

        // ================= POST =================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoteMinerioDto input)
        {
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest("CodigoLote é obrigatório.");
            if (string.IsNullOrWhiteSpace(input.MinaOrigem))
                return BadRequest("MinaOrigem é obrigatória.");
            if (string.IsNullOrWhiteSpace(input.LocalizacaoAtual))
                return BadRequest("LocalizacaoAtual é obrigatória.");
            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");
            if (input.Umidade is < 0 or > 100)
                return BadRequest("Umidade deve estar entre 0 e 100 (%).");
            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");
            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            var exists = await _db.LotesMinerio.AnyAsync(x => x.CodigoLote == input.CodigoLote);
            if (exists)
                return Conflict($"Já existe um lote com CodigoLote '{input.CodigoLote}'.");

            var lote = new LoteMinerio
            {
                CodigoLote = input.CodigoLote,
                MinaOrigem = input.MinaOrigem,
                TeorFe = input.TeorFe,
                Umidade = input.Umidade,
                SiO2 = input.SiO2,
                P = input.P,
                Toneladas = input.Toneladas,
                DataProducao = input.DataProducao ?? DateTime.UtcNow,
                Status = (StatusLote)input.Status,
                LocalizacaoAtual = input.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            await _db.SaveChangesAsync();

            // ================= ENVIO PARA O REDIS (ADICIONADO) =================
            await _publisher.PublishAsync(new

       {
    
        lote.Id,
        lote.CodigoLote,
        lote.MinaOrigem,
        lote.Toneladas,
        lote.Status,
        lote.LocalizacaoAtual,
        DataEnvio = DateTime.UtcNow
      });

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

        // ================= GET BY ID =================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var l = await _db.LotesMinerio.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (l is null) return NotFound();

            var dto = new LoteMinerioResponseDto(
                l.Id, l.CodigoLote, l.MinaOrigem, l.TeorFe, l.Umidade, l.SiO2, l.P,
                l.Toneladas, l.DataProducao, l.Status, l.LocalizacaoAtual
            );

            return Ok(dto);
        }

        // ================= GET ALL =================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lotes = await _db.LotesMinerio
                .AsNoTracking()
                .Select(l => new LoteMinerioResponseDto(
                    l.Id, l.CodigoLote, l.MinaOrigem, l.TeorFe, l.Umidade, l.SiO2, l.P,
                    l.Toneladas, l.DataProducao, l.Status, l.LocalizacaoAtual
                ))
                .ToListAsync();

            return Ok(lotes);
        }

        // ================= DELETE =================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);

            if (lote is null)
                return NotFound($"Lote com Id {id} não encontrado.");

            _db.LotesMinerio.Remove(lote);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // ================= UPDATE =================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] LotesMinerioUpdateDto input)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);

            if (lote is null)
                return NotFound($"Lote com Id {id} não encontrado.");

            if (string.IsNullOrWhiteSpace(input.MinaOrigem))
                return BadRequest("MinaOrigem é obrigatória.");

            if (string.IsNullOrWhiteSpace(input.LocalizacaoAtual))
                return BadRequest("LocalizacaoAtual é obrigatória.");

            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");

            if (input.Umidade is < 0 or > 100)
                return BadRequest("Umidade deve estar entre 0 e 100 (%).");

            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");

            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            lote.MinaOrigem = input.MinaOrigem;
            lote.TeorFe = input.TeorFe;
            lote.Umidade = input.Umidade;
            lote.SiO2 = input.SiO2;
            lote.P = input.P;
            lote.Toneladas = input.Toneladas;
            lote.Status = (StatusLote)input.Status;
            lote.LocalizacaoAtual = input.LocalizacaoAtual;

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
