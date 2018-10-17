using MateralTools.MHttpRequest.Manager;
using MateralTools.MHttpRequest.Model;
using MateralTools.MResult.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Task.Run(async () =>
            {
                try
                {
                    var apiUrl = "http://192.168.0.252:8069";
                    var dataStr = "{\"Account\":\"Admin\",\"Password\":\"123456\"}";
                    var postHeads = new Dictionary<string, string>
                    {
                        ["Content-Type"] = "application/json",
                        ["Access-Control-Allow-Origin"] = "*",
                        ["Timeout"] = "100"
                    };
                    DataResult<string> loginResult =
                        await HttpRequestManager.SendPostAsync<DataResult<string>>(apiUrl + "/api/Session/Login", dataStr, postHeads);
                    string token = loginResult.Data;
                    postHeads.Add("Authorization", "Bearer " + token);
                    var getHeads = new Dictionary<string, string>
                    {
                        ["Access-Control-Allow-Origin"] = "*",
                        ["Timeout"] = "100",
                        ["Authorization"] = "Bearer " + token
                    };
                    string getAllSexResult =
                        await HttpRequestManager.SendGetAsync(apiUrl + "/api/User/GetAllSex", null, getHeads);

                    var editMyPassword = new Dictionary<string, string>
                    {
                        ["OldPassword"] = "1",
                        ["NewPassword"] = "1",
                        ["Token"] = token
                    };
                    string editMyPasswordResult =
                        await HttpRequestManager.SendPostAsync(apiUrl + "/api/User/EditMyPassword", editMyPassword, postHeads);
                }
                catch (MHttpRequestException ex)
                {
                    Console.WriteLine((int)ex.StatusCode + " " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            Console.ReadKey();
            //var data = new Dictionary<string, string>
            //{
            //    ["OldPassword"] = "1",
            //    ["NewPassword"] = "1",
            //    ["Token"] = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjRkY2JkMjY2YWMwMjJlNjRhOTI1OTE2MzRhYTk2MmMwIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1Mzk1ODY4MzAsImV4cCI6MTU1NTM1NDgzMCwiaXNzIjoiaHR0cDovLzE5Mi4xNjguMC4yNTI6NTAwMSIsImF1ZCI6WyJodHRwOi8vMTkyLjE2OC4wLjI1Mjo1MDAxL3Jlc291cmNlcyIsImFwaTEiXSwiY2xpZW50X2lkIjoicm8uY2xpZW50Iiwic3ViIjoiNzAwZjc5ODgtYmI1YS00N2E4LWI3YzktZTBkNzJiNjdjYjVmIiwiYXV0aF90aW1lIjoxNTM5NTg2ODMwLCJpZHAiOiJsb2NhbCIsInNjb3BlIjpbImFwaTEiXSwiYW1yIjpbImN1c3RvbSJdfQ.l-bPhIjh2tt1ClL58Fnown817s-aZ5S7jUiwuSxcxiJZVCrhUEIyAm4PPyflQfo-poiDyrCK_NCdzK2kRfUZzZAamQu7fXMKYWLrOmgcQxb8O5rVhDIdgOL1-U2CoHKqea-VZh3DFjrNyN2dGPPMPasjxkJpxaoQkVG-chzzXLslU6tC4fhDJyPlt6zEuSsAqXeLOCApLaVlHg2Y99-EzWHJcCX2xVo_sug_RLVWXuwnwQxSFXXOA5-Fxx-2ixMvIk__S_i-4nw8Vei_AyCVMIMF78sBBBafHcoxtNMNOyu11Uc-9WjpSZGdeV3TwxKV9Oh3RZvj-YTVkWqOSjlqGA",
            //};
            //var heads = new Dictionary<string, string>
            //{
            //    ["Authorization"] = "Bearer " + data["Token"]
            //};
            //DataResult<string> httpResult = HttpRequestManager.SendPost<DataResult<string>>("http://localhost:55589/api/User/EditMyPassword", data, heads);
        }
    }
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            string filePath = @"D:\qwe.xlsx";
    //            string savePath = @"D:\TEST\";
    //            ExcelManager excelManager = new ExcelManager();
    //            DataSet ds = excelManager.ReadExcelToDataSet(filePath);
    //            DataTable dt = ds.Tables[0];
    //            List<SetDeviceParam> setDeviceParams = new List<SetDeviceParam>();
    //            foreach (DataRow item in dt.Rows)
    //            {
    //                setDeviceParams.Add(new SetDeviceParam
    //                {
    //                    Code = item[0].ToString(),
    //                    Type = item[1].ToString(),
    //                    Value = item[2].ToString(),
    //                    Remark = item[3].ToString()
    //                });
    //            }
    //            setDeviceParams = setDeviceParams.OrderBy(m => m.Code).ToList();
    //            string EnumText = "";
    //            string IWebStockClientText = "";
    //            foreach (var item in setDeviceParams)
    //            {
    //                EnumText += item.EnumTxt;
    //                IWebStockClientText += item.IWebStockClientText;
    //                var di = new DirectoryInfo(savePath);
    //                using (StreamWriter streamWriter = new StreamWriter(savePath + item.EventFileName + ".cs"))
    //                {
    //                    streamWriter.Write(item.EventText);
    //                }
    //                //using (StreamWriter streamWriter = new StreamWriter(savePath + item.DTOFileName + ".cs"))
    //                //{
    //                //    streamWriter.Write(item.DTOText);
    //                //}
    //                //using (StreamWriter streamWriter = new StreamWriter(savePath + item.EventHandlerFileName + ".cs"))
    //                //{
    //                //    streamWriter.Write(item.EventHandlerText);
    //                //}
    //            }
    //#if DEBUG
    //            Console.WriteLine("Debug");
    //            Console.ReadKey();
    //#else   
    //            Console.WriteLine("Release");
    //            Console.ReadKey();
    //#endif
    //        }
    //    }

    //    public class SetDeviceParam
    //    {
    //        /// <summary>
    //        /// 编码
    //        /// </summary>
    //        public string Code { get; set; }
    //        /// <summary>
    //        /// 首字母大写编码
    //        /// </summary>
    //        public string UpCode => char.ToUpper(Code[0]) + Code.Substring(1);

    //        public string EnumTxt
    //        {
    //            get
    //            {
    //                string text = "";
    //                text += "        /// <summary>\r\n";
    //                text += $"        /// {Remark}\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += $"        [Description(\"{Remark}\")]\r\n";
    //                text += $"        SetDeviceParam{UpCode}Event,\r\n";
    //                return text;
    //            }
    //        }

    //        /// <summary>
    //        /// 事件文件名
    //        /// </summary>
    //        public string EventFileName => $"SetDeviceParam{UpCode}Event";
    //        /// <summary>
    //        /// 事件文本
    //        /// </summary>
    //        public string EventText
    //        {
    //            get
    //            {
    //                string text = "";
    //                text += "using System;\r\n";
    //                text += "\r\n";
    //                text += "namespace BeiDouDP.BeiDouEvents.Device\r\n";
    //                text += "{\r\n";
    //                text += "    /// <summary>\r\n";
    //                text += "    /// 设备参数事件\r\n";
    //                text += $"    /// {Remark}\r\n";
    //                text += "    /// </summary>\r\n";
    //                text += "    [Serializable]\r\n";
    //                text += $"    public class {EventFileName} : SetDeviceParamEvent<{CSharpType}>\r\n";
    //                text += "    {\r\n";
    //                text += $"        public {EventFileName}(string[] deviceNos, {CSharpType} value)\r\n";
    //                text += "        {\r\n";
    //                text += $"            Key = \"{Code}\";\r\n";
    //                text += "            DeviceNos = deviceNos;\r\n";
    //                text += "            Value = value;\r\n";
    //                text += "        }\r\n";
    //                text += "    }\r\n";
    //                text += "}\r\n";
    //                return text;
    //            }
    //        }
    //        /// <summary>
    //        /// 事件处理器文件名
    //        /// </summary>
    //        public string EventHandlerFileName => $"SetDeviceParam{UpCode}EventHandler";

    //        public string EventHandlerText
    //        {
    //            get
    //            {
    //                string text = "";
    //                text += "using BeiDouDP.BeiDouEvents.Device;\r\n";
    //                text += "using BeiDouDP.ExternalCommands.SetParam;\r\n";
    //                text += "using BeiDouDP.ExternalWebStockClient;\r\n";
    //                text += "using Infrastructure;\r\n";
    //                text += "using Messaging.ShareKennel;\r\n";
    //                text += "using System.Collections.Generic;\r\n";
    //                text += "using System.Threading.Tasks;\r\n";
    //                text += "\r\n";
    //                text += "namespace BeiDouDP.BeiDouEventHandlers.Device\r\n";
    //                text += "{\r\n";
    //                text += "    /// <summary>\r\n";
    //                text += "    /// 设置设备参数事件处理器\r\n";
    //                text += $"    /// {Remark}\r\n";
    //                text += "    /// </summary>\r\n";
    //                text += $"    public class {EventHandlerFileName}: IEventHandler\r\n";
    //                text += "    {\r\n";
    //                text += "        private readonly IExternalWebStockClient _externalWebStockClient;\r\n";
    //                text += $"        public {EventHandlerFileName}(IExternalWebStockClient externalWebStockClient)\r\n";
    //                text += "        {\r\n";
    //                text += "            _externalWebStockClient = externalWebStockClient;\r\n";
    //                text += "        }\r\n";
    //                text += "        public async Task Handle(byte[] data = null)\r\n";
    //                text += "        {\r\n";
    //                text += $"            var args = SerializableHelper.Deserialize<SetDeviceParam{UpCode}Event>(data);\r\n";
    //                text += $"            var command = new SetParamCommand<{CSharpType}>\r\n";
    //                text += "            {\r\n";
    //                text += "                devIdnos = string.Join(\",\", args.DeviceNos),\r\n";
    //                text += $"                parameters = new Dictionary<string, {CSharpType}>()\r\n";
    //                text += "            };\r\n";
    //                text += "            command.parameters[args.Key] = args.Value;\r\n";
    //                text += "            await _externalWebStockClient.SendMessageByCommandAsync(command);\r\n";
    //                text += "        }\r\n";
    //                text += "    }\r\n";
    //                text += "}\r\n";
    //                return text;
    //            }
    //        }
    //        /// <summary>
    //        /// DTO文件名
    //        /// </summary>
    //        public string DTOFileName => $"SetDeviceParam{UpCode}DTO";
    //        /// <summary>
    //        /// DTO文本
    //        /// </summary>
    //        public string DTOText
    //        {
    //            get
    //            {
    //                string text = "";
    //                text += "using System;\r\n";
    //                text += "\r\n";
    //                text += "namespace BeiDouDP.DTO.Device\r\n";
    //                text += "{\r\n";
    //                text += "    /// <summary>\r\n";
    //                text += $"    /// {Remark}\r\n";
    //                text += "    /// </summary>\r\n";
    //                text += "    [Serializable]\r\n";
    //                text += $"    public class {DTOFileName}\r\n";
    //                text += "    {\r\n";
    //                text += "        /// <summary>\r\n";
    //                text += "        /// 键\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += "        public string Key { get; set; }\r\n";
    //                text += "        /// <summary>\r\n";
    //                text += "        /// 值\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += $"        public {CSharpType} Value" + " { get; set; }\r\n";
    //                text += "        /// <summary>\r\n";
    //                text += "        /// 设备唯一标识\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += "        public string[] DeviceNos { get; set; }\r\n";
    //                text += "    }\r\n";
    //                text += "}\r\n";
    //                return text;
    //            }
    //        }

    //        public string IWebStockClientText
    //        {
    //            get
    //            {
    //                string text = "";
    //                text += "        /// <summary>\r\n";
    //                text += $"        /// 处理{Remark}事件\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += "        /// <param name=\"e\">事件</param>\r\n";
    //                text += "        /// <returns></returns>\r\n";
    //                text += $"        void Handle{EventFileName}Message({EventFileName} e);\r\n";
    //                text += "        /// <summary>\r\n";
    //                text += $"        /// 当处理{Remark}事件成功时\r\n";
    //                text += "        /// </summary>\r\n";
    //                text += $"        event HandleMessageEvent<{DTOFileName}> On{EventFileName}MessageSuccess;\r\n";
    //                return text;
    //            }
    //        }
    //        /// <summary>
    //        /// 类型
    //        /// </summary>
    //        public string Type { get; set; }
    //        /// <summary>
    //        /// C#类型
    //        /// </summary>
    //        public string CSharpType
    //        {
    //            get
    //            {
    //                var result = "";
    //                switch (Type)
    //                {
    //                    case "DWORD":
    //                        result = "uint";
    //                        break;
    //                    case "WORD":
    //                        result = "ushort";
    //                        break;
    //                    case "STRING":
    //                        result = "string";
    //                        break;
    //                    default:
    //                        throw new NotImplementedException();
    //                }
    //                return result;
    //            }
    //        }

    //        /// <summary>
    //        /// 值
    //        /// </summary>
    //        public string Value { get; set; }
    //        /// <summary>
    //        /// 备注
    //        /// </summary>
    //        public string Remark { get; set; }
    //    }
}
