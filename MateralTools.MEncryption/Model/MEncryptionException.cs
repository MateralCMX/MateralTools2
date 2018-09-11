using MateralTools.Base.Model;
using System;

namespace MateralTools.MVerify
{
    public class MEncryptionException : MException
    {
        public MEncryptionException(){ }
        public MEncryptionException(string message) : base(message) { }
        public MEncryptionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
