using ApnaDhobi.Infrastructure.Services;
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Enums;
using Microsoft.Extensions.Caching.Distributed;

namespace ApnaDhobi.Infrastructure.Factories;

public sealed class CacheFactory
{
    /// <summary>
    /// Creates an instance of ICache based on the specified cache provider and default expiration time. This factory method abstracts the creation logic for different cache implementations, allowing for easy switching between memory cache and Redis cache based on configuration. The method takes a CacheProvider enumeration value to determine which cache implementation to create and a TimeSpan value to set the default expiration time for cache entries. If an invalid cache provider is specified, an ArgumentException is thrown.
    /// </summary>
    /// <param name="provider">Cache provider to use (Memory or Redis)</param>
    /// <param name="defaultExpiration">Default expiration time for cache entries</param>
    /// <returns>It will returns the <see cref="ICacheService"/> </returns>
    /// <exception cref="ArgumentException"></exception>
    public static ICacheService CreateCache(CacheSettings settings, IDistributedCache cache)
    {
        return settings.CacheType switch
        {
            CacheType.Memory => new MemoryCacheService(settings),
            CacheType.Redis => new RedisCacheService(cache, settings),
            _ => throw new ArgumentException("Invalid cache provider specified.")
        };
    }
}