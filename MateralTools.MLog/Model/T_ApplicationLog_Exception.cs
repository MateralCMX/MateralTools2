using System;
using System.ComponentModel.DataAnnotations;
namespace MateralTools.MLog
{
    public class T_ApplicationLog_Exception
    {
        /// <summary>
        /// ��־ID
        /// </summary>
        [Key]
        public int FK_Log_ID { get; set; }
        /// <summary>
        /// �쳣˵��
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// �쳣����
        /// </summary>
        public string Types { get; set; }
    }
}
