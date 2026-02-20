using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<EquipamentoMinas> Equipamentos => Set<EquipamentoMinas>();
    public DbSet<Manutencao> Manutencoes => Set<Manutencao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipamentoMinas>()
            .HasIndex(e => e.Codigo)
            .IsUnique();

        modelBuilder.Entity<EquipamentoMinas>()
            .Property(e => e.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<EquipamentoMinas>()
            .Property(e => e.StatusOperacional)
            .HasConversion<string>();

        modelBuilder.Entity<Manutencao>()
            .Property(m => m.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Manutencao>()
            .HasOne(m => m.EquipamentoMinas)
            .WithMany(e => e.Manutencoes)
            .HasForeignKey(m => m.EquipamentoMinasId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}