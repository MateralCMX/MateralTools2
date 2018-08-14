using MateralTools.Base;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MateralTools.MExcel
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
        public IWorkbook Workbook { get;}
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
        public short MaxCellNum
        {
            get
            {
                return Rows.Max(m => m.LastCellNum);
            }
        }
    }
}
