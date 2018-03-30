using MateralTools.Base;
using System;

namespace MateralTools.MHttpWeb
{
    public class MHttpWebException : MException
    {
        public MHttpWebException() : base() { }
        public MHttpWebException(string message) : base(message) { }
        public MHttpWebException(string message, Exception innerException) : base(message, innerException) { }
    }
}
