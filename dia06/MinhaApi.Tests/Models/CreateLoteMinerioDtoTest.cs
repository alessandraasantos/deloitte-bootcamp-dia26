using MinhaApi.Dtos;
using Xunit;

namespace MinhaApi.Tests.Dtos
{
    public class CreateLoteMinerioDtoTests
    {
        [Fact]
        public void CreateLoteMinerioDto_DeveArmazenarDadosCorretamente()
        {
            // Arrange - Criando um cenário com todos os campos preenchidos
            var dataTeste = new DateTime(2026, 02, 09);
            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "MNA-123",
                MinaOrigem = "Mina Norte",
                TeorFe = 65.5m,
                Umidade = 5.2m,
                SiO2 = 3.1m,
                P = 0.05m,
                Toneladas = 1000,
                DataProducao = dataTeste,
                Status = 1,
                LocalizacaoAtual = "Pátio A"
            };

            // Act & Assert - Verificando se o que entrou é o que saiu
            Assert.Equal("MNA-123", dto.CodigoLote);
            Assert.Equal("Mina Norte", dto.MinaOrigem);
            Assert.Equal(65.5m, dto.TeorFe);
            Assert.Equal(5.2m, dto.Umidade);
            Assert.Equal(3.1m, dto.SiO2);
            Assert.Equal(0.05m, dto.P);
            Assert.Equal(1000, dto.Toneladas);
            Assert.Equal(dataTeste, dto.DataProducao);
            Assert.Equal(1, dto.Status);
            Assert.Equal("Pátio A", dto.LocalizacaoAtual);
        }

        [Fact]
        public void CreateLoteMinerioDto_CamposOpcionaisPodemSerNulos()
        {
            // Arrange & Act
            var dto = new CreateLoteMinerioDto
            {
                SiO2 = null,
                P = null,
                DataProducao = null
            };

            // Assert
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
            Assert.Null(dto.DataProducao);
        }

        [Fact]
        public void CreateLoteMinerioDto_DeveTerValoresPadraoAoInstanciar()
        {
            // Act
            var dto = new CreateLoteMinerioDto();

            // Assert - Verifica se os campos string iniciam vazios e não nulos
            Assert.Equal(string.Empty, dto.CodigoLote);
            Assert.Equal(string.Empty, dto.MinaOrigem);
            Assert.Equal(string.Empty, dto.LocalizacaoAtual);
        }
    }
}

 