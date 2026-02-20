using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFinal.Models
{
    public class Manutencao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EquipamentoMinasId { get; set; }

        [ForeignKey(nameof(EquipamentoMinasId))]
        public EquipamentoMinas EquipamentoMinas { get; set; } = null!;

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [Required]
        public StatusManutencao Status { get; set; }

        public string? Descricao { get; set; }
    }

    public enum StatusManutencao
    {
        Pendente,
        EmAndamento,
        Concluida
    }
}