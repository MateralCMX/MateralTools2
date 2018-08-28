using MateralTools.Base;
using MateralTools.MConvert;
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
        #region 读取
        /// <summary>
        /// 读取Excel到数据集
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="startRowNums">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet ReadExcelToDataSet(string filePath, params int[] startRowNums)
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
                bool isAdd = false;
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (!row.GetCell(i).MIsNullOrEmptyStr())
                    {
                        isAdd = true;
                        dataRow[i] = row.GetCell(i).ToString();
                    }
                }
                if (isAdd)
                {
                    result.Rows.Add(dataRow);
                }
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
            if (sheet.LastRowNum < startRowNum)
            {
                throw new MException($"表{sheet.SheetName}无数据");
            }
            for (int i = startRowNum; i <= sheet.LastRowNum; i++)
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
            #region 开始行数组
            if (startRowNums != null && startRowNums.Length != 0)
            {
                if (startRowNums.Length == 1)
                {
                    startRowNums = GetStartRowNums(workbook.NumberOfSheets, startRowNums[0]);
                }
                else if (startRowNums.Length != workbook.NumberOfSheets)
                {
                    throw new MException("提供的开始行数数量与数据表数量不匹配");
                }
            }
            else
            {
                startRowNums = GetStartRowNums(workbook.NumberOfSheets, 0);
            }
            #endregion
            DataSet result = new DataSet();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                DataTable dataTable = SheetToDataTable(workbook.GetSheetAt(i), startRowNums[i]);
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
        #endregion
        #region 生成
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <param name="setTableHeads"></param>
        /// <returns></returns>
        public IWorkbook DataSetToWorkbook<T>(DataSet dataSet, params Func<IWorkbook, ISheet, int>[] setTableHeads) where T:IWorkbook
        {
            T workbook = default(T);
            workbook = workbook.MGetDefultObject<T>();
            #region 表头委托
            if (setTableHeads.Length != dataSet.Tables.Count)
            {
                if (setTableHeads.Length == 0)
                {
                    setTableHeads = new Func<IWorkbook, ISheet, int>[dataSet.Tables.Count];
                }
                else if (setTableHeads.Length == 1)
                {
                    Func<IWorkbook, ISheet, int> temFunc = setTableHeads[0];
                    setTableHeads = new Func<IWorkbook, ISheet, int>[dataSet.Tables.Count];
                    for (int i = 0; i < dataSet.Tables.Count; i++)
                    {
                        setTableHeads[i] = temFunc;
                    }
                }
                else
                {
                    throw new MException("提供的设置表头委托与表数量不匹配");
                }
            }
            #endregion
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                DataTableToSheet(workbook, dataSet.Tables[i], setTableHeads[i]);
            }
            return workbook;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="dataTable"></param>
        /// <param name="setTableHead"></param>
        /// <returns></returns>
        public ISheet DataTableToSheet(IWorkbook workbook, DataTable dataTable, Func<IWorkbook, ISheet, int> setTableHead = null)
        {
            ISheet sheet;
            if (dataTable.TableName.MIsNullOrEmpty())
            {
                sheet = workbook.CreateSheet();
            }
            else
            {
                sheet = workbook.CreateSheet(dataTable.TableName);
            }
            int rowNum = 0;
            if (setTableHead != null)
            {
                rowNum = setTableHead(workbook, sheet);
            }
            int ColumnNum = dataTable.Columns.Count;
            foreach (DataRow dr in dataTable.Rows)
            {
                IRow row = sheet.CreateRow(rowNum++);
                for (int i = 0; i < ColumnNum; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dr[i].ToString());
                }
            }
            return sheet;
        }
        #endregion
    }
}
