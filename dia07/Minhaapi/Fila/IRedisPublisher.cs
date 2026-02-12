using System.Threading.Tasks;

namespace MinhaApi.Fila
{
    public interface IRedisPublisher
    {
        Task PublishAsync(object message);
    }
}
