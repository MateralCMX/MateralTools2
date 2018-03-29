using System.ComponentModel;

namespace MateralTools.Base.MEnum
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum SexEnum
    {
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女")]
        Female = 0,
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男")]
        Male = 1,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 2
    }
}
