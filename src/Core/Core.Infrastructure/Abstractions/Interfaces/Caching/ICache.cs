namespace Core.Infrastructure.Abstractions.Interfaces.Caching;
using Core.Infrastructure.Models;
public interface ICache
{
    /// <summary>
    /// Gets a value from the cache. Returns null if the key does not exist or if the value cannot be deserialized to type T.
    /// </summary>
    /// <typeparam name="T">Type of the value to retrieve.</typeparam>
    /// <param name="key">The key name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The value of type T if it exists in the cache, otherwise null.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a value in the cache with the specified key and options. The value will be serialized to JSON before being stored.
    /// </summary>
    /// <typeparam name="T">Type of the value to set.</typeparam>
    /// <param name="key">The key name.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="options">The cache options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T value, CacheOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a value from the cache. If the key does not exist, this method does nothing.
    /// </summary>
    /// <param name="key">The key name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes a value in the cache, resetting its expiration time. If the key does not exist, this method does nothing.
    /// </summary>
    /// <param name="key">The key name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RefreshAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a value from the cache if it exists; otherwise, creates the value using the provided factory function, stores it in the cache with the specified options, and returns it. This method is atomic to prevent multiple concurrent calls from creating multiple values for the same key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key name.</param>
    /// <param name="factory">The factory function to create the value if it does not exist in the cache.</param>
    /// <param name="options">The cache options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The value of type T.</returns>
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        CacheOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Builds the full cache key by applying the configured prefix to the provided key. This ensures that all cache keys are consistently prefixed, which can help avoid collisions when multiple applications share the same cache. The method should be used internally by the cache implementation to ensure that all keys are stored and retrieved with the correct prefix.
    /// </summary>
    /// <param name="key">The key name.</param>
    /// <returns>The full cache key with prefix applied.</returns>
    string BuildKey(string key);
}
