using System.ComponentModel;

namespace MateralTools.Base.MEnum
{
    /// <summary>
    /// 时间类型
    /// </summary>
    public enum TimeTypeEnum
    {
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Years = 0,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Months = 1,
        /// <summary>
        /// 日
        /// </summary>
        [Description("日")]
        Day = 2,
        /// <summary>
        /// 时
        /// </summary>
        [Description("时")]
        Hours = 3,
        /// <summary>
        /// 分
        /// </summary>
        [Description("分")]
        Minutes = 4,
        /// <summary>
        /// 秒
        /// </summary>
        [Description("秒")]
        Seconds = 5,
        /// <summary>
        /// 毫秒
        /// </summary>
        [Description("毫秒")]
        Milliseconds = 6
    }
}
