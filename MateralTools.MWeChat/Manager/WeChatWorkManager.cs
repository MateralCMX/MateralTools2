using MateralTools.MCache;
using MateralTools.MConvert;
using MateralTools.MHttpWeb;
using MateralTools.MImage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace MateralTools.MWeChat
{
    /// <summary>
    /// 微信企业号管理类
    /// </summary>
    public class WeChatWorkManager
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatWorkManager()
        {
            string cprpID = ConfigurationManager.AppSettings["WXWorkCorpID"];
            string appsecret = ConfigurationManager.AppSettings["WXWorkAppSecret"];
            CorpID = cprpID;
            AppSecret = appsecret;
            WebCacheManager.init();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cprpID">APPID</param>
        /// <param name="appsecret">APPSecret</param>
        public WeChatWorkManager(string cprpID, string appsecret)
        {
            CorpID = cprpID;
            AppSecret = appsecret;
        }
        /// <summary>
        /// APPID
        /// </summary>
        public string CorpID { get; set; }
        /// <summary>
        /// APPSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// AccessToken
        /// </summary>
        private string _accessTokenKey = "MATERAWECHATACCESSTOKENKEY";
        /// <summary>
        /// JsapiTicket
        /// </summary>
        private string _jsapiTicketKey = "MATERAWECHATJSAPITICKETKEY";
        /// <summary>
        /// AccessTokenKey
        /// </summary>
        public string AccessTokenKey { get => _accessTokenKey; set => _accessTokenKey = value; }
        /// <summary>
        /// JsapiTicketKey
        /// </summary>
        public string JsapiTicketKey { get => _jsapiTicketKey; set => _jsapiTicketKey = value; }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public WeChatWorkAccessTokenModel GetAccessToken()
        {
            WeChatWorkAccessTokenModel tokenM = WebCacheManager.Get<WeChatWorkAccessTokenModel>(AccessTokenKey);

            tokenM = new WeChatWorkAccessTokenModel
            {
                access_token = "9-B9hxkFc3j8L_fFo7wYoxGX6RcOyQ0G8XZwrz1zrTo4-UKJVHJW66LXnVxKiwCCShgPhLWYa4SCgu1g6SCs1c9rnS2uRIGeHETP1rvgE9mYa6G5ZGCS4p8nIEyvtAbfyrzM1iG5nyrHFGFU-OSflxfH5s7b-GvEjcTPim4O-pCslYZzDbeMu-9yQukIwCOD7G_9ngSbGyIArc8eZoKqqg",
                expires_in = 7200
            };
            WebCacheManager.Set(AccessTokenKey, tokenM, DateTimeOffset.Now.AddSeconds(tokenM.expires_in - 60));

            if (tokenM == null)
            {
                if (!string.IsNullOrEmpty(CorpID) && !string.IsNullOrEmpty(AppSecret))
                {
                    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CorpID, AppSecret);
                    string resStr = HttpWebManager.SendRequest(url, "", MethodTypeEnum.GET, ParamTypeEnum.Text, Encoding.UTF8);
                    tokenM = resStr.MJsonToObject<WeChatWorkAccessTokenModel>();
                    WebCacheManager.Set(AccessTokenKey, tokenM, DateTimeOffset.Now.AddSeconds(tokenM.expires_in - 60));
                }
            }
            return tokenM;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userCode">用户信息</param>
        public WeChatWorkUserInfoModel GetUserInfo(string userCode)
        {
            WeChatWorkAccessTokenModel tokenM = GetAccessToken();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", tokenM.access_token, userCode);
            string resStr = HttpWebManager.SendRequest(url, "", MethodTypeEnum.POST, ParamTypeEnum.Text, Encoding.UTF8);
            WeChatWorkUserInfoModel userInfo = resStr.MJsonToObject<WeChatWorkUserInfoModel>();
            return userInfo;
        }
        /// <summary>
        /// 获得JsapiTicket
        /// </summary>
        public WeChatWorkJsapiTicketModel GetJsapiTicket()
        {
            WeChatWorkJsapiTicketModel jsapiTicketM = WebCacheManager.Get<WeChatWorkJsapiTicketModel>(JsapiTicketKey);

            //jsapiTicketM = new WeChatWorkJsapiTicketModel();
            //jsapiTicketM.ticket = "HoagFKDcsGMVCIY2vOjf9puj5Nu6JtjQf83kBqTzXnSckHDZq6dfz_zVKxiwys8g_Y4Wyw-n1PISK3v2tjsioA";
            //jsapiTicketM.expires_in = 7200;
            //WebCacheManager.Set(JsapiTicketKey, jsapiTicketM, DateTimeOffset.Now.AddSeconds(jsapiTicketM.expires_in - 60));

            if (jsapiTicketM == null)
            {
                if (!string.IsNullOrEmpty(CorpID) && !string.IsNullOrEmpty(AppSecret))
                {
                    WeChatWorkAccessTokenModel tokenM = GetAccessToken();
                    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", tokenM.access_token);
                    string resStr = HttpWebManager.SendRequest(url, "", MethodTypeEnum.POST, ParamTypeEnum.Text, Encoding.UTF8);
                    jsapiTicketM = resStr.MJsonToObject<WeChatWorkJsapiTicketModel>();
                    WebCacheManager.Set(JsapiTicketKey, jsapiTicketM, DateTimeOffset.Now.AddSeconds(tokenM.expires_in - 60));
                }
            }
            return jsapiTicketM;
        }
        /// <summary>
        /// 获得微信企业号JSSDKConfig数据
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="jsApiLists">调用接口参数</param>
        /// <returns>config配置信息</returns>
        public WeChatWorkJSSDKConfigModel GetJSSDKConfigInfo(string url, string[] jsApiLists)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long timestamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
            WeChatWorkJsapiTicketModel jsapiTicketM = GetJsapiTicket();
            string nonce = "Materal";
            string signature = GetSignature(timestamp, url, nonce, jsapiTicketM.ticket);
            WeChatWorkJSSDKConfigModel config = new WeChatWorkJSSDKConfigModel
            {
                beta = true,
                debug = true,
                appId = CorpID,
                timestamp = timestamp.ToString(),
                nonceStr = nonce,
                signature = signature,
                jsApiList = jsApiLists
            };
            return config;
        }
        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="url">url地址</param>
        /// <param name="noceStr">随机字符串</param>
        /// <param name="jsapiTicket">调用票据</param>
        /// <returns>签名字符串</returns>
        public string GetSignature(long timestamp, string url, string noceStr, string jsapiTicket)
        {
            Hashtable hs = new Hashtable
            {
                { "noncestr", noceStr },
                { "jsapi_ticket", jsapiTicket },
                { "timestamp", timestamp },
                { "url", url }
            };
            ArrayList keys = new ArrayList(hs.Keys); keys.Sort();
            string string1 = string.Empty;
            foreach (string key in keys)
            {
                string value = string.Format("{0}", hs[key]);
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    string1 += string.Format("{0}={1}&", key, value);
                }
            }
            string1 = string1.TrimEnd('&');
#pragma warning disable IDE0618 // 命名样式
            return FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();
#pragma warning restore IDE0618 // 命名样式
        }
        /// <summary>
        /// 下载微信服务器上的图片
        /// </summary>
        /// <param name="serverImage">服务器图片</param>
        /// <returns>图片</returns>
        public Image DownloadImage(string serverImage)
        {
            WeChatWorkAccessTokenModel tokenM = GetAccessToken();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", tokenM.access_token, serverImage);
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"; ;
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "get";
            Stream responseStream = httpRequest.GetResponse().GetResponseStream();
            Image img = Image.FromStream(responseStream);
            responseStream.Close();
            return img;
        }
        /// <summary>
        /// 下载微信服务器上的图片
        /// </summary>
        /// <param name="serverImage">服务器图片</param>
        /// <param name="savePath">保存路径</param>
        /// <returns>图片</returns>
        public void DownloadImage(string serverImage, string savePath)
        {
            Image img = DownloadImage(serverImage);
            Bitmap bitImage = ImageManager.PixeIFormatConvertBitMap(img);
            bitImage.Save(savePath);
        }
    }
}