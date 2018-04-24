using System;
using System.ComponentModel;
using System.Reflection;

namespace MateralTools.Base.MEnum
{
    public static class EnumExtended
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="enumM">枚举</param>
        /// <returns>描述</returns>
        public static string GetDescription(this Enum enumM)
        {
            string name = string.Empty;
            Type enumType = enumM.GetType();
            FieldInfo fieldInfo = enumType.GetField(enumM.ToString());
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
                throw new ApplicationException("该对象不包含EnumShowNameAttribute");
            }
            return name;
        }
    }
}
