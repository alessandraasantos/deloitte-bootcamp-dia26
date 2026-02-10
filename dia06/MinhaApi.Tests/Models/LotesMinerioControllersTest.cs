using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Controllers;
using MinhaApi.Data;
using MinhaApi.Dtos;
using MinhaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MinhaApi.Tests
{
    public class LotesMinerioControllerTests
    {
        private AppDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        #region TESTES DE CRIAÇÃO (POST)

        [Fact]
        public async Task Create_DeveRetornarCreatedAtAction_QuandoDadosSaoValidos()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            var dto = new CreateLoteMinerioDto 
            { 
                CodigoLote = "L001", MinaOrigem = "Mina A", LocalizacaoAtual = "Pátio 1", 
                TeorFe = 65, Umidade = 5, Toneladas = 100, Status = 0 
            };

            var result = await controller.Create(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(LotesMinerioController.GetById), createdResult.ActionName);
        }

        [Theory]
        [InlineData("", "Mina A", "Pátio", "CodigoLote é obrigatório.")]
        [InlineData("L1", "", "Pátio", "MinaOrigem é obrigatória.")]
        [InlineData("L1", "Mina A", "", "LocalizacaoAtual é obrigatória.")]
        [InlineData("L1", "Mina A", "Pátio", "TeorFe deve estar entre 0 e 100 (%).", 110)]
        [InlineData("L1", "Mina A", "Pátio", "Umidade deve estar entre 0 e 100 (%).", 60, 110)]
        [InlineData("L1", "Mina A", "Pátio", "Toneladas deve ser > 0.", 60, 5, 0)]
        [InlineData("L1", "Mina A", "Pátio", "Status inválido (use 0, 1 ou 2).", 60, 5, 100, 5)]
        public async Task Create_DeveValidarRegrasDeNegocio(string cod, string mina, string loc, string erroMsg, decimal fe = 60, decimal umid = 5, decimal ton = 100, int status = 0)
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            var dto = new CreateLoteMinerioDto { CodigoLote = cod, MinaOrigem = mina, LocalizacaoAtual = loc, TeorFe = fe, Umidade = umid, Toneladas = ton, Status = status };

            var result = await controller.Create(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(erroMsg, badRequest.Value);
        }

        [Fact]
        public async Task Create_DeveRetornarConflict_QuandoCodigoJaExiste()
        {
            var db = GetDatabase();
            var codigoExistente = "DUP001";
            db.LotesMinerio.Add(new LoteMinerio { CodigoLote = codigoExistente, MinaOrigem = "Mina 1", LocalizacaoAtual = "Pátio 1" });
            await db.SaveChangesAsync();
            
            var controller = new LotesMinerioController(db);

            // Ajuste: Preenchendo todos os campos obrigatórios para passar pela validação inicial
            var dto = new CreateLoteMinerioDto 
            { 
                CodigoLote = codigoExistente, 
                MinaOrigem = "Mina 2", 
                LocalizacaoAtual = "Pátio 2",
                Toneladas = 50,
                TeorFe = 60,
                Umidade = 5,
                Status = 0
            };

            var result = await controller.Create(dto);

            Assert.IsType<ConflictObjectResult>(result);
        }

        #endregion

        #region TESTES DE BUSCA (GET)

        [Fact]
        public async Task GetById_DeveRetornarOk_QuandoExiste()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "L1", MinaOrigem = "M1", LocalizacaoAtual = "P1" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var result = await controller.GetById(lote.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<LoteMinerioResponseDto>(okResult.Value);
            Assert.Equal("L1", dto.CodigoLote);
        }

        [Fact]
        public async Task GetById_DeveRetornarNotFound_QuandoNaoExiste()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            var result = await controller.GetById(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_DeveRetornarTodosOsLotes()
        {
            var db = GetDatabase();
            db.LotesMinerio.AddRange(new List<LoteMinerio> {
                new LoteMinerio { CodigoLote = "A1", MinaOrigem = "M1", LocalizacaoAtual = "P1" },
                new LoteMinerio { CodigoLote = "A2", MinaOrigem = "M2", LocalizacaoAtual = "P2" }
            });
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var lista = Assert.IsAssignableFrom<IEnumerable<LoteMinerioResponseDto>>(okResult.Value);
            Assert.NotEmpty(lista);
        }

        #endregion

        #region TESTES DE ATUALIZAÇÃO (PUT)

        [Fact]
        public async Task Update_DeveRetornarNoContent_QuandoSucesso()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "L1", MinaOrigem = "Mina Original", LocalizacaoAtual = "Pátio" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var updateDto = new LotesMinerioUpdateDto { MinaOrigem = "Mina Nova", LocalizacaoAtual = "Porto", Toneladas = 50, Status = 1 };
            var result = await controller.Update(lote.Id, updateDto);

            Assert.IsType<NoContentResult>(result);
            var atualizado = await db.LotesMinerio.FindAsync(lote.Id);
            Assert.Equal("Mina Nova", atualizado!.MinaOrigem);
        }

        [Fact]
        public async Task Update_DeveRetornarNotFound_QuandoIdInexistente()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            var result = await controller.Update(99, new LotesMinerioUpdateDto());
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData("", "Pátio", "MinaOrigem é obrigatória.")]
        [InlineData("Mina", "", "LocalizacaoAtual é obrigatória.")]
        [InlineData("Mina", "Pátio", "TeorFe deve estar entre 0 e 100 (%).", 150)]
        [InlineData("Mina", "Pátio", "Umidade deve estar entre 0 e 100 (%).", 60, 110)]
        [InlineData("Mina", "Pátio", "Toneladas deve ser > 0.", 60, 5, -1)]
        [InlineData("Mina", "Pátio", "Status inválido (use 0, 1 ou 2).", 60, 5, 100, 9)]
        public async Task Update_DeveValidarCampos(string mina, string loc, string erro, decimal fe = 60, decimal umid = 5, decimal ton = 100, int status = 0)
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "L1", MinaOrigem = "M1", LocalizacaoAtual = "P1" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var dto = new LotesMinerioUpdateDto { MinaOrigem = mina, LocalizacaoAtual = loc, TeorFe = fe, Umidade = umid, Toneladas = ton, Status = status };
            var result = await controller.Update(lote.Id, dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(erro, badRequest.Value);
        }

        #endregion

        #region TESTES DE EXCLUSÃO (DELETE)

        [Fact]
        public async Task Delete_DeveRetornarNoContent_QuandoSucesso()
        {
            var db = GetDatabase();
            var lote = new LoteMinerio { CodigoLote = "DEL", MinaOrigem = "M1", LocalizacaoAtual = "P1" };
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();
            var controller = new LotesMinerioController(db);

            var result = await controller.Delete(lote.Id);

            Assert.IsType<NoContentResult>(result);
            var deletado = await db.LotesMinerio.FindAsync(lote.Id);
            Assert.Null(deletado);
        }

        [Fact]
        public async Task Delete_DeveRetornarNotFound_QuandoNaoExiste()
        {
            var db = GetDatabase();
            var controller = new LotesMinerioController(db);
            var result = await controller.Delete(999);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #endregion
    }
}