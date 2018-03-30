using MateralTools.MCache;
using MateralTools.MConvert;
using MateralTools.MHttpWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace MateralTools.MWeChat
{
    public class WeChatManager
    {
        public WeChatManager()
        {
            WebCacheManager.init();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="appid">APPID</param>
        /// <param name="appsecret">APPSecret</param>
        public WeChatManager(string appid, string appsecret)
        {
            APPID = appid;
            AppSecret = appsecret;
        }
        /// <summary>
        /// APPID
        /// </summary>
        public string APPID { get; set; }
        /// <summary>
        /// APPSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 缓存Key
        /// </summary>
        private string ServiceTokenKey = "MATERALWECHATTOKENKEY";
        /// <summary>
        /// 缓存Key
        /// </summary>
        public string ServiceTokenKey1 { get => ServiceTokenKey; set => ServiceTokenKey = value; }
        /// <summary>
        /// 公众号认证
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage WeChatAuthentication(string token, string signature, string timestamp, string nonce, string echostr)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            string result = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();
            if (result == signature)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded")
                };
            }
            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("", Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded")
                };
            }
        }
        /// <summary>
        /// 获取微信Token
        /// </summary>
        /// <returns></returns>
        public WeChatTokenModel GetWeChatToken()
        {
            WeChatTokenModel tokenM = WebCacheManager.Get<WeChatTokenModel>(ServiceTokenKey1);
            if (tokenM == null)
            {
                if (!string.IsNullOrEmpty(APPID) && !string.IsNullOrEmpty(AppSecret))
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", APPID, AppSecret);
                    string resStr = HttpWebManager.SendRequest(url, "", MethodTypeEnum.GET, ParamTypeEnum.Text, Encoding.UTF8);
                    tokenM = resStr.MJsonToObject<WeChatTokenModel>();
                    WebCacheManager.Set(ServiceTokenKey1, tokenM, DateTimeOffset.Now.AddSeconds(tokenM.expires_in - 60));
                }
            }
            return tokenM;
        }
        /// <summary>
        /// 设置微信菜单
        /// </summary>
        /// <param name="wxMenuM">微信菜单模型</param>
        public void SetWeChatMenu(WeChatMenuModel wxMenuM)
        {
            string jsonStr = wxMenuM.MToJson();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", GetWeChatToken().access_token);
            string resStr = HttpWebManager.SendRequest(url, jsonStr, MethodTypeEnum.POST, ParamTypeEnum.Json, Encoding.UTF8);

        }
    }
}