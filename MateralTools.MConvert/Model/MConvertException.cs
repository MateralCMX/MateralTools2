using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MConvert
{
    public class MConvertException : MException
    {
        public MConvertException() : base() { }
        public MConvertException(string message) : base(message) { }
        public MConvertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
