using MateralTools.Base;
using System;

namespace MateralTools.MCache
{
    public class MCacheException : MException
    {
        public MCacheException() : base() { }
        public MCacheException(string message) : base(message) { }
        public MCacheException(string message, Exception innerException) : base(message, innerException) { }
    }
}
