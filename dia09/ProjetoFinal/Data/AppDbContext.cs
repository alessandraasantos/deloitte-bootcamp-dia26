using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EquipamentoMinas> Equipamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Índice Único para o Código (Regra do Desafio)
            modelBuilder.Entity<EquipamentoMinas>()
                .HasIndex(e => e.Codigo)
                .IsUnique();

            // Conversão de Enums para String
            modelBuilder.Entity<EquipamentoMinas>()
                .Property(e => e.Tipo)
                .HasConversion<string>();

            modelBuilder.Entity<EquipamentoMinas>()
                .Property(e => e.StatusOperacional)
                .HasConversion<string>();
        }
    }
}