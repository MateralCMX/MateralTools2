using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MVerify;
using ThoughtWorks.QRCode.Codec;

namespace MateralTools.MEncryption
{
    /// <summary>
    /// 加密管理类
    /// </summary>
    public class EncryptionManager
    {
        #region MD5
        /// <summary>
        /// 获得文件的MD5值
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>MD5值</returns>
        /// <exception cref="MEncryptionException"></exception>
        public static string GetFileMD5(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch(Exception e)
            {
                throw new MEncryptionException("获取文件MD5值错误", e);
            }
        }
        /// <summary>
        /// MD5加密32位
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <param name="IsLower">是小写形式</param>
        /// <returns>加密后的字符串64位</returns>
        public static string MD5Encode_32(string InputStr, bool IsLower = false)
        {
            if (!InputStr.MIsNullOrEmpty())
            {
                byte[] result = Encoding.Default.GetBytes(InputStr);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(InputStr));
                string OutputStr = BitConverter.ToString(output).Replace("-", "");
                OutputStr = IsLower ? OutputStr.ToLower() : OutputStr.ToUpper();
                return OutputStr;
            }
            return string.Empty;
        }
        /// <summary>
        /// MD5加密16位
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <param name="IsLower">是小写形式</param>
        /// <returns>加密后的字符串64位</returns>
        public static string MD5Encode_16(string InputStr, bool IsLower = false)
        {
            return MD5Encode_32(InputStr, IsLower).Substring(8, 16);
        }
        #endregion
        #region Base64
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string InputStr)
        {
            byte[] Input = Encoding.ASCII.GetBytes(InputStr);
            return Convert.ToBase64String(Input);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string InputStr)
        {
            try
            {
                byte[] Input = Convert.FromBase64String(InputStr);
                return Encoding.Default.GetString(Input);
            }
            catch (Exception ex)
            {
                throw new MEncryptionException("解密错误", ex);
            }
        }
        /// <summary>
        /// 迅雷URL加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>迅雷URL加密链接</returns>
        public static string ThunderURLEncode(string InputStr)
        {
            return "thunder://" + Base64Encode("AA" + InputStr + "ZZ");
        }
        /// <summary>
        /// 链接解密验证
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="URLStr"></param>
        /// <returns></returns>
        private static int VerifyURLDecode(string InputStr, string URLStr)
        {
            int length = URLStr.Length;
            if (InputStr.Length >= length)
            {
                if (InputStr.Substring(0, length) == URLStr)
                {
                    return length;
                }
            }
            return 0;
        }
        /// <summary>
        /// 迅雷URL解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>迅雷URL解密链接</returns>
        public static string ThunderURLDecode(string InputStr)
        {
            int length = VerifyURLDecode(InputStr, "thunder://");
            if (length != 0)
            {
                InputStr = InputStr.Substring(length);
                InputStr = Base64Decode(InputStr);
                InputStr = InputStr.Substring(2);
                return InputStr.Remove(InputStr.Length - 2);
            }
            return "不是迅雷链接";
        }
        /// <summary>
        /// QQ旋风URL加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>迅雷URL加密链接</returns>
        public static string QQdlURLEncode(string InputStr)
        {
            return "qqdl://" + Base64Encode(InputStr);
        }
        /// <summary>
        /// QQ旋风URL解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>迅雷URL解密链接</returns>
        public static string QQdlURLDecode(string InputStr)
        {
            int length = VerifyURLDecode(InputStr, "qqdl://");
            if (length != 0)
            {
                InputStr = InputStr.Substring(length);
                return Base64Decode(InputStr);
            }
            return "不是QQ旋风链接";
        }
        /// <summary>
        /// 快车URL加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>快车URL加密链接</returns>
        public static string FlashgetURLEncode(string InputStr)
        {
            return "flashget://" + Base64Encode("[FLASHGET]" + InputStr + "[FLASHGET]");
        }
        /// <summary>
        /// 快车URL解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>快车URL解密链接</returns>
        public static string FlashgetURLDecode(string InputStr)
        {
            int length = VerifyURLDecode(InputStr, "flashget://");
            if (length != 0)
            {
                InputStr = InputStr.Substring(length);
                InputStr = Base64Decode(InputStr);
                length = "[FLASHGET]".Length;
                InputStr = InputStr.Substring(length);
                return InputStr.Remove(InputStr.Length - length);
            }
            return "不是网际快车链接";
        }
        #endregion
        #region 二维码
        /// <summary>
        /// 获得二维码
        /// </summary>
        /// <param name="InputStr">需要加密的字符串</param>
        /// <returns>二维码图片181px*181px</returns>
        public static Bitmap QRCodeEncode(string InputStr)
        {
            QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
            //设置二维码编码格式  
            qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qRCodeEncoder.QRCodeScale = InputStr.Length;
            //设置编码版本  
            qRCodeEncoder.QRCodeVersion = 0;
            //设置错误校验  
            qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            return qRCodeEncoder.Encode(InputStr, Encoding.UTF8);
        }
        #endregion
        #region 栅栏加密法
        /// <summary>
        /// 栅栏加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>加密后字符串</returns>
        public static string FenceEncode(string InputStr)
        {
            string OutPutStr = "";
            string OutPutStr2 = "";
            int count = InputStr.Length;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    OutPutStr += InputStr[i];
                }
                else
                {
                    OutPutStr2 += InputStr[i];
                }
            }
            return OutPutStr + OutPutStr2;
        }
        /// <summary>
        /// 栅栏解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <returns>解密后字符串</returns>
        public static string FenceDecode(string InputStr)
        {
            int count = InputStr.Length;
            string OutPutStr = "";
            string OutPutStr1 = "";
            string OutPutStr2 = "";
            int num1 = 0;
            int num2 = 0;
            if (count % 2 == 0)
            {
                OutPutStr1 = InputStr.Substring(0, count / 2);
                OutPutStr2 = InputStr.Substring(count / 2);
            }
            else
            {
                OutPutStr1 = InputStr.Substring(0, (count / 2) + 1);
                OutPutStr2 = InputStr.Substring((count / 2) + 1);
            }
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    OutPutStr += OutPutStr1[num1++];
                }
                else
                {
                    OutPutStr += OutPutStr2[num2++];
                }
            }
            return OutPutStr;
        }
        #endregion
        #region 移位密码
        /// <summary>
        /// 移位加密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string DisplacementEncode(string InputStr, int Key = 3)
        {
            if (InputStr.Replace(" ", "").MIsLetter())
            {
                InputStr = InputStr.ToUpper();
                string OutputStr = "";
                char[] Alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                int aCount = Alphabet.Length;
                int eIndex = 0;
                int count = InputStr.Length;
                for (int i = 0; i < count; i++)
                {
                    if (InputStr[i] != ' ')
                    {
                        for (int j = 0; j < aCount; j++)
                        {
                            if (InputStr[i] == Alphabet[j])
                            {
                                eIndex = j + Key;
                                if (eIndex < 0)
                                {
                                    eIndex = aCount + eIndex;
                                }
                                while (eIndex >= aCount)
                                {
                                    eIndex -= aCount;
                                }
                                OutputStr += Alphabet[eIndex];
                                break;
                            }
                        }
                    }
                    else
                    {
                        OutputStr += " ";
                    }
                }
                return OutputStr;
            }
            return "格式错误,只能输入英文字母,不包括标点符号";
        }
        /// <summary>
        /// 移位解密
        /// </summary>
        /// <param name="InputStr">输入字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DisplacementDecode(string InputStr, int Key = 3)
        {
            return DisplacementEncode(InputStr, -Key);
        }
        #endregion
        #region DES
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="InputStr">需要加密的字符串</param>
        /// <param name="InputKey">密钥,必须为8位字符串</param>
        /// <param name="InputIV">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncode(string InputStr, string InputKey, string InputIV, Encoding ed = null)
        {
            string resM = "";
            if (InputKey.Length == 8 && InputIV.Length == 8)
            {
                if (ed == null)
                {
                    ed = Encoding.UTF8;
                }
                byte[] Str = ed.GetBytes(InputStr);
                byte[] Key = ed.GetBytes(InputKey);
                byte[] IV = ed.GetBytes(InputIV);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                try
                {
                    cStream.Write(Str, 0, Str.Length);
                    cStream.FlushFinalBlock();
                    resM = Convert.ToBase64String(mStream.ToArray());
                }
                finally
                {
                    cStream.Close();
                    mStream.Close();
                }
            }
            return resM;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="InputStr">需要解密的字符串</param>
        /// <param name="InputKey">密钥,必须为8位字符串</param>
        /// <param name="InputIV">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecode(string InputStr, string InputKey, string InputIV, Encoding ed = null)
        {
            string resM = "";
            if (InputKey.Length == 8 && InputIV.Length == 8)
            {
                if (ed == null)
                {
                    ed = Encoding.UTF8;
                }
                byte[] Str = Convert.FromBase64String(InputStr);
                byte[] Key = ed.GetBytes(InputKey);
                byte[] IV = ed.GetBytes(InputIV);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                try
                {
                    cStream.Write(Str, 0, Str.Length);
                    cStream.FlushFinalBlock();
                    resM = ed.GetString(mStream.ToArray());
                }
                finally
                {
                    cStream.Close();
                    mStream.Close();
                }
            }
            return resM;
        }
        #endregion
    }
}
