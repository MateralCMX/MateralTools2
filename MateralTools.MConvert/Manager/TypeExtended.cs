using System;
using System.Data;
using System.Reflection;

namespace MateralTools.MConvert
{
    public static class TypeExtended
    {
        /// <summary>
        /// 将类型转换为数据表
        /// 该数据表的列即为类型的属性
        /// </summary>
        /// <param name="TType">类型</param>
        /// <returns>数据表</returns>
        public static DataTable MToDataTable(this Type TType)
        {
            Type colType;
            DataTable dt = new DataTable();
            object[] obj = new object[0];
            PropertyInfo[] props = TType.GetProperties();
            DataColumn dc;
            foreach (PropertyInfo item in props)
            {
                colType = item.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                dc = new DataColumn(item.Name, colType);
                dt.Columns.Add(dc);
            }
            dt.TableName = TType.Name;
            return dt;
        }
    }
}
