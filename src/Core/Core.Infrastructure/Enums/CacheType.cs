public enum CacheType
{
    /// <summary>
    /// In-memory cache. This cache is stored in the memory of the application and is not shared across multiple instances of the application. It is fast but does not persist data across application restarts.
    /// </summary>
    MemoryCache,

    /// <summary>
    /// Distributed cache. This cache is shared across multiple instances of the application and can be persisted across application restarts. Examples include Redis, Memcached, and SQL Server-based caches.
    /// </summary>
    DistributedCache
}