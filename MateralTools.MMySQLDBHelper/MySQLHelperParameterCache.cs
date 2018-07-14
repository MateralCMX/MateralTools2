using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MMySQLDBHelper
{
    /// <summary>   
    /// MySQLHelperParameterCache提供缓存存储过程参数,并能够在运行时从存储过程中探索参数.   
    /// </summary>   
    public sealed class MySQLHelperParameterCache
    {
        #region 私有方法,字段,构造函数   
        // 私有构造函数,妨止类被实例化.   
        private MySQLHelperParameterCache() { }

        // 这个方法要注意   
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>   
        /// 探索运行时的存储过程,返回MySqlParameter参数数组.   
        /// 初始化参数值为 DBNull.Value.   
        /// </summary>   
        /// <param name="connection">一个有效的数据库连接</param>   
        /// <param name="spName">存储过程名称</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        private static MySqlParameter[] DiscoverSpParameterSet(MySqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            MySqlCommand cmd = new MySqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            // 检索cmd指定的存储过程的参数信息,并填充到cmd的Parameters参数集中.   
            MySqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();
            // 如果不包含返回值参数,将参数集中的每一个参数删除.   
            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            // 创建参数数组   
            MySqlParameter[] discoveredParameters = new MySqlParameter[cmd.Parameters.Count];
            // 将cmd的Parameters参数集复制到discoveredParameters数组.   
            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // 初始化参数值为 DBNull.Value.   
            foreach (MySqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>   
        /// MySqlParameter参数数组的深层拷贝.   
        /// </summary>   
        /// <param name="originalParameters">原始参数数组</param>   
        /// <returns>返回一个同样的参数数组</returns>   
        private static MySqlParameter[] CloneParameters(MySqlParameter[] originalParameters)
        {
            MySqlParameter[] clonedParameters = new MySqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (MySqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion 私有方法,字段,构造函数结束  

        #region 缓存方法  

        /// <summary>   
        /// 追加参数数组到缓存.   
        /// </summary>   
        /// <param name="connectionString">一个有效的数据库连接字符串</param>   
        /// <param name="commandText">存储过程名或SQL语句</param>   
        /// <param name="commandParameters">要缓存的参数数组</param>   
        public static void CacheParameterSet(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>   
        /// 从缓存中获取参数数组.   
        /// </summary>   
        /// <param name="connectionString">一个有效的数据库连接字符</param>   
        /// <param name="commandText">存储过程名或SQL语句</param>   
        /// <returns>参数数组</returns>   
        public static MySqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            MySqlParameter[] cachedParameters = paramCache[hashKey] as MySqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion 缓存方法结束  

        #region 检索指定的存储过程的参数集  

        /// <summary>   
        /// 返回指定的存储过程的参数集   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connectionString">一个有效的数据库连接字符</param>   
        /// <param name="spName">存储过程名</param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        public static MySqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>   
        /// 返回指定的存储过程的参数集   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connectionString">一个有效的数据库连接字符.</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        public static MySqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>   
        /// [内部]返回指定的存储过程的参数集(使用连接对象).   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connection">一个有效的数据库连接字符</param>   
        /// <param name="spName">存储过程名</param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        internal static MySqlParameter[] GetSpParameterSet(MySqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>   
        /// [内部]返回指定的存储过程的参数集(使用连接对象)   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connection">一个有效的数据库连接对象</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">   
        /// 是否包含返回值参数   
        /// </param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        internal static MySqlParameter[] GetSpParameterSet(MySqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (MySqlConnection clonedConnection = (MySqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>   
        /// [私有]返回指定的存储过程的参数集(使用连接对象)   
        /// </summary>   
        /// <param name="connection">一个有效的数据库连接对象</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回MySqlParameter参数数组</returns>   
        private static MySqlParameter[] GetSpParameterSetInternal(MySqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            MySqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as MySqlParameter[];
            if (cachedParameters == null)
            {
                MySqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion 参数集检索结束  
    }
}
