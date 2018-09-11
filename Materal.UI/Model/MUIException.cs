using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace Materal.UI
{
    public class MUIException : MException
    {
        public MUIException() : base() { }
        public MUIException(string message) : base(message) { }
        public MUIException(string message, Exception innerException) : base(message, innerException) { }
    }
}
