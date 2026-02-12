namespace MinhaApi.Fila
{
    public interface IRedisPublisher
    {
        Task PublishAsync(string channel, object message);
    }
}
