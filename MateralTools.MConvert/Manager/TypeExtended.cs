using System;
using System.Data;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// Type扩展
    /// </summary>
    public static class TypeExtended
    {
        /// <summary>
        /// 将类型转换为数据表
        /// 该数据表的列即为类型的属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>数据表</returns>
        public static DataTable MToDataTable(this Type type)
        {
            var dt = new DataTable();
            var props = type.GetProperties();
            foreach (var item in props)
            {
                var colType = item.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                var dc = new DataColumn(item.Name, colType);
                dt.Columns.Add(dc);
            }
            dt.TableName = type.Name;
            return dt;
        }
    }
}
