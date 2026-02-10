using MinhaApi.Dtos;
using Xunit;

namespace MinhaApi.Tests.Dtos
{
    public class LotesMinerioDeleteDtoTests
    {
        [Fact]
        public void Deve_Permitir_Atribuir_E_Ler_Id()
        {
            // Arrange
            var dto = new LotesMinerioDeleteDto();
            var idEsperado = 505;

            // Act
            dto.Id = idEsperado;

            // Assert
            Assert.Equal(idEsperado, dto.Id);
        }

        [Fact]
        public void Deve_Iniciar_Com_Id_Zero()
        {
            // Act
            var dto = new LotesMinerioDeleteDto();

            // Assert
            // Como 'int' é um tipo de valor, o padrão deve ser 0
            Assert.Equal(0, dto.Id);
        }
    }
}