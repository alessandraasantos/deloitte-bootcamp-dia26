namespace MinhaApi.Models
{
    public class MovimentacaoLote
    {
        public int Id { get; set; }

        public int LoteMinerioId { get; set; }
        public LoteMinerio LoteMinerio { get; set; } = null!;

        public string Local { get; set; } = "";
        public StatusLote Status { get; set; }

        public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;
    }
}
