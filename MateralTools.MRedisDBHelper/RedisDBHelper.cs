using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace MateralTools.MRedisDBHelper
{
    public class RedisDBHelper : IDisposable
    {
        private IConfigurationRoot _config;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config">
        /// {
        ///   "RedisConfig": {
        ///     "Redis_Default": {
        ///       "Connection": "127.0.0.1:6379",
        ///       "InstanceName": "Redis1:"
        ///     }
        ///   }
        /// }
        /// </param>
        public RedisDBHelper(IConfigurationRoot config)
        {
            _config = config;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }
        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <param name="redisConfig">RedisConfig配置文件</param>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect(IConfigurationSection redisConfig)
        {
            var redisInstanceName = redisConfig["InstanceName"];
            var connStr = redisConfig["Connection"];
            return _connections.GetOrAdd(redisInstanceName, p => ConnectionMultiplexer.Connect(connStr));
        }
        /// <summary>
        /// 检查入参数
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis 名称</param>
        /// <returns></returns>
        private IConfigurationSection CheckeConfig(string configName)
        {
            IConfigurationSection redisConfig = _config.GetSection("RedisConfig").GetSection(configName);
            if (redisConfig == null)
            {
                throw new ArgumentNullException($"{configName}找不到对应的RedisConfig配置！");
            }
            var redisInstanceName = redisConfig["InstanceName"];
            var connStr = redisConfig["Connection"];
            if (string.IsNullOrEmpty(redisInstanceName))
            {
                throw new ArgumentNullException($"{configName}找不到对应的InstanceName");
            }
            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException($"{configName}找不到对应的Connection");
            }
            return redisConfig;
        }
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis 名称</param>
        /// <param name="defaultDb">默认为null：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase(string configName = null, int defaultDb = 0)
        {
            IConfigurationSection redisConfig = CheckeConfig(configName);
            var strDefalutDatabase = redisConfig["Connection"];
            if (!string.IsNullOrEmpty(strDefalutDatabase) && Int32.TryParse(strDefalutDatabase, out var intDefaultDatabase))
            {
                defaultDb = intDefaultDatabase;
            }
            return GetConnect(redisConfig).GetDatabase(defaultDb);
        }
        /// <summary>
        /// 获取服务器
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis 名称</param>
        /// <param name="endPointsIndex"></param>
        /// <returns></returns>
        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            IConfigurationSection redisConfig = CheckeConfig(configName);
            var connStr = redisConfig["Connection"];

            var confOption = ConfigurationOptions.Parse((string)connStr);
            return GetConnect(redisConfig).GetServer(confOption.EndPoints[endPointsIndex]);
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis 名称</param>
        /// <returns></returns>
        public ISubscriber GetSubscriber(string configName = null)
        {
            IConfigurationSection redisConfig = CheckeConfig(configName);
            return GetConnect(redisConfig).GetSubscriber();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}
