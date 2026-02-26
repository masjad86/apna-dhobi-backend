using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Interfaces;

namespace ApnaDhobi.Infrastructure.Services;
public sealed class RedisCacheService(IDistributedCache cache, CacheSettings settings) : ICacheService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (!settings.Enabled) return default;

        var fullKey = BuildKey(key);
        var bytes = await cache.GetAsync(fullKey, cancellationToken);

        if (bytes is null || bytes.Length == 0) return default;

        return JsonSerializer.Deserialize<T>(bytes, JsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        if (!settings.Enabled) return;

        var fullKey = BuildKey(key);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(value, JsonOptions);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = settings.Expiration,
            SlidingExpiration = settings.SlidingExpiration
        };

        await cache.SetAsync(fullKey, bytes, options, cancellationToken);
    }

    public async Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(key);
        await cache.RefreshAsync(fullKey, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(key);
        await cache.RemoveAsync(fullKey, cancellationToken);
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        CancellationToken cancellationToken = default)
    {
        // Try cache first
        var cached = await GetAsync<T>(key, cancellationToken);
        if (cached is not null) return cached;

        // Create
        var created = await factory(cancellationToken);

        // Cache it (avoid caching null if that's your convention)
        await SetAsync(key, created!, cancellationToken);

        return created!;
    }

    public string BuildKey(string key)
        => $"{settings.CacheKeyPrefix}_{key}";
}
