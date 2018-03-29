using MateralTools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">枚举对象</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj)
        {
            string name = string.Empty;
            Type objType = inputObj.GetType();
            FieldInfo fieldInfo = objType.GetField(inputObj.ToString());
            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                foreach (DescriptionAttribute attr in attrs)
                {
                    name = attr.Description;
                }
            }
            else
            {
                throw new MException("需要特性DescriptionAttribute");
            }
            return name;
        }
    }
}
