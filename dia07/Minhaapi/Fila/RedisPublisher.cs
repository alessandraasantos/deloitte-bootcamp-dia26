using System.Text.Json;
using StackExchange.Redis;

namespace MinhaApi.Fila
{
    public class RedisPublisher : IRedisPublisher
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPublisher(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task PublishAsync(string channel, object message)
        {
            var subscriber = _redis.GetSubscriber();
            var json = JsonSerializer.Serialize(message);

            await subscriber.PublishAsync(channel, json);
        }
    }
}
