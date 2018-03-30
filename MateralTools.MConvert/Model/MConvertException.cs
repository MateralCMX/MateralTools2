using MateralTools.Base;
using System;

namespace MateralTools.MConvert
{
    public class MConvertException : MException
    {
        public MConvertException() : base() { }
        public MConvertException(string message) : base(message) { }
        public MConvertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
