using MateralTools.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MateralTools.MEntityFramework
{

    /// <summary>
    /// LinQ扩展
    /// </summary>
    public static class LinQExtended
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="index">第几页</param>
        /// <param name="size">显示数量</param>
        /// <param name="startIndex">开始的页数</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> Paging<T>(this IAsyncEnumerable<T> first, int index, int size, int startIndex = 1)
        {
            return first.Skip((index - startIndex) * size).Take(size);
        }
    }
}
