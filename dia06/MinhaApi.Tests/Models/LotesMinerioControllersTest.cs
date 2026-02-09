using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Controllers;
using MinhaApi.Data;
using MinhaApi.Dtos;
using MinhaApi.Models;
using Xunit;

namespace MinhaApi.Tests
{
    public class LotesMinerioControllerTests
    {
        // Helper para criar um contexto de banco em memória limpo para cada teste
        private AppDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_DeveRetornarCreatedAtAction_QuandoDadosSaoValidos()
        {
            // Arrange
            var db = GetDatabaseContext();
            var controller = new LotesMinerioController(db);
            var novoLoteDto = new CreateLoteMinerioDto 
            { 
                CodigoLote = "LOTE-001", 
                MinaOrigem = "Mina Norte", 
                TeorFe = 60, 
                Umidade = 5, 
                Toneladas = 500,
                Status = 0,
                LocalizacaoAtual = "Patio 1"
            };

            // Act
            var result = await controller.Create(novoLoteDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var loteSalvo = Assert.IsType<LoteMinerio>(createdResult.Value);
            Assert.Equal("LOTE-001", loteSalvo.CodigoLote);
            Assert.Equal(1, await db.LotesMinerio.CountAsync());
        }

        [Fact]
        public async Task Create_DeveRetornarBadRequest_QuandoTeorFeInvalido()
        {
            // Arrange
            var db = GetDatabaseContext();
            var controller = new LotesMinerioController(db);
            var dtoInvalido = new CreateLoteMinerioDto { TeorFe = 150 }; // Erro: > 100

            // Act
            var result = await controller.Create(dtoInvalido);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetById_DeveRetornarNotFound_QuandoLoteNaoExiste()
        {
            // Arrange
            var db = GetDatabaseContext();
            var controller = new LotesMinerioController(db);

            // Act
            var result = await controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeveRemoverLote_QuandoIdExiste()
        {
            // Arrange
            var db = GetDatabaseContext();
            var lote = new LoteMinerio { Id = 10, CodigoLote = "DEL-01", MinaOrigem = "Mina Sul" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            
            var controller = new LotesMinerioController(db);

            // Act
            var result = await controller.Delete(10);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Empty(db.LotesMinerio);
        }
    }
}