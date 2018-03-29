using MateralTools.Base.MEnum;
using System;
using System.Collections.Generic;

namespace MateralTools.MCache
{
    /// <summary>
    /// 添加缓存委托
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cacheOffset"></param>
    public delegate void SetCacheD(string key, object value, DateTimeOffset cacheOffset);
    /// <summary>
    /// 获得缓存委托
    /// </summary>
    /// <param name="key"></param>
    public delegate object GetCacheD(string key);
    /// <summary>
    /// 缓存基类
    /// </summary>
    public class BaseCacheManager
    {
        /// <summary>
        /// 设置缓存事件
        /// </summary>
        public static event SetCacheD SetCache;
        /// <summary>
        /// 获得缓存事件
        /// </summary>
        public static event GetCacheD GetCache;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        protected static bool _isInit = false;
        /// <summary>
        /// 缓存键值
        /// </summary>
        protected static List<string> _cacheKeys = new List<string>();
        /// <summary>
        /// 默认保存时间
        /// </summary>
        private static int _defaultSaveTime = 30;
        /// <summary>
        /// 默认保存时间
        /// </summary>
        public static int DefaultSaveTime
        {
            get
            {
                return _defaultSaveTime;
            }
            set
            {
                _defaultSaveTime = value;
            }
        }
        /// <summary>
        /// 获得缓存时间点
        /// </summary>
        /// <param name="saveTime">时间基数</param>
        /// <param name="timeTypeE">时间类型</param>
        /// <returns>时间点</returns>
        private static DateTimeOffset GetCacheDateTimeOffset(int saveTime, TimeTypeEnum timeTypeE)
        {
            DateTimeOffset cacheOffset;
            switch (timeTypeE)
            {
                case TimeTypeEnum.Years:
                    cacheOffset = DateTimeOffset.Now.AddYears(saveTime);
                    break;
                case TimeTypeEnum.Months:
                    cacheOffset = DateTimeOffset.Now.AddMonths(saveTime);
                    break;
                case TimeTypeEnum.Day:
                    cacheOffset = DateTimeOffset.Now.AddDays(saveTime);
                    break;
                case TimeTypeEnum.Hours:
                    cacheOffset = DateTimeOffset.Now.AddHours(saveTime);
                    break;
                case TimeTypeEnum.Minutes:
                    cacheOffset = DateTimeOffset.Now.AddMinutes(saveTime);
                    break;
                case TimeTypeEnum.Seconds:
                    cacheOffset = DateTimeOffset.Now.AddSeconds(saveTime);
                    break;
                case TimeTypeEnum.Milliseconds:
                    cacheOffset = DateTimeOffset.Now.AddMilliseconds(saveTime);
                    break;
                default:
                    cacheOffset = DateTimeOffset.Now.AddMinutes(saveTime);
                    break;
            }
            return cacheOffset;
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">Key 唯一</param>
        /// <param name="value">值</param>
        /// <param name="saveTime">保存时间</param>
        public static void Set(string key, object value, int? saveTime = null, TimeTypeEnum timeTypeE = TimeTypeEnum.Minutes)
        {
            saveTime = saveTime ?? DefaultSaveTime;
            SetCache(key, value, GetCacheDateTimeOffset(saveTime.Value, timeTypeE));
        }
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = GetCache(key);
            if (obj != null && obj is T)
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 读取所有缓存
        /// </summary>
        /// <returns></returns>
        public static object GetAll()
        {
            List<object> listM = new List<object>();
            foreach (string key in _cacheKeys)
            {
                listM.Add(GetCache(key));
            }
            return listM;
        }
    }
}
