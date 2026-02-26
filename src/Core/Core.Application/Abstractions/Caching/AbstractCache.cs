using Core.Infrastructure.Abstractions.Interfaces.Caching;
using Core.Infrastructure.Models;
namespace Core.Infrastructure.Abstractions;

/// <summary>
/// An abstract base class for cache implementations that provides common functionality such as building cache keys with a prefix. Concrete cache implementations (e.g., in-memory cache, distributed cache) should inherit from this class and implement the abstract methods to provide the actual caching logic. This design allows for code reuse and consistency across different cache implementations while still allowing for flexibility in how the caching is performed.
/// </summary>
/// <param name="options">The cache options.</param>
public abstract class AbstractCache(CacheOptions options) : ICache
{
    protected readonly CacheOptions _options = options;

    public string BuildKey(string key)
    {
        return $"{_options.CacheKeyPrefix}:{key}";
    }

    public abstract Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    public abstract Task SetAsync<T>(string key, T value, CacheOptions options, CancellationToken cancellationToken = default);

    public abstract Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    public abstract Task RefreshAsync(string key, CancellationToken cancellationToken = default);

    public abstract Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        CacheOptions options,
        CancellationToken cancellationToken = default);
}