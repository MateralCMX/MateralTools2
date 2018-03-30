using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.Base.Base.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.Base.Base.Manager.Tests
{
    [TestClass()]
    public class CommonManagerTests
    {
        [TestMethod()]
        public void GetRandomStrTest()
        {
            CommonManager.GetRandomStrByGUID(587);
        }
    }
}