using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<EquipamentoMinas> Equipamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Regra de Negócio: Codigo único no banco de dados
        modelBuilder.Entity<EquipamentoMinas>()
            .HasIndex(e => e.Codigo)
            .IsUnique();

        // Salvar Enums como Strings no banco (opcional, mas melhor para leitura manual no Postgres)
        modelBuilder.Entity<EquipamentoMinas>()
            .Property(e => e.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<EquipamentoMinas>()
            .Property(e => e.StatusOperacional)
            .HasConversion<string>();
    }
}