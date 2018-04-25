using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MateralTools.MSystemInfo
{
    /// <summary>
    /// 系统信息管理器
    /// </summary>
    public class SystemInfoManager
    {
        /// <summary>
        /// 获得本机IPV4地址
        /// 可能有多个
        /// </summary>
        /// <returns>IPV4地址组</returns>
        public static List<string> GetLocalIPv4()
        {
            List<string> resM = new List<string>();
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ip in ipadrlist)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    resM.Add(ip.ToString());
                }
            }
            return resM;
        }
    }
}
