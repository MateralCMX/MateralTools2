using LitJson;
using MateralTools.Base;
using MateralTools.MConvert;
using MateralTools.MVerify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MateralTools.MWeChat.WeChatPay
{
    /// <summary>
    /// JS支付管理器
    /// </summary>
    public class JsAPIPayManager : WeChatPayManager
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM">配置对象</param>
        public JsAPIPayManager(WeChatConfigModel configM) : base(configM) { }
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// AccessToken
        /// </summary>
        private string _accessToken;
        /// <summary>
        /// 获得OpenID和AccessToken
        /// </summary>
        /// <param name="code">网址Code</param>
        public void GetOpenidAndAccessTokenFromCode(string code)
        {
            try
            {
                //构造获取openid及access_token的url
                WeChatDataModel data = new WeChatDataModel();
                data.SetValue("appid", _config.APPID);
                data.SetValue("secret", _config.APPSECRET);
                data.SetValue("code", code);
                data.SetValue("grant_type", "authorization_code");
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrlParams();
                //请求url以获取数据
                string result = WeChatHttpManager.Get(url);
                //保存access_token，用于收货地址获取
                JsonData jd = JsonMapper.ToObject(result);
                _accessToken = (string)jd["access_token"];
                //获取用户openid
                OpenID = (string)jd["openid"];
            }
            catch (Exception ex)
            {
                throw new MWeChatException(ex.ToString());
            }
        }
        /// <summary>
        /// 调用统一下单获取下单结果
        /// </summary>
        /// <param name="orderM">订单模型</param>
        /// <returns>下单结果</returns>
        public WeChatDataModel GetUnifiedOrderResult(WeChatPayOrderModel orderM)
        {
            WeChatDataModel data = new WeChatDataModel();
            data.SetValue("body", orderM.Description);
            data.SetValue("attach", orderM.Attach);
            data.SetValue("out_trade_no", orderM.ID);
            data.SetValue("total_fee", orderM.TotalPrice);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(orderM.EffectiveTime).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", orderM.Tag);
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", OpenID);
            WeChatDataModel result = UnifiedOrder(data);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString().MIsNullOrEmpty())
            {
                throw new MWeChatException("配置错误");
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// 调用统一下单获取下单结果
        /// </summary>
        /// <param name="code">网址Code</param>
        /// <param name="orderM">订单模型</param>
        /// <returns>下单结果</returns>
        public WeChatDataModel GetUnifiedOrderResult(string code, WeChatPayOrderModel orderM)
        {
            GetOpenidAndAccessTokenFromCode(code);
            return GetUnifiedOrderResult(orderM);
        }
        /// <summary>
        /// 获得JsAPI参数
        /// </summary>
        /// <param name="unifiedOrderResult">下单对象</param>
        /// <returns>JsAPI参数</returns>
        public string GetJsApiParameters(WeChatDataModel unifiedOrderResult)
        {
            WeChatDataModel jsApiParam = new WeChatDataModel();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", CommonManager.GetTimeStamp());
            jsApiParam.SetValue("nonceStr", CommonManager.GetRandomStrByGUID());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign(_config.KEY));
            string parameters = jsApiParam.ToJson();
            return parameters;
        }
        /// <summary>
        /// 获得JsAPI参数
        /// </summary>
        /// <param name="code">网址Code</param>
        /// <param name="orderM">订单模型</param>
        /// <returns>JsAPI参数</returns>
        public string GetJsApiParameters(string code, WeChatPayOrderModel orderM)
        {
            WeChatDataModel unifiedOrderResult = GetUnifiedOrderResult(code, orderM);
            return GetJsApiParameters(unifiedOrderResult);
        }
        /// <summary>
        /// 获得支付网址
        /// </summary>
        /// <param name="redirect_uri">网址(包含http://)</param>
        /// <returns>网址</returns>
        public string GetPayURL(string redirect_uri)
        {
            redirect_uri = HttpUtility.UrlEncode(redirect_uri);
            WeChatDataModel data = new WeChatDataModel();
            data.SetValue("appid", _config.APPID);
            data.SetValue("redirect_uri", redirect_uri);
            data.SetValue("response_type", "code");
            data.SetValue("scope", "snsapi_base");
            data.SetValue("state", "STATE" + "#wechat_redirect");
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrlParams();
            return url;
        }
    }
}
