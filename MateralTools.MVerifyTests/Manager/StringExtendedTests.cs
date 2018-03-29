using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MVerify.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MVerify.Manager.Tests
{
    [TestClass()]
    public class StringExtendedTests
    {
        [TestMethod()]
        public void IsRegexStr()
        {
            Assert.IsTrue(@"E:\Project\MateralTools\Project\MateralTools".IsAbsolutePath(false));
            Assert.IsFalse(@"Project\MateralTools\Project\MateralTools".IsAbsolutePath(false));
            Assert.IsTrue(@"E:\Project\MateralTools\Project\MateralTools".IsAbsolutePath(true));
            Assert.IsFalse(@"G\Project\MateralTools\Project\MateralTools".IsAbsolutePath(true));

            Assert.IsTrue(@"C:\".IsDiskPath(false));
            Assert.IsFalse(@"Cq".IsDiskPath(false));
            Assert.IsTrue(@"C:\".IsDiskPath(true));
            Assert.IsFalse(@"G:\".IsDiskPath(true));

            Assert.IsTrue(@"2016/02/29".IsDate());
            Assert.IsFalse(@"2018/02/29".IsDate());
            Assert.IsTrue(@"2018/03/29".IsDate("/"));
            Assert.IsFalse(@"2018.03.29".IsDate("/"));
            Assert.IsTrue(@"2018/03/29".IsDate("/-"));
            Assert.IsFalse(@"2018.03.29".IsDate("/-"));

            Assert.IsTrue(@"17:30".IsTime());
            Assert.IsTrue(@"17:30:10".IsTime());
            Assert.IsTrue(@"17:30:10.265".IsTime());
            Assert.IsFalse(@"17:60:10".IsTime());
            Assert.IsFalse(@"24:30:10".IsTime());
            Assert.IsFalse(@"12:30:10.1000".IsTime());

            Assert.IsTrue(@"1993/04/20 17:30:21.123".IsDateTime());
            Assert.IsTrue(@"1993/04/20T17:30:21.123".IsDateTime("/"));
            Assert.IsFalse(@"1993/13/20T17:30:21.123".IsDateTime());

            Assert.IsTrue(@"陈明旭".IsChinese());
            Assert.IsFalse(@"Materal".IsChinese());

            Assert.IsTrue(@"陈明旭Materal123456".IsChineseOrLetterOrNumber());
            Assert.IsFalse(@"请问请问++请问请问".IsChineseOrLetterOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".IsEMail());
            Assert.IsFalse(@"qqqqqqqqqq".IsEMail());

            Assert.IsTrue(@"qqqqqqqqqq".IsFileName());
            Assert.IsFalse(@"qqqqqqqqqq".IsFileName());

            Assert.IsTrue(@"qqqqqqqqqq".IsIPv4());
            Assert.IsFalse(@"qqqqqqqqqq".IsIPv4());

            Assert.IsTrue(@"qqqqqqqqqq".IsIPv4AndPort());
            Assert.IsFalse(@"qqqqqqqqqq".IsIPv4AndPort());

            Assert.IsTrue(@"qqqqqqqqqq".IsIDCardForChina());
            Assert.IsFalse(@"qqqqqqqqqq".IsIDCardForChina());

            Assert.IsTrue(@"qqqqqqqqqq".IsIDCard15ForChina());
            Assert.IsFalse(@"qqqqqqqqqq".IsIDCard15ForChina());

            Assert.IsTrue(@"qqqqqqqqqq".IsIDCard18ForChina());
            Assert.IsFalse(@"qqqqqqqqqq".IsIDCard18ForChina());

            Assert.IsTrue(@"qqqqqqqqqq".IsInteger());
            Assert.IsFalse(@"qqqqqqqqqq".IsInteger());

            Assert.IsTrue(@"qqqqqqqqqq".IsIntegerNegative());
            Assert.IsFalse(@"qqqqqqqqqq".IsIntegerNegative());

            Assert.IsTrue(@"qqqqqqqqqq".IsIntegerPositive());
            Assert.IsFalse(@"qqqqqqqqqq".IsIntegerPositive());

            Assert.IsTrue(@"qqqqqqqqqq".IsJapanese());
            Assert.IsFalse(@"qqqqqqqqqq".IsJapanese());

            Assert.IsTrue(@"qqqqqqqqqq".IsJapaneseOrLetterOrNumber());
            Assert.IsFalse(@"qqqqqqqqqq".IsJapaneseOrLetterOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".IsLetter());
            Assert.IsFalse(@"qqqqqqqqqq".IsLetter());

            Assert.IsTrue(@"qqqqqqqqqq".IsLowerLetterr());
            Assert.IsFalse(@"qqqqqqqqqq".IsLowerLetterr());

            Assert.IsTrue(@"qqqqqqqqqq".IsLowerLetterrOrNumber());
            Assert.IsFalse(@"qqqqqqqqqq".IsLowerLetterrOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".IsNullOrEmpty());
            Assert.IsFalse(@"qqqqqqqqqq".IsNullOrEmpty());

            Assert.IsTrue(@"qqqqqqqqqq".IsNullOrEmptyStr());
            Assert.IsFalse(@"qqqqqqqqqq".IsNullOrEmptyStr());

            Assert.IsTrue(@"qqqqqqqqqq".IsPhoneNumber());
            Assert.IsFalse(@"qqqqqqqqqq".IsPhoneNumber());

            Assert.IsTrue(@"qqqqqqqqqq".IsRelativePath());
            Assert.IsFalse(@"qqqqqqqqqq".IsRelativePath());

            Assert.IsTrue(@"qqqqqqqqqq".IsUpperLetterr());
            Assert.IsFalse(@"qqqqqqqqqq".IsUpperLetterr());

            Assert.IsTrue(@"qqqqqqqqqq".IsURL());
            Assert.IsFalse(@"qqqqqqqqqq".IsURL());

        }
    }
}