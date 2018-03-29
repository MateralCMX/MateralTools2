using MateralTools.MVerify.Data;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MateralTools.MVerify.Manager
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtended
    {
        /// <summary>
        /// 是否为空或空字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsNullOrEmpty(this string inputStr)
        {
            return string.IsNullOrEmpty(inputStr);
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="InputStr">要验证的字符串</param>
        /// <param name="REGStr">验证正则表达式</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string InputStr, string REGStr)
        {
            bool resM = REGStr.IsNullOrEmpty();
            if (!resM && !InputStr.IsNullOrEmpty())
            {
                resM = Regex.IsMatch(InputStr, REGStr);
            }
            return resM;
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="InputStr">要验证的字符串</param>
        /// <param name="REGStr">验证正则表达式</param>
        /// <param name="IsPerfect">完全匹配</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string InputStr, string REGStr, bool IsPerfect)
        {
            REGStr = IsPerfect ? GetPerfectRegStr(REGStr) : REGStr;
            return InputStr.VerifyRegex(REGStr);
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="InputStr">要验证的字符串</param>
        /// <param name="REGStr">验证正则表达式</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string InputStr, string REGStr)
        {
            MatchCollection resM = null;
            if (!InputStr.IsNullOrEmpty() && !REGStr.IsNullOrEmpty())
            {
                resM = Regex.Matches(InputStr, REGStr);
            }
            return resM;
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="InputStr">要验证的字符串</param>
        /// <param name="REGStr">验证正则表达式</param>
        /// <param name="IsPerfect">完全匹配</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string InputStr, string REGStr, bool IsPerfect)
        {
            REGStr = IsPerfect ? GetPerfectRegStr(REGStr) : REGStr;
            return InputStr.GetVerifyRegex(REGStr);
        }
        /// <summary>
        /// 获得完全匹配的正则表达式
        /// </summary>
        /// <param name="ResStr">部分匹配的正则表达式</param>
        /// <returns>完全匹配的正则表达式</returns>
        public static string GetPerfectRegStr(string ResStr)
        {
            int Length = ResStr.Length;
            if (Length > 0)
            {
                char First = '^';
                char Last = '$';
                if (ResStr[0] != First)
                {
                    ResStr = First + ResStr;
                }
                if (ResStr[ResStr.Length - 1] != Last)
                {
                    ResStr += Last;
                }
            }
            return ResStr;
        }
        /// <summary>
        /// 获得不完全匹配的正则表达式
        /// </summary>
        /// <param name="ResStr">部分匹配的正则表达式</param>
        /// <returns>完全匹配的正则表达式</returns>
        public static string GetNoPerfectRegStr(string ResStr)
        {
            int Length = ResStr.Length;
            if (Length > 0)
            {
                char First = '^';
                char Last = '$';
                if (ResStr[0] == First)
                {
                    ResStr = ResStr.Substring(1);
                }
                if (ResStr[ResStr.Length - 1] == Last)
                {
                    ResStr = ResStr.Substring(0, ResStr.Length - 1);
                }
            }
            return ResStr;
        }
        #region 简单验证
        /// <summary>
        /// 验证输入字符串是否为IPv4地址(无端口号)
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(无端口号)
        /// false不是IPv4地址(无端口号)
        /// </returns>
        public static bool IsIPv4(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_IPv4, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(无端口号)
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(无端口号)
        /// </returns>
        public static MatchCollection GetIPv4InStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_IPv4, false);
        }
        /// <summary>
        /// 验证输入字符串是否为IPv4地址(带端口号)
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(带端口号)
        /// false不是IPv4地址(带端口号)
        /// </returns>
        public static bool IsIPv4AndPort(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_IPv4_Port, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(带端口号)
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(带端口号)
        /// </returns>
        public static MatchCollection GetIPv4AndPortInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_IPv4_Port, false);
        }
        /// <summary>
        /// 验证输入字符串是否为邮箱
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是邮箱
        /// false不是邮箱
        /// </returns>
        public static bool IsEMail(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_EMail, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的邮箱
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的邮箱
        /// </returns>
        public static MatchCollection GetEMailInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_EMail, false);
        }
        /// <summary>
        /// 验证输入字符串是否为实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是实数
        /// false不是实数
        /// </returns>
        public static bool IsNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的实数
        /// </returns>
        public static MatchCollection GetNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是正实数
        /// false不是正实数
        /// </returns>
        public static bool IsNumberPositive(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Number_Positive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正实数
        /// </returns>
        public static MatchCollection GetNumberPositiveInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Number_Positive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是负实数
        /// false不是负实数
        /// </returns>
        public static bool IsNumberNegative(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Number_Negative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负实数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负实数
        /// </returns>
        public static MatchCollection GetNumberNegativeInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Number_Negative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是整数
        /// false不是整数
        /// </returns>
        public static bool IsInteger(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Integer, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的整数
        /// </returns>
        public static MatchCollection GetIntegerInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Integer, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是正整数
        /// false不是正整数
        /// </returns>
        public static bool IsIntegerPositive(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Integer_Positive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正整数
        /// </returns>
        public static MatchCollection GetIntegerPositiveInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Integer_Positive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是负整数
        /// false不是负整数
        /// </returns>
        public static bool IsIntegerNegative(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Integer_Negative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负整数
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负整数
        /// </returns>
        public static MatchCollection GetIntegerNegativeInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Integer_Negative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为URL地址
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是URL地址
        /// false不是URL地址
        /// </returns>
        public static bool IsURL(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_URL, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的URL地址
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的URL地址
        /// </returns>
        public static MatchCollection GetURLInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_URL, false);
        }
        /// <summary>
        /// 验证输入字符串是否为相对路径
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是相对路径
        /// false不是相对路径
        /// </returns>
        public static bool IsRelativePath(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_RelativePath, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的相对路径
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的相对路径
        /// </returns>
        public static MatchCollection GetRelativePathInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_RelativePath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为文件名
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是文件名
        /// false不是文件名
        /// </returns>
        public static bool IsFileName(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_FileName, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的文件名
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的文件名
        /// </returns>
        public static MatchCollection GetFileNameInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_FileName, false);
        }
        /// <summary>
        /// 验证输入字符串是否为手机号码
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是手机号码
        /// false不是手机号码
        /// </returns>
        public static bool IsPhoneNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_PhoneNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的手机号码
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的手机号码
        /// </returns>
        public static MatchCollection GetPhoneNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_PhoneNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="Delimiter">分隔符</param>
        /// <returns>
        /// true是日期
        /// false不是日期
        /// </returns>
        public static bool IsDate(this string InputStr, string Delimiter = "-/.")
        {
            bool resM = false;
            if (InputStr.VerifyRegex(RegexData.REG_Date(Delimiter), true))
            {
                char[] Delimiters = Delimiter.ToCharArray();
                string[] dateStr = new string[3];
                for (int i = 0; i < Delimiters.Length; i++)
                {
                    dateStr = InputStr.Split(Delimiters[i]);
                    if (dateStr.Length == 3)
                    {
                        int Month = int.Parse(dateStr[1]);
                        if (Month == 2)
                        {
                            int Year = int.Parse(dateStr[0]);
                            int day = int.Parse(dateStr[2]);
                            if (Year % 4 == 0)
                            {
                                resM = true;
                            }
                            else
                            {
                                if (day <= 28)
                                {
                                    resM = true;
                                }
                            }
                        }
                        else
                        {
                            resM = true;
                        }
                        break;
                    }
                }
            }
            return resM;
        }
        /// <summary>
        /// 获取输入字符串中所有的日期
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="Delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期
        /// </returns>
        public static MatchCollection GetDateInStr(this string InputStr, string Delimiter = "-/.")
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Date(Delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为时间
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是时间
        /// false不是时间
        /// </returns>
        public static bool IsTime(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Time, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的时间
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的时间
        /// </returns>
        public static MatchCollection GetTimeInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Time, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期和时间
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="Delimiter">分隔符</param>
        /// <returns>
        /// true是日期和时间
        /// false不是日期和时间
        /// </returns>
        public static bool IsDateTime(this string InputStr, string Delimiter = "-/.")
        {
            return InputStr.VerifyRegex(RegexData.REG_DateTime(Delimiter), true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日期和时间
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="Delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期和时间
        /// </returns>
        public static MatchCollection GetDateTimeInStr(this string InputStr, string Delimiter = "-/.")
        {
            return InputStr.GetVerifyRegex(RegexData.REG_DateTime(Delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLetter(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Letter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLetterInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Letter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母或数字
        /// false不是字母或数字
        /// </returns>
        public static bool IsLetterOrNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Letter_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母或数字
        /// </returns>
        public static MatchCollection GetLetterOrNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Letter_Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterr(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_LowerLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetterInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_LowerLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterrOrNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_LowerLetter_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetterOrNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_LowerLetter_Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterr(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_UpperLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetterInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_UpperLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterrOrNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_UpperLetter_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetterOrNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_UpperLetter_Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是中文
        /// false不是中文
        /// </returns>
        public static bool IsChinese(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Chinese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或数字
        /// </returns>
        public static MatchCollection GetChineseInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Chinese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文或字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是中文或字母或数字
        /// false不是中文或字母或数字
        /// </returns>
        public static bool IsChineseOrLetterOrNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Chinese_Letter_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文或字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或字母或数字
        /// </returns>
        public static MatchCollection GetChineseOrLetterOrNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Chinese_Letter_Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是日文
        /// false不是日文
        /// </returns>
        public static bool IsJapanese(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Japanese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或数字
        /// </returns>
        public static MatchCollection GetJapaneseInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Japanese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文或字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是日文或字母或数字
        /// false不是日文或字母或数字
        /// </returns>
        public static bool IsJapaneseOrLetterOrNumber(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_Japanese_Letter_Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文或字母或数字
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或字母或数字
        /// </returns>
        public static MatchCollection GetJapaneseOrLetterOrNumberInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_Japanese_Letter_Number, false);
        }
        #endregion
        #region 复杂验证
        /// <summary>  
        /// 验证输入字符串是否为(中国)身份证 
        /// </summary>  
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="Accurate">详细验证</param>  
        /// <returns>
        /// true是(中国)身份证
        /// false不是(中国)身份证
        /// </returns>
        public static bool IsIDCardForChina(this string InputStr, bool Accurate = false)
        {
            bool resM = false;
            if (!InputStr.IsNullOrEmpty())
            {
                if (InputStr.Length == 18)
                {
                    if (Accurate)
                    {
                        return CheckIDCard18(InputStr);
                    }
                    else
                    {
                        return IsIDCard18ForChina(InputStr);
                    }
                }
                else if (InputStr.Length == 15)
                {
                    if (Accurate)
                    {
                        return CheckIDCard15(InputStr);
                    }
                    else
                    {
                        return IsIDCard15ForChina(InputStr);
                    }
                }
            }
            return resM;
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证18位
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证18位
        /// false不是(中国)身份证18位
        /// </returns>
        public static bool IsIDCard18ForChina(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_IDCard_18_China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证18位
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证18位
        /// </returns>
        public static MatchCollection GetIDCard18ForChinaInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_IDCard_18_China, false);
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证15位
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证15位
        /// false不是(中国)身份证15位
        /// </returns>
        public static bool IsIDCard15ForChina(this string InputStr)
        {
            return InputStr.VerifyRegex(RegexData.REG_IDCard_15_China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证15位
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证15位
        /// </returns>
        public static MatchCollection GetIDCard15ForChinaInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_IDCard_15_China, false);
        }
        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        /// <param name="InputStr">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns>
        private static bool CheckIDCard18(this string InputStr)
        {
            long n = 0;
            if (long.TryParse(InputStr.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(InputStr.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(InputStr.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = InputStr.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = InputStr.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != InputStr.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }
        /// <summary>  
        /// 15位身份证号码验证  
        /// </summary>  
        /// <param name="InputStr">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns> 
        private static bool CheckIDCard15(this string InputStr)
        {
            long n = 0;
            if (long.TryParse(InputStr, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(InputStr.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = InputStr.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }
        /// <summary>
        /// 验证输入字符串是否为磁盘根目录
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="IsReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是磁盘根目录
        /// false不是磁盘根目录
        /// </returns>
        public static bool IsDiskPath(this string InputStr, bool IsReal = false)
        {
            bool IsOk = false;
            if (InputStr.VerifyRegex(RegexData.REG_DiskPath, true))
            {
                if (IsReal)
                {
                    if (InputStr.Length == 2)
                    {
                        InputStr += "\\";
                    }
                    else if (InputStr.Last() != '\\')
                    {
                        InputStr = InputStr.Substring(0, 2) + "\\";
                    }
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    foreach (DriveInfo Disk in allDrives)
                    {
                        if (Disk.Name == InputStr)
                        {
                            IsOk = true;
                            break;
                        }
                    }
                }
                else
                {
                    IsOk = true;
                }
            }
            return IsOk;
        }
        /// <summary>
        /// 获取输入字符串中所有的磁盘根目录
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的磁盘根目录
        /// </returns>
        public static MatchCollection GetDiskPathInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_DiskPath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为绝对路径
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <param name="IsReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是绝对路径
        /// false不是绝对路径
        /// </returns>
        public static bool IsAbsolutePath(this string InputStr, bool IsReal = false)
        {
            bool IsOK = false;
            if (InputStr.VerifyRegex(RegexData.REG_AbsolutePath, true))
            {
                int Length = InputStr.Length;
                string DiskPath;
                if (Length >= 3)
                {
                    DiskPath = InputStr.Substring(0, 3);
                }
                else
                {
                    DiskPath = InputStr;
                }
                IsOK = IsDiskPath(DiskPath, IsReal);
            }
            return IsOK;
        }
        /// <summary>
        /// 获取输入字符串中所有的绝对路径
        /// </summary>
        /// <param name="InputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的绝对路径
        /// </returns>
        public static MatchCollection GetAbsolutePathInStr(this string InputStr)
        {
            return InputStr.GetVerifyRegex(RegexData.REG_AbsolutePath, false);
        }
        #endregion
    }
}
