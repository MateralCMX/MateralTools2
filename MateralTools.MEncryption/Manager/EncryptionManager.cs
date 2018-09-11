using MateralTools.MVerify;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MateralTools.MEncryption.Manager
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
        public static string GetFileMd5(string fileName)
        {
            var sb = new StringBuilder();
            try
            {
                var file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                file.Close();
                foreach (var item in retVal)
                {
                    sb.Append(item.ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                throw new MEncryptionException("获取文件MD5值错误", e);
            }
        }
        /// <summary>
        /// MD5加密32位
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">是小写形式</param>
        /// <returns>加密后的字符串64位</returns>
        public static string MD5Encode_32(string inputStr, bool isLower = false)
        {
            if (inputStr.MIsNullOrEmpty()) return string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(Encoding.Default.GetBytes(inputStr));
            var outputStr = BitConverter.ToString(output).Replace("-", "");
            outputStr = isLower ? outputStr.ToLower() : outputStr.ToUpper();
            return outputStr;
        }
        /// <summary>
        /// MD5加密16位
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">是小写形式</param>
        /// <returns>加密后的字符串64位</returns>
        public static string MD5Encode_16(string inputStr, bool isLower = false)
        {
            return MD5Encode_32(inputStr, isLower).Substring(8, 16);
        }
        #endregion
        #region Base64
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string inputStr)
        {
            var input = Encoding.ASCII.GetBytes(inputStr);
            return Convert.ToBase64String(input);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string inputStr)
        {
            try
            {
                var input = Convert.FromBase64String(inputStr);
                return Encoding.Default.GetString(input);
            }
            catch (Exception ex)
            {
                throw new MEncryptionException("解密错误", ex);
            }
        }
        /// <summary>
        /// 迅雷URL加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>迅雷URL加密链接</returns>
        public static string ThunderUrlEncode(string inputStr)
        {
            return "thunder://" + Base64Encode("AA" + inputStr + "ZZ");
        }
        /// <summary>
        /// 链接解密验证
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="urlStr"></param>
        /// <returns></returns>
        private static int VerifyUrlDecode(string inputStr, string urlStr)
        {
            var length = urlStr.Length;
            if (inputStr.Length < length) return 0;
            return inputStr.Substring(0, length) == urlStr ? length : 0;
        }
        /// <summary>
        /// 迅雷URL解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>迅雷URL解密链接</returns>
        public static string ThunderUrlDecode(string inputStr)
        {
            var length = VerifyUrlDecode(inputStr, "thunder://");
            if (length == 0) return "不是迅雷链接";
            inputStr = inputStr.Substring(length);
            inputStr = Base64Decode(inputStr);
            inputStr = inputStr.Substring(2);
            return inputStr.Remove(inputStr.Length - 2);
        }
        /// <summary>
        /// QQ旋风URL加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>迅雷URL加密链接</returns>
        public static string QQdlUrlEncode(string inputStr)
        {
            return "qqdl://" + Base64Encode(inputStr);
        }
        /// <summary>
        /// QQ旋风URL解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>迅雷URL解密链接</returns>
        public static string QQdlUrlDecode(string inputStr)
        {
            var length = VerifyUrlDecode(inputStr, "qqdl://");
            if (length == 0) return "不是QQ旋风链接";
            inputStr = inputStr.Substring(length);
            return Base64Decode(inputStr);
        }
        /// <summary>
        /// 快车URL加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>快车URL加密链接</returns>
        public static string FlashgetUrlEncode(string inputStr)
        {
            return "flashget://" + Base64Encode("[FLASHGET]" + inputStr + "[FLASHGET]");
        }
        /// <summary>
        /// 快车URL解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>快车URL解密链接</returns>
        public static string FlashgetUrlDecode(string inputStr)
        {
            var length = VerifyUrlDecode(inputStr, "flashget://");
            if (length == 0) return "不是网际快车链接";
            inputStr = inputStr.Substring(length);
            inputStr = Base64Decode(inputStr);
            length = "[FLASHGET]".Length;
            inputStr = inputStr.Substring(length);
            return inputStr.Remove(inputStr.Length - length);
        }
        #endregion
        #region 二维码
        /// <summary>
        /// 获得二维码
        /// </summary>
        /// <param name="inputStr">需要加密的字符串</param>
        /// <param name="pixelsPerModule">每个模块的像素</param>
        /// <param name="darkColor">暗色</param>
        /// <param name="lightColor">亮色</param>
        /// <param name="icon">图标</param>
        /// <returns>二维码图片</returns>
        public static Bitmap QrCodeEncode(string inputStr, int pixelsPerModule = 20, Color? darkColor = null, Color? lightColor = null, Bitmap icon = null)
        {
            if (darkColor == null) darkColor = Color.Black;
            if (lightColor == null) lightColor = Color.White;
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(inputStr, QRCodeGenerator.ECCLevel.H);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(pixelsPerModule, darkColor.Value, lightColor.Value, icon);
            return qrCodeImage;
        }
        #endregion
        #region 栅栏加密法
        /// <summary>
        /// 栅栏加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>加密后字符串</returns>
        public static string FenceEncode(string inputStr)
        {
            var outPutStr = "";
            var outPutStr2 = "";
            var count = inputStr.Length;
            for (var i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    outPutStr += inputStr[i];
                }
                else
                {
                    outPutStr2 += inputStr[i];
                }
            }
            return outPutStr + outPutStr2;
        }
        /// <summary>
        /// 栅栏解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>解密后字符串</returns>
        public static string FenceDecode(string inputStr)
        {
            var count = inputStr.Length;
            var outPutStr = "";
            string outPutStr1;
            string outPutStr2;
            var num1 = 0;
            var num2 = 0;
            if (count % 2 == 0)
            {
                outPutStr1 = inputStr.Substring(0, count / 2);
                outPutStr2 = inputStr.Substring(count / 2);
            }
            else
            {
                outPutStr1 = inputStr.Substring(0, (count / 2) + 1);
                outPutStr2 = inputStr.Substring((count / 2) + 1);
            }
            for (var i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    outPutStr += outPutStr1[num1++];
                }
                else
                {
                    outPutStr += outPutStr2[num2++];
                }
            }
            return outPutStr;
        }
        #endregion
        #region 移位密码
        /// <summary>
        /// 移位加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string DisplacementEncode(string inputStr, int key = 3)
        {
            string outputStr = "";
            if (!inputStr.Replace(" ", "").MIsLetter()) return "格式错误,只能输入英文字母,不包括标点符号";
            inputStr = inputStr.ToUpper();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var aCount = alphabet.Length;
            var count = inputStr.Length;
            for (var i = 0; i < count; i++)
            {
                if (inputStr[i] != ' ')
                {
                    for (var j = 0; j < aCount; j++)
                    {
                        if (inputStr[i] != alphabet[j]) continue;
                        var eIndex = j + key;
                        if (eIndex < 0)
                        {
                            eIndex = aCount + eIndex;
                        }
                        while (eIndex >= aCount)
                        {
                            eIndex -= aCount;
                        }
                        outputStr += alphabet[eIndex];
                        break;
                    }
                }
                else
                {
                    outputStr += " ";
                }
            }
            return outputStr;
        }
        /// <summary>
        /// 移位解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DisplacementDecode(string inputStr, int key = 3)
        {
            return DisplacementEncode(inputStr, -key);
        }
        #endregion
        #region DES
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputStr">需要加密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>加密后的字符串</returns>
        public static string DesEncode(string inputStr, string inputKey, string inputIv, Encoding ed = null)
        {
            var resM = "";
            if (inputKey.Length != 8 || inputIv.Length != 8) return resM;
            if (ed == null)
            {
                ed = Encoding.UTF8;
            }
            var str = ed.GetBytes(inputStr);
            var key = ed.GetBytes(inputKey);
            var iv = ed.GetBytes(inputIv);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            try
            {
                cStream.Write(str, 0, str.Length);
                cStream.FlushFinalBlock();
                resM = Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cStream.Close();
                mStream.Close();
            }
            return resM;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputStr">需要解密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecode(string inputStr, string inputKey, string inputIv, Encoding ed = null)
        {
            var resM = "";
            if (inputKey.Length != 8 || inputIv.Length != 8) return resM;
            if (ed == null)
            {
                ed = Encoding.UTF8;
            }
            var str = Convert.FromBase64String(inputStr);
            var key = ed.GetBytes(inputKey);
            var iv = ed.GetBytes(inputIv);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateDecryptor(key, iv), CryptoStreamMode.Write);
            try
            {
                cStream.Write(str, 0, str.Length);
                cStream.FlushFinalBlock();
                resM = ed.GetString(mStream.ToArray());
            }
            finally
            {
                cStream.Close();
                mStream.Close();
            }
            return resM;
        }
        #endregion
    }
}
