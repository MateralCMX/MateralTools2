using System.ComponentModel;
using System.Reflection;
using MateralTools.Base.Model;

namespace MateralTools.Base.Manager
{
    /// <summary>
    /// 属性信息扩展
    /// </summary>
    public static class PropertyInfoExtended
    {
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="pi">属性</param>
        /// <returns>描述</returns>
        public static string MGetDescription(this PropertyInfo pi)
        {
            object attr = pi.GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attr != null ? (attr as DescriptionAttribute)?.Description : throw new MException("需要特性DescriptionAttribute");
        }
    }
}
