using System;
using System.IO;
using MateralTools.MConvert;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string dateStr = "Tue Jul 24 2018 09:58:27 GMT+0800 (中国标准时间)";
            DateTime dt = (DateTime)dateStr.ConvertTo(typeof(DateTime));
        }
    }
}
