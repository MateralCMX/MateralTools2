using MateralTools.Base;
using System;

namespace MateralTools.MAlipay
{
    /// <summary>
    /// M支付宝支付异常
    /// </summary>
    public class MAlipayException : MException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MAlipayException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MAlipayException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MAlipayException(string message, Exception innerException) : base(message, innerException) { }
    }
}
