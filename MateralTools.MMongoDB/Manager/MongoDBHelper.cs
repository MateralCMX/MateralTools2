using MateralTools.Base;
using MateralTools.MData;
using MateralTools.MVerify;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MMongoDB
{
    public class MongoDBHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connStr = string.Empty;
        /// <summary>
        /// 数据库名称
        /// </summary>
        private string _dataBaseName = string.Empty;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="dataBaseName">数据库名称</param>
        public MongoDBHelper(string connStr, string dataBaseName)
        {
            _connStr = connStr;
            _dataBaseName = dataBaseName;
        }
        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <typeparam name="T">要连接的对象类</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns>连接对象</returns>
        public IMongoCollection<T> GetCollection<T>(string tableName)
        {
            MongoClient client = new MongoClient(_connStr);
            IMongoDatabase database = client.GetDatabase(_dataBaseName);
            tableName = tableName.MIsNullOrEmpty() ? GetTableName<T>() : tableName;
            IMongoCollection<T> collection = database.GetCollection<T>(tableName);
            return collection;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">要插入的对象类</typeparam>
        /// <param name="model">要插入的对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>插入后的对象</returns>
        public T Insert<T>(T model, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            collection.InsertOne(model);
            return model;
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T">要修改的对象类</typeparam>
        /// <param name="model">要修改的对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>修改结果</returns>
        public UpdateResult Update<T>(T model, string tableName = null)
        {
            PropertyInfo keyPi = GetKey<T>();
            if (keyPi != null)
            {
                FilterInfo<T>[] filters = { new FilterInfo<T>(keyPi, keyPi.GetValue(model)) };
                FilterDefinition<T> filterDefinition = GetFilterDefinition(filters);
                UpdateDefinition<T> updateDefinition = GetUpdateDefinition(keyPi, model);
                IMongoCollection<T> collection = GetCollection<T>(tableName);
                UpdateResult updateResult = collection.UpdateMany(filterDefinition, updateDefinition);
                return updateResult;
            }
            else
            {
                throw new MMongoDBException("未定义主键");
            }
        }
        /// <summary>
        /// 修改单个数据
        /// </summary>
        /// <typeparam name="T">要修改的对象类</typeparam>
        /// <param name="model">要修改的对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>修改结果</returns>
        public UpdateResult UpdateOne<T>(T model, string tableName = null)
        {
            PropertyInfo keyPi = GetKey<T>();
            if (keyPi != null)
            {
                FilterInfo<T>[] filters = { new FilterInfo<T>(keyPi, keyPi.GetValue(model)) };
                FilterDefinition<T> filterDefinition = GetFilterDefinition(filters);
                UpdateDefinition<T> updateDefinition = GetUpdateDefinition(keyPi, model);
                IMongoCollection<T> collection = GetCollection<T>(tableName);
                UpdateResult updateResult = collection.UpdateOne(filterDefinition, updateDefinition);
                return updateResult;
            }
            else
            {
                throw new MMongoDBException("未定义主键");
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">要删除的对象类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>删除结果</returns>
        public DeleteResult Delete<T>(FilterInfo<T>[] filters,string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition(filters);
            DeleteResult resM = collection.DeleteMany(filter);
            return resM;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">要删除的对象类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>删除结果</returns>
        public DeleteResult DeleteOne<T>(FilterInfo<T>[] filters, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition(filters);
            DeleteResult resM = collection.DeleteOne(filter);
            return resM;
        }
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T">要查询的目标类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名称</param>
        /// <returns>查询结果</returns>
        public T QueryOne<T>(FilterInfo<T>[] filters, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition<T>(filters);
            T resM = collection.Find(filter).FirstOrDefault();
            return resM;
        }
        /// <summary>
        /// 异步查询单个
        /// </summary>
        /// <typeparam name="T">要查询的目标类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名称</param>
        /// <returns>查询结果</returns>
        public async Task<T> QueryOneAsync<T>(FilterInfo<T>[] filters, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition<T>(filters);
            IAsyncCursor<T> listM = await collection.FindAsync(filter);
            T resM = listM.FirstOrDefault();
            return resM;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">要查询的目标类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名称</param>
        /// <returns>查询结果</returns>
        public List<T> Query<T>(FilterInfo<T>[] filters, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition<T>(filters);
            List<T> resM = collection.Find(filter).ToList();
            return resM;
        }
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="T">要查询的目标类</typeparam>
        /// <param name="filters">过滤器集合</param>
        /// <param name="tableName">表名称</param>
        /// <returns>查询结果</returns>
        public async Task<List<T>> QueryAsync<T>(FilterInfo<T>[] filters, string tableName = null)
        {
            IMongoCollection<T> collection = GetCollection<T>(tableName);
            FilterDefinition<T> filter = GetFilterDefinition<T>(filters);
            IAsyncCursor<T> listM = await collection.FindAsync(filter);
            List<T> resM = listM.ToList();
            return resM;
        }
        /// <summary>
        /// 获得过滤器集合
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <param name="filters">过滤器信息集合</param>
        /// <returns>过滤器结婚</returns>
        private FilterDefinition<T> GetFilterDefinition<T>(FilterInfo<T>[] filters)
        {
            FilterDefinition<T> leftFilter = null;
            FilterDefinition<T> rightFilter = null;
            foreach (FilterInfo<T> filter in filters)
            {
                if (leftFilter == null)
                {
                    leftFilter = ConvertToMongoFilter(filter);
                }
                else
                {
                    rightFilter = ConvertToMongoFilter(filter);
                    switch (filter.Condition)
                    {
                        case ConditionEnum.Or:
                            leftFilter = Builders<T>.Filter.Or(leftFilter, rightFilter);
                            break;
                        case ConditionEnum.And:
                            leftFilter = Builders<T>.Filter.And(leftFilter, rightFilter);
                            break;
                    }
                }
            }
            return leftFilter;
        }
        /// <summary>
        /// 获取修改合集
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <param name="keyPi">主键属性</param>
        /// <param name="model">目标对象</param>
        /// <returns>修改合集</returns>
        private UpdateDefinition<T> GetUpdateDefinition<T>(PropertyInfo keyPi, T model)
        {
            Type ttype = typeof(T);
            PropertyInfo[] pis = ttype.GetProperties();
            UpdateDefinition<T> updateDefinition = null;
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != keyPi.Name)
                {
                    if (updateDefinition == null)
                    {
                        updateDefinition = Builders<T>.Update.Set(pi.Name, pi.GetValue(model));
                    }
                    else
                    {
                        updateDefinition = updateDefinition.Set(pi.Name, pi.GetValue(model));
                    }
                }
            }
            return updateDefinition;
        }
        /// <summary>
        /// 转换为Mongo过滤器
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <param name="filter">过去器信息</param>
        /// <returns>Mongo过滤器</returns>
        private FilterDefinition<T> ConvertToMongoFilter<T>(FilterInfo<T> filter)
        {
            FilterDefinition<T> filterDefinition = null;
            switch (filter.Comparison)
            {
                case ComparisonEnum.NotEqual:
                    filterDefinition = Builders<T>.Filter.Ne(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.Equal:
                    filterDefinition = Builders<T>.Filter.Eq(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.GreaterThan:
                    filterDefinition = Builders<T>.Filter.Gt(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.LessThan:
                    filterDefinition = Builders<T>.Filter.Lt(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.GreaterThanOrEqual:
                    filterDefinition = Builders<T>.Filter.Gte(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.LessThanOrEqual:
                    filterDefinition = Builders<T>.Filter.Lte(filter.PropertyInfo.Name, filter.Value);
                    break;
                case ComparisonEnum.Contains:
                    break;
            }
            return filterDefinition;
        }
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <returns>表名</returns>
        private string GetTableName<T>()
        {
            Type ttype = typeof(T);
            object[] attr = ttype.GetCustomAttributes(typeof(MTableModelAttribute), true);
            if (attr.Length > 0)
            {
                string TableName = (attr[0] as MTableModelAttribute).TabelName;
                return TableName;
            }
            else
            {
                throw new MMongoDBException($"类需要拥有特性{typeof(MTableModelAttribute).Name}");
            }
        }
        /// <summary>
        /// 获得主键
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <returns>主键</returns>
        private PropertyInfo GetKey<T>()
        {
            Type ttype = typeof(T);
            PropertyInfo[] pis = ttype.GetProperties();
            PropertyInfo keypi = null;
            foreach (PropertyInfo pi in pis)
            {
                Attribute attr = pi.GetCustomAttribute(typeof(MKeyAttribute), true);
                if (attr != null)
                {
                    keypi = pi;
                    break;
                }
            }
            if (keypi == null)
            {
                keypi = pis.Where(m => m.PropertyType.GUID == typeof(ObjectId).GUID).FirstOrDefault();
            }
            return keypi;
        }
    }
}
