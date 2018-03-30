using System;

namespace MateralTools.Base.MTable
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public class ColumnModelAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="IsAutoNumber">是否为自动编号</param>
        public ColumnModelAttribute(string ColumnName, string dbType = "varchar(200)", bool IsAutoNumber = false)
        {
            DBColumnName = ColumnName;
            AutoNumber = IsAutoNumber;
            DBType = dbType;
        }
        /// <summary>
        /// 数据库中的列名
        /// </summary>
        public string DBColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DBType { get; set; }
        /// <summary>
        /// 自用编号
        /// </summary>
        public bool AutoNumber { get; set; }
    }
}
