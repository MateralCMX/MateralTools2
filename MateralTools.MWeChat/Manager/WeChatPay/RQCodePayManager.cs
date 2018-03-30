using MateralTools.Base;
using MateralTools.MEncryption;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat.WeChatPay
{
    /// <summary>
    /// 扫描二维码支付管理类
    /// </summary>
    public class RQCodePayManager: WeChatPayManager
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM">配置对象</param>
        public RQCodePayManager(WeChatPayConfigModel configM) : base(configM){}
        /// <summary>
        /// 生成扫描支付URL
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <param name="modeE">模式选择</param>
        /// <returns>URL地址</returns>
        public string GetPayUrl(string productID, RQCodePayModeEnum modeE = RQCodePayModeEnum.Mode2)
        {
            string url = string.Empty;
            switch (modeE)
            {
                case RQCodePayModeEnum.Mode1:
                    url = GetPrePayUrlByMode1(productID);
                    break;
                default:
                    url = GetPrePayUrlByMode2(productID);
                    break;
            }
            return url;
        }
        /// <summary>
        /// 生成扫描支付二维码
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <param name="modeE">模式选择</param>
        /// <returns>二维码图片</returns>
        public Bitmap GetPayQRCodeImage(string productID, RQCodePayModeEnum modeE = RQCodePayModeEnum.Mode2)
        {
            string url = GetPayUrl(productID, modeE);
            return EncryptionManager.QRCodeEncode(url);
        }
        /// <summary>
        /// 生成扫描支付模式一二维码
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <returns>二维码图片</returns>
        public Bitmap GetPayQRCodeMode1(string productID)
        {
            string url = GetPrePayUrlByMode1(productID);
            return EncryptionManager.QRCodeEncode(url);
        }
        /// <summary>
        /// 生成扫描支付模式一URL
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <returns>模式一URL</returns>
        public string GetPrePayUrlByMode1(string productID)
        {
            WeChatPayConfigModel configM = new WeChatPayConfigModel();
            WeChatPayDataModel data = new WeChatPayDataModel();
            data.SetValue("appid", configM.APPID);//公众帐号id
            data.SetValue("mch_id", configM.MCHID);//商户号
            data.SetValue("time_stamp", CommonManager.GetTimeStamp());//时间戳
            data.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            data.SetValue("product_id", productID);//商品ID
            data.SetValue("sign", data.MakeSign(configM.KEY));//签名
            string url = "weixin://wxpay/bizpayurl?" + data.ToUrlParams();
            return url;
        }
        /// <summary>
        /// 生成扫描支付模式二二维码
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <returns>二维码图片</returns>
        public Bitmap GetPayQRCodeMode2(string productID)
        {
            string url = GetPrePayUrlByMode2(productID);
            return EncryptionManager.QRCodeEncode(url);
        }
        /// <summary>
        /// 生成扫描支付模式二URL
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <returns>模式二URL(2小时内有效)</returns>
        public string GetPrePayUrlByMode2(string productID)
        {
            WeChatPayDataModel data = new WeChatPayDataModel();
            data.SetValue("body", "test");//商品描述
            data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", CommonManager.GetRandomStrByGUID(32));//随机字符串
            data.SetValue("total_fee", 1);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", productID);//商品ID
            WeChatPayDataModel result = UnifiedOrder(data);//调用统一下单接口
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            return url;
        }
    }
}
