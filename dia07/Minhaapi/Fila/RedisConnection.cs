using StackExchange.Redis;

namespace MinhaApi.Fila
{
    public static class RedisConnection
    {
        public static IConnectionMultiplexer Connect(string connectionString)
        {
            return ConnectionMultiplexer.Connect(connectionString);
        }
    }
}
