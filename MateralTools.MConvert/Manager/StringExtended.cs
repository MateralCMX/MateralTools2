﻿using MateralTools.MVerify;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Xml;
using MateralTools.MConvert.Model;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtended
    {
        /// <summary>
        /// Json转换为XML文档对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>XML文档对象</returns>
        public static XmlDocument MJsonToXml(this string jsonStr)
        {
            return JsonConvert.DeserializeXmlNode(jsonStr);
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object MJsonToObject(this string jsonStr)
        {
            try
            {
                var model = new object();
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
                var model = ConvertManager.GetDefultObject<T>();
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
            if (!hexString.MIsHexNumber())throw new MConvertException("16进制字符串有误");
            try
            {
                hexString = hexString.Replace(" ", "");
                if ((hexString.Length % 2) != 0)
                {
                    hexString += " ";
                }
                var returnBytes = new byte[hexString.Length / 2];
                for (var i = 0; i < returnBytes.Length; i++)
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
        /// <summary>
        /// 文本转换为二进制字符
        /// </summary>
        /// <param name="inputStr">文本</param>
        /// <param name="digit">位数</param>
        /// <returns>二进制字符串</returns>
        public static string MToBinaryStr(this string inputStr, int digit = 8)
        {
            var data = Encoding.UTF8.GetBytes(inputStr);
            var resStr = new StringBuilder(data.Length * digit);
            foreach (var item in data)
            {
                resStr.Append(Convert.ToString(item, 2).PadLeft(digit, '0'));
            }
            return resStr.ToString();
        }
        /// <summary>
        /// 二进制字符转换为文本
        /// </summary>
        /// <param name="inputStr">二进制字符串</param>
        /// <param name="digit">位数</param>
        /// <returns>文本</returns>
        public static string MBinaryToStr(this string inputStr, int digit = 8)
        {
            var numOfBytes = inputStr.Length / digit;
            var bytes = new byte[numOfBytes];
            for (var i = 0; i < numOfBytes; i++)
            {
                bytes[i] = Convert.ToByte(inputStr.Substring(digit * i, digit), 2);
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
