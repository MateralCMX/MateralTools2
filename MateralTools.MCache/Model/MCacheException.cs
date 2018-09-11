using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MCache
{
    public class MCacheException : MException
    {
        public MCacheException() : base() { }
        public MCacheException(string message) : base(message) { }
        public MCacheException(string message, Exception innerException) : base(message, innerException) { }
    }
}
