namespace ApnaDhobi.Infrastructure.Enums;
public enum CacheType
{
    /// <summary>
    /// No cache. This cache type disables caching entirely.
    /// </summary>
    None,

    /// <summary>
    /// In-memory cache. This cache is stored in the memory of the application and is not shared across multiple instances of the application. It is fast but does not persist data across application restarts.
    /// </summary>
    Memory,

    /// <summary>
    /// Redis cache. This cache is shared across multiple instances of the application and can be persisted across application restarts. Examples include Redis, Memcached, and SQL Server-based caches.
    /// </summary>
    Redis
}