using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat
{
#pragma warning disable IDE1006 // 命名样式
    /// <summary>
    /// 微信企业号调用JSSDK票据模型
    /// </summary>
    public class WeChatWorkJsapiTicketModel
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 票据信息
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public int expires_in { get; set; }
    }
#pragma warning restore IDE1006 // 命名样式
}