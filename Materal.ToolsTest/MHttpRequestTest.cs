using System;
using System.Collections.Generic;
using MateralTools.MHttpRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Materal.ToolsTest
{
    [TestClass]
    public class MHttpRequestTest
    {
        [TestMethod]
        public void SendGetTest()
        {
            string url = "https://myqa.materalcmx.com/api/Bill/GetViewInfoByWhere";
            Dictionary<string, string> urlParams = new Dictionary<string, string>
            {
                ["userID"] = "5fee0f0a-1e8e-4109-96b1-feec06d6d5b2",
                ["minDate"] = "null",
                ["maxDate"] = "null",
                ["pagingIndex"] = "1",
                ["pagingSize"] = "20",
                ["LoginUserID"] = "5fee0f0a-1e8e-4109-96b1-feec06d6d5b2",
                ["Token"] = "fad07fda91c641e2b67bd25ded88f756"
            };
            string result = HttpRequestManager.SendGet(url, urlParams);
        }
        [TestMethod]
        public void SendPostTest()
        {
            string url = "https://myqa.materalcmx.com/api/User/Login";
            Dictionary<string, string> urlParams = new Dictionary<string, string>
            {
                ["Account"] = "Admin",
                ["Password"] = "12345",
            };
            string result = HttpRequestManager.SendPost(url, urlParams);
        }
    }
}
