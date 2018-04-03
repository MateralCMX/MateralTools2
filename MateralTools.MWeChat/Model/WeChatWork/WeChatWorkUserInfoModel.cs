using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat
{
    /// <summary>
    /// 微信且油耗用户模型
    /// </summary>
    public class WeChatWorkUserInfoModel
    {
#pragma warning disable IDE1006 // 命名样式
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 成员UserID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 手机设备号
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 成员票据
        /// </summary>
        public string user_ticket { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string OpenId { get; set; }
#pragma warning restore IDE1006 // 命名样式
    }
}