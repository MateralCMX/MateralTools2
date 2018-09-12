using System.Collections.Generic;
using System.IO;
using System.Linq;
using MateralTools.Base.Model;
using NPOI.SS.UserModel;

namespace MateralTools.MExcel.Model
{
    public class ExcelWorkbook
    {
        public ExcelWorkbook(string filePatch)
        {
            if (!File.Exists(filePatch))
            {
                throw new MException("文件不存在");
            }
            FilePatch = filePatch;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePatch { get;}
    }
    public class ExcelRowModel
    {
        /// <summary>
        /// 表格行
        /// </summary>
        public List<IRow> Rows { get; set; }
        /// <summary>
        /// 最大的单元格数
        /// </summary>
        public short MaxCellNum => Rows.Count > 0 ? Rows.Max(m => m.LastCellNum) : throw new MException("未能读取到有效数据");
    }
}
