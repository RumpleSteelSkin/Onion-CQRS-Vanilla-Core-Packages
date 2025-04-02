using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching
{
    public class AddCachePipeline<TRequest, TResponse>(IDistributedCache cache, IConfiguration configuration)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheAbleRequest
    {
        private readonly CacheSettings _cacheSettings = configuration.GetSection("RedisConfigurations").Get<CacheSettings>();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Cache bypass kontrolü
            if (_cacheSettings.ByPassCache)
            {
                return await next();
            }

            // Cache'den veriyi al
            byte[]? cachedResponse = await cache.GetAsync(request.CacheKey, cancellationToken);
            if (cachedResponse is not null)
            {
                return JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(cachedResponse));
            }

            // Cache'de veri yoksa handler'ı çalıştır ve cache'e ekle
            TResponse response = await next();
            await SetCacheAsync(request, response, cancellationToken);
            return response;
        }

        /// <summary>
        /// Yanıtı cache'e ekler ve grup anahtarı varsa ilgili grubu günceller.
        /// </summary>
        private async Task SetCacheAsync(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            // Sliding expiration değerini belirle
            TimeSpan slidingExpiration =
                request.SlidingExpiration ?? TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration);

            // Cache seçenekleri
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration
            };

            // Yanıtı cache'e ekle
            byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
            await cache.SetAsync(request.CacheKey, serializedData, cacheEntryOptions, cancellationToken);

            // Eğer CacheGroupKey tanımlı ise, cache grubunu güncelle
            if (request.CacheGroupKey is not null)
            {
                await AddCacheKeyToGroupAsync(request, slidingExpiration, cancellationToken);
            }
        }

        /// <summary>
        /// Cache grubundaki anahtarları günceller; yeni anahtar ekler veya mevcut grubu günceller.
        /// </summary>
        private async Task AddCacheKeyToGroupAsync(TRequest request, TimeSpan expiration,
            CancellationToken cancellationToken)
        {
            byte[]? cachedGroupBytes = await cache.GetAsync(request.CacheGroupKey, cancellationToken);
            HashSet<string> cacheKeysInGroup = cachedGroupBytes is not null
                ? JsonSerializer.Deserialize<HashSet<string>>(Encoding.UTF8.GetString(cachedGroupBytes)) ??
                  new HashSet<string>()
                : new HashSet<string>();

            // Anahtarın grup içerisinde olup olmadığını kontrol et
            if (!cacheKeysInGroup.Contains(request.CacheKey))
            {
                cacheKeysInGroup.Add(request.CacheKey);
            }

            // Grubu serialize edip cache'e ekle
            byte[] newGroupCache = JsonSerializer.SerializeToUtf8Bytes(cacheKeysInGroup);
            var groupCacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = expiration
            };

            await cache.SetAsync(request.CacheGroupKey, newGroupCache, groupCacheOptions, cancellationToken);

            // Opsiyonel: Grup expiration bilgisini ayrı bir anahtar altında tutmak istersen
            string slidingExpirationKey = $"{request.CacheGroupKey}:SlidingExpiration";
            byte[] expirationBytes = Encoding.UTF8.GetBytes(expiration.TotalMinutes.ToString());
            await cache.SetAsync(slidingExpirationKey, expirationBytes, groupCacheOptions, cancellationToken);
        }
    }
}