using MateralTools.Base;
using System;

namespace MateralTools.MHttpRequest
{
    public class MHttpRequestException : MException
    {
        public MHttpRequestException() : base() { }
        public MHttpRequestException(string message) : base(message) { }
        public MHttpRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
