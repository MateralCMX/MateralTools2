using System;
using MateralTools.MCache.Model;
using MateralTools.MVerify;
using Microsoft.Extensions.Caching.Memory;

namespace MateralTools.MCache.Manager
{
    /// <summary>
    /// 内存缓存管理器
    /// </summary>
    public class RamCacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        public IMemoryCache MemoryCache { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public RamCacheManager()
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="memoryCache">缓存对象</param>
        public RamCacheManager(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }
        /// <summary>
        /// 获得缓存对象
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存值</returns>
        public object Get(string key)
        {
            return Get<object>(key);
        }
        /// <summary>
        /// 获得缓存对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>缓存值</returns>
        public T Get<T>(string key)
        {
            return !key.MIsNullOrEmpty() ? (MemoryCache.TryGetValue(key, out T value) ? value : throw new MCacheException($"缓存中不存在{nameof(key)}为{key}的{typeof(T).Name}对象")) : throw new MCacheException($"{nameof(key)}值不能为空。");
        }
        /// <summary>
        /// 保存缓存对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">保存的值</param>
        public void Set<T>(string key, T value)
        {
            if (key.MIsNullOrEmpty())throw new MCacheException($"{nameof(key)}值不能为空。");
            MemoryCache.Set(key, value);
        }
        /// <summary>
        /// 保存缓存对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">保存的值</param>
        /// <param name="absoluteExpiration">过期时间点</param>
        public void Set<T>(string key, T value, DateTimeOffset absoluteExpiration)
        {
            if (key.MIsNullOrEmpty())throw new MCacheException($"{nameof(key)}值不能为空。");
            MemoryCache.Set(key, value, absoluteExpiration);
        }
        /// <summary>
        /// 保存缓存对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">保存的值</param>
        /// <param name="absoluteExpirationRelativeToNow">过期时间</param>
        public void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow)
        {
            if (key.MIsNullOrEmpty())throw new MCacheException($"{nameof(key)}值不能为空。");
            MemoryCache.Set(key, value, absoluteExpirationRelativeToNow);
        }
    }
}
