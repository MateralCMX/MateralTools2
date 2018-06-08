using MateralTools.Base;
using System;

namespace MateralTools.MEntityFramework
{
    public class MEntityFrameworkException : MException
    {
        public MEntityFrameworkException() : base() { }
        public MEntityFrameworkException(string message) : base(message) { }
        public MEntityFrameworkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
