using MateralTools.Base;
using System;
using MateralTools.Base.Model;

namespace MateralTools.MVerify
{
    public class MEncryptionException : MException
    {
        public MEncryptionException() : base() { }
        public MEncryptionException(string message) : base(message) { }
        public MEncryptionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
