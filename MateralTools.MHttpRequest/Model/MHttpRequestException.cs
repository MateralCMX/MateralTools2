using System;
using System.Net;
using MateralTools.Base.Model;

namespace MateralTools.MHttpRequest.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MHttpRequestException : MException
    {
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        public MHttpRequestException(HttpStatusCode httpStatusCode) : base()
        {
            StatusCode = httpStatusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        public MHttpRequestException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            StatusCode = httpStatusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MHttpRequestException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = httpStatusCode;
        }
    }
}
