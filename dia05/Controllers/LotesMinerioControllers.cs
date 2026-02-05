using System.Runtime.Versioning;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;
        public LotesMinerioController(AppDbContext db)  => _db = db;

        [HttpPost]
        public IActionResult CreateLote([FromBody] CreateLoteMinerioDto dto)
        {
            if (dto == null)
                return BadRequest("Dados do lote são obrigatórios.");

            if (string.IsNullOrWhiteSpace(dto.CodigoLote))
                return BadRequest("Código do lote é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.MinaOrigem))
                return BadRequest("Mina de origem é obrigatória.");

            if (dto.TeorFe < 0 || dto.TeorFe > 100)
                return BadRequest("Teor de ferro deve ser entre 0 e 100%.");

            if (dto.Umidade < 0 || dto.Umidade > 100)
                return BadRequest("Umidade deve ser entre 0 e 100%.");

            if (dto.Toneladas <= 0)
                return BadRequest("Toneladas deve ser maior que zero.");

            var lote = new LoteMinerio
            {
                CodigoLote = dto.CodigoLote,
                MinaOrigem = dto.MinaOrigem,
                TeorFe = dto.TeorFe,
                Umidade = dto.Umidade,
                SiO2 = dto.SiO2,
                P = dto.P,
                Toneladas = dto.Toneladas,
                DataProducao = dto.DataProducao ?? DateTime.UtcNow,
                Status = (StatusLote)dto.Status,
                LocalizacaoAtual = dto.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetLoteById), new { id = lote.Id }, lote);
        }

        

        
    }

}
