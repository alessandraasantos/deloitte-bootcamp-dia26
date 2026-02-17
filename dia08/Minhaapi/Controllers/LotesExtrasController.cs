using Microsoft.AspNetCore.Mvc;
using MinhaApi.Models;

[ApiController]
[Route("api/lotes-extras")]
public class LotesExtrasController : ControllerBase
{
private readonly LoteService _service = new();


// GET → Classificação (1)
[HttpGet("classificacao")]
public IActionResult GetClassificacao(
double teorFe,
double umidade,
double siO2)
{
var resultado = _service.ClassificarQualidade(teorFe, umidade, siO2);
return Ok(new { classificacao = resultado });
}


// GET → Preço (2)
[HttpGet("preco")]
public IActionResult GetPreco(
double teorFe,
double umidade,
double siO2,
double toneladas)
{
var (precoTonelada, valorTotal) = _service.CalcularPreco(teorFe, umidade, siO2, toneladas);


return Ok(new
{
precoTonelada,
valorTotal
});
}


// GET → Penalidade (5)
[HttpGet("penalidade")]
public IActionResult GetPenalidade(double umidade, double toneladas)
{
var penalidade = _service.CalcularPenalidadeUmidade(umidade, toneladas);


return Ok(new { penalidade });
}


// POST → Registrar movimentação (3)
[HttpPost("movimentacao")]
public IActionResult PostMovimentacao(string local, string status)
{
var historico = _service.RegistrarMovimentacao(null, local, status);


return Ok(new { historico });
}


// POST → Avançar status (4)
[HttpPost("avancar-status")]
public IActionResult PostAvancarStatus(string statusAtual)
{
var novoStatus = _service.AvancarStatus(statusAtual);


return Ok(new { novoStatus });
}
}