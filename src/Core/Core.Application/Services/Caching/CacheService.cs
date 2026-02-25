using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Core.Infrastructure.Abstractions.Interfaces;
using Core.Infrastructure.Abstractions.Interfaces.Caching;
using Core.Infrastructure.Models;

namespace Core.Application.Services.Caching
{
    public class CacheService(ICache cache) : ICacheService
    {
        private readonly ICache _cache = cache;

        public string BuildKey(string key)
        {
            return _cache.BuildKey(key);
        }

        public Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, CacheOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, CacheOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}