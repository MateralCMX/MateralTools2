using System.ComponentModel.DataAnnotations;

namespace MateralTools.MLog.Model
{
    public class ApplicationLogException
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        [Key]
        public int LogID { get; set; }
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
