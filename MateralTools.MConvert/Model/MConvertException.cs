using MateralTools.Base.Model;
using System;

namespace MateralTools.MConvert.Model
{
    public class MConvertException : MException
    {
        public MConvertException(){ }
        public MConvertException(string message) : base(message) { }
        public MConvertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
