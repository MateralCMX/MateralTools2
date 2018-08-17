using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MateralTools.MConvert;
using MateralTools.MExcel;
using NPOI.SS.UserModel;
using MateralTools.MVerify;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string openFileName = @"D:\qwer.xlsx";
            string saveFileName = @"D:\qwer.xls";
            ExcelManager excelMa = new ExcelManager();
            //DataSet ds = excelMa.ReadExcelToDataSet(fileName, 2);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("qwer");
            DataColumn dc = new DataColumn("Index");
            dt.Columns.Add(dc);
            dc = new DataColumn("Name");
            dt.Columns.Add(dc);
            dc = new DataColumn("Remark");
            dt.Columns.Add(dc);
            DataRow dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "Materal1";
            dr[2] = "XXXXXXXXX";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "Materal2";
            dr[2] = "";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "3";
            dr[1] = "Materal3";
            dr[2] = "XXXXXXXXX";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            IWorkbook workbook = excelMa.DataSetToWorkbook<HSSFWorkbook>(ds, (work, sheet) =>
            {
                int rowNum = 0;
                IRow row = sheet.CreateRow(rowNum++);
                ICell cell = row.CreateCell(0);
                cell.SetCellValue(dt.TableName);
                cell.CellStyle.Alignment = HorizontalAlignment.Center;
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                return rowNum;
            });
            FileStream fs2 = File.Create(saveFileName);
            workbook.Write(fs2);
            fs2.Close();
            Console.ReadKey();
        }
    }
}
