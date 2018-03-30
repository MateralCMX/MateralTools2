using MateralTools.Base;
using System;

namespace MateralTools.MVerify
{
    public class MVerifyException : MException
    {
        public MVerifyException() : base() { }
        public MVerifyException(string message) : base(message) { }
        public MVerifyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
