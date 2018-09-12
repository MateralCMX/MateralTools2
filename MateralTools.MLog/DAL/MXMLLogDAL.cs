using System;
using System.Globalization;
using System.Xml;
using MateralTools.MLog.Model;

namespace MateralTools.MLog.DAL
{
    public class MxmlLogDAL
    {
        /// <summary>
        /// XML文件地址
        /// </summary>
        private readonly string _xmlFilePath;
        /// <summary>
        /// XML文件对象
        /// </summary>
        private XmlDocument _xmlDoc;
        /// <summary>
        /// Root上最后ID的名称
        /// </summary>
        private const string LastIDName = "LastID";
        /// <summary>
        /// XML文件地址
        /// </summary>
        /// <param name="xmlFilePath"></param>
        public MxmlLogDAL(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
            BindXmlDocument();
        }
        /// <summary>
        /// 绑定XMLDoc
        /// </summary>
        private void BindXmlDocument()
        {
            _xmlDoc = new XmlDocument();
            if (System.IO.File.Exists(_xmlFilePath))
            {
                _xmlDoc.Load(_xmlFilePath);
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
        /// 构造方法
        /// </summary>
        public MxmlLogDAL()
        {
            BindXmlDocument();
        }

        /// <summary>
        /// 插入一个日志
        /// </summary>
        /// <param name="model">日志对象</param>
        public int InsertLog(ApplicationLog model)
        {
            if (model.Types != (byte)ApplicationLogTypeEnum.Exception)
            {
                XmlElement logXml = GetLogNode(model);
                _xmlDoc.LastChild.AppendChild(logXml);
                if (_xmlDoc.LastChild.Attributes != null)
                    _xmlDoc.LastChild.Attributes[LastIDName].Value = logXml.Attributes[nameof(ApplicationLog.ID)].Value;
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
        public int InsertExceptionLog(ApplicationLog model, ApplicationLogException exceptionModel)
        {
            if (model.Types == (byte)ApplicationLogTypeEnum.Exception)
            {
                exceptionModel.LogID = model.ID;
                XmlElement logXml = GetLogNode(model);
                XmlElement exceptionXml = _xmlDoc.CreateElement("Exception");
                exceptionXml.SetAttribute(nameof(ApplicationLogException.StackTrace), exceptionModel.StackTrace);
                exceptionXml.SetAttribute(nameof(ApplicationLogException.Types), exceptionModel.Types);
                logXml.AppendChild(exceptionXml);
                _xmlDoc.LastChild.AppendChild(logXml);
                if (_xmlDoc.LastChild.Attributes != null)
                    _xmlDoc.LastChild.Attributes[LastIDName].Value = logXml.Attributes[nameof(ApplicationLog.ID)].Value;
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
        private XmlElement GetLogNode(ApplicationLog model)
        {
            XmlElement xmlNode = _xmlDoc.CreateElement("ApplicationLog");
            int lastID = GetLastID();
            model.ID = ++lastID;
            xmlNode.SetAttribute(nameof(ApplicationLog.ID), model.ID.ToString());
            xmlNode.SetAttribute(nameof(ApplicationLog.Title), model.Title);
            xmlNode.SetAttribute(nameof(ApplicationLog.Message), model.Message);
            xmlNode.SetAttribute(nameof(ApplicationLog.CreateTime), model.CreateTime.ToString(CultureInfo.InvariantCulture));
            xmlNode.SetAttribute(nameof(ApplicationLog.Types), model.Types.ToString());
            xmlNode.SetAttribute(nameof(ApplicationLog.ParentID), model.ParentID == null ? "Null" : model.ParentID.Value.ToString());
            return xmlNode;
        }
        /// <summary>
        /// 获得最后一个ID
        /// </summary>
        /// <returns></returns>
        private int GetLastID()
        {
            if (_xmlDoc.LastChild.Attributes == null) return 0;
            var lastID = _xmlDoc.LastChild.Attributes["LastID"].Value;
            return Convert.ToInt32(lastID);
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        public void SaveChange()
        {
            _xmlDoc.Save(_xmlFilePath);
        }
    }
}
