using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat.WXSS
{
    /// <summary>
    /// 微信小程序管理类
    /// </summary>
    public class WXSSManager
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        protected WeChatConfigModel _config;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM"></param>
        public WXSSManager(WeChatConfigModel configM)
        {
            _config = configM;
        }
        /// <summary>
        /// 根据Code获得OpenID
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>OpenID</returns>
        public string GetOpenIDByCode(string code)
        {
            WeChatDataModel data = new WeChatDataModel();
            data.SetValue("appid", _config.APPID);
            data.SetValue("secret", _config.APPSECRET);
            data.SetValue("grant_type", "authorization_code");
            data.SetValue("code", code);
            string url = "https://api.weixin.qq.com/sns/jscode2session?" + data.ToUrlParams();
            string result = WeChatHttpManager.Get(url);
            JsonData jd = JsonMapper.ToObject(result);
            string OpenID = (string)jd["openid"];
            return OpenID;
        }
    }
}
