using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MateralTools.MLog
{
    public class MXMLLogDAL
    {
        /// <summary>
        /// XML文件地址
        /// </summary>
        private const string XMLFilePath = "Data/MateralToolsLogDB.xml";
        /// <summary>
        /// XML文件对象
        /// </summary>
        private XmlDocument _xmlDoc = null;
        /// <summary>
        /// Root上最后ID的名称
        /// </summary>
        private const string LastIDName = "LastID";
        /// <summary>
        /// 构造方法
        /// </summary>
        public MXMLLogDAL()
        {
            _xmlDoc = new XmlDocument();
            if (System.IO.File.Exists(XMLFilePath))
            {
                _xmlDoc.Load(XMLFilePath);
            }
            else
            {
                XmlDeclaration dec = _xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                _xmlDoc.AppendChild(dec);
                XmlElement root = _xmlDoc.CreateElement("Root");
                root.SetAttribute(LastIDName, "0");
                _xmlDoc.AppendChild(root);
                SaveChange();
            }
        }
        /// <summary>
        /// 插入一个日志
        /// </summary>
        /// <param name="model">日志对象</param>
        public int InsertLog(T_ApplicationLog model)
        {
            if (model.Types != (byte)ApplicationLogTypeEnum.Exception)
            {
                XmlElement logXml = GetLogNode(model);
                _xmlDoc.LastChild.AppendChild(logXml);
                _xmlDoc.LastChild.Attributes[LastIDName].Value = logXml.Attributes[nameof(T_ApplicationLog.ID)].Value;
                return model.ID;
            }
            else
            {
                throw new ArgumentException("缺少异常日志对象");
            }
        }
        /// <summary>
        /// 插入一个异常日志
        /// </summary>
        /// <param name="model">日志对象</param>
        /// <param name="exceptionModel">异常日志对象</param>
        public int InsertExceptionLog(T_ApplicationLog model, T_ApplicationLog_Exception exceptionModel)
        {
            if (model.Types == (byte)ApplicationLogTypeEnum.Exception)
            {
                exceptionModel.FK_Log_ID = model.ID;
                XmlElement logXml = GetLogNode(model);
                XmlElement exceptionXml = _xmlDoc.CreateElement("Exception");
                exceptionXml.SetAttribute(nameof(T_ApplicationLog_Exception.StackTrace), exceptionModel.StackTrace);
                exceptionXml.SetAttribute(nameof(T_ApplicationLog_Exception.Types), exceptionModel.Types);
                logXml.AppendChild(exceptionXml);
                _xmlDoc.LastChild.AppendChild(logXml);
                _xmlDoc.LastChild.Attributes[LastIDName].Value = logXml.Attributes[nameof(T_ApplicationLog.ID)].Value;
                return model.ID;
            }
            else
            {
                throw new ArgumentException("该日志不是异常日志");
            }
        }
        /// <summary>
        /// 获得日志XML
        /// </summary>
        /// <param name="model">Log模型</param>
        /// <returns>日志XML</returns>
        private XmlElement GetLogNode(T_ApplicationLog model)
        {
            XmlElement xmlNode = _xmlDoc.CreateElement("ApplicationLog");
            int LastID = GetLastID();
            model.ID = ++LastID;
            xmlNode.SetAttribute(nameof(T_ApplicationLog.ID), model.ID.ToString());
            xmlNode.SetAttribute(nameof(T_ApplicationLog.Title), model.Title);
            xmlNode.SetAttribute(nameof(T_ApplicationLog.Message), model.Message);
            xmlNode.SetAttribute(nameof(T_ApplicationLog.CreateTime), model.CreateTime.ToString());
            xmlNode.SetAttribute(nameof(T_ApplicationLog.Types), model.Types.ToString());
            xmlNode.SetAttribute(nameof(T_ApplicationLog.FK_Parent_ID), model.FK_Parent_ID == null ? "Null" : model.FK_Parent_ID.Value.ToString());
            return xmlNode;
        }
        /// <summary>
        /// 获得最后一个ID
        /// </summary>
        /// <returns></returns>
        private int GetLastID()
        {
            string lastID = _xmlDoc.LastChild.Attributes["LastID"].Value;
            return Convert.ToInt32(lastID);
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        public void SaveChange()
        {
            _xmlDoc.Save(XMLFilePath);
        }
    }
}
