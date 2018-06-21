using MateralTools.Base;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace MateralTools.MExcel
{
    public class ExcelManager
    {
        /// <summary>
        /// 获得Excel信息
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">表名</param>
        /// <param name="isFirstRowColumn">第一行是否为表头</param>
        /// <returns></returns>
        public DataTable GetExcelInfo(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            if (File.Exists(fileName))
            {
                DataTable dt = null;
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    dt = GetExcelInfo(fs, sheetName, isFirstRowColumn);
                }
                return dt;
            }
            else
            {
                throw new MException("文件不存在");
            }
        }
        /// <summary>
        /// 获得工作簿对象
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns>工作簿对象</returns>
        private IWorkbook GetWorkbook(FileStream fs)
        {
            IWorkbook workbook = null;
            if (fs.Name.IndexOf(".xlsx") > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (fs.Name.IndexOf(".xls") > 0)
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
            if (sheetName != null)
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
        /// <summary>
        /// 获得默认表
        /// </summary>
        /// <param name="rowColumn">表头行</param>
        /// <returns></returns>
        private DataTable GetDefultTable(IRow rowColumn)
        {
            DataTable dt = new DataTable();
            int cellCount = rowColumn.LastCellNum;
            for (int i = rowColumn.FirstCellNum; i < cellCount; ++i)
            {
                ICell cell = rowColumn.GetCell(i);
                if (cell != null)
                {
                    string cellValue = cell.StringCellValue;
                    if (cellValue != null)
                    {
                        DataColumn column = new DataColumn(cellValue);
                        dt.Columns.Add(column);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获得Excel信息
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="sheetName">表名</param>
        /// <param name="isFirstRowColumn">第一行是否为表头</param>
        /// <returns></returns>
        public DataTable GetExcelInfo(FileStream fs, string sheetName = null, bool isFirstRowColumn = true)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                int startRow = 0;
                IWorkbook workbook = GetWorkbook(fs);
                ISheet sheet = GetSheet(workbook, sheetName);
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum;
                    if (isFirstRowColumn)
                    {
                        dt = GetDefultTable(firstRow);
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            DataRow dataRow = dt.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
