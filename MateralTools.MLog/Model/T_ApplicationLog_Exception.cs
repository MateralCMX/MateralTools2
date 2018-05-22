using System;
using System.ComponentModel.DataAnnotations;
namespace MateralTools.MLog
{
    public class T_ApplicationLog_Exception
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        [Key]
        public int FK_Log_ID { get; set; }
        /// <summary>
        /// 异常说明
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常类型
        /// </summary>
        public string Types { get; set; }
    }
}
