using Core.Infrastructure.Abstractions.Interfaces.Caching;
using Core.Infrastructure.Models;

namespace Core.Application.Implementations.Caching;

public sealed class DistributedCache : ICache
{
    private readonly CacheOptions _options;
    private readonly ICache cache;

    public DistributedCache(CacheOptions options)
    {
        _options = options;
        cache = CacheFactory.CreateCache(CacheType.DistributedCache, options);    
    }

    public string BuildKey(string key)
    {
        return $"{_options.CacheKeyPrefix}:{key}";
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildKey(key);
        var value = await cache.GetAsync<T>(cacheKey, cancellationToken);
        if (value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, CacheOptions options, CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildKey(key);
        var value = await cache.GetAsync<T>(cacheKey, cancellationToken);
        if (value is T typedValue)
        {
            return typedValue;
        }

        return await CreateAndSetAsync(cacheKey, factory, options, cancellationToken);
    }

    private async Task<T> CreateAndSetAsync<T>(string cacheKey, Func<CancellationToken, Task<T>> factory, CacheOptions options, CancellationToken cancellationToken)
    {
        var value = await factory(cancellationToken);
        SetAsync(cacheKey, value, options, cancellationToken).Wait(cancellationToken);
        return value;
    }

    public Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        // MemoryCache does not support refreshing. This method can be a no-op or can be implemented to reset the expiration time.
        return Task.CompletedTask;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildKey(key);
        await cache.RemoveAsync(cacheKey, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, CacheOptions options, CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildKey(key);
        await cache.SetAsync(cacheKey, value, options, cancellationToken);
    }
}