using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.Base
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
            string name = string.Empty;
            object attr = pi.GetCustomAttribute(typeof(DescriptionAttribute), false);
            if (attr != null)
            {
                name = (attr as DescriptionAttribute).Description;
            }
            else
            {
                throw new MException("需要特性DescriptionAttribute");
            }
            return name;
        }
    }
}
