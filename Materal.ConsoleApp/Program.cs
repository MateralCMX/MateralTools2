using MateralTools.MConvert;
using MateralTools.MHttpRequest;
using MateralTools.MResult;
using System.Collections.Generic;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string url = "http://localhost:8901/api/Car/SynchronizationAllTTXCarInfo";
            //MResultModel result = HttpRequestManager.SendGet<MResultModel>(url);
            //string jsonStr = "{\"ResultType\": 0,\"ResultTypeStr\": \"string\",\"Message\": \"string\"}";
            //MResultModel result = jsonStr.MJsonToObject<MResultModel>();
            string url = "http://localhost:60647/api/User/Login";
            Dictionary<string, string> urlParams = new Dictionary<string, string>
            {
                ["Account"] = "Admin",
                ["Password"] = "123456",
            };
            string result = HttpRequestManager.SendPost(url, urlParams);
        }
    }
}
