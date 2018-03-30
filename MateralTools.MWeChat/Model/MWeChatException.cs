using MateralTools.Base;
using System;

namespace MateralTools.MWeChat
{
    public class MWeChatException : MException
    {
        public MWeChatException() : base() { }
        public MWeChatException(string message) : base(message) { }
        public MWeChatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
