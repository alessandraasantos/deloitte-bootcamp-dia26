using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFinal.Models
{
    public class EquipamentoMinas
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório.")]
        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        public TipoEquipamento Tipo { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "O horímetro não pode ser negativo.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Horimetro { get; set; }

        [Required]
        public StatusOperacional StatusOperacional { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }

        public string? LocalizacaoAtual { get; set; }

        // Relacionamento 1:N → Um equipamento pode ter várias manutenções
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();
    }

    public enum TipoEquipamento
    {
        Caminhao,
        Escavadeira,
        Perfuratriz,
        Carregadeira,
        Trator
    }

    public enum StatusOperacional
    {
        Operacional,
        EmManutencao,
        Parado
    }
}