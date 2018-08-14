using System;
using System.Data;
using System.IO;
using MateralTools.MConvert;
using MateralTools.MExcel;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\qwer.xlsx";
            ExcelManager excelMa = new ExcelManager();
            var result = excelMa.ReadExcelToDataTable(fileName, null, 2);
        }
    }
}
