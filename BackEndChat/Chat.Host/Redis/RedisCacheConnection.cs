using Chat.Host.Configurations;
using Chat.Host.Redis.Abstractions;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Chat.Host.Redis
{
    public class RedisCacheConnection : IRedisCacheConnection
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;
        
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
