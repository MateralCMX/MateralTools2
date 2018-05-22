using System;
using System.ComponentModel.DataAnnotations;
namespace MateralTools.MLog
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class T_ApplicationLog
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 日志类型[0:操作日志|1:调试日志|255:异常日志]
        /// </summary>
        public byte Types { get; set; }
        /// <summary>
        /// 父级日志
        /// </summary>
        public int? FK_Parent_ID { get; set; }
    }
}
