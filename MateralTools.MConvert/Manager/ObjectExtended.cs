using MateralTools.MData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MateralTools.MConvert.Model;
using MateralTools.MData.Model;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtended
    {
        /// <summary>
        /// 可转换类型字典
        /// </summary>
        private static readonly Dictionary<Type, Func<object, object>> Dict = new Dictionary<Type, Func<object, object>>();
        /// <summary>
        /// 构造方法
        /// </summary>
        static ObjectExtended()
        {
            Dict.Add(typeof(int), WrapValueConvert(Convert.ToInt32));
            Dict.Add(typeof(long), WrapValueConvert(Convert.ToInt64));
            Dict.Add(typeof(short), WrapValueConvert(Convert.ToInt16));
            Dict.Add(typeof(int?), WrapValueConvert(Convert.ToInt32));
            Dict.Add(typeof(double), WrapValueConvert(Convert.ToDouble));
            Dict.Add(typeof(float), WrapValueConvert(Convert.ToSingle));
            Dict.Add(typeof(Guid), f => new Guid(f.ToString()));
            Dict.Add(typeof(string), Convert.ToString);
            Dict.Add(typeof(DateTime), WrapValueConvert(Convert.ToDateTime));
        }
        /// <summary>
        /// 写入值转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Func<object, object> WrapValueConvert<T>(Func<object, T> input) where T : struct
        {
            return i =>
            {
                if (i == null || i is DBNull) return null;
                return input(i);
            };
        }
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="dr">数据行模版</param>
        /// <returns>数据行</returns>
        public static DataRow MToDataRow(this object obj, DataRow dr)
        {
            if (dr == null)throw new MConvertException("数据行不可为空");
            var type = obj.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj, null);
                if (value == null)
                {
                    dr[prop.Name] = DBNull.Value;
                }
                else
                {
                    dr[prop.Name] = value;
                }
            }

            return dr;
        }
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>数据行</returns>
        public static DataRow MToDataRow(this object obj)
        {
            var type = obj.GetType();
            var dt = type.MToDataTable();
            var dr = dt.NewRow();
            return obj.MToDataRow(dr);
        }
        /// <summary>
        /// 通过数据行设置对象的值
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="dr">数据行</param>
        public static void MSetValueByDataRow(this object obj, DataRow dr)
        {
            var type = obj.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                try
                {
                    prop.SetValue(obj, dr[prop.Name], null);
                }
                catch
                {
                    // ignored
                }

                //if (!(dr[prop.Name] is System.DBNull) && dr[prop.Name].GetType().Name == prop.PropertyType.Name)
                //{
                //    prop.SetValue(obj, dr[prop.Name], null);
                //}
                //else
                //{
                //    PropertyInfo[] subProps = prop.PropertyType.GetProperties();
                //    if (!(dr[prop.Name] is System.DBNull) && subProps.Length == 2 && dr[prop.Name].GetType().Name == subProps[1].PropertyType.Name)
                //    {
                //        prop.SetValue(obj, dr[prop.Name], null);
                //    }
                //}
            }
        }
        /// <summary>
        /// 通过列模型特性设置对象的值
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="dr">数据行</param>
        public static void MSetValueByColumnModelAttribute(this object obj, DataRow dr)
        {

            var type = obj.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                foreach (var attr in Attribute.GetCustomAttributes(prop))
                {
                    if (attr.GetType() != typeof(MColumnModelAttribute)) continue;
                    var cma = attr as MColumnModelAttribute;
                    try
                    {
                        if (cma != null) prop.SetValue(obj, dr[cma.DbColumnName], null);
                    }
                    catch
                    {
                        // ignored
                    }

                    //if (!(dr[cma.DBColumnName] is System.DBNull) && dr[cma.DBColumnName].GetType().Name == prop.PropertyType.Name)
                    //{
                    //    prop.SetValue(obj, dr[cma.DBColumnName], null);
                    //}
                    //else
                    //{
                    //    PropertyInfo[] subProps = prop.PropertyType.GetProperties();
                    //    if (!(dr[cma.DBColumnName] is System.DBNull) && subProps.Length == 2 && dr[cma.DBColumnName].GetType().Name == subProps[1].PropertyType.Name)
                    //    {
                    //        prop.SetValue(obj, dr[cma.DBColumnName], null);
                    //    }
                    //}
                }
            }
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="type">要设置的类型</param>
        /// <returns>默认对象</returns>
        public static object MGetDefultObject(this object obj, Type type)
        {
            return ConvertManager.GetDefultObject(type);
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <typeparam name="T">要设置的类型</typeparam>
        /// <param name="obj">要设置的对象</param>
        /// <returns>默认对象</returns>
        public static T MGetDefultObject<T>(this object obj)
        {
            return ConvertManager.GetDefultObject<T>();
        }
        /// <summary>
        /// 对象转换为Josn
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的Json字符串</returns>
        public static string MToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="targetM">复制目标对象</param>
        /// <param name="notCopyPropertieNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static void MCopyProperties<T>(this object sourceM, T targetM, params string[] notCopyPropertieNames)
        {
            if (sourceM == null) return;
            var t1Props = sourceM.GetType().GetProperties();
            var t2Props = typeof(T).GetProperties();
            foreach (var prop in t1Props)
            {
                if (notCopyPropertieNames.Contains(prop.Name)) continue;
                var tempProp = t2Props.FirstOrDefault(m => m.Name == prop.Name);
                if (tempProp != null)
                {
                    tempProp.SetValue(targetM, prop.GetValue(sourceM, null), null);
                }
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="notCopyPropertieNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static T MCopyProperties<T>(this object sourceM, params string[] notCopyPropertieNames)
        {
            if (sourceM == null) return default(T);
            var targetM = ConvertManager.GetDefultObject<T>();
            sourceM.MCopyProperties(targetM);
            return targetM;
        }
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] MToBytes(this object obj)
        {
            byte[] buff;
            using (var ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// 判断是否提供到特定类型的转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool CanConvertTo(this object obj, Type targetType)
        {
            return Dict.ContainsKey(targetType);
        }
        /// <summary>
        /// 转换到特定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object obj)
        {
            return (T)ConvertTo(obj, typeof(T));
        }
        /// <summary>
        /// 转换到特定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ConvertTo(this object obj, Type targetType)
        {
            if (obj == null)return !targetType.IsValueType ? (object) null : throw new ArgumentNullException(nameof(obj), "不能将null转换为" + targetType.Name);
            if (obj.GetType() == targetType || targetType.IsInstanceOfType(obj))return obj;
            if (Dict.ContainsKey(targetType))return Dict[targetType](obj);
            try
            {
                return Convert.ChangeType(obj, targetType);
            }
            catch
            {
                throw new NotImplementedException("未实现到" + targetType.Name + "的转换");
            }
        }
    }
}
