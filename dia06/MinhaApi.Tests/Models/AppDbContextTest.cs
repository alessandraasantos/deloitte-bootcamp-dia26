using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using Xunit;

namespace MinhaApi.Tests
{
    public class AppDbContextTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task DbContext_DeveSalvarLote_ComStatusComoInt()
        {
            // Arrange
            using var db = GetContext();
            var lote = new LoteMinerio
            {
                CodigoLote = "TEST-001",
                MinaOrigem = "Mina Teste",
                LocalizacaoAtual = "Patio Teste",
                Status = StatusLote.EmTransporte // Valor Enum (1)
            };

            // Act
            db.LotesMinerio.Add(lote);
            await db.SaveChangesAsync();

            // Assert
            var loteNoBanco = await db.LotesMinerio.FirstAsync();
            Assert.Equal(StatusLote.EmTransporte, loteNoBanco.Status);
            // O EF In-Memory simula a conversão definida em HasConversion<int>()
        }

        [Fact]
        public async Task DbContext_NaoDevePermitirCodigoLoteDuplicado()
        {
            // Arrange
            using var db = GetContext();
            var lote1 = new LoteMinerio { CodigoLote = "DUPLICADO", MinaOrigem = "A", LocalizacaoAtual = "X" };
            var lote2 = new LoteMinerio { CodigoLote = "DUPLICADO", MinaOrigem = "B", LocalizacaoAtual = "Y" };

            // Act & Assert
            db.LotesMinerio.Add(lote1);
            await db.SaveChangesAsync();

            db.LotesMinerio.Add(lote2);
            
            // O banco em memória do EF tem limitações com índices únicos reais, 
            // mas em um banco real (Postgres/SQL), o SaveChanges lançaria uma exceção aqui.
            await Assert.ThrowsAnyAsync<Exception>(async () => await db.SaveChangesAsync());
        }

        [Fact]
        public void DbContext_VerificaConfiguracaoDeSchemaETabela()
        {
            // Arrange
            using var db = GetContext();
            var entityType = db.Model.FindEntityType(typeof(LoteMinerio));

            // Assert
            Assert.Equal("lotes_minerio", entityType?.GetTableName());
            Assert.Equal("public", entityType?.GetSchema());
        }
    }
}