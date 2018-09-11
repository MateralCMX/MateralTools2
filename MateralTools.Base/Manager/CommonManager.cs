using System;
using MateralTools.Base.Model;

namespace MateralTools.Base.Manager
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
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByGuid(int minLength, int maxLength)
        {
            if (minLength <= 0)throw new MException("长度必须大于0");
            if (minLength >= maxLength) throw new MException("最大长度必须大于最小长度");
            var rd = new Random();
            var length = rd.Next(minLength, maxLength);
            return GetRandomStrByGuid(length);
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByGuid(int length = 32)
        {
            if (length <= 0)throw new MException("长度必须大于0");
            var resM = string.Empty;
            var count = length % 32 == 0 ? length / 32 : length / 32 + 1;
            for (var i = 0; i < count; i++)
            {
                resM += Guid.NewGuid().ToString().Replace("-", "");
            }
            return resM.Substring(0, length);
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
            if (minLength <= 0)throw new MException("长度必须大于0");
            if (minLength >= maxLength)throw new MException("最大长度必须大于最小长度");
            var rd = new Random();
            var length = rd.Next(minLength, maxLength);
            return GetRandomStrByDictionarie(length, dictionarie);
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
            if (length <= 0)throw new MException("长度必须大于0");
            var resM = string.Empty;
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                resM += dictionarie[rd.Next(0, dictionarie.Length)];
            }
            return resM;
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MException"></exception>
        public static string GetRandomStrByTick(int length)
        {
            if (length <= 0)throw new MException("长度必须大于0");
            var rep = 0;
            var str = string.Empty;
            var tick = DateTime.Now.Ticks + rep++;
            var random = new Random(((int) (((ulong) tick) & 0xffffffffL)) | ((int) (tick >> rep)));
            for (var i = 0; i < length; i++)
            {
                char ch;
                var num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char) (0x30 + ((ushort) (num % 10)));
                }
                else
                {
                    ch = (char) (0x41 + ((ushort) (num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}
