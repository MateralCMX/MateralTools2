using System.Collections.Generic;
using System.Data;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtended
    {
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>目标对象</returns>
        public static T MToObj<T>(this DataRow dr)
        {
            var model = ConvertManager.GetDefultObject<T>();
            if (model != null)
            {
                model.MSetValueByDataRow(dr);
            }
            return model == null ? default(T) : model;
        }
        /// <summary>
        /// 根据列模型转换数据行为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>目标对象</returns>
        public static T MToObjByColumnModelAttribute<T>(this DataRow dr)
        {
            var model = ConvertManager.GetDefultObject<T>();
            if (model != null)
            {
                model.MSetValueByColumnModelAttribute(dr);
            }
            return model == null ? default(T) : model;
        }
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <param name="isColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>目标对象</returns>
        public static T MToObj<T>(this DataRow dr, bool isColumnModelAttribut)
        {
            var model = isColumnModelAttribut ? dr.MToObjByColumnModelAttribute<T>() : dr.MToObj<T>();
            return model;
        }
        /// <summary>
        /// 把数据表转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dt">数据表</param>
        /// <param name="isColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>转换后的List</returns>
        public static List<T> MToList<T>(this DataTable dt, bool isColumnModelAttribut = false)
        {
            var listMs = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                listMs.Add(dr.MToObj<T>(isColumnModelAttribut));
            }
            return listMs;
        }
        /// <summary>
        /// 把数据集转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="ds">数据集</param>
        /// <param name="isColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>转换后的List</returns>
        public static List<List<T>> MToList<T>(this DataSet ds, bool isColumnModelAttribut = false)
        {
            var listMs = new List<List<T>>();
            foreach (DataTable dt in ds.Tables)
            {
                listMs.Add(dt.MToList<T>(isColumnModelAttribut));
            }
            return listMs;
        }
    }
}
