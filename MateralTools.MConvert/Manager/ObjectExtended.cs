using MateralTools.Base.MTable;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MateralTools.MConvert
{
    public static class ObjectExtended
    {
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="dt">数据行模版</param>
        /// <returns>数据行</returns>
        public static DataRow MToDataRow(this object obj, DataRow dr)
        {
            if (dr != null)
            {
                Type TType = obj.GetType();
                object Value;
                PropertyInfo[] props = TType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    Value = prop.GetValue(obj, null);
                    if (Value == null)
                    {
                        dr[prop.Name] = DBNull.Value;
                    }
                    else
                    {
                        dr[prop.Name] = Value;
                    }
                }
                return dr;
            }
            else
            {
                throw new MConvertException("数据行不可为空");
            }
        }
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="dt">数据行模版</param>
        /// <returns>数据行</returns>
        public static DataRow MToDataRow(this object obj)
        {
            Type TType = obj.GetType();
            DataTable dt = TType.MToDataTable();
            DataRow dr = dt.NewRow();
            return obj.MToDataRow(dr);
        }
        /// <summary>
        /// 通过数据行设置对象的值
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="dr">数据行</param>
        public static void MSetValueByDataRow(this object obj, DataRow dr)
        {
            Type TType = obj.GetType();
            PropertyInfo[] props = TType.GetProperties();
            PropertyInfo[] subProps;
            foreach (PropertyInfo prop in props)
            {
                if (!(dr[prop.Name] is System.DBNull) && dr[prop.Name].GetType().Name == prop.PropertyType.Name)
                {
                    prop.SetValue(obj, dr[prop.Name], null);
                }
                else
                {
                    subProps = prop.PropertyType.GetProperties();
                    if (!(dr[prop.Name] is System.DBNull) && subProps.Length == 2 && dr[prop.Name].GetType().Name == subProps[1].PropertyType.Name)
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
            }
        }
        /// <summary>
        /// 通过列模型特性设置对象的值
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="dr">数据行</param>
        public static void MSetValueByColumnModelAttribute(this object obj, DataRow dr)
        {

            Type TType = obj.GetType();
            PropertyInfo[] props = TType.GetProperties();
            PropertyInfo[] subProps;
            foreach (PropertyInfo prop in props)
            {
                ColumnModelAttribute cma = null;
                foreach (Attribute attr in Attribute.GetCustomAttributes(prop))
                {
                    if (attr.GetType() == typeof(ColumnModelAttribute))
                    {
                        cma = attr as ColumnModelAttribute;
                        if (!(dr[cma.DBColumnName] is System.DBNull) && dr[cma.DBColumnName].GetType().Name == prop.PropertyType.Name)
                        {
                            prop.SetValue(obj, dr[cma.DBColumnName], null);
                        }
                        else
                        {
                            subProps = prop.PropertyType.GetProperties();
                            if (!(dr[cma.DBColumnName] is System.DBNull) && subProps.Length == 2 && dr[cma.DBColumnName].GetType().Name == subProps[1].PropertyType.Name)
                            {
                                prop.SetValue(obj, dr[cma.DBColumnName], null);
                            }
                        }
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        public static void MSetDefultObject(this object obj)
        {
            obj = null;
            Type TType = obj.GetType();
            ConstructorInfo[] cis = TType.GetConstructors();
            bool isOk = false;
            if (cis.Length > 0)
            {
                foreach (ConstructorInfo ci in cis)
                {
                    ParameterInfo[] pis = ci.GetParameters();
                    if (pis.Length == 0)
                    {
                        obj = ci.Invoke(new object[0]);
                        isOk = true;
                        break;
                    }
                }
            }
            if (!isOk)
            {
                throw new MConvertException("没有可用构造方法，需要一个无参数的构造方法");
            }
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
        /// <returns>复制的对象</returns>
        public static void MCopyProperties<T>(this object sourceM, T targetM)
        {
            PropertyInfo tempProp;
            PropertyInfo[] T1Props = sourceM.GetType().GetProperties();
            PropertyInfo[] T2Props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in T1Props)
            {
                tempProp = T2Props.Where(m => m.Name == prop.Name).FirstOrDefault();
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
        /// <returns>复制的对象</returns>
        public static T MCopyProperties<T>(this object sourceM)
        {
            T targetM = default(T);
            targetM.MSetDefultObject();
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
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }
    }
}
