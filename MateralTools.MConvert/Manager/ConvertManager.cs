using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MConvert
{
    /// <summary>
    /// 转换控制器
    /// </summary>
    public class ConvertManager
    {
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <typeparam name="T">要设置的类型</typeparam>
        /// <returns>默认对象</returns>
        public static T GetDefultObject<T>()
        {
            return (T)GetDefultObject(typeof(T));
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <param name="type">要设置的类型</param>
        /// <returns>默认对象</returns>
        public static object GetDefultObject(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = (from m in constructors
                                           where m.GetParameters().Length == 0
                                           select m).FirstOrDefault();
            if (constructor != null)
            {
                return constructor.Invoke(new object[0]);
            }
            else
            {
                throw new MConvertException("没有可用构造方法，需要一个无参数的构造方法");
            }
        }
    }
}
