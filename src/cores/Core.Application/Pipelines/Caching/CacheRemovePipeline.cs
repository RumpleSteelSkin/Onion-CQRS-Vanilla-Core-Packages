using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Core.Application.Pipelines.Caching;

public class CacheRemovePipeline<TRequest, TResponse>(IDistributedCache cache) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next();

        if (request.CacheGroupKey is not null)
        {
            byte[]? cachedGroup = await cache.GetAsync(request.CacheGroupKey, cancellationToken);
            if (cachedGroup is not null)
            {
                HashSet<string>? keysInGroup =
                    JsonSerializer.Deserialize<HashSet<string>>(Encoding.UTF8.GetString(cachedGroup));
                if (keysInGroup != null)
                    foreach (var key in keysInGroup)
                        await cache.RemoveAsync(key, cancellationToken);
                await cache.RemoveAsync(request.CacheGroupKey, cancellationToken);
            }
        }

        if (request.CacheKey is not null)
            await cache.RemoveAsync(request.CacheKey, cancellationToken);

        return await next();
    }
}