using MateralTools.MLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Materal.ToolsTest
{
    [TestClass]
    public class MLogTest
    {
        [TestMethod]
        public void WriteOptionsLog()
        {
            int id = MSQLiteLogBLL.WriteOptionsLog("测试操作日志标题", "测试操作日志消息");
            id = MSQLiteLogBLL.WriteOptionsLog("测试子级日志", "测试子级操作日志消息", id);
        }
    }
}
