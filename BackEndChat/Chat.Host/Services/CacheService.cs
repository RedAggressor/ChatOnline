using Chat.Host.Helpers.Abstractions;
using Chat.Host.Redis.Abstractions;
using Chat.Host.Services.Abstractions;
using StackExchange.Redis;

namespace Chat.Host.Services
{
    public class CacheService : ICacheService
    {
        private readonly IRedisCacheConnection _redis;
        private readonly IJsonProcess _jsonProcess;

        public CacheService(
            IRedisCacheConnection redisCacheConnection,
            IJsonProcess jsonProcess
            )
        {
            _redis = redisCacheConnection;
            _jsonProcess = jsonProcess;
        }

        public Task AddOrUpdateAsync<T>(string key, T value)
        => AddOrUpdatePrivateAsync(key, value);

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var serialized = await redis.StringGetAsync(key);

            return serialized.HasValue ?
                _jsonProcess.Deserialize<T>(serialized.ToString())
                : default(T)!;
        }

        private async Task AddOrUpdatePrivateAsync<T>(string key, T value,
            IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            //expiry = expiry ?? _config.CacheTimeout;

            var serialized = _jsonProcess.Serialize(value);

            await redis.StringSetAsync(key, serialized);
        }

        private IDatabase GetRedisDatabase() => _redis.Connection.GetDatabase();

        private async Task DeleteKeyPrivateAsync(string key, IDatabase redis = null!)
        {
            redis = redis ?? GetRedisDatabase();

            await redis.KeyDeleteAsync(key);
        }

        public async Task DeleteKeyAsync(string key) =>
            await DeleteKeyPrivateAsync(key);
    }
}

