using MateralTools.Base;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MateralTools.Base
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtended
    {
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string MGetDescription(this object inputObj)
        {
            string name = string.Empty;
            Type objType = inputObj.GetType();
            FieldInfo fieldInfo = objType.GetField(inputObj.ToString());
            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                foreach (DescriptionAttribute attr in attrs)
                {
                    name = attr.Description;
                }
            }
            else
            {
                throw new MException("需要特性DescriptionAttribute");
            }
            return name;
        }
        /// <summary>
        /// 克隆对象(XML序列化)
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MCloneByXml<T>(this T inputObj)
        {
            Type tType = inputObj.GetType();
            Attribute attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr != null)
            {
                object resM;
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    xml.Serialize(ms, inputObj);
                    ms.Seek(0, SeekOrigin.Begin);
                    resM = xml.Deserialize(ms);
                    ms.Close();
                }
                return (T)resM;
            }
            else
            {
                throw new MException("拷贝类型需要拥有特性[SerializableAttribute]");
            }
        }
        /// <summary>
        /// 克隆对象(反射)
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MCloneByReflex<T>(this T inputObj)
        {
            Type tType = inputObj.GetType();
            T resM = (T)Activator.CreateInstance(tType);
            PropertyInfo[] pis = tType.GetProperties();
            object piValue;
            foreach (PropertyInfo pi in pis)
            {
                piValue = pi.GetValue(inputObj);
                if (piValue is ValueType)
                {
                    pi.SetValue(resM, piValue);
                }
                else
                {
                    pi.SetValue(resM, MClone(piValue));
                }
            }
            return resM;
        }
        /// <summary>
        /// 克隆对象(二进制序列化)
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MCloneBySerializable<T>(this T inputObj)
        {
            Type tType = inputObj.GetType();
            Attribute attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr != null)
            {
                BinaryFormatter BF2 = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    BF2.Serialize(stream, inputObj);
                    stream.Position = 0;
                    return (T)BF2.Deserialize(stream);
                }
            }
            else
            {
                throw new MException("拷贝类型需要拥有特性[SerializableAttribute]");
            }
        }
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MClone<T>(this T inputObj)
        {
            Type tType = inputObj.GetType();
            Attribute attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr != null)
            {
                return MCloneBySerializable(inputObj);
            }
            else
            {
                return MCloneByReflex(inputObj);
            }
        }
    }
}
