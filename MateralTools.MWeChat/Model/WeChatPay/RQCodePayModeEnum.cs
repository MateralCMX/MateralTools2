using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat.WeChatPay
{
    /// <summary>
    /// 二维码支付模式枚举
    /// </summary>
    public enum RQCodePayModeEnum
    {
        /// <summary>
        /// 模式一
        /// 优点：
        ///     可以使用短链接(二维码更简单，可以缩小很多)
        /// 缺点：
        ///     需要提前设置URL，设置方式：进入商户平台-->产品中心-->开发配置
        ///     详见：https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=6_3
        /// 详细说明：
        ///     https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=6_4
        /// </summary>
        Mode1,
        /// <summary>
        /// 模式一
        /// 优点：
        ///     不需要提前配置URL
        /// 缺点：
        ///     不可以使用短链接
        ///     参数配置比模式一复杂
        /// 详细说明：
        ///     https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=6_5
        /// </summary>
        Mode2
    }
}
