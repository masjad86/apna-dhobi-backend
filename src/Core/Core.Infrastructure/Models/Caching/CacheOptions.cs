namespace Core.Infrastructure.Models;

public sealed class CacheOptions
{
    /// <summary>
    /// The prefix to be added to all cache keys. This can be useful to avoid key collisions when multiple applications are using the same cache. Default is an empty string.
    /// </summary>
    public string CacheKeyPrefix { get; set; } = string.Empty;
    /// <summary>
    /// The absolute expiration time. The cache entry will expire after this time regardless of whether it has been accessed or not. Default is 15 minutes.
    /// </summary>
    public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(15);
    /// <summary>
    /// The sliding expiration time. If the cache entry is accessed within this time, the expiration time will be reset. Default is 30 seconds.
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// The type of cache to use. This can be used to specify whether to use a distributed cache or an in-memory cache. Default is DistributedCache.
    /// </summary>
    public CacheType CacheType { get; set; } = CacheType.DistributedCache;

    /// <summary>
    /// The connection options for the cache. This can include settings such as the connection string, database number, and other configuration options specific to the cache implementation being used (e.g., Redis, Memcached). This property is optional and may not be required for all cache types.
    /// </summary>
    public CacheConnectionOptions? ConnectionOptions { get; set; }
}