namespace MateralTools.MVerify
{
    public static class ObjectExtended
    {
        /// <summary>
        /// 验证传入对象是NULL或空字符串
        /// </summary>
        /// <param name="inputObj">输入对象</param>
        /// <returns>验证结果</returns>
        public static bool MIsNullOrEmptyStr(this object inputObj)
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
