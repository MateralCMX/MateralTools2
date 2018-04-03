using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MWeChat.WeChatPay
{
    /// <summary>
    /// 微信支付商品模型
    /// </summary>
    public class WeChatPayProductModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 总金额(1=1分，10=1角，100=1元)
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 优惠券标记
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 有效时间(分钟)
        /// </summary>
        public int EffectiveTime { get => _effectiveTime; set => _effectiveTime = value; }
        /// <summary>
        /// 有效时间(分钟)
        /// </summary>
        private int _effectiveTime = 10;
    }
}
