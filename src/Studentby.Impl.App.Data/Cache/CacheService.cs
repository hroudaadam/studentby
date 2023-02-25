using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.Shared.Helpers;
using System.Threading;

namespace Studentby.Impl.App.Data.Cache;

internal sealed class CacheService<TEntity> : ICacheService<TEntity>, IDisposable where TEntity : BaseEntity
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheSettings _cacheSettings;
    private CancellationTokenSource _resetCacheToken = new();
    private bool _disposed = false;

    public CacheService(IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
    {
        _memoryCache = memoryCache;
        _cacheSettings = cacheSettings.Value;
    }

    public async Task<TResult> UseCacheAsync<TResult>(CacheKey cacheKey, Func<Task<TResult>> valueProvider)
    {
        Guard.NotNull(cacheKey, nameof(cacheKey));
        Guard.NotNullOrEmpty(cacheKey.Key, nameof(cacheKey.Key));
        Guard.NotNull(valueProvider, nameof(valueProvider));

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSize(1)
            .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheSettings.Lifetime));

        if (!_memoryCache.TryGetValue(cacheKey.Key, out TResult value))
        {
            value = await valueProvider();
            _memoryCache.Set(cacheKey.Key, value, cacheOptions);
        }
        return value;
    }

    public void Remove(CacheKey cacheKey)
    {
        Guard.NotNull(cacheKey, nameof(cacheKey));
        Guard.NotNull(cacheKey.Key, nameof(cacheKey.Key));
        _memoryCache.Remove(cacheKey.Key);
    }

    public void Clear()
    {
        if (_resetCacheToken != null)
        {
            if (!_resetCacheToken.IsCancellationRequested)
            {
                _resetCacheToken.Cancel();
            }
            _resetCacheToken.Dispose();
        }
        _resetCacheToken = new();
    }

    public void Dispose()
    {
        if (_disposed) return;
        if (_resetCacheToken != null)
        {
            _resetCacheToken.Dispose();
            _resetCacheToken = null;
        }
        _disposed = true;
    }
}