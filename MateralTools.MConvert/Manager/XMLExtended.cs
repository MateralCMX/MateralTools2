using System.Xml;
using Newtonsoft.Json;

namespace MateralTools.MConvert.Manager
{
    /// <summary>
    /// XML扩展
    /// </summary>
    public static class XmlExtended
    {
        /// <summary>
        /// XML文档对象转换为Json字符串
        /// </summary>
        /// <param name="xmlDoc">XML文档对象</param>
        /// <returns>Json字符串</returns>
        public static string MToJson(this XmlDocument xmlDoc)
        {
            return JsonConvert.SerializeXmlNode(xmlDoc);
        }
        /// <summary>
        /// XML节点对象转换为Json字符串
        /// </summary>
        /// <param name="xmlNode">XML节点对象</param>
        /// <returns>Json字符串</returns>
        public static string MToJson(this XmlNode xmlNode)
        {
            return JsonConvert.SerializeXmlNode(xmlNode);
        }
    }
}
