using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MateralTools.MWeChat
{
    /// <summary>
    /// 微信Http管理器
    /// </summary>
    public class WeChatHttpManager
    {
        /// <summary>
        /// 确认返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   
            return true;//不验证直接通过验证
        }
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="url">url地址</param>
        /// <param name="isUseCert">是否使用证书</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="config">微信支付配置对象</param>
        /// <returns>请求返回数据</returns>
        public static string Post(string xml, string url, bool isUseCert, int timeout, WeChatConfigModel config)
        {
            GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.ContentType = "text/xml";
                byte[] data = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;
                if (isUseCert)
                {
                    string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    X509Certificate2 cert = new X509Certificate2(path + config.SSLCERT_PATH, config.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                }
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (WebException e)
            {
                throw new MWeChatException(e.ToString());
            }
            catch (Exception e)
            {
                throw new MWeChatException(e.ToString());
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (WebException e)
            {
                throw new MWeChatException(e.ToString());
            }
            catch (Exception e)
            {
                throw new MWeChatException(e.ToString());
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
    }
}