using MateralTools.MHttpRequest;
using MateralTools.MResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Materal.CoreToolsTest
{
    [TestClass]
    public class MHttpRequestTest
    {
        [TestMethod]
        public void SendGetTest()
        {
            //string url = "https://myqa.materalcmx.com/api/Bill/GetViewInfoByWhere";
            //Dictionary<string, string> urlParams = new Dictionary<string, string>
            //{
            //    ["userID"] = "5fee0f0a-1e8e-4109-96b1-feec06d6d5b2",
            //    ["minDate"] = "null",
            //    ["maxDate"] = "null",
            //    ["pagingIndex"] = "1",
            //    ["pagingSize"] = "20",
            //    ["LoginUserID"] = "5F93B1B6-439B-4934-9FFB-A534D6867422",
            //    ["Token"] = "516dcf11722c417eb38bf29a4fb9cbe8"
            //};
            //string result = HttpRequestManager.SendGet(url, urlParams);
            string url = "http://localhost:8901/api/Car/SynchronizationAllTTXCarInfo";
            MResultModel result = HttpRequestManager.SendGet<MResultModel>(url);
        }
        [TestMethod]
        public void SendPostTest()
        {
            string url = "http://localhost:60647/api/User/Login";
            Dictionary<string, string> urlParams = new Dictionary<string, string>
            {
                ["Account"] = "Admin",
                ["Password"] = "12345",
            };
            string result = HttpRequestManager.SendPost(url, urlParams);
        }
    }
}
