using MateralTools.MVerify;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace MateralTools.MCache
{
    /// <summary>
    /// 内存缓存管理器
    /// </summary>
    public class RAMCacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        private IMemoryCache _memoryCache;
        /// <summary>
        /// 缓存对象
        /// </summary>
        public IMemoryCache MemoryCache { get => _memoryCache; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public RAMCacheManager()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="memoryCache">缓存对象</param>
        public RAMCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
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
            if (!key.MIsNullOrEmpty())
            {
                if (_memoryCache.TryGetValue(key, out T value))
                {
                    return value;
                }
                else
                {
                    throw new MCacheException($"缓存中不存在{nameof(key)}为{key}的{typeof(T).Name}对象");
                }
            }
            else
            {
                throw new MCacheException($"{nameof(key)}值不能为空。");
            }
        }
        /// <summary>
        /// 保存缓存对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">保存的值</param>
        public void Set<T>(string key, T value)
        {
            if (!key.MIsNullOrEmpty())
            {
                _memoryCache.Set(key, value);
            }
            else
            {
                throw new MCacheException($"{nameof(key)}值不能为空。");
            }
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
            if (!key.MIsNullOrEmpty())
            {
                _memoryCache.Set(key, value, absoluteExpiration);
            }
            else
            {
                throw new MCacheException($"{nameof(key)}值不能为空。");
            }
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
            if (!key.MIsNullOrEmpty())
            {
                _memoryCache.Set(key, value, absoluteExpirationRelativeToNow);
            }
            else
            {
                throw new MCacheException($"{nameof(key)}值不能为空。");
            }
        }
    }
}
