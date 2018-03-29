using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MVerify.Manager
{
    public static class ObjectExtended
    {
        /// <summary>
        /// 验证传入对象是NULL或空字符串
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>验证结果</returns>
        public static bool IsNullOrEmptyStr(this object inputObj)
        {
            bool resM = inputObj == null;
            if (!resM && inputObj is string inputStr)
            {
                resM = inputStr == string.Empty;
            }
            return resM;
        }
    }
}
