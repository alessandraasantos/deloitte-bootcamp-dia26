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
        // O índice único será configurado via Fluent API no DbContext
        public string Codigo { get; set; }

        [Required]
        public TipoEquipamento Tipo { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O horímetro não pode ser negativo.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Horimetro { get; set; }

        [Required]
        public StatusOperacional StatusOperacional { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }

        public string LocalizacaoAtual { get; set; }

        // Construtor para garantir o Trim() no código conforme a regra
        public EquipamentoMinas()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                Codigo = Codigo.Trim();
            }
        }
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
