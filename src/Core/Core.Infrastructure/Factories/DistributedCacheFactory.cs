using Core.Infrastructure.Abstractions.Interfaces;
using Core.Infrastructure.Models;

namespace Core.Infrastructure.Factories;

public sealed class DistributedCacheFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DistributedCacheFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public IDistributedCache Create(DistributedCacheOptions options)
    {
        var cache = _serviceProvider.GetRequiredService<IDistributedCache>();
        return new PrefixedDistributedCache(cache, options.CacheKeyPrefix, options.Expiration, options.SlidingExpiration);
    }
}