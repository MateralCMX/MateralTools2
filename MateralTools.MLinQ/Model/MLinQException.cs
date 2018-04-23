using MateralTools.Base;
using System;

namespace MateralTools.MLinQ
{
    public class MLinQException : MException
    {
        public MLinQException() : base() { }
        public MLinQException(string message) : base(message) { }
        public MLinQException(string message, Exception innerException) : base(message, innerException) { }
    }
}
