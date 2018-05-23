using MateralTools.MLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Materal.ToolsTest
{
    [TestClass]
    public class MLogTest
    {
        /// <summary>
        /// sqllitLog业务对象
        /// </summary>
        private readonly IMLogBLL sqliteLogBLL;
        /// <summary>
        /// xml数据对象
        /// </summary>
        private readonly IMLogBLL xmlLogBLL;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MLogTest()
        {
            sqliteLogBLL = new MSQLiteLogBLL();
            xmlLogBLL = new MXMLLogBLL();
        }
        [TestMethod]
        public void WriteOptionsLog()
        {
            int id = 0;
            id = sqliteLogBLL.WriteOptionsLog("测试操作日志标题", "测试操作日志消息");
            id = sqliteLogBLL.WriteOptionsLog("测试子级日志", "测试子级操作日志消息", id);
            id = xmlLogBLL.WriteOptionsLog("测试操作日志标题", "测试操作日志消息");
            id = xmlLogBLL.WriteOptionsLog("测试子级日志", "测试子级操作日志消息", id);
        }
        [TestMethod]
        public void WriteExceptionLog()
        {
            Exception ex1 = new Exception("第一级异常");
            Exception ex2 = new Exception("第二级异常", ex1);
            Exception ex3 = new Exception("第三级异常", ex2);
            int id = 0;
            id = sqliteLogBLL.WriteExceptionLog(ex3);
            id = xmlLogBLL.WriteExceptionLog(ex3);
        }
    }
}
