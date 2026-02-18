using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LoteMinerio> LotesMinerio => Set<LoteMinerio>();
        public DbSet<MovimentacaoLote> MovimentacoesLote => Set<MovimentacaoLote>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<LoteMinerio>(e =>
            {
                e.ToTable("lotes_minerio");

                e.HasKey(x => x.Id);

                e.Property(x => x.CodigoLote)
                    .HasMaxLength(50)
                    .IsRequired();

                e.HasIndex(x => x.CodigoLote)
                    .IsUnique();

                e.Property(x => x.MinaOrigem)
                    .HasMaxLength(120)
                    .IsRequired();

                e.Property(x => x.LocalizacaoAtual)
                    .HasMaxLength(200)
                    .IsRequired();

                e.Property(x => x.TeorFe).HasColumnType("numeric(5,2)");
                e.Property(x => x.Umidade).HasColumnType("numeric(5,2)");
                e.Property(x => x.SiO2).HasColumnType("numeric(5,2)");
                e.Property(x => x.P).HasColumnType("numeric(5,3)");
                e.Property(x => x.Toneladas).HasColumnType("numeric(12,3)");

                e.Property(x => x.Status).HasConversion<int>();

                modelBuilder.Entity<MovimentacaoLote>(e =>
{
                e.ToTable("movimentacoes_lote");

                 e.HasKey(x => x.Id);

               e.Property(x => x.Local)
              .HasMaxLength(200)
        .IsRequired();

          e.Property(x => x.Status)
        .HasConversion<int>();

          e.Property(x => x.DataMovimentacao)
        .IsRequired();

          e.HasOne(x => x.LoteMinerio)
        .WithMany()
        .HasForeignKey(x => x.LoteMinerioId)
        .OnDelete(DeleteBehavior.Cascade);
});

            });
        }
    }
}