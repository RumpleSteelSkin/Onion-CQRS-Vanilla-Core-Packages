using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Pipelines.Caching
{
    public class CacheRemovePipeline<TRequest, TResponse>(IDistributedCache cache, IConfiguration configuration)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (configuration.GetSection("RedisConfigurations:ByPassCache").Get<bool>())
                return await next();
            if (request.CacheGroupKey is not null)
                await RemoveCacheGroupAsync(request.CacheGroupKey, cancellationToken);
            if (!string.IsNullOrEmpty(request.CacheKey))
                await cache.RemoveAsync(request.CacheKey, cancellationToken);
            return await next();
        }

        private async Task RemoveCacheGroupAsync(string cacheGroupKey, CancellationToken cancellationToken)
        {
            byte[]? cachedGroupBytes = await cache.GetAsync(cacheGroupKey, cancellationToken);
            if (cachedGroupBytes is not null)
            {
                foreach (var key in JsonSerializer.Deserialize<HashSet<string>>(
                             Encoding.UTF8.GetString(cachedGroupBytes)) ?? [])
                    await cache.RemoveAsync(key, cancellationToken);
                await cache.RemoveAsync(cacheGroupKey, cancellationToken);
                await cache.RemoveAsync($"{cacheGroupKey}:SlidingExpiration", cancellationToken);
            }
        }
    }
}