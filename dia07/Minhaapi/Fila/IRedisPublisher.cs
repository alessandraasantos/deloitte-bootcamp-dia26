using MinhaApi.Models;

namespace MinhaApi.Fila
{
    public interface IRedisPublisher
    {
        Task PublicarLoteAsync(LoteMinerio lote);
    }
}
