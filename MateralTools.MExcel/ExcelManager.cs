using MateralTools.Base;
using MateralTools.MVerify;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MateralTools.MExcel
{
    public class ExcelManager
    {
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public DataTable ReadExcelToDataTable(string fileName, string sheetName = null, int startRowNum = 0)
        {
            if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
            DataTable result = new DataTable();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = GetWorkbook(fs);
                ISheet sheet = GetSheet(workbook, sheetName);
                IRow headRow;
                if (startRowNum == 0)
                {
                    headRow = sheet.GetRow(0);
                }
                else
                {
                    headRow = sheet.GetRow(startRowNum - 1);
                }
                result = GetDefultTable(headRow);
                List<IRow> excelRows = ReadExcel(sheet, sheetName, startRowNum);
                foreach (IRow row in excelRows)
                {
                    DataRow dataRow = result.NewRow();
                    for (int i = row.FirstCellNum; i < row.LastCellNum; ++i)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell != null)
                        {
                            dataRow[i] = cell.ToString();
                        }
                    }
                    result.Rows.Add(dataRow);
                }
            }
            return result;
        }
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<IRow> ReadExcel(string fileName, string sheetName = null, int startRowNum = 0)
        {
            if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
            List<IRow> result = new List<IRow>();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = GetWorkbook(fs);
                ISheet sheet = GetSheet(workbook, sheetName);
                result = ReadExcel(sheet, sheetName, startRowNum);
            }
            return result;
        }
        /// <summary>
        /// 获得默认表
        /// </summary>
        /// <param name="headRow">表头行</param>
        /// <returns></returns>
        private DataTable GetDefultTable(IRow headRow)
        {
            DataTable dt = new DataTable();
            for (int i = headRow.FirstCellNum; i <= headRow.LastCellNum; ++i)
            {
                ICell cell = headRow.GetCell(i);
                if (cell != null)
                {
                    DataColumn column = new DataColumn(cell.ToString(), typeof(string));
                    dt.Columns.Add(column);
                }
            }
            return dt;
        }
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private List<IRow> ReadExcel(ISheet sheet, string sheetName = null, int startRowNum = 0)
        {
            if (startRowNum < 0) throw new ArgumentNullException("开始行数不能小于0");
            List<IRow> result = new List<IRow>();
            for (int i = startRowNum; i <= sheet.LastRowNum; i++)
            {
                result.Add(sheet.GetRow(i));
            }
            return result;
        }
        /// <summary>
        /// 获得工作簿对象
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns>工作簿对象</returns>
        private IWorkbook GetWorkbook(FileStream fs)
        {
            IWorkbook workbook = null;
            if (fs.Name.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (fs.Name.IndexOf(".xls", StringComparison.Ordinal) > 0)
            {
                workbook = new HSSFWorkbook(fs);
            }
            return workbook;
        }
        /// <summary>
        /// 获得工作表
        /// </summary>
        /// <param name="workbook">目标工作簿</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        private ISheet GetSheet(IWorkbook workbook, string sheetName = null)
        {
            ISheet sheet = null;
            if (!sheetName.MIsNullOrEmpty())
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            return sheet;
        }
    }
}
