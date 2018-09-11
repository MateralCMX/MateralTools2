using System;
using MateralTools.Base.Model;

namespace MateralTools.MCache.Model
{
    public class MCacheException : MException
    {
        public MCacheException(){ }
        public MCacheException(string message) : base(message) { }
        public MCacheException(string message, Exception innerException) : base(message, innerException) { }
    }
}
