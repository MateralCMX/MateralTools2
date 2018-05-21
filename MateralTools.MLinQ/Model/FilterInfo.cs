using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLinQ
{
    /// <summary>
    /// 过滤器信息
    /// </summary>
    public class FilterInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="piName">属性名称</param>
        public FilterInfo(Type type, string piName)
        {
            PropertyInfo = type.GetProperty(piName);
            if (PropertyInfo == null)
            {
                throw new ArgumentNullException($"类型{type.Name}上不存属性{piName}");
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pi">属性</param>
        public FilterInfo(PropertyInfo pi)
        {
            PropertyInfo = pi;
        }
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
        Contain,
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
