using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;

[ApiController]
[Route("api/lotes-extras")]
public class LotesExtrasController : ControllerBase
{
    private readonly LoteService _service = new();
    private readonly AppDbContext _context;

    public LotesExtrasController(AppDbContext context)
    {
        _context = context;
    }

    // GET → Classificação por ID
    [HttpGet("{id}/classificacao")]
    public async Task<IActionResult> GetClassificacao(int id)
    {
        var lote = await _context.LotesMinerio.FindAsync(id);

        if (lote == null)
            return NotFound("Lote não encontrado");

        var classificacao = _service.ClassificarQualidade(
            (double)lote.TeorFe,
            (double)lote.Umidade,
            (double)(lote.SiO2 ?? 0)
        );

        return Ok(new { id, classificacao });
    }

    // GET → Preço por ID
    [HttpGet("{id}/preco")]
    public async Task<IActionResult> GetPreco(int id)
    {
        var lote = await _context.LotesMinerio.FindAsync(id);

        if (lote == null)
            return NotFound("Lote não encontrado");

        var (precoTonelada, valorTotal) = _service.CalcularPreco(
            (double)lote.TeorFe,
            (double)lote.Umidade,
            (double)(lote.SiO2 ?? 0),
            (double)lote.Toneladas
        );

        return Ok(new
        {
            id,
            precoTonelada,
            valorTotal
        });
    }

    // GET → Penalidade por ID
    [HttpGet("{id}/penalidade")]
    public async Task<IActionResult> GetPenalidade(int id)
    {
        var lote = await _context.LotesMinerio.FindAsync(id);

        if (lote == null)
            return NotFound("Lote não encontrado");

        var penalidade = _service.CalcularPenalidadeUmidade(
            (double)lote.Umidade,
            (double)lote.Toneladas
        );

        return Ok(new { id, penalidade });
    }

    // POST → Avançar status do lote
    [HttpPost("{id}/avancar-status")]
    public async Task<IActionResult> PostAvancarStatus(int id)
    {
        var lote = await _context.LotesMinerio.FindAsync(id);

        if (lote == null)
            return NotFound("Lote não encontrado");

        var novoStatus = _service.AvancarStatus(lote.Status);

        lote.Status = novoStatus;

        await _context.SaveChangesAsync();

        return Ok(new { id, novoStatus = lote.Status.ToString() });
    }

    // POST → Registrar movimentação com histórico
    [HttpPost("{id}/movimentacao")]
    public async Task<IActionResult> PostMovimentacao(int id, string local)
    {
        var lote = await _context.LotesMinerio.FindAsync(id);

        if (lote == null)
            return NotFound("Lote não encontrado");

        // Atualiza estado atual do lote
        lote.LocalizacaoAtual = local;

        // Cria registro de histórico
        var movimentacao = new MovimentacaoLote
        {
            LoteMinerioId = lote.Id,
            Local = local,
            Status = lote.Status,
            DataMovimentacao = DateTime.UtcNow
        };

        _context.MovimentacoesLote.Add(movimentacao);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            id = lote.Id,
            localAtual = lote.LocalizacaoAtual,
            status = lote.Status.ToString(),
            data = movimentacao.DataMovimentacao
        });
    }
}
