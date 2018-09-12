using System.ComponentModel;

namespace MateralTools.MLog.Model
{
    /// <summary>
    /// 日志类型枚举
    /// </summary>
    public enum ApplicationLogTypeEnum : byte
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        [Description("操作日志")]
        Options = 0,
        /// <summary>
        /// 调试日志
        /// </summary>
        [Description("调试日志")]
        Debug = 1,
        /// <summary>
        /// 异常日志
        /// </summary>
        [Description("异常日志")]
        Exception = byte.MaxValue,
    }
}
