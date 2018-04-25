using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MAlipay
{
    /// <summary>
    /// 支付宝支付商品模型
    /// </summary>
    public class AlipayProductModel
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Nubmer { get; set; }
    }
}
