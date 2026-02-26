using ApnaDhobi.Infrastructure.Enums;

namespace ApnaDhobi.Infrastructure.Models;

public sealed class CacheSettings
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
    /// The type of cache to use. This can be used to specify whether to use a distributed cache or an in-memory cache. Default is Redis.
    /// </summary>
    public CacheType CacheType { get; set; } = CacheType.Redis;

    public bool Enabled => CacheType != CacheType.None;
}