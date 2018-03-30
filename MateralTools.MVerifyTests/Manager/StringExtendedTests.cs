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
            Assert.IsTrue(@"E:\Project\MateralTools\Project\MateralTools".MIsAbsolutePath(false));
            Assert.IsFalse(@"Project\MateralTools\Project\MateralTools".MIsAbsolutePath(false));
            Assert.IsTrue(@"E:\Project\MateralTools\Project\MateralTools".MIsAbsolutePath(true));
            Assert.IsFalse(@"G\Project\MateralTools\Project\MateralTools".MIsAbsolutePath(true));

            Assert.IsTrue(@"C:\".MIsDiskPath(false));
            Assert.IsFalse(@"Cq".MIsDiskPath(false));
            Assert.IsTrue(@"C:\".MIsDiskPath(true));
            Assert.IsFalse(@"G:\".MIsDiskPath(true));

            Assert.IsTrue(@"2016/02/29".MIsDate());
            Assert.IsFalse(@"2018/02/29".MIsDate());
            Assert.IsTrue(@"2018/03/29".MIsDate("/"));
            Assert.IsFalse(@"2018.03.29".MIsDate("/"));
            Assert.IsTrue(@"2018/03/29".MIsDate("/-"));
            Assert.IsFalse(@"2018.03.29".MIsDate("/-"));

            Assert.IsTrue(@"17:30".MIsTime());
            Assert.IsTrue(@"17:30:10".MIsTime());
            Assert.IsTrue(@"17:30:10.265".MIsTime());
            Assert.IsFalse(@"17:60:10".MIsTime());
            Assert.IsFalse(@"24:30:10".MIsTime());
            Assert.IsFalse(@"12:30:10.1000".MIsTime());

            Assert.IsTrue(@"1993/04/20 17:30:21.123".MIsDateTime());
            Assert.IsTrue(@"1993/04/20T17:30:21.123".MIsDateTime("/"));
            Assert.IsFalse(@"1993/13/20T17:30:21.123".MIsDateTime());

            Assert.IsTrue(@"陈明旭".MIsChinese());
            Assert.IsFalse(@"Materal".MIsChinese());

            Assert.IsTrue(@"陈明旭Materal123456".MIsChineseOrLetterOrNumber());
            Assert.IsFalse(@"请问请问++请问请问".MIsChineseOrLetterOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".MIsEMail());
            Assert.IsFalse(@"qqqqqqqqqq".MIsEMail());

            Assert.IsTrue(@"qqqqqqqqqq".MIsFileName());
            Assert.IsFalse(@"qqqqqqqqqq".MIsFileName());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIPv4());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIPv4());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIPv4AndPort());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIPv4AndPort());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIDCardForChina());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIDCardForChina());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIDCard15ForChina());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIDCard15ForChina());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIDCard18ForChina());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIDCard18ForChina());

            Assert.IsTrue(@"qqqqqqqqqq".MIsInteger());
            Assert.IsFalse(@"qqqqqqqqqq".MIsInteger());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIntegerNegative());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIntegerNegative());

            Assert.IsTrue(@"qqqqqqqqqq".MIsIntegerPositive());
            Assert.IsFalse(@"qqqqqqqqqq".MIsIntegerPositive());

            Assert.IsTrue(@"qqqqqqqqqq".MIsJapanese());
            Assert.IsFalse(@"qqqqqqqqqq".MIsJapanese());

            Assert.IsTrue(@"qqqqqqqqqq".MIsJapaneseOrLetterOrNumber());
            Assert.IsFalse(@"qqqqqqqqqq".MIsJapaneseOrLetterOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".MIsLetter());
            Assert.IsFalse(@"qqqqqqqqqq".MIsLetter());

            Assert.IsTrue(@"qqqqqqqqqq".MIsLowerLetterr());
            Assert.IsFalse(@"qqqqqqqqqq".MIsLowerLetterr());

            Assert.IsTrue(@"qqqqqqqqqq".MIsLowerLetterrOrNumber());
            Assert.IsFalse(@"qqqqqqqqqq".MIsLowerLetterrOrNumber());

            Assert.IsTrue(@"qqqqqqqqqq".MIsNullOrEmpty());
            Assert.IsFalse(@"qqqqqqqqqq".MIsNullOrEmpty());

            Assert.IsTrue(@"qqqqqqqqqq".MIsNullOrEmptyStr());
            Assert.IsFalse(@"qqqqqqqqqq".MIsNullOrEmptyStr());

            Assert.IsTrue(@"qqqqqqqqqq".MIsPhoneNumber());
            Assert.IsFalse(@"qqqqqqqqqq".MIsPhoneNumber());

            Assert.IsTrue(@"qqqqqqqqqq".MIsRelativePath());
            Assert.IsFalse(@"qqqqqqqqqq".MIsRelativePath());

            Assert.IsTrue(@"qqqqqqqqqq".MIsUpperLetterr());
            Assert.IsFalse(@"qqqqqqqqqq".MIsUpperLetterr());

            Assert.IsTrue(@"qqqqqqqqqq".MIsURL());
            Assert.IsFalse(@"qqqqqqqqqq".MIsURL());

        }
    }
}