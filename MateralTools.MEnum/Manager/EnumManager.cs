using MateralTools.Base.Manager;
using System;
using System.Collections.Generic;
using MateralTools.MEnum.Model;

namespace MateralTools.MEnum.Manager
{
    /// <summary>
    /// 枚举管理类
    /// </summary>
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
            return typeof(T).IsEnum ? (T) Enum.Parse(typeof(T), enumName) : throw new MEnumException("该类型不是枚举");
        }
        /// <summary>
        /// 通过描述获得枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="Description">描述</param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(string Description)
        {
            var allEnum = GetAllEnum(typeof(T));
            var result = default(T);
            foreach (var item in allEnum)
            {
                if (Description != item.MGetDescription()) continue;
                result = GetEnumByName<T>(item.ToString());
                break;
            }
            return result;
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int GetCount(Type enumType)
        {
            return enumType.IsEnum ? Enum.GetValues(enumType).Length : throw new MEnumException("该类型不是枚举类型");
        }
        /// <summary>
        /// 获得所有枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举模型列表</returns>
        public static List<Enum> GetAllEnum(Type enumType)
        {
            if (!enumType.IsEnum)throw new MEnumException("该类型不是枚举类型");
            var listM = new List<Enum>();
            var allEnums = Enum.GetValues(enumType);
            foreach (var item in allEnums)
            {
                listM.Add((Enum) item);
            }
            return listM;
        }
    }
}
