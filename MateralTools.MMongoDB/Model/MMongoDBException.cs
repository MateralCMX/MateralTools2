using System;

namespace MateralTools.MMongoDB
{
    /// <summary>
    /// MongoDB异常类
    /// </summary>
    public class MMongoDBException : ApplicationException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MMongoDBException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MMongoDBException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MMongoDBException(string message, Exception innerException) : base(message, innerException) { }
    }
}
