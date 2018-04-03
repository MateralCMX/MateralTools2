using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.Base
{
    /// <summary>
    /// 共用管理类
    /// </summary>
    public class CommonManager
    {
        /// <summary>
        /// 获得时间戳
        /// 1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns>时间戳</returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByGUID(int minLength, int maxLength)
        {
            if (minLength > 0)
            {
                if (minLength < maxLength)
                {
                    Random rd = new Random();
                    int length = rd.Next(minLength, maxLength);
                    return GetRandomStrByGUID(length);
                }
                else
                {
                    throw new MException("最大长度必须大于最小长度");
                }
            }
            else
            {
                throw new MException("长度必须大于0");
            }
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByGUID(int length = 32)
        {
            if (length > 0)
            {
                string resM = string.Empty;
                int count = length % 32 == 0 ? length / 32 : length / 32 + 1;
                for (int i = 0; i < count; i++)
                {
                    resM += Guid.NewGuid().ToString().Replace("-", "");
                }
                return resM.Substring(0, length);
            }
            else
            {
                throw new MException("长度必须大于0");
            }
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionarie">字典</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByDictionarie(int minLength, int maxLength, string dictionarie = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (minLength > 0)
            {
                if (minLength < maxLength)
                {
                    Random rd = new Random();
                    int length = rd.Next(minLength, maxLength);
                    return GetRandomStrByDictionarie(length, dictionarie);
                }
                else
                {
                    throw new MException("最大长度必须大于最小长度");
                }
            }
            else
            {
                throw new MException("长度必须大于0");
            }
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionarie">字典</param>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByDictionarie(int length = 32, string dictionarie = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (length > 0)
            {
                string resM = string.Empty;
                Random rd = new Random();
                for (int i = 0; i < length; i++)
                {
                    resM += dictionarie[rd.Next(0, dictionarie.Length)];
                }
                return resM;
            }
            else
            {
                throw new MException("长度必须大于0");
            }
        }
    }
}
