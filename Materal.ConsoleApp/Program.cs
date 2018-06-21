using MateralTools.MConvert;
using MateralTools.MExcel;
using MateralTools.MHttpRequest;
using MateralTools.MResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"E:\Project\MateralTools\Project\MateralTools\Materal.ConsoleApp\qwer.xlsx";
            FileStream fs = new FileStream(fileName, FileMode.Open);
            ExcelManager eMa = new ExcelManager();
            eMa.GetExcelInfo(fs);
        }
    }
}
