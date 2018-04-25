using MateralTools.Base;
using System;

namespace MateralTools.MEnum
{
    /// <summary>
    /// M枚举异常
    /// </summary>
    public class MEnumException : MException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MEnumException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MEnumException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MEnumException(string message, Exception innerException) : base(message, innerException) { }
    }
}
