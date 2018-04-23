using MateralTools.MVerify;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MateralTools.MConvert
{
    public static class StringExtended
    {
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object MJsonToObject(this string jsonStr)
        {
            try
            {
                object model = new object();
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new MConvertException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static T MJsonToObject<T>(this string jsonStr)
        {
            try
            {
                T model = ConvertManager.GetDefultObject<T>();
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new MConvertException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] MToHexByte(this string hexString)
        {
            if (hexString.MIsHexNumber())
            {
                try
                {
                    hexString = hexString.Replace(" ", "");
                    if ((hexString.Length % 2) != 0)
                    {
                        hexString += " ";
                    }
                    byte[] returnBytes = new byte[hexString.Length / 2];
                    for (int i = 0; i < returnBytes.Length; i++)
                    {
                        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                    }
                    return returnBytes;
                }
                catch (Exception ex)
                {
                    throw new MConvertException("16进制字符串有误", ex);
                }
            }
            else
            {
                throw new MConvertException("16进制字符串有误");
            }
        }
        /// <summary>
        /// 文本转换为二进制字符
        /// </summary>
        /// <param name="InputStr">文本</param>
        /// <param name="digit">位数</param>
        /// <returns>二进制字符串</returns>
        public static string MToBinaryStr(this string InputStr, int digit = 8)
        {
            byte[] data = Encoding.UTF8.GetBytes(InputStr);
            StringBuilder resStr = new StringBuilder(data.Length * digit);
            foreach (var item in data)
            {
                resStr.Append(Convert.ToString(item, 2).PadLeft(digit, '0'));
            }
            return resStr.ToString();
        }
        /// <summary>
        /// 二进制字符转换为文本
        /// </summary>
        /// <param name="InputStr">二进制字符串</param>
        /// <param name="digit">位数</param>
        /// <returns>文本</returns>
        public static string MBinaryToStr(this string InputStr, int digit = 8)
        {
            StringBuilder resStr = new StringBuilder();
            int numOfBytes = InputStr.Length / digit;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
            {
                bytes[i] = Convert.ToByte(InputStr.Substring(digit * i, digit), 2);
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
