using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using MateralTools.Base.Model;

namespace MateralTools.Base.Manager
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
            var name = string.Empty;
            var objType = inputObj.GetType();
            var fieldInfo = objType.GetField(inputObj.ToString());
            if (fieldInfo == null)throw new MException("需要特性DescriptionAttribute");
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
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
            var tType = inputObj.GetType();
            var attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr == null)throw new MException("拷贝类型需要拥有特性[SerializableAttribute]");
            object resM;
            using (var ms = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, inputObj);
                ms.Seek(0, SeekOrigin.Begin);
                resM = xml.Deserialize(ms);
                ms.Close();
            }
            return (T) resM;
        }
        /// <summary>
        /// 克隆对象(反射)
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MCloneByReflex<T>(this T inputObj)
        {
            var tType = inputObj.GetType();
            var resM = (T)Activator.CreateInstance(tType);
            var pis = tType.GetProperties();
            foreach (var pi in pis)
            {
                var piValue = pi.GetValue(inputObj);
                pi.SetValue(resM, piValue is ValueType ? piValue : MClone(piValue));
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
            var tType = inputObj.GetType();
            var attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr == null)throw new MException("拷贝类型需要拥有特性[SerializableAttribute]");
            using (var stream = new MemoryStream())
            {
                var bf2 = new BinaryFormatter();
                bf2.Serialize(stream, inputObj);
                stream.Position = 0;
                return (T) bf2.Deserialize(stream);
            }
        }
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T MClone<T>(this T inputObj)
        {
            var tType = inputObj.GetType();
            var attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            return attr != null ? MCloneBySerializable(inputObj) : MCloneByReflex(inputObj);
        }
    }
}
