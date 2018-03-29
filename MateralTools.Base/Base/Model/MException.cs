using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.Base
{
    public class MException : ApplicationException
    {
        public MException() : base() { }
        public MException(string message) : base(message) { }
        public MException(string message, Exception innerException) : base(message, innerException) { }
    }
}
