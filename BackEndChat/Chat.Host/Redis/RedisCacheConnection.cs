using Chat.Host.Configurations;
using Chat.Host.Redis.Abstractions;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Runtime.Intrinsics.X86;

namespace Chat.Host.Redis
{
    public class RedisCacheConnection : IRedisCacheConnection
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;
        //private readonly string _connectionStringRedis = "cacheforchat.redis.cache.windows.net:6380,password=femJrrzXg5Rh6YPaOCW5oECYMFPCF9rawAzCaPe0cq0=,ssl=True,abortConnect=False";

        public RedisCacheConnection(IOptions<RedisConfig> config)
        {
            var redisConfigurationOptions = ConfigurationOptions.Parse(config.Value.ConectionString);

            _connectionLazy = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(redisConfigurationOptions));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if(!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}
