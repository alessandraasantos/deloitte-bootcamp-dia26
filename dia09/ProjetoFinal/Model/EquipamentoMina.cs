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
        public required string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public TipoEquipamento Tipo { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public required string Modelo { get; set; } = string.Empty;

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "O horímetro não pode ser negativo.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Horimetro { get; set; }

        [Required]
        public StatusOperacional StatusOperacional { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }

        public string? LocalizacaoAtual { get; set; } = string.Empty;
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