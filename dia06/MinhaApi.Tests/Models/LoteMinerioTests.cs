using Xunit;
using MinhaApi.Models;

public class LoteMinerioTests
{
    // TESTE DE CRIAÇÃO (Fact)
    [Fact]
    public void Lote_DeveSerInstanciadoComStatusCorreto()
    {
        // Arrange & Act
        var lote = new LoteMinerio { Status = StatusLote.EmTransporte };

        // Assert
        Assert.Equal(StatusLote.EmTransporte, lote.Status);
        Assert.Empty(lote.CodigoLote); 
    }

    // TESTE DE VALIDAÇÃO DE LIMITES (Theory)
    [Theory]
    [InlineData(65.0, 5.0, 100, true)]   
    [InlineData(110.0, 5.0, 100, false)]  
    [InlineData(65.0, -1.0, 100, false)] 
    [InlineData(65.0, 5.0, 0, false)]   
    public void ValidarQualidade_DeveRetornarResultadoEsperado(decimal fe, decimal umid, decimal ton, bool resultadoEsperado)
    {
        // Arrange
        var lote = new LoteMinerio
        {
            TeorFe = fe,
            Umidade = umid,
            Toneladas = ton
        };

        // Act
        bool resultadoReal = lote.ValidarQualidade();

        // Assert
        Assert.Equal(resultadoEsperado, resultadoReal);
    }

    // TESTE DE INTEGRIDADE DE RASTREIO
    [Fact]
    public void Lote_DeveGerarDescricaoDeLocalizacao()
    {
        // Arrange
        var lote = new LoteMinerio { MinaOrigem = "Carajás", LocalizacaoAtual = "Pátio A" };

        // Act
        var info = $"Origem: {lote.MinaOrigem} | Atual: {lote.LocalizacaoAtual}";

        // Assert
        Assert.Contains("Carajás", info);
        Assert.Contains("Pátio A", info);
    }
}