using Core.Application.Implementations.Caching;
using Core.Infrastructure.Abstractions.Interfaces.Caching;
using Core.Infrastructure.Models;

public static class CacheFactory
{
    public static ICache CreateCache(CacheType cacheType, CacheOptions options)
    {
        return cacheType switch
        {
            CacheType.MemoryCache => new MemoryCache(options),
            CacheType.DistributedCache => new DistributedCache(options),
            _ => throw new ArgumentException($"Unsupported cache type: {cacheType}")
        };
    }
}