using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Materal.UI
{
    /// <summary>
    /// 注释XML文件合并
    /// </summary>
    public class NotesXMLFileMergeManager
    {
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        /// <param name="xmlPaths">xml文件路径</param>
        public static void Merge(string mainXMLPath, string[] secondaryXMLPaths)
        {
            XmlDocument mainXML = new XmlDocument();
            mainXML.Load(mainXMLPath);
            XmlDocument secondaryXML;
            foreach (string secondaryXMLPath in secondaryXMLPaths)
            {
                secondaryXML = new XmlDocument();
                secondaryXML.Load(secondaryXMLPath);
                XMLMerge(mainXML, secondaryXML);
            }
            mainXML.Save(mainXMLPath);
        }

        private static void XMLMerge(XmlDocument mainXML, XmlDocument secondaryXML)
        {
            mainXML.LastChild.LastChild.InnerXml = mainXML.LastChild.LastChild.InnerXml + secondaryXML.LastChild.LastChild.InnerXml;
        }
    }
}
