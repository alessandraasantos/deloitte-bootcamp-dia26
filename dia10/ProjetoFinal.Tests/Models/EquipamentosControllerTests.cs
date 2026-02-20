using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Controller;
using ProjetoFinal.Data;
using ProjetoFinal.Model;
using System.Threading.Tasks;
using System.Linq;

namespace ProjetoFinal.Tests
{
    public class EquipamentosControllerTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoEquipamentoForNull()
        {
            var context = GetContext();
            var controller = new EquipamentosController(context);

            var result = await controller.Post(null);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Dados inválidos.", badRequest.Value);
        }

        [Fact]
        public async Task Post_DeveRetornarConflict_QuandoCodigoJaExiste()
        {
            var context = GetContext();
            context.Equipamentos.Add(new EquipamentoMinas { Codigo = "EQ001" });
            await context.SaveChangesAsync();

            var controller = new EquipamentosController(context);

            var novo = new EquipamentoMinas { Codigo = "EQ001" };

            var result = await controller.Post(novo);

            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task Post_DeveCriarEquipamento_QuandoValido()
        {
            var context = GetContext();
            var controller = new EquipamentosController(context);

            var equipamento = new EquipamentoMinas
            {
                Codigo = "EQ002"
            };

            var result = await controller.Post(equipamento);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(controller.GetById), created.ActionName);

            Assert.Equal(1, context.Equipamentos.Count());
        }

        [Fact]
        public async Task GetById_DeveRetornarNotFound_QuandoNaoExiste()
        {
            var context = GetContext();
            var controller = new EquipamentosController(context);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_DeveRetornarOk_QuandoExiste()
        {
            var context = GetContext();
            var equipamento = new EquipamentoMinas { Codigo = "EQ003" };
            context.Equipamentos.Add(equipamento);
            await context.SaveChangesAsync();

            var controller = new EquipamentosController(context);

            var result = await controller.GetById(equipamento.Id);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<EquipamentoMinas>(ok.Value);
        }

        [Fact]
        public async Task Delete_DeveRetornarNotFound_QuandoNaoExiste()
        {
            var context = GetContext();
            var controller = new EquipamentosController(context);

            var result = await controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeveRemover_QuandoExiste()
        {
            var context = GetContext();
            var equipamento = new EquipamentoMinas { Codigo = "EQ004" };
            context.Equipamentos.Add(equipamento);
            await context.SaveChangesAsync();

            var controller = new EquipamentosController(context);

            var result = await controller.Delete(equipamento.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Equipamentos);
        }
    }
}