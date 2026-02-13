using MinhaApi.Models;
using System; 
using System.Collections.Generic;


public class LoteService
{
// 1. Classificação de Qualidade
public string ClassificarQualidade(double teorFe, double umidade, double siO2)
{
if (teorFe >= 65 && umidade <= 8 && siO2 <= 4)
return "Premium";


if (teorFe >= 58 && umidade <= 12 && siO2 <= 8)
return "Padrão";


return "Baixa";
}


// 2. Preço por Tonelada + Valor Total
public (double precoTonelada, double valorTotal) CalcularPreco(
double teorFe,
double umidade,
double siO2,
double toneladas)
{
var qualidade = ClassificarQualidade(teorFe, umidade, siO2);


double precoBase = qualidade switch
{
"Premium" => 120.0,
"Padrão" => 90.0,
_ => 60.0
};


double valorTotal = precoBase * toneladas;


return (precoBase, valorTotal);
}


// 3. Histórico de Movimentação
public List<string> RegistrarMovimentacao(List<string> historico, string local, string status)
{
historico ??= new List<string>();


var registro = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm} | {local} | {status}";
historico.Add(registro);


return historico;
}


// 4. Avançar Status do Lote
public string AvancarStatus(string statusAtual)
{
return statusAtual switch
{
"Estoque" => "Transporte",
"Transporte" => "Embarcado",
"Embarcado" => "Finalizado",
_ => "Status desconhecido"
};
}


// 5. Penalidade por Umidade
public double CalcularPenalidadeUmidade(double umidade, double toneladas)
{
const double limite = 8.0;
const double multaPorTonelada = 2.5; // valor fictício


if (umidade <= limite)
return 0;


double excesso = umidade - limite;


return excesso * multaPorTonelada * toneladas;
}
}