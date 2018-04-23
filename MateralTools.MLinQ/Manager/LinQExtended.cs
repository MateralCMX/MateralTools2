using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MateralTools.MLinQ
{
    public static class LinQExtended
    {
        /// <summary>
        /// 初始化True条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        /// <summary>
        /// 初始化False条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }
        /// <summary>
        /// 拼接条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        /// <summary>
        /// 拼接并且条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }
        /// <summary>
        /// 拼接或者条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="index">第几页</param>
        /// <param name="size">显示数量</param>
        /// <param name="startIndex">开始的页数</param>
        /// <returns></returns>
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> first, int index,int size,int startIndex = 1)
        {
            return first.Skip((index - startIndex) * size).Take(size);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="index">第几页</param>
        /// <param name="size">显示数量</param>
        /// <param name="startIndex">开始的页数</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> first, int index, int size, int startIndex = 1)
        {
            return first.Skip((index - startIndex) * size).Take(size);
        }
    }
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (map.TryGetValue(p, out var replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
