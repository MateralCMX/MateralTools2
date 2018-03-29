using System;

namespace MateralTools.Base.MEnum
{
    public class MEnumException : MException
    {
        public MEnumException() : base() { }
        public MEnumException(string message) : base(message) { }
        public MEnumException(string message, Exception innerException) : base(message, innerException) { }
    }
}
