using System;

namespace MateralTools.Base.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 工具库异常类
    /// </summary>
    public class MException : ApplicationException
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        public MException(){ }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MException(string message) : base(message) { }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MException(string message, Exception innerException) : base(message, innerException) { }
    }
}
