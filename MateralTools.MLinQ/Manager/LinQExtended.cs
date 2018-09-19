using MateralTools.Base.Model;
using MateralTools.MResult.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MateralTools.MLinQ.Manager
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
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source,FilterInfo<T>[] filters)
        {
            return source.AsQueryable().Where(filters).AsEnumerable();
        }
        /// <summary>
        /// 条件扩展
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="filters">过滤器信息</param>
        /// <returns>扩展后的数据源</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, FilterInfo<T>[] filters)
        {
            var param = Expression.Parameter(typeof(T), "m");
            Expression be = null;
            foreach (var filter in filters)
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
                        default:
                            be = Expression.And(be, GetWhere(filter, param));
                            break;
                    }
                }
            }
            var func = True<T>();
            if (be != null)
            {
                func = Expression.Lambda<Func<T, bool>>(be, param);
            }
            return source.Where(func).AsQueryable();
        }
        /// <summary>
        /// 条件扩展(异步)
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="filters">过滤器信息</param>
        /// <returns>扩展后的数据源</returns>
        public static IAsyncEnumerable<T> WhereAnsy<T>(this IQueryable<T> source, FilterInfo<T>[] filters)
        {
            return Where(source, filters).ToAsyncEnumerable();
        }
        /// <summary>
        /// 条件扩展(异步)
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="filters">过滤器信息</param>
        /// <returns>扩展后的数据源</returns>
        public static IAsyncEnumerable<T> WhereAnsy<T>(this IEnumerable<T> source, FilterInfo<T>[] filters)
        {
            return Where(source, filters).ToAsyncEnumerable();
        }
        /// <summary>
        /// 获得拼装条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Expression GetWhere<T>(FilterInfo<T> filter, ParameterExpression param)
        {
            Expression be;
            var left1 = Expression.Property(param, filter.PropertyInfo);
            var right1 = Expression.Constant(filter.Value);
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
                    be = GetContainsExpression(param, filter);
                    break;
                default:
                    be = Expression.Equal(left1, right1);
                    break;
            }
            return be;
        }
        /// <summary>
        /// 获得模糊查询表达式
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <param name="param">参数表达式</param>
        /// <param name="filter">过滤器</param>
        /// <returns>模糊查询表达式</returns>
        public static Expression GetContainsExpression<T>(ParameterExpression param, FilterInfo<T> filter)
        {
            var left = Expression.Property(param, filter.PropertyInfo);
            return GetContainsExpression(left, filter);
        }
        /// <summary>
        /// 获得模糊查询表达式
        /// </summary>
        /// <typeparam name="T">目标对象类</typeparam>
        /// <param name="left">左边的表达式</param>
        /// <param name="filter">过滤器</param>
        /// <returns>模糊查询表达式</returns>
        private static Expression GetContainsExpression<T>(MemberExpression left, FilterInfo<T> filter)
        {
            var method = filter.PropertyInfo.PropertyType.GetMethod("Contains", new[] { filter.PropertyInfo.PropertyType });
            var someValue = Expression.Constant(filter.Value, filter.PropertyInfo.PropertyType);
            if (method == null)throw new ArgumentException($"类型{filter.PropertyInfo.PropertyType.Name}未实现方法Contains({filter.PropertyInfo.PropertyType.Name} value)");
            Expression be = Expression.Call(left, method, someValue);
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
        /// <param name="skip">跳过</param>
        /// <param name="take">取多少</param>
        /// <returns></returns>
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> first, int skip, int take) => first.Skip(skip).Take(take);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="pageM">分页对象</param>
        /// <returns></returns>
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> first, MPageModel pageM)
        {
            return Paging(first, pageM.Skip, pageM.Take);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="skip">跳过</param>
        /// <param name="take">取多少</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> first, int skip, int take)
        {
            return first.Skip(skip).Take(take);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="pageM">分页对象</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> first, MPageModel pageM)
        {
            return Paging(first, pageM.Skip, pageM.Take);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="skip">跳过</param>
        /// <param name="take">取多少</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> PagingAsync<T>(this IEnumerable<T> first, int skip, int take)
        {
            return Paging(first, skip, take).ToAsyncEnumerable();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="pageM">分页对象</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> PagingAsync<T>(this IEnumerable<T> first, MPageModel pageM)
        {
            return Paging(first, pageM.Skip, pageM.Take).ToAsyncEnumerable();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="skip">跳过</param>
        /// <param name="take">取多少</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> PagingAsync<T>(this IQueryable<T> first, int skip, int take)
        {
            return Paging(first, skip, take).ToAsyncEnumerable();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="first">LinQ对象</param>
        /// <param name="pageM">分页对象</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> PagingAsync<T>(this IQueryable<T> first, MPageModel pageM)
        {
            return Paging(first, pageM.Skip, pageM.Take).ToAsyncEnumerable();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map"></param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
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
            if (_map.TryGetValue(p, out var replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
