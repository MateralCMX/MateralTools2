using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MAlipay
{
    /// <summary>
    /// 支付宝支付订单模型
    /// </summary>
    public class AlipayOrderModel
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string StoreID { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public string TotalPrice { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string DiscounPrice { get; set; }
        /// <summary>
        /// 不参与优惠的金额
        /// </summary>
        public string UnDiscounPrice { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 商品对象
        /// </summary>
        public List<AlipayProductModel> Items { get; set; }
        /// <summary>
        /// 回调URL
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 回调Params
        /// </summary>
        public string NotifyParams { get; set; }
    }
}
