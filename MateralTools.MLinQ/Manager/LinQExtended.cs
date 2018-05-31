using MateralTools.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MateralTools.MLinQ
{

    /// <summary>
    /// LinQ扩展
    /// </summary>
    public static class LinQExtended
    {
        /// <summary>
        /// 条件扩展
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="filters">过滤器信息</param>
        /// <returns>扩展后的数据源</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, FilterInfo<T>[] filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "f");
            Expression be = null;
            foreach (FilterInfo<T> filter in filters)
            {
                if (be == null)
                {
                    be = GetWhere(filter, param);
                }
                else
                {
                    switch (filter.Condition)
                    {
                        case ConditionEnum.Or:
                            be = Expression.Or(be, GetWhere(filter, param));
                            break;
                        case ConditionEnum.And:
                        default:
                            be = Expression.And(be, GetWhere(filter, param));
                            break;
                    }
                }
            }
            Expression<Func<T, bool>> func = True<T>();
            if (be != null)
            {
               func = Expression.Lambda<Func<T, bool>>(be, param);
            }
            return source.Where(func).AsQueryable();
        }
        /// <summary>
        /// 获得拼装条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private static Expression GetWhere<T>(FilterInfo<T> filter, ParameterExpression param)
        {
            MemberExpression left1 = Expression.Property(param, filter.PropertyInfo);
            ConstantExpression right1 = Expression.Constant(filter.Value);
            Expression be;
            switch (filter.Comparison)
            {
                case ComparisonEnum.NotEqual:
                    be = Expression.NotEqual(left1, right1);
                    break;
                case ComparisonEnum.GreaterThan:
                    be = Expression.GreaterThan(left1, right1);
                    break;
                case ComparisonEnum.LessThan:
                    be = Expression.LessThan(left1, right1);
                    break;
                case ComparisonEnum.GreaterThanOrEqual:
                    be = Expression.GreaterThanOrEqual(left1, right1);
                    break;
                case ComparisonEnum.LessThanOrEqual:
                    be = Expression.LessThanOrEqual(left1, right1);
                    break;
                case ComparisonEnum.Contains:
                    Type strType = typeof(String);
                    if (filter.PropertyInfo.PropertyType.GUID == strType.GUID)
                    {
                        MethodInfo method = strType.GetMethod("Contains", new[] { strType });
                        ConstantExpression someValue = Expression.Constant(filter.Value, strType);
                        be = Expression.Call(left1, method, someValue);
                    }
                    else
                    {
                        throw new ArgumentException("只有类型String可以使用Contains");
                    }
                    break;
                case ComparisonEnum.Equal:
                default:
                    be = Expression.Equal(left1, right1);
                    break;
            }
            return be;
        }
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
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
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
        public static IAsyncEnumerable<T> Paging<T>(this IAsyncEnumerable<T> first, int index, int size, int startIndex = 1)
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
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> first, int index, int size, int startIndex = 1)
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
    /// <summary>
    /// 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
