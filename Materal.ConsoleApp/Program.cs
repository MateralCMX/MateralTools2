using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MateralTools.MConvert;
using MateralTools.MExcel;
using NPOI.SS.UserModel;
using MateralTools.MVerify;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\qwer.xlsx";
            ExcelManager excelMa = new ExcelManager();
            DataSet ds = excelMa.ReadExcelToDataSet(fileName, 2);
            Console.ReadKey();
        }
    }
}
