namespace MinhaApi.Fila
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = "";
        public string StreamName { get; set; } = "";
        public string ConsumerGroup { get; set; } = "";
        public string ConsumerName { get; set; } = "";
        public int MaxDeliveries { get; set; }
        public string DeadLetterStream { get; set; } = "";
    }
}
