using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MLinQ
{
    public class MLinQException : MException
    {
        public MLinQException() : base() { }
        public MLinQException(string message) : base(message) { }
        public MLinQException(string message, Exception innerException) : base(message, innerException) { }
    }
}
