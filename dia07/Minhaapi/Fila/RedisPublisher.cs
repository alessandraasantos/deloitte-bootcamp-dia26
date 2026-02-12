using System.Text.Json;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace MinhaApi.Fila
{
    public class RedisPublisher : IRedisPublisher
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _streamName;

        public RedisPublisher(IConnectionMultiplexer redis, IConfiguration config)
        {
            _redis = redis;
            _streamName = config["Redis:StreamName"]!;
        }

        public async Task PublishAsync(object message)
        {
            var db = _redis.GetDatabase();

            var json = JsonSerializer.Serialize(message);

            await db.StreamAddAsync(
                _streamName,
                new NameValueEntry[]
                {
                    new("data", json)
                }
            );
        }
    }
}
