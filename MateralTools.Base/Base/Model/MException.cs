using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.Base
{
    /// <summary>
    /// 工具库异常类
    /// </summary>
    public class MException : ApplicationException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MException() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        public MException(string message) : base(message) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">上级异常</param>
        public MException(string message, Exception innerException) : base(message, innerException) { }
    }
}
