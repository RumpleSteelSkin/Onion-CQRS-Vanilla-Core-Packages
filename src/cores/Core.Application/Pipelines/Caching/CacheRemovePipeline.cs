using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Pipelines.Caching
{
    /// <summary>
    /// MediatR pipeline behavior'ı: İstek sonrası ilgili cache anahtarlarını temizler.
    /// </summary>
    public class CacheRemovePipeline<TRequest, TResponse>(IDistributedCache cache, IConfiguration configuration)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Cache bypass kontrolü
            if (configuration.GetSection("RedisConfigurations:ByPassCache").Get<bool>())
            {
                return await next();
            }


            // Handler'ı çalıştır ve yanıtı al
            TResponse response = await next();

            // Grup bazlı cache temizleme
            if (request.CacheGroupKey is not null)
            {
                await RemoveCacheGroupAsync(request.CacheGroupKey, cancellationToken);
            }

            // Bireysel cache anahtarını temizle
            if (!string.IsNullOrEmpty(request.CacheKey))
            {
                await cache.RemoveAsync(request.CacheKey, cancellationToken);
            }

            return response;
        }

        /// <summary>
        /// Belirtilen grup anahtarına ait tüm cache anahtarlarını kaldırır.
        /// </summary>
        private async Task RemoveCacheGroupAsync(string cacheGroupKey, CancellationToken cancellationToken)
        {
            byte[]? cachedGroupBytes = await cache.GetAsync(cacheGroupKey, cancellationToken);
            if (cachedGroupBytes is not null)
            {
                HashSet<string> keysInGroup =
                    JsonSerializer.Deserialize<HashSet<string>>(Encoding.UTF8.GetString(cachedGroupBytes))
                    ?? new HashSet<string>();

                // Grup içerisindeki her anahtar için cache'i temizle
                foreach (var key in keysInGroup)
                {
                    await cache.RemoveAsync(key, cancellationToken);
                }

                // Grup anahtarını da temizle
                await cache.RemoveAsync(cacheGroupKey, cancellationToken);

                // Opsiyonel: Grup expiration bilgisini tutan anahtarı da kaldır
                string slidingExpirationKey = $"{cacheGroupKey}:SlidingExpiration";
                await cache.RemoveAsync(slidingExpirationKey, cancellationToken);
            }
        }
    }
}