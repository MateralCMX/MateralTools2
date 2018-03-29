using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat
{
#pragma warning disable IDE1006 // 命名样式
    /// <summary>
    /// 微信Token模型
    /// </summary>
    public class WeChatTokenModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public int expires_in { get; set; }
    }
#pragma warning restore IDE1006 // 命名样式
}