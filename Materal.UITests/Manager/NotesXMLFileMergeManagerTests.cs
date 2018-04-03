using Microsoft.VisualStudio.TestTools.UnitTesting;
using Materal.UI.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.UI.Manager.Tests
{
    [TestClass()]
    public class NotesXMLFileMergeManagerTests
    {
        [TestMethod()]
        public void MergeTest()
        {
            NotesXMLFileMergeManager.Merge(@"D:\Test\RTPay.API.xml", new string[]{
                @"D:\Test\RTPay.Common.xml"
            });
        }
    }
}