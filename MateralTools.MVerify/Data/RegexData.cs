namespace MateralTools.MVerify
{
    /// <summary>
    /// 正则表达式数据类
    /// </summary>
    public class RegexData
    {
        /// <summary>
        /// IPv4正则表达式(不带端口号)
        /// </summary>
        public const string REG_IPv4 = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)";
        /// <summary>
        /// IPv4正则表达式(带端口号)
        /// </summary>
        public const string REG_IPv4_Port = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?):(6553[0-5]|655[0-2]\d|65[0,4]\d{2}|6[0-4]\d{3}|[1-5]\d{0,4}|[1-9]\d{0,3})";
        /// <summary>
        /// 邮箱地址正则表达式
        /// </summary>
        public const string REG_EMail = @"\w+@\w+\.\w+";
        /// <summary>
        /// 实数正则表达式
        /// </summary>
        public const string REG_Number = @"-?\d+(\.?\d?)";
        /// <summary>
        /// 正实数正则表达式
        /// </summary>
        public const string REG_Number_Positive = @"\d+(\.?\d?)";
        /// <summary>
        /// 负实数正则表达式
        /// </summary>
        public const string REG_Number_Negative = @"-\d+(\.?\d?)";
        /// <summary>
        /// 整数正则表达式
        /// </summary>
        public const string REG_Integer = @"-?\d+";
        /// <summary>
        /// 正整数正则表达式
        /// </summary>
        public const string REG_Integer_Positive = @"\d+";
        /// <summary>
        /// 负整数正则表达式
        /// </summary>
        public const string REG_Integer_Negative = @"-\d+";
        /// <summary>
        /// URL地址(http|https|ftp|rtsp|mms)正则表达式
        /// </summary>
        public const string REG_URL = @"(https|http|ftp|rtsp|mms)://[\S]+";
        /// <summary>
        /// 磁盘根目录正则表达式
        /// </summary>
        private const string _REG_DiskPath = @"\w:";
        /// <summary>
        /// 相对路径正则表达式
        /// </summary>
        public const string REG_RelativePath = @"(((~|..)?(\\|/))?[^\\\?\/\*\|<>:" + "\"" + @"]+)*[\\/]?";
        /// <summary>
        /// 文件名正则表达式
        /// </summary>
        public const string REG_FileName = @"[^\\\?\/\*\|<>:" + "\"" + @"]+([^\.]|\.[^\\\?\/\*\|<>:" + "\"" + @"]+)";
        /// <summary>
        /// 磁盘根目录正则表达式
        /// </summary>
        public static string REG_DiskPath
        {
            get
            {
                return _REG_DiskPath + @"[\\/]?";
            }
        }
        /// <summary>
        /// 绝对路径正则表达式
        /// </summary>
        public static string REG_AbsolutePath
        {
            get
            {
                return _REG_DiskPath + REG_RelativePath;
            }
        }
        /// <summary>
        /// 身份证号码18位_中国正则表达式
        /// </summary>
        public const string REG_IDCard_18_China = @"\d{17}[\d|x|X]{1}";
        /// <summary>
        /// 身份证号码15位_中国正则表达式
        /// </summary>
        public const string REG_IDCard_15_China = @"\d{15}";
        /// <summary>
        /// 手机号码正则表达式
        /// </summary>
        public const string REG_PhoneNumber = @"(13|14|15|18|17)\d{9}";
        /// <summary>
        /// 日期正则表达式
        /// </summary>
        /// <param name="Delimiter">分隔符</param>
        /// <returns></returns>
        public static string REG_Date(string Delimiter = "-/.")
        {
            return @"\d{4}[" + Delimiter + @"](0?2[" + Delimiter + @"]([1-2]\d|0?[1-9])|(1[02]|0?[13578])[" + Delimiter + @"]([1-2]\d|3[0-1]|0?[1-9])|(11|0?[469])[" + Delimiter + @"]([1-2]\d|30|0?[1-9]))";
        }
        /// <summary>
        /// 时间正则表达式
        /// 00:00:00.000-23:59:59.999
        /// </summary>
        public const string REG_Time = @"(2[0-3]|1\d|0?\d)(:([1-5]\d|0?\d)){1,2}(.\d{1,3})?";
        /// <summary>
        /// 日期加时间正则表达式
        /// 1993/04/20 23:59:59.999
        /// 1993-04-20T00:00:00.000
        /// </summary>
        /// <param name="Delimiter">分隔符</param>
        /// <returns></returns>
        public static string REG_DateTime(string Delimiter = "-/.")
        {
            return REG_Date(Delimiter) + @"[\sT]" + REG_Time;
        }
        /// <summary>
        /// 字母正则表达式
        /// </summary>
        public const string REG_Letter = @"[a-zA-Z]+";
        /// <summary>
        /// 小写字母正则表达式
        /// </summary>
        public const string REG_LowerLetter = @"[a-z]+";
        /// <summary>
        /// 大写字母正则表达式
        /// </summary>
        public const string REG_UpperLetter = @"[A-Z]+";
        /// <summary>
        /// 字母或数字正则表达式
        /// </summary>
        public const string REG_Letter_Number = @"[a-zA-Z\d]+";
        /// <summary>
        /// 小写字母或数字正则表达式
        /// </summary>
        public const string REG_LowerLetter_Number = @"[a-z\d]+";
        /// <summary>
        /// 大写字母或数字正则表达式
        /// </summary>
        public const string REG_UpperLetter_Number = @"[A-Z\d]+";
        /// <summary>
        /// 中文正则表达式
        /// </summary>
        public const string REG_Chinese = @"[\u4E00-\u9FA5]+";
        /// <summary>
        /// 中文、字母、数字正则表达式
        /// </summary>
        public const string REG_Chinese_Letter_Number = @"[\u4E00-\u9FA5a-zA-Z0-9]+";
        /// <summary>
        /// 日文正则表达式
        /// </summary>
        public const string REG_Japanese = @"[\u0800-\u9fa5]+";
        /// <summary>
        /// 日文正则表达式
        /// </summary>
        public const string REG_Japanese_Letter_Number = @"[\u0800-\u9fa5a-zA-Z0-9]+";
        /// <summary>
        /// 16进制字符串
        /// </summary>
        public const string REG_HEX_Number = @"[\dA-Fa-f]+";
    }
}
