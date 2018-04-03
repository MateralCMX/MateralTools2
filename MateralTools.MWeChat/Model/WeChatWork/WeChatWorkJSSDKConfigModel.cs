using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat
{
#pragma warning disable IDE1006 // 命名样式
    /// <summary>
    /// 微信企业号JSSDK配置模型
    /// </summary>
    public class WeChatWorkJSSDKConfigModel
    {
        /// <summary>
        /// 测试版本
        /// </summary>
        public bool beta { get; set; }
        /// <summary>
        /// 调试模式
        /// </summary>
        public bool debug { get; set; }
        /// <summary>
        /// APPID
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 接口列表
        /// </summary>
        public string[] jsApiList { get; set; }
    }
#pragma warning restore IDE1006 // 命名样式
}