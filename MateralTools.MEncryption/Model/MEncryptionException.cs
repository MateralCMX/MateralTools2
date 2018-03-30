using MateralTools.Base;
using System;

namespace MateralTools.MVerify
{
    public class MEncryptionException : MException
    {
        public MEncryptionException() : base() { }
        public MEncryptionException(string message) : base(message) { }
        public MEncryptionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
