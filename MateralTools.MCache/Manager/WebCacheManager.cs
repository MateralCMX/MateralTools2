using MateralTools.Base.MEnum;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace MateralTools.MCache
{
    /// <summary>
    /// Web缓存管理类
    /// </summary>
    public class WebCacheManager : BaseCacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        private static Cache _cacheM = HttpRuntime.Cache;
        /// <summary>
        /// 初始化
        /// </summary>
        public static void init()
        {
            if (!_isInit)
            {
                SetCache += Set;
                GetCache += Get;
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">Key 唯一</param>
        /// <param name="value">值</param>
        /// <param name="cacheOffset">超时时间</param>
        public static void Set(string key, object value, DateTimeOffset cacheOffset)
        {
            if (_cacheKeys.Contains(key))
            {
                Remove(key);
            }
            _cacheKeys.Add(key);
            _cacheM.Insert(key, value, null, cacheOffset.UtcDateTime, TimeSpan.Zero);
        }
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return _cacheKeys.Contains(key) ? _cacheM[key] : null;
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">Key</param>
        public static void Remove(string key)
        {
            if (_cacheKeys.Contains(key))
            {
                _cacheKeys.Remove(key);
                _cacheM.Remove(key);
            }
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void Clear()
        {
            foreach (string value in _cacheKeys)
            {
                _cacheM.Remove(value);
            }
            _cacheKeys.Clear();
        }
    }
}