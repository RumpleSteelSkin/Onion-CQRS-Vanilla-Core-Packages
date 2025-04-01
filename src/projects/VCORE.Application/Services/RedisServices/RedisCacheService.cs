using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace VCORE.Application.Services.RedisServices;

public class RedisCacheService(IDistributedCache distributedCache) : IRedisService
{
    public async Task AddDataAsync<T>(string key, T value)
    {
        await distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(30)
            });
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        byte[]? datas = await distributedCache.GetAsync(key);
        return datas == null ? default! : JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(datas));
    }

    public async Task RemoveDataAsync(string key)
    {
        await distributedCache.RemoveAsync(key);
    }
}