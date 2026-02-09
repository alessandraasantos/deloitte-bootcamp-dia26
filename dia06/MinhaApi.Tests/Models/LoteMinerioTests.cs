using Xunit;
using MinhaApi.Models; // Importante para reconhecer a classe e o enum

public class LoteMinerioTests
{
    [Fact]
    public void TestaCriacaoLoteMinerio_DeveAtribuirPropriedadesCorretamente()
    {
        // Arrange
        var dataTeste = new DateTime(2026, 02, 09);
        
        var lote = new LoteMinerio
        {
            Id = 1,
            CodigoLote = "MNA-2026-000123",
            MinaOrigem = "Carajás N4E",
            TeorFe = 65.5m,
            Umidade = 5.0m,
            SiO2 = 3.0m,
            P = 0.5m,
            Toneladas = 100,
            DataProducao = dataTeste,
            Status = StatusLote.EmEstoque, // Usando o Enum definido na model
            LocalizacaoAtual = "Pátio Carajás"
        };

        // Act & Assert
        Assert.Equal(1, lote.Id);
        Assert.Equal("MNA-2026-000123", lote.CodigoLote);
        Assert.Equal("Carajás N4E", lote.MinaOrigem);
        Assert.Equal(65.5m, lote.TeorFe);
        Assert.Equal(100, lote.Toneladas);
        Assert.Equal(dataTeste, lote.DataProducao);
        
        // Verificando o Enum e os campos opcionais (Nullable)
        Assert.Equal(StatusLote.EmEstoque, lote.Status);
        Assert.NotNull(lote.SiO2);
        Assert.Equal(3.0m, lote.SiO2);
    }

    [Fact]
    public void TestaLoteMinerio_ValoresOpcionaisPodemSerNulos()
    {
        // Arrange & Act
        var lote = new LoteMinerio { SiO2 = null, P = null };

        // Assert
        Assert.Null(lote.SiO2);
        Assert.Null(lote.P);
    }
}