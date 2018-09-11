using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MEntityFramework
{
    public class MEntityFrameworkException : MException
    {
        public MEntityFrameworkException() : base() { }
        public MEntityFrameworkException(string message) : base(message) { }
        public MEntityFrameworkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
