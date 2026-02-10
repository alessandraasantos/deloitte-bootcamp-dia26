using MinhaApi.Dtos;
using Xunit;

namespace MinhaApi.Tests.Dtos
{
    public class LotesMinerioUpdateDtoTests
    {
        [Fact]
        public void Deve_Permitir_Atribuir_E_Ler_Valores_Nas_Propriedades()
        {
            // Arrange
            var dto = new LotesMinerioUpdateDto();
            var minaEsperada = "Mina de Ferro Central";
            var teorEsperado = 62.5m;
            var umidadeEsperada = 5.0m;
            var sio2Esperado = 2.1m;
            var pEsperado = 0.045m;
            var toneladasEsperadas = 1200.5m;
            var statusEsperado = 2;
            var localizacaoEsperada = "Pátio de Triagem";

            // Act
            dto.MinaOrigem = minaEsperada;
            dto.TeorFe = teorEsperado;
            dto.Umidade = umidadeEsperada;
            dto.SiO2 = sio2Esperado;
            dto.P = pEsperado;
            dto.Toneladas = toneladasEsperadas;
            dto.Status = statusEsperado;
            dto.LocalizacaoAtual = localizacaoEsperada;

            // Assert
            Assert.Equal(minaEsperada, dto.MinaOrigem);
            Assert.Equal(teorEsperado, dto.TeorFe);
            Assert.Equal(umidadeEsperada, dto.Umidade);
            Assert.Equal(sio2Esperado, dto.SiO2);
            Assert.Equal(pEsperado, dto.P);
            Assert.Equal(toneladasEsperadas, dto.Toneladas);
            Assert.Equal(statusEsperado, dto.Status);
            Assert.Equal(localizacaoEsperada, dto.LocalizacaoAtual);
        }

        [Fact]
        public void Deve_Iniciar_Com_Strings_Vazias_E_Valores_Nulos()
        {
            // Act
            var dto = new LotesMinerioUpdateDto();

            // Assert
            Assert.Equal(string.Empty, dto.MinaOrigem);
            Assert.Equal(string.Empty, dto.LocalizacaoAtual);
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
            Assert.Equal(0m, dto.TeorFe);
            Assert.Equal(0, dto.Status);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Nulos_Em_Campos_Opcionais()
        {
            // Arrange
            var dto = new LotesMinerioUpdateDto
            {
                SiO2 = null,
                P = null
            };

            // Assert
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
        }
    }
}