using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MateralTools.Base
{
    /// <summary>
    /// 过滤器信息
    /// </summary>
    public class FilterInfo<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="value">值</param>
        /// <param name="condition">条件类型</param>
        private FilterInfo(Func<object, bool> func, object value, ConditionEnum condition = ConditionEnum.And)
        {
            PropertyInfo = null;
            Value = value;
            Comparison = ComparisonEnum.Contains;
            Condition = condition;
            TargetFunc = func;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="piName">属性名称</param>
        /// <param name="value">值</param>
        /// <param name="comparison">比较类型</param>
        /// <param name="condition">条件类型</param>
        public FilterInfo(string piName, object value, ComparisonEnum comparison = ComparisonEnum.Equal, ConditionEnum condition = ConditionEnum.And)
        {
            Type type = typeof(T);
            PropertyInfo pi = type.GetProperty(piName);
            if (pi != null)
            {
                PropertyInfo = pi;
                Value = value;
                Comparison = comparison;
                Condition = condition;
                TargetFunc = null;
            }
            else
            {
                throw new ArgumentNullException($"类型{type.Name}上不存属性{piName}");
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pi">属性</param>
        /// <param name="value">值</param>
        /// <param name="comparison">比较类型</param>
        /// <param name="condition">条件类型</param>
        public FilterInfo(PropertyInfo pi, object value, ComparisonEnum comparison = ComparisonEnum.Equal, ConditionEnum condition = ConditionEnum.And)
        {
            PropertyInfo = pi;
            Value = value;
            Comparison = comparison;
            Condition = condition;
            TargetFunc = null;
        }
        /// <summary>
        /// 目标委托
        /// </summary>
        public Func<object, bool> TargetFunc { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }
        /// <summary>
        /// 比较类型
        /// </summary>
        public ComparisonEnum Comparison { get; set; }
        /// <summary>
        /// 条件类型
        /// </summary>
        public ConditionEnum Condition { get; set; }
        /// <summary>
        /// 转换过滤器
        /// </summary>
        /// <typeparam name="TModel">要转换的类型</typeparam>
        /// <param name="inputM">输入模型</param>
        /// <returns>转换模型</returns>
        public static FilterInfo<TModel> Convert<TModel>(FilterInfo<T> inputM)
        {
            Type type = typeof(TModel);
            PropertyInfo pi = type.GetProperty((inputM.PropertyInfo.Name));
            if (pi != null)
            {
                FilterInfo<TModel> resM = new FilterInfo<TModel>(inputM.PropertyInfo.Name, inputM.Value, inputM.Comparison, inputM.Condition);
                return resM;
            }
            else
            {
                throw new ArgumentNullException($"类型{type.Name}上不存属性{inputM.PropertyInfo.Name}");
            }
        }
        /// <summary>
        /// 转换过滤器
        /// </summary>
        /// <typeparam name="TModel">要转换的类型</typeparam>
        /// <param name="inputM">输入模型</param>
        /// <returns>转换模型</returns>
        public static FilterInfo<TModel>[] Convert<TModel>(FilterInfo<T>[] inputM)
        {
            List<FilterInfo<TModel>> resM = new List<FilterInfo<TModel>>();
            foreach (FilterInfo<T> item in inputM)
            {
                try
                {
                    resM.Add(Convert<TModel>(item));
                }
                catch { }
            }
            return resM.ToArray();
        }
    }
    /// <summary>
    /// 比较类型
    /// </summary>
    public enum ComparisonEnum
    {
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("<>")]
        NotEqual,
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        Equal,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        GreaterThan,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        LessThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=")]
        GreaterThanOrEqual,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=")]
        LessThanOrEqual,
        /// <summary>
        /// 包含
        /// </summary>
        [Description("Contain")]
        Contains,
    }
    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionEnum
    {
        /// <summary>
        /// 或者
        /// </summary>
        [Description("OR")]
        Or,
        /// <summary>
        /// 并且
        /// </summary>
        [Description("AND")]
        And,
    }
}
