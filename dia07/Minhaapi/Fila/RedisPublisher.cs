using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using MinhaApi.Models;

namespace MinhaApi.Fila
{
    public class RedisPublisher : IRedisPublisher
    {
        private readonly IDatabase _db;
        private readonly RedisSettings _settings;

        public RedisPublisher(IOptions<RedisSettings> options)
        {
            _settings = options.Value;

            var connection = ConnectionMultiplexer.Connect(_settings.ConnectionString);
            _db = connection.GetDatabase();
        }

        public async Task PublicarLoteAsync(LoteMinerio lote)
        {
            var json = JsonSerializer.Serialize(lote);

            await _db.StreamAddAsync(
                _settings.StreamName,
                new NameValueEntry[]
                {
                    new("data", json)
                }
            );
        }
    }
}
