using System;
using System.ComponentModel.DataAnnotations;

namespace MateralTools.MLog.Model
{
    /// <summary>
    /// 异常日志视图
    /// </summary>
    public class ApplicationLogExceptionView
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
        /// 异常说明
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常类型
        /// </summary>
        public string Types { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 父级日志
        /// </summary>
        public int? ParentID { get; set; }
    }
}
