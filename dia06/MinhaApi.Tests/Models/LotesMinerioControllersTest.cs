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
        private AppDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco novo para cada teste
                .Options;
            return new AppDbContext(options);
        }

        #region TESTES DO POST (CREATE)
        [Fact]
        public async Task Create_DeveRetornarBadRequest_QuandoDadosInvalidos()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            
            // Testando um caso de TeorFe inválido
            var dto = new CreateLoteMinerioDto { CodigoLote = "L001", MinaOrigem = "Mina A", LocalizacaoAtual = "Pátio", TeorFe = 110 };

            var result = await controller.Create(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("TeorFe deve estar entre 0 e 100 (%).", badRequest.Value);
        }

        [Fact]
        public async Task Create_DeveRetornarConflict_QuandoCodigoJaExiste()
        {
            var db = GetDatabase();
            db.LotesMinerio.Add(new LoteMinerio { CodigoLote = "DUPLICADO", MinaOrigem = "M1", LocalizacaoAtual = "P1" });
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var dto = new CreateLoteMinerioDto { CodigoLote = "DUPLICADO", MinaOrigem = "M2", LocalizacaoAtual = "P2", Toneladas = 10 };

            var result = await controller.Create(dto);

            Assert.IsType<ConflictObjectResult>(result);
        }
        #endregion

        #region TESTES DO GET
        [Fact]
        public async Task GetById_DeveRetornarNotFound_QuandoNaoExiste()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaVazia_OuPopulada()
        {
            var db = GetDatabase();
            db.LotesMinerio.Add(new LoteMinerio { CodigoLote = "L1", MinaOrigem = "M1", LocalizacaoAtual = "P1" });
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var lista = Assert.IsAssignableFrom<IEnumerable<LoteMinerioResponseDto>>(okResult.Value);
            Assert.Single(lista);
        }
        #endregion

        #region TESTES DO DELETE
        [Fact]
        public async Task Delete_DeveRetornarNoContent_QuandoSucesso()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "L1", MinaOrigem = "M1", LocalizacaoAtual = "P1" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var result = await controller.Delete(lote.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(db.LotesMinerio);
        }

        [Fact]
        public async Task Delete_DeveRetornarNotFound_QuandoIdInexistente()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);

            var result = await controller.Delete(88);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region TESTES DO UPDATE
        [Fact]
        public async Task Update_DeveAtualizarDados_QuandoValido()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "OLD", MinaOrigem = "Mina Antiga", LocalizacaoAtual = "Pátio 1", Toneladas = 100 };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var updateDto = new LotesMinerioUpdateDto 
            { 
                MinaOrigem = "Mina Nova", 
                LocalizacaoAtual = "Porto", 
                Toneladas = 500,
                TeorFe = 65,
                Status = 1 
            };

            var result = await controller.Update(lote.Id, updateDto);

            Assert.IsType<NoContentResult>(result);
            var loteDb = await db.LotesMinerio.FindAsync(lote.Id);
            Assert.Equal("Mina Nova", loteDb.MinaOrigem);
        }

        [Fact]
        public async Task Update_DeveRetornarBadRequest_QuandoStatusInvalido()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "L1", MinaOrigem = "M1", LocalizacaoAtual = "P1" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var updateDto = new LotesMinerioUpdateDto { Status = 99, MinaOrigem = "M1", LocalizacaoAtual = "P1", Toneladas = 10 };

            var result = await controller.Update(lote.Id, updateDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion
    }
}