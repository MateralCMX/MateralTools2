using MateralTools.Base;
using System;
using System.ComponentModel;
using System.Reflection;

namespace MateralTools.Base
{
    public static class ObjectExtended
    {
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">枚举对象</param>
        /// <returns>描述</returns>
        public static string MGetDescription(this object inputObj)
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
