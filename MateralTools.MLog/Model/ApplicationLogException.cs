using System.ComponentModel.DataAnnotations;

namespace MateralTools.MLog.Model
{
    public class ApplicationLogException
    {
        /// <summary>
        /// ��־ID
        /// </summary>
        [Key]
        public int LogID { get; set; }
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
