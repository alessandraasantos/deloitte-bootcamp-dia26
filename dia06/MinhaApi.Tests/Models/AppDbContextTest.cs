using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Tests.Data
{
    public class AppDbContextTests
    {
        private AppDbContext CriarContextoSqlite()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public void Deve_Ter_DbSet_LotesMinerio()
        {
            using var context = CriarContextoSqlite();

            Assert.NotNull(context.LotesMinerio);
        }

        [Fact]
        public void Deve_Mapear_Tabela_LotesMinerio_Corretamente()
        {
            using var context = CriarContextoSqlite();

            var entityType = context.Model.FindEntityType(typeof(LoteMinerio));

            Assert.NotNull(entityType);
            Assert.Equal("lotes_minerio", entityType!.GetTableName());
            Assert.Equal("public", entityType.GetSchema());
        }

        [Fact]
        public void Deve_Configurar_Chave_Primaria_Id()
        {
            using var context = CriarContextoSqlite();

            var entityType = context.Model.FindEntityType(typeof(LoteMinerio));
            var pk = entityType!.FindPrimaryKey();

            Assert.NotNull(pk);
            Assert.Equal("Id", pk!.Properties.First().Name);
        }

        [Fact]
        public void Deve_Configurar_CodigoLote_Obrigatorio_E_Com_Tamanho_Maximo()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));
            var prop = entity!.FindProperty(nameof(LoteMinerio.CodigoLote));

            Assert.False(prop!.IsNullable);
            Assert.Equal(50, prop.GetMaxLength());
        }

        [Fact]
        public void Deve_Ter_Indice_Unico_Em_CodigoLote()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));
            var index = entity!.GetIndexes()
                .FirstOrDefault(i => i.Properties.Any(p => p.Name == nameof(LoteMinerio.CodigoLote)));

            Assert.NotNull(index);
            Assert.True(index!.IsUnique);
        }

        [Fact]
        public void Deve_Configurar_MinaOrigem_Obrigatoria_Com_Tamanho_120()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));
            var prop = entity!.FindProperty(nameof(LoteMinerio.MinaOrigem));

            Assert.False(prop!.IsNullable);
            Assert.Equal(120, prop.GetMaxLength());
        }

        [Fact]
        public void Deve_Configurar_LocalizacaoAtual_Obrigatoria_Com_Tamanho_200()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));
            var prop = entity!.FindProperty(nameof(LoteMinerio.LocalizacaoAtual));

            Assert.False(prop!.IsNullable);
            Assert.Equal(200, prop.GetMaxLength());
        }

        [Fact]
        public void Deve_Configurar_Tipos_Decimais_Corretamente()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));

            Assert.Equal("TEXT", entity!.FindProperty(nameof(LoteMinerio.TeorFe))!.GetColumnType()?.ToUpper());
            Assert.Equal("TEXT", entity.FindProperty(nameof(LoteMinerio.Umidade))!.GetColumnType()?.ToUpper());
            Assert.Equal("TEXT", entity.FindProperty(nameof(LoteMinerio.SiO2))!.GetColumnType()?.ToUpper());
            Assert.Equal("TEXT", entity.FindProperty(nameof(LoteMinerio.P))!.GetColumnType()?.ToUpper());
            Assert.Equal("TEXT", entity.FindProperty(nameof(LoteMinerio.Toneladas))!.GetColumnType()?.ToUpper());
        }

        [Fact]
        public void Deve_Converter_Status_Para_Int()
        {
            using var context = CriarContextoSqlite();

            var entity = context.Model.FindEntityType(typeof(LoteMinerio));
            var prop = entity!.FindProperty(nameof(LoteMinerio.Status));

            Assert.NotNull(prop!.GetValueConverter());
        }
    }
}
