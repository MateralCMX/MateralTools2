using System.Collections.Generic;
using System.Data;

namespace MateralTools.MConvert
{
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
            T Model = ConvertManager.GetDefultObject<T>();
            if (Model != null)
            {
                Model.MSetValueByDataRow(dr);
            }
            return Model == null ? default(T) : (T)Model;
        }
        /// <summary>
        /// 根据列模型转换数据行为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>目标对象</returns>
        public static T MToObjByColumnModelAttribute<T>(this DataRow dr)
        {
            T Model = ConvertManager.GetDefultObject<T>();
            if (Model != null)
            {
                Model.MSetValueByColumnModelAttribute(dr);
            }
            return Model == null ? default(T) : (T)Model;
        }
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <param name="IsColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>目标对象</returns>
        public static T MToObj<T>(this DataRow dr, bool IsColumnModelAttribut)
        {
            T Model;
            if (IsColumnModelAttribut)
            {
                Model = dr.MToObjByColumnModelAttribute<T>();
            }
            else
            {
                Model = dr.MToObj<T>();
            }
            return Model;
        }
        /// <summary>
        /// 把数据表转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dt">数据表</param>
        /// <param name="IsColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>转换后的List</returns>
        public static List<T> MToList<T>(this DataTable dt, bool IsColumnModelAttribut = false)
        {
            List<T> listMs = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                listMs.Add(dr.MToObj<T>(IsColumnModelAttribut));
            }
            return listMs;
        }
        /// <summary>
        /// 把数据集转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="dt">数据集</param>
        /// <param name="IsColumnModelAttribut">是否按照Attribut转换</param>
        /// <returns>转换后的List</returns>
        public static List<List<T>> MToList<T>(this DataSet ds, bool IsColumnModelAttribut = false)
        {
            List<List<T>> listMs = new List<List<T>>();
            foreach (DataTable dt in ds.Tables)
            {
                listMs.Add(dt.MToList<T>(IsColumnModelAttribut));
            }
            return listMs;
        }
    }
}
