using MateralTools.Base;
using System;

namespace MateralTools.MWeChat
{
    /// <summary>
    /// M微信异常
    /// </summary>
    public class MWeChatException : MException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MWeChatException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MWeChatException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MWeChatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
