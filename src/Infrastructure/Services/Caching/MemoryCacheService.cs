using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ApnaDhobi.Infrastructure.Services;

public sealed class MemoryCacheService(CacheSettings settings) : ICacheService
{
    private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions
    {
        SizeLimit = 1024,
        ExpirationScanFrequency = TimeSpan.FromMinutes(1)
    });
    private readonly CacheSettings settings = settings ?? new CacheSettings();

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        _cache.TryGetValue(BuildKey(key), out T? value);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = settings.Expiration,
            SlidingExpiration = settings.SlidingExpiration
        };
        _cache.Set(BuildKey(key), value, cacheEntryOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _cache.Remove(BuildKey(key));
        return Task.CompletedTask;
    }

    public Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(BuildKey(key), out T? value))
        {
            return value!;
        }

        value = await factory(cancellationToken);
        await SetAsync(key, value, cancellationToken);
        return value;
    }

    public string BuildKey(string key)
    {
        return $"{settings.CacheKeyPrefix}_{key}";
    }
}