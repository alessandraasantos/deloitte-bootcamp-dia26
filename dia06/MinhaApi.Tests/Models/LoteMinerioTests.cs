using Xunit;
using MinhaApi.Models;
using System;

namespace MinhaApi.Tests
{
    public class LoteMinerioTests
    {
        // 1. TESTE DE PROPRIEDADES (Cobre todos os Getters e Setters)
        [Fact]
        public void Lote_DeveAtribuirERecuperarValoresCorretamente()
        {
            // Arrange
            var dataTeste = DateTime.Now;
            var lote = new LoteMinerio();

            // Act
            lote.Id = 10;
            lote.CodigoLote = "LOTE123";
            lote.MinaOrigem = "Mina Norte";
            lote.TeorFe = 65.5m;
            lote.Umidade = 8.2m;
            lote.SiO2 = 2.1m;
            lote.P = 0.05m;
            lote.Toneladas = 1000m;
            lote.DataProducao = dataTeste;
            lote.Status = StatusLote.Embarcado;
            lote.LocalizacaoAtual = "Porto";

            // Assert
            Assert.Equal(10, lote.Id);
            Assert.Equal("LOTE123", lote.CodigoLote);
            Assert.Equal("Mina Norte", lote.MinaOrigem);
            Assert.Equal(65.5m, lote.TeorFe);
            Assert.Equal(8.2m, lote.Umidade);
            Assert.Equal(2.1m, lote.SiO2);
            Assert.Equal(0.05m, lote.P);
            Assert.Equal(1000m, lote.Toneladas);
            Assert.Equal(dataTeste, lote.DataProducao);
            Assert.Equal(StatusLote.Embarcado, lote.Status);
            Assert.Equal("Porto", lote.LocalizacaoAtual);
        }

        // 2. TESTE DE VALIDAÇÃO (Cobre todos os IFs do método ValidarQualidade)
        [Theory]
        // Caminho Feliz
        [InlineData(65, 5, 100, true)]
        // Falhas no TeorFe
        [InlineData(-1, 5, 100, false)]
        [InlineData(101, 5, 100, false)]
        // Falhas na Umidade
        [InlineData(65, -0.1, 100, false)]
        [InlineData(65, 100.1, 100, false)]
        // Falhas na Tonelada
        [InlineData(65, 5, 0, false)]
        [InlineData(65, 5, -10, false)]
        public void ValidarQualidade_DeveTestarTodosOsLimites(decimal fe, decimal umid, decimal ton, bool esperado)
        {
            // Arrange
            var lote = new LoteMinerio 
            { 
                TeorFe = fe, 
                Umidade = umid, 
                Toneladas = ton 
            };

            // Act
            var resultado = lote.ValidarQualidade();

            // Assert
            Assert.Equal(esperado, resultado);
        }

        // 3. TESTE DE VALORES PADRÃO (Cobre as inicializações de string vazia "")
        [Fact]
        public void Lote_DeveIniciarComStringsVazias()
        {
            // Act
            var lote = new LoteMinerio();

            // Assert
            Assert.Equal(string.Empty, lote.CodigoLote);
            Assert.Equal(string.Empty, lote.MinaOrigem);
            Assert.Equal(string.Empty, lote.LocalizacaoAtual);
        }
    }
}