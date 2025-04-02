namespace Core.Application.Pipelines.Caching;

public interface ICacheAbleRequest
{
    string? CacheKey { get; }
    string? CacheGroupKey { get; }
    TimeSpan? SlidingExpiration { get; }
}