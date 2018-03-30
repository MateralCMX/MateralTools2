using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MateralTools.MConvert
{
    /// <summary>
    /// 列表扩展
    /// </summary>
    public static class ListExtended
    {
        /// <summary>
        /// 将列表转换为动态数据集合
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="listM">列表</param>
        /// <returns>动态数据集</returns>
        public static ObservableCollection<T> MToObservableCollection<T>(this List<T> listM)
        {
            ObservableCollection<T> obsM = new ObservableCollection<T>();
            foreach (T item in listM)
            {
                obsM.Add(item);
            }
            return obsM;
        }
        /// <summary>
        /// 转换List为DataTable
        /// </summary>
        /// <typeparam name="T">转换模型</typeparam>
        /// <param name="listM">要转换的List</param>
        /// <returns></returns>
        public static DataTable MToDataTable<T>(this List<T> listM)
        {
            if (listM == null)
            {
                listM = new List<T>();
            }
            Type TType = typeof(T);
            DataTable dt = TType.MToDataTable();
            foreach (T item in listM)
            {
                dt.Rows.Add(item.MToDataRow(dt.NewRow()));
            }
            return dt;
        }
    }
}
