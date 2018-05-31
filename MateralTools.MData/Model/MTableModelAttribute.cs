using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MData
{
    /// <summary>
    /// 表模型
    /// </summary>
    public class MTableModelAttribute : Attribute
    { 
        /// <summary>
        /// 表名
        /// </summary>
        public string TabelName { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tableName">表名</param>
        public MTableModelAttribute(string tableName)
        {
            TabelName = tableName;
        }
    }
}
