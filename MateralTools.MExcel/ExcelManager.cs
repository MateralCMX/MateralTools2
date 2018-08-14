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
        /// 读取Excel到数据集
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="startRowNums">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet ReadExcelToDataSet(string filePath,params int[] startRowNums)
        {
            IWorkbook workbook = ReadExcelToWorkbook(filePath);
            return WorkbookToDataSet(workbook, startRowNums);
        }
        /// <summary>
        /// 读取Excel到数据集
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="startRowNums">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet ReadExcelToDataSet(FileStream fileStream, params int[] startRowNums)
        {
            IWorkbook workbook = ReadExcelToWorkbook(fileStream);
            return WorkbookToDataSet(workbook, startRowNums);
        }
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>工作簿对象</returns>
        public IWorkbook ReadExcelToWorkbook(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new MException("文件不存在");
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook result = ReadExcelToWorkbook(fs);
                return result;
            }
        }
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <returns>工作簿对象</returns>
        public IWorkbook ReadExcelToWorkbook(FileStream fileStream)
        {
            IWorkbook workbook = null;
            try
            {
                if (fileStream.Name.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (fileStream.Name.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new MException("不识别的Excel文件", ex);
            }
            return workbook;
        }
        /// <summary>
        /// 读取Excel到工作表组
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>工作表组</returns>
        public List<ISheet> ReadExcelToSheets(string filePath)
        {
            IWorkbook workbook = ReadExcelToWorkbook(filePath);
            return GetAllSheets(workbook);
        }
        /// <summary>
        /// 读取Excel到工作表组
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <returns>工作表组</returns>
        public List<ISheet> ReadExcelToSheets(FileStream fileStream)
        {
            IWorkbook workbook = ReadExcelToWorkbook(fileStream);
            return GetAllSheets(workbook);
        }
        /// <summary>
        /// 获得所有工作表
        /// </summary>
        /// <param name="workbook">工作簿对象</param>
        /// <returns>工作表对象</returns>
        public List<ISheet> GetAllSheets(IWorkbook workbook)
        {
            List<ISheet> sheets = new List<ISheet>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                sheets.Add(workbook.GetSheetAt(i));
            }
            return sheets;
        }
        /// <summary>
        /// 工作表转换为数据表
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns>数据表</returns>
        public DataTable SheetToDataTable(ISheet sheet, int startRowNum = 0)
        {
            DataTable result = new DataTable(sheet.SheetName);
            ExcelRowModel excelRowModel = GetRows(sheet, startRowNum);
            for (int i = 0; i < excelRowModel.MaxCellNum; i++)
            {
                DataColumn dataColumn = new DataColumn(i.ToString(), typeof(string));
                result.Columns.Add(dataColumn);
            }
            foreach (IRow row in excelRowModel.Rows)
            {
                DataRow dataRow = result.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    dataRow[i] = row.GetCell(i).ToString();
                }
                result.Rows.Add(dataRow);
            }
            return result;
        }
        /// <summary>
        /// 获得行
        /// </summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns>工作行</returns>
        public ExcelRowModel GetRows(ISheet sheet, int startRowNum = 0)
        {
            ExcelRowModel result = new ExcelRowModel();
            result.Rows = new List<IRow>();
            for (int i = startRowNum; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                result.Rows.Add(row);
            }
            return result;
        }
        /// <summary>
        /// 工作簿转换为数据集
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="startRowNums">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet WorkbookToDataSet(IWorkbook workbook, params int[] startRowNums)
        {
            List<ISheet> sheets = GetAllSheets(workbook);
            #region 开始行数组
            if (startRowNums != null && startRowNums.Length != 0)
            {
                if (startRowNums.Length == 1)
                {
                    startRowNums = GetStartRowNums(sheets.Count, startRowNums[0]);
                }
                else if (startRowNums.Length != sheets.Count)
                {
                    throw new MException("提供的开始行数数量与数据表数量不匹配");
                }
            }
            else
            {
                startRowNums = GetStartRowNums(sheets.Count, 0);
            }
            #endregion
            DataSet result = new DataSet();
            for (int i = 0; i < sheets.Count; i++)
            {
                ISheet sheet = sheets[i];
                DataTable dataTable = SheetToDataTable(sheet, startRowNums[i]);
                result.Tables.Add(dataTable);
            }
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 获得开始行数组
        /// </summary>
        /// <param name="count">总数</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private int[] GetStartRowNums(int count, int value)
        {
            int[] startRowNums = new int[count];
            for (int i = 0; i < count; i++)
            {
                startRowNums[i] = value;
            }
            return startRowNums;
        }
        #endregion









































        //public DataSet ReadExcelToDataSetByFileName(string fileName, params int[] startRowNums)
        //{
        //    if (!File.Exists(fileName))
        //    {
        //        throw new MException("文件不存在");
        //    }
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        DataSet result = ReadExcelToDataSetByFileStream(fs, startRowNums);
        //        string[] fileNameArray = fileName.Split('\\');
        //        result.DataSetName = fileNameArray[fileNameArray.Length - 1];
        //        return result;
        //    }
        //}
        //public DataSet ReadExcelToDataSetByFileStream(FileStream fs, params int[] startRowNums)
        //{
        //    DataSet result = new DataSet();
        //    IWorkbook workbook = GetWorkbook(fs);
        //    #region 组装StartRowNums
        //    if (startRowNums != null && startRowNums.Length > 1 && startRowNums.Length != workbook.NumberOfSheets)
        //    {
        //        throw new MException("提供的开始行数数量与表格数量不匹配");
        //    }
        //    else if (startRowNums == null || startRowNums.Length == 0)
        //    {
        //        startRowNums = new int[workbook.NumberOfSheets];
        //        for (int i = 0; i < workbook.NumberOfSheets; i++)
        //        {
        //            startRowNums[i] = 0;
        //        }
        //    }
        //    else if (startRowNums.Length == 1)
        //    {
        //        int tempStartRowNum = startRowNums[0];
        //        startRowNums = new int[workbook.NumberOfSheets];
        //        for (int i = 0; i < workbook.NumberOfSheets; i++)
        //        {
        //            startRowNums[i] = tempStartRowNum;
        //        }
        //    }
        //    #endregion
        //    for (int i = 0; i < workbook.NumberOfSheets; i++)
        //    {
        //        ISheet sheet = workbook.GetSheetAt(i);
        //        DataTable dt = ReadExcelToDataTableBySheet(sheet, startRowNums[i]);
        //        if (dt != null)
        //        {
        //            result.Tables.Add(dt);
        //        }
        //    }
        //    return result;
        //}
        //private DataTable ReadExcelToDataTableBySheet(ISheet sheet, int startRowNum = 0)
        //{
        //    DataTable result = GetNewTable(sheet, startRowNum);
        //    for (int i = sheet.FirstRowNum; i < sheet.LastRowNum; i++)
        //    {
        //        IRow row = sheet.GetRow(startRowNum);
        //        DataRow dataRow = result.NewRow();
        //        for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
        //        {
        //            ICell cell = row.GetCell(j);
        //            try
        //            {
        //                dataRow[j] = cell.StringCellValue;
        //            }
        //            catch
        //            {
        //                dataRow[j] = cell.ToString();
        //            }
        //        }
        //        result.Rows.Add(dataRow);
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 开始行
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <param name="startRowNum"></param>
        ///// <returns></returns>
        //private DataTable GetNewTable(ISheet sheet, int startRowNum = 0)
        //{
        //    DataTable result = new DataTable(sheet.SheetName);
        //    IRow row = sheet.GetRow(0);
        //    for (int i = 0; i < row.LastCellNum; i++)
        //    {
        //        result.Columns.Add(new DataColumn(i.ToString(), typeof(string)));
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 读取Excel文件
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <param name="sheetName">工作簿名称</param>
        ///// <param name="startRowNum">开始行数</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public DataTable ReadExcelToDataTableBySheetName(string fileName, string sheetName = null, int startRowNum = 0)
        //{
        //    if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
        //    DataTable result = new DataTable();
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        IWorkbook workbook = GetWorkbook(fs);
        //        ISheet sheet = GetSheet(workbook, sheetName);
        //        IRow headRow;
        //        if (startRowNum == 0)
        //        {
        //            headRow = sheet.GetRow(0);
        //        }
        //        else
        //        {
        //            headRow = sheet.GetRow(startRowNum - 1);
        //        }
        //        result = GetDefultTable(headRow);
        //        List<IRow> excelRows = ReadExcel(sheet, startRowNum);
        //        foreach (IRow row in excelRows)
        //        {
        //            DataRow dataRow = result.NewRow();
        //            for (int i = row.FirstCellNum; i < row.LastCellNum; ++i)
        //            {
        //                ICell cell = row.GetCell(i);
        //                if (cell != null)
        //                {
        //                    dataRow[i] = cell.ToString();
        //                }
        //            }
        //            result.Rows.Add(dataRow);
        //        }
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 读取Excel文件
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <param name="sheetName">工作簿名称</param>
        ///// <param name="startRowNum">开始行数</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public DataTable ReadExcelToDataTableBySheetIndex(string fileName, int sheetIndex = 0, int startRowNum = 0)
        //{
        //    if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
        //    DataTable result = new DataTable();
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        IWorkbook workbook = GetWorkbook(fs);
        //        ISheet sheet = workbook.GetSheetAt(sheetIndex);
        //        IRow headRow;
        //        if (startRowNum == 0)
        //        {
        //            headRow = sheet.GetRow(0);
        //        }
        //        else
        //        {
        //            headRow = sheet.GetRow(startRowNum - 1);
        //        }
        //        result = GetDefultTable(headRow);
        //        List<IRow> excelRows = ReadExcel(sheet, startRowNum);
        //        foreach (IRow row in excelRows)
        //        {
        //            DataRow dataRow = result.NewRow();
        //            for (int i = row.FirstCellNum; i < row.LastCellNum; ++i)
        //            {
        //                ICell cell = row.GetCell(i);
        //                if (cell != null)
        //                {
        //                    dataRow[i] = cell.ToString();
        //                }
        //            }
        //            result.Rows.Add(dataRow);
        //        }
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 读取Excel文件
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <param name="sheetName">工作簿名称</param>
        ///// <param name="startRowNum">开始行数</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public List<IRow> ReadExcelBySheetName(string fileName, string sheetName = null, int startRowNum = 0)
        //{
        //    if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
        //    List<IRow> result = new List<IRow>();
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        IWorkbook workbook = GetWorkbook(fs);
        //        ISheet sheet = GetSheet(workbook, sheetName);
        //        result = ReadExcel(sheet, startRowNum);
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 读取Excel文件
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <param name="sheetIndex">工作簿位序</param>
        ///// <param name="startRowNum">开始行数</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public List<IRow> ReadExcelBySheetIndex(string fileName, int sheetIndex = 0, int startRowNum = 0)
        //{
        //    if (!File.Exists(fileName)) throw new ArgumentNullException("文件不存在");
        //    List<IRow> result = new List<IRow>();
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        IWorkbook workbook = GetWorkbook(fs);
        //        ISheet sheet = workbook.GetSheetAt(sheetIndex);
        //        result = ReadExcel(sheet, startRowNum);
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 获得默认表
        ///// </summary>
        ///// <param name="headRow">表头行</param>
        ///// <returns></returns>
        //private DataTable GetDefultTable(IRow headRow)
        //{
        //    DataTable dt = new DataTable();
        //    for (int i = headRow.FirstCellNum; i <= headRow.LastCellNum; ++i)
        //    {
        //        ICell cell = headRow.GetCell(i);
        //        if (cell != null)
        //        {
        //            DataColumn column = new DataColumn(cell.ToString(), typeof(string));
        //            dt.Columns.Add(column);
        //        }
        //    }
        //    return dt;
        //}
        ///// <summary>
        ///// 读取Excel文件
        ///// </summary>
        ///// <param name="workbook">工作簿</param>
        ///// <param name="startRowNum">开始行数</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //private List<IRow> ReadExcel(ISheet sheet, int startRowNum = 0)
        //{
        //    if (startRowNum < 0) throw new ArgumentNullException("开始行数不能小于0");
        //    List<IRow> result = new List<IRow>();
        //    for (int i = startRowNum; i <= sheet.LastRowNum; i++)
        //    {
        //        result.Add(sheet.GetRow(i));
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 获得工作簿对象
        ///// </summary>
        ///// <param name="fs">文件流</param>
        ///// <returns>工作簿对象</returns>
        //public IWorkbook GetWorkbook(FileStream fs)
        //{
        //    IWorkbook workbook = null;
        //    if (fs.Name.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
        //    {
        //        workbook = new XSSFWorkbook(fs);
        //    }
        //    else if (fs.Name.IndexOf(".xls", StringComparison.Ordinal) > 0)
        //    {
        //        workbook = new HSSFWorkbook(fs);
        //    }
        //    return workbook;
        //}
        ///// <summary>
        ///// 获得工作表
        ///// </summary>
        ///// <param name="workbook">目标工作簿</param>
        ///// <param name="sheetName">工作表名称</param>
        ///// <returns></returns>
        //private ISheet GetSheet(IWorkbook workbook, string sheetName = null)
        //{
        //    ISheet sheet = null;
        //    if (!sheetName.MIsNullOrEmpty())
        //    {
        //        sheet = workbook.GetSheet(sheetName);
        //        if (sheet == null)
        //        {
        //            sheet = workbook.GetSheetAt(0);
        //        }
        //    }
        //    else
        //    {
        //        sheet = workbook.GetSheetAt(0);
        //    }
        //    return sheet;
        //}
    }
}
