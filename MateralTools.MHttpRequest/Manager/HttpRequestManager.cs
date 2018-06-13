using MateralTools.MConvert;
using MateralTools.MVerify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MHttpRequest
{
    /// <summary>
    /// HTTP请求管理器
    /// </summary>
    public class HttpRequestManager
    {
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回值</returns>
        public static string SendGet(string url, Dictionary<string, string> data = null, int timeout = 100)
        {
            if (!url.MIsNullOrEmpty())
            {
                url = SpliceURLParams(url, data);
                string resutlStr = string.Empty;
                using (HttpClient client = GetHttpClient(timeout))
                {
                    Task<byte[]> result = client.GetByteArrayAsync(url);
                    byte[] resultBytes = result.Result;
                    resutlStr = Encoding.UTF8.GetString(resultBytes);
                }
                return resutlStr;
            }
            else
            {
                throw new MHttpRequestException($"参数{nameof(url)}不能为空");
            }
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <param name="headers">Http头</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回值</returns>
        public static T SendGet<T>(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, int timeout = 100)
        {
            string resutlStr = SendGet(url, data, timeout);
            T model = resutlStr.MJsonToObject<T>();
            return model;
        }
        /// <summary>
        /// 获得HttpContent
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        private static HttpContent GetHttpContent(Dictionary<string, string> data, HttpContentTypeEnum contentType)
        {
            HttpContent content = null;
            switch (contentType)
            {
                case HttpContentTypeEnum.ApplicationJson:
                    string dataJson = data.MToJson();
                    var formDataBytes = dataJson == null ? new byte[0] : Encoding.UTF8.GetBytes(dataJson);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(formDataBytes, 0, formDataBytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);
                        content = new StreamContent(ms);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    }
                    break;
            }
            return content;
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <param name="contentType">Content-Type</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回值</returns>
        public static string SendPost(string url, Dictionary<string, string> data = null, HttpContentTypeEnum contentType = HttpContentTypeEnum.ApplicationJson, int timeout = 100)
        {
            if (!url.MIsNullOrEmpty())
            {
                string resutlStr = string.Empty;
                using (HttpClient client = GetHttpClient(timeout))
                {
                    using (HttpContent content = GetHttpContent(data, contentType))
                    {
                        using (HttpResponseMessage responseMessage = client.PostAsync(url, content).Result)
                        {
                            Byte[] resultBytes = responseMessage.Content.ReadAsByteArrayAsync().Result;
                            resutlStr = Encoding.UTF8.GetString(resultBytes);
                        }
                    }
                    return resutlStr;
                }
            }
            else
            {
                throw new MHttpRequestException($"参数{nameof(url)}不能为空");
            }
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <param name="contentType">Content-Type</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回值</returns>
        public static T SendPost<T>(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, HttpContentTypeEnum contentType = HttpContentTypeEnum.ApplicationJson, int timeout = 100)
        {
            string resutlStr = SendPost(url, data, contentType, timeout);
            T model = resutlStr.MJsonToObject<T>();
            return model;
        }
        /// <summary>
        /// 拼接URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <returns>带参数的Url地址</returns>
        private static string SpliceURLParams(string url, Dictionary<string, string> data)
        {
            if (!url.MIsNullOrEmpty() && data != null)
            {
                List<string> urlParamsStrs = new List<string>();
                foreach (KeyValuePair<string, string> param in data)
                {
                    urlParamsStrs.Add($"{param.Key}={param.Value}");
                }
                url += $"?{string.Join("&", urlParamsStrs)}";
            }
            return url;
        }
        /// <summary>
        /// 获得Http客户端
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>Http客户端</returns>
        private static HttpClient GetHttpClient(int timeout)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.Timeout = new TimeSpan(0, 0, timeout);
            return client;
        }
    }
}
