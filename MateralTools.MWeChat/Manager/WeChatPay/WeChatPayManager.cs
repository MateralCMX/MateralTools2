using MateralTools.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat.WeChatPay
{
    /// <summary>
    /// 微信支付管理器
    /// </summary>
    public class WeChatPayManager
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM"></param>
        public WeChatPayManager(WeChatConfigModel configM)
        {
            _config = configM;
        }
        /// <summary>
        /// 配置文件
        /// </summary>
        protected WeChatConfigModel _config;
        /// <summary>
        /// 提交被扫支付API
        /// 收银员使用扫码设备读取微信用户刷卡授权码以后，二维码或条码信息传送至商户收银台，
        /// 由商户收银台或者商户后台调用该接口发起支付。
        /// </summary>
        /// <param name="inputObj">提交给被扫支付API的参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel Micropay(WeChatDataModel inputObj, int timeOut = 10)
        {
            string url = "https://api.mch.weixin.qq.com/pay/micropay";
            //检测必填参数
            if (!inputObj.IsSet("body"))
            {
                throw new MWeChatException("提交被扫支付API接口中，缺少必填参数body！");
            }
            else if (!inputObj.IsSet("out_trade_no"))
            {
                throw new MWeChatException("提交被扫支付API接口中，缺少必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new MWeChatException("提交被扫支付API接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("auth_code"))
            {
                throw new MWeChatException("提交被扫支付API接口中，缺少必填参数auth_code！");
            }
            inputObj.SetValue("spbill_create_ip", _config.IP);//终端ip
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;//请求开始时间
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);//调用HTTP通信接口以提交数据到API
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时
            //将xml格式的结果转换为对象以返回
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="inputObj">提交给查询订单API的参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>成功时返回订单查询结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel OrderQuery(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new MWeChatException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);//调用HTTP通信接口提交数据
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时
            //将xml格式的数据转化为对象以返回
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 撤销订单API接口
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数，out_trade_no和transaction_id必填一个</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel Reverse(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new MWeChatException("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            string response = WeChatHttpManager.Post(xml, url, true, timeOut, _config);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }
        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel Refund(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new MWeChatException("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new MWeChatException("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new MWeChatException("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new MWeChatException("退款申请接口中，缺少必填参数refund_fee！");
            }
            else if (!inputObj.IsSet("op_user_id"))
            {
                throw new MWeChatException("退款申请接口中，缺少必填参数op_user_id！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;
            string response = WeChatHttpManager.Post(xml, url, true, timeOut, _config);//调用HTTP通信接口提交数据到API
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时
            //将xml格式的结果转换为对象以返回
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 查询退款
        /// 提交退款申请后，通过该接口查询退款状态。退款有一定延时，
        /// 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数,out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel RefundQuery(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/refundquery";
            //检测必填参数
            if (!inputObj.IsSet("out_refund_no") && !inputObj.IsSet("out_trade_no") &&
                !inputObj.IsSet("transaction_id") && !inputObj.IsSet("refund_id"))
            {
                throw new MWeChatException("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;//请求开始时间
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);//调用HTTP通信接口以提交数据到API
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时
            //将xml格式的结果转换为对象以返回
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 下载对账单
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel DownloadBill(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/downloadbill";
            //检测必填参数
            if (!inputObj.IsSet("bill_date"))
            {
                throw new MWeChatException("对账单接口中，缺少必填参数bill_date！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);//调用HTTP通信接口以提交数据到API
            WeChatDataModel result = new WeChatDataModel();
            //若接口调用失败会返回xml格式的结果
            if (response.Substring(0, 5) == "<xml>")
            {
                result.FromXml(response, _config.KEY);
            }
            //接口调用成功则返回非xml格式的数据
            else
            {
                result.SetValue("result", response);
            }
            return result;
        }
        /// <summary>
        /// 转换短链接
        /// 该接口主要用于扫码原生支付模式一中的二维码链接转成短链接(weixin://wxpay/s/XXXXXX)，
        /// 减小二维码数据量，提升扫描速度和精确度。
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel ShortUrl(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/tools/shorturl";
            //检测必填参数
            if (!inputObj.IsSet("long_url"))
            {
                throw new MWeChatException("需要转换的URL，签名用原串，传输需URL encode！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串	
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;//请求开始时间
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel UnifiedOrder(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new MWeChatException("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new MWeChatException("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new MWeChatException("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new MWeChatException("缺少统一支付接口必填参数trade_type！");
            }
            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new MWeChatException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new MWeChatException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }
            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                inputObj.SetValue("notify_url", _config.NOTIFY_URL);//异步通知url
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("spbill_create_ip", _config.IP);//终端ip	  	    
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            //签名
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));
            string xml = inputObj.ToXml();
            var start = DateTime.Now;
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="inputObj">提交给撤销订单API接口的参数</param>
        /// <param name="timeOut">接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel CloseOrder(WeChatDataModel inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new MWeChatException("关闭订单接口中，out_trade_no必填！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串		
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            var start = DateTime.Now;//请求开始时间
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);
            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            ReportCostTime(url, timeCost, result);//测速上报
            return result;
        }
        /// <summary>
        /// 测速上报
        /// </summary>
        /// <param name="interface_url">接口URL</param>
        /// <param name="timeCost">接口耗时</param>
        /// <param name="inputObj">inputObj参数数组</param>
        private void ReportCostTime(string interface_url, int timeCost, WeChatDataModel inputObj)
        {
            //如果不需要进行上报
            if (_config.REPORT_LEVENL == 0)
            {
                return;
            }
            //如果仅失败上报
            if (_config.REPORT_LEVENL == 1 && inputObj.IsSet("return_code") && inputObj.GetValue("return_code").ToString() == "SUCCESS" &&
             inputObj.IsSet("result_code") && inputObj.GetValue("result_code").ToString() == "SUCCESS")
            {
                return;
            }
            //上报逻辑
            WeChatDataModel data = new WeChatDataModel();
            data.SetValue("interface_url", interface_url);
            data.SetValue("execute_time_", timeCost);
            //返回状态码
            if (inputObj.IsSet("return_code"))
            {
                data.SetValue("return_code", inputObj.GetValue("return_code"));
            }
            //返回信息
            if (inputObj.IsSet("return_msg"))
            {
                data.SetValue("return_msg", inputObj.GetValue("return_msg"));
            }
            //业务结果
            if (inputObj.IsSet("result_code"))
            {
                data.SetValue("result_code", inputObj.GetValue("result_code"));
            }
            //错误代码
            if (inputObj.IsSet("err_code"))
            {
                data.SetValue("err_code", inputObj.GetValue("err_code"));
            }
            //错误代码描述
            if (inputObj.IsSet("err_code_des"))
            {
                data.SetValue("err_code_des", inputObj.GetValue("err_code_des"));
            }
            //商户订单号
            if (inputObj.IsSet("out_trade_no"))
            {
                data.SetValue("out_trade_no", inputObj.GetValue("out_trade_no"));
            }
            //设备号
            if (inputObj.IsSet("device_info"))
            {
                data.SetValue("device_info", inputObj.GetValue("device_info"));
            }
            try
            {
                Report(data);
            }
            catch (MWeChatException)
            {
                //不做任何处理
            }
        }
        /// <summary>
        /// 测速上报接口实现
        /// </summary>
        /// <param name="inputObj">提交给测速上报接口的参数</param>
        /// <param name="timeOut">测速上报接口超时时间</param>
        /// <returns>成功时返回API调用结果，其他抛异常</returns>
        /// <exception cref="MWeChatException"></exception>
        public WeChatDataModel Report(WeChatDataModel inputObj, int timeOut = 1)
        {
            string url = "https://api.mch.weixin.qq.com/payitil/report";
            //检测必填参数
            if (!inputObj.IsSet("interface_url"))
            {
                throw new MWeChatException("接口URL，缺少必填参数interface_url！");
            }
            if (!inputObj.IsSet("return_code"))
            {
                throw new MWeChatException("返回状态码，缺少必填参数return_code！");
            }
            if (!inputObj.IsSet("result_code"))
            {
                throw new MWeChatException("业务结果，缺少必填参数result_code！");
            }
            if (!inputObj.IsSet("user_ip"))
            {
                throw new MWeChatException("访问接口IP，缺少必填参数user_ip！");
            }
            if (!inputObj.IsSet("execute_time_"))
            {
                throw new MWeChatException("接口耗时，缺少必填参数execute_time_！");
            }
            inputObj.SetValue("appid", _config.APPID);//公众账号ID
            inputObj.SetValue("mch_id", _config.MCHID);//商户号
            inputObj.SetValue("user_ip", _config.IP);//终端ip
            inputObj.SetValue("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//商户上报时间	 
            inputObj.SetValue("nonce_str", CommonManager.GetRandomStrByGUID(32));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(_config.KEY));//签名
            string xml = inputObj.ToXml();
            string response = WeChatHttpManager.Post(xml, url, false, timeOut, _config);
            WeChatDataModel result = new WeChatDataModel();
            result.FromXml(response, _config.KEY);
            return result;
        }
    }
}
