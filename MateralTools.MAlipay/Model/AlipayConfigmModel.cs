using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MAlipay
{
    /// <summary>
    /// 支付宝支付配置模型
    /// </summary>
    public class AlipayConfigmModel
    {
        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// 开发者私钥
        /// </summary>
        public string MerchantPrivateKey { get; set; }
        /// <summary>
        /// 开发者公钥
        /// </summary>
        public string MerchantPublicKey { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 账号PID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 支付宝服务器网关
        /// </summary>
        private string _serverUrl = "https://openapi.alipay.com/gateway.do";
        /// <summary>
        /// 支付宝服务器网关
        /// </summary>
        public string ServerURL
        {
            get
            {
                return _serverUrl;
            }
        }
        /// <summary>
        /// 支付宝网关
        /// </summary>
        private string _mapiUrl = "https://mapi.alipay.com/gateway.do";
        /// <summary>
        /// 支付宝服务器网关
        /// </summary>
        public string MapiUrl
        {
            get
            {
                return _mapiUrl;
            }
        }
        /// <summary>
        /// 支付宝网关
        /// </summary>
        private string _monitorUrl = "http://mcloudmonitor.com/gateway.do";
        /// <summary>
        /// 支付宝服务器网关
        /// </summary>
        public string MonitorUrl
        {
            get
            {
                return _monitorUrl;
            }
        }
        /// <summary>
        /// 编码
        /// </summary>
        private string _charset = "utf-8";
        /// <summary>
        /// 编码
        /// </summary>
        public string CharSet
        {
            get
            {
                return _charset;
            }
        }
        /// <summary>
        /// 签名类型，支持RSA2（推荐！）、RSA
        /// </summary>
        private string _sign_type = "RSA2";
        /// <summary>
        /// 签名类型，支持RSA2（推荐！）、RSA
        /// </summary>
        public string SignType
        {
            get
            {
                return _sign_type;
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        private string _version = "1.0";
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return _version;
            }
        }
        /// <summary>
        /// 公钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型公钥</returns>
        public string GetMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(MerchantPublicKey);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }
        /// <summary>
        /// 私钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型私钥</returns>
        public string GetMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(MerchantPrivateKey);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }
    }
}
