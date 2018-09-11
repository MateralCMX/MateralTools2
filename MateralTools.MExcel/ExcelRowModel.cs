using MateralTools.Base;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MateralTools.Base.Model;

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
                if (Rows.Count > 0)
                {
                    return Rows.Max(m => m.LastCellNum);
                }
                else
                {
                    throw new MException("未能读取到有效数据");
                }
            }
        }
    }
}
