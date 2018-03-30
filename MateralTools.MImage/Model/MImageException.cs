using MateralTools.Base;
using System;

namespace MateralTools.MImage
{
    public class MImageException : MException
    {
        public MImageException() : base() { }
        public MImageException(string message) : base(message) { }
        public MImageException(string message, Exception innerException) : base(message, innerException) { }
    }
}
