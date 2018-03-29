using System;
using System.Collections.Generic;

namespace MateralTools.Base.MEnum
{
    public static class EnumManager
    {
        /// <summary>
        /// 通过名称获得枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumName">枚举名称</param>
        /// <returns></returns>
        public static T GetEnumByName<T>(string enumName)
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), enumName);
            }
            else
            {
                throw new MEnumException("该类型不是枚举");
            }
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int GetCount(Type enumType)
        {
            if (enumType.IsEnum)
            {
                return Enum.GetValues(enumType).Length;
            }
            else
            {
                throw new MEnumException("该类型不是枚举类型");
            }
        }
        /// <summary>
        /// 获得所有枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举模型列表</returns>
        public static List<Enum> GetAllEnum(Type enumType)
        {
            if (enumType.IsEnum)
            {
                List<Enum> listM = new List<Enum>();
                Array allEnums = Enum.GetValues(enumType);
                foreach (object item in allEnums)
                {
                    listM.Add((Enum)item);
                }
                return listM;
            }
            else
            {
                throw new MEnumException("该类型不是枚举类型");
            }
        }
    }
}
