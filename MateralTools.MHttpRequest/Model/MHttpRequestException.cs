using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MHttpRequest
{
    /// <summary>
    /// 
    /// </summary>
    public class MHttpRequestException : MException
    {
        /// <summary>
        /// 
        /// </summary>
        public MHttpRequestException() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MHttpRequestException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MHttpRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
