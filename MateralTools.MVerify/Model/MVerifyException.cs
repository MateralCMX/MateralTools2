using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MVerify
{
    /// <summary>
    /// 验证异常类
    /// </summary>
    public class MVerifyException : MException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MVerifyException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">异常消息</param>
        public MVerifyException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">父级异常</param>
        public MVerifyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
