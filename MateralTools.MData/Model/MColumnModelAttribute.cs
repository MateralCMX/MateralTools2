using System;

namespace MateralTools.MData.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 数据模型
    /// </summary>
    public class MColumnModelAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="isAutoNumber">是否为自动编号</param>
        public MColumnModelAttribute(string columnName, string dbType = "varchar(200)", bool isAutoNumber = false)
        {
            DbColumnName = columnName;
            AutoNumber = isAutoNumber;
            DbType = dbType;
        }
        /// <summary>
        /// 数据库中的列名
        /// </summary>
        public string DbColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        /// 自动编号
        /// </summary>
        public bool AutoNumber { get; set; }
    }
}
