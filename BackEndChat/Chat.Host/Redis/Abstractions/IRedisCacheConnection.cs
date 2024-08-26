using StackExchange.Redis;

namespace Chat.Host.Redis.Abstractions
{
    public interface IRedisCacheConnection
    {
        IConnectionMultiplexer Connection { get; }
    }
}
