using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// Byte扩展
    /// </summary>
    public static class ByteExtended
    {
        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object MToObject(this byte[] buff)
        {
            object obj;
            using (var ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return obj;
        }
        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T MToObject<T>(this byte[] buff)
        {
            var obj = MToObject(buff);
            return obj is T model ? model : default(T);
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string MToHexStr(this byte[] bytes)
        {
            return bytes == null ? string.Empty : bytes.Aggregate(string.Empty, (current, item) => current + item.ToString("X2"));
        }
    }
}
