using System;
using MinhaApi.Dtos;
using MinhaApi.Models;
using Xunit;

namespace MinhaApi.Tests.Dtos
{
    public class LoteMinerioResponseDtoTests
    {
        [Fact]
        public void Deve_Criar_Dto_Com_Sucesso_E_Atribuir_Propriedades()
        {
            // Arrange
            var id = 1;
            var codigo = "LOTE-2024-001";
            var mina = "Mina Norte";
            var teor = 65.5m;
            var umidade = 8.2m;
            var sIO2 = 1.5m;
            var p = 0.05m;
            var toneladas = 500.0m;
            var data = DateTime.Now;
            
            // AQUI: Pegamos o primeiro valor disponível no seu Enum para evitar erro de compilação
            var status = Enum.GetValues<StatusLote>()[0]; 
            var local = "Terminal Ferroviário";

            // Act
            var dto = new LoteMinerioResponseDto(
                id, codigo, mina, teor, umidade, sIO2, p, toneladas, data, status, local
            );

            // Assert
            Assert.Equal(id, dto.Id);
            Assert.Equal(codigo, dto.CodigoLote);
            Assert.Equal(mina, dto.MinaOrigem);
            Assert.Equal(teor, dto.TeorFe);
            Assert.Equal(umidade, dto.Umidade);
            Assert.Equal(sIO2, dto.SiO2);
            Assert.Equal(p, dto.P);
            Assert.Equal(toneladas, dto.Toneladas);
            Assert.Equal(data, dto.DataProducao);
            Assert.Equal(status, dto.Status);
            Assert.Equal(local, dto.LocalizacaoAtual);
        }

        [Fact]
        public void Records_Com_Mesmos_Valores_Devem_Ser_Iguais()
        {
            // Arrange
            var data = DateTime.Now;
            var status = Enum.GetValues<StatusLote>()[0];
            
            var dto1 = new LoteMinerioResponseDto(1, "A", "Mina", 60, 10, null, null, 100, data, status, "Local");
            var dto2 = new LoteMinerioResponseDto(1, "A", "Mina", 60, 10, null, null, 100, data, status, "Local");

            // Assert
            Assert.Equal(dto1, dto2);
        }

        [Fact]
        public void Deve_Permitir_Valores_Nulos_Para_SiO2_e_P()
        {
            // Arrange
            var status = Enum.GetValues<StatusLote>()[0];

            // Act
            var dto = new LoteMinerioResponseDto(
                1, "L-01", "Mina", 60, 5, null, null, 100, DateTime.Now, status, "Local"
            );

            // Assert
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
        }
    }
}