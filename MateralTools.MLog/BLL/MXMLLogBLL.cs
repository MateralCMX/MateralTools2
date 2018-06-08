using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLog
{
    public class MXMLLogBLL : IMLogBLL
    {
        /// <summary>
        /// 数据处理对象
        /// </summary>
        private readonly MXMLLogDAL _dal = new MXMLLogDAL();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="XMLFilePath"></param>
        public MXMLLogBLL(string XMLFilePath)
        {
            _dal = new MXMLLogDAL(XMLFilePath);
        }
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public int WriteExceptionLog(Exception ex)
        {
            T_ApplicationLog logM;
            T_ApplicationLog_Exception exceptionM;
            string message = string.Empty;
            int? parentID = null;
            int? fistID = null;
            do
            {
                message = ex.Message;
                logM = new T_ApplicationLog
                {
                    Types = (byte)ApplicationLogTypeEnum.Exception,
                    CreateTime = DateTime.Now,
                    Title = "发生异常",
                    Message = message,
                    FK_Parent_ID = parentID
                };
                exceptionM = new T_ApplicationLog_Exception
                {
                    StackTrace = ex.StackTrace,
                    Types = ex.GetType().Name
                };
                parentID = _dal.InsertExceptionLog(logM, exceptionM);
                if (fistID == null)
                {
                    fistID = parentID;
                }
                ex = ex.InnerException;
            } while (ex != null);
            _dal.SaveChange();
            return fistID.Value;
        }
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        public int WriteOptionsLog(string title, string message, int? parentID = null)
        {
            int ID = WriteLog(title, message, ApplicationLogTypeEnum.Options, parentID);
            _dal.SaveChange();
            return ID;
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        public int WriteDebugLog(string title, string message, int? parentID = null)
        {
            int ID = WriteLog(title, message, ApplicationLogTypeEnum.Debug, parentID);
            _dal.SaveChange();
            return ID;
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="types">类型</param>
        /// <param name="parentID">父级ID</param>
        private int WriteLog(string title, string message, ApplicationLogTypeEnum types, int? parentID = null)
        {
            T_ApplicationLog logM = new T_ApplicationLog
            {
                Types = (byte)types,
                CreateTime = DateTime.Now,
                Title = title,
                Message = message,
                FK_Parent_ID = parentID
            };
            return _dal.InsertLog(logM);
        }
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        public List<T_ApplicationLog> GetLogInfoByCreateTime(DateTime start, DateTime end)
        {
            //byte[] types = {
            //    (byte)ApplicationLogTypeEnum.Debug,
            //    (byte)ApplicationLogTypeEnum.Options
            //};
            //return _dal.GetLogInfoByCreateTimeAndTypes(start, end, types);
            return null;
        }
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        public List<V_ApplicationLog_Exception> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end)
        {
            //return _applicationLogExceptionDAL.GetExceptionLogInfoByCreateTime(start, end);
            return null;
        }
        /// <summary>
        /// 根据唯一标识获得日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>日志信息</returns>
        public T_ApplicationLog GetLogInfoByID(int id)
        {
            //return _dal.GetDBModelInfoByID(id);
            return null;
        }
        /// <summary>
        /// 根据唯一标识获得异常日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>异常日志信息</returns>
        public V_ApplicationLog_Exception GetExceptionLogInfoByID(int id)
        {
            //return _applicationLogExceptionDAL.GetDBModelViewInfoByID(id);
            return null;
        }
    }
}
