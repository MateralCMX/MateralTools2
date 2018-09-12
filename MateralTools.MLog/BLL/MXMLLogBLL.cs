using System;
using System.Collections.Generic;
using MateralTools.MLog.DAL;
using MateralTools.MLog.Model;

namespace MateralTools.MLog.BLL
{
    public class MxmlLogBLL : IMLogBLL
    {
        /// <summary>
        /// 数据处理对象
        /// </summary>
        private readonly MxmlLogDAL _dal;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFilePath"></param>
        public MxmlLogBLL(string xmlFilePath)
        {
            _dal = new MxmlLogDAL(xmlFilePath);
        }
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public int WriteExceptionLog(Exception ex)
        {
            int? parentID = null;
            int? fistID = null;
            do
            {
                var message = ex.Message;
                var logM = new ApplicationLog
                {
                    Types = (byte)ApplicationLogTypeEnum.Exception,
                    CreateTime = DateTime.Now,
                    Title = "发生异常",
                    Message = message,
                    ParentID = parentID
                };
                var exceptionM = new ApplicationLogException
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
            var id = WriteLog(title, message, ApplicationLogTypeEnum.Options, parentID);
            _dal.SaveChange();
            return id;
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        public int WriteDebugLog(string title, string message, int? parentID = null)
        {
            var id = WriteLog(title, message, ApplicationLogTypeEnum.Debug, parentID);
            _dal.SaveChange();
            return id;
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
            ApplicationLog logM = new ApplicationLog
            {
                Types = (byte)types,
                CreateTime = DateTime.Now,
                Title = title,
                Message = message,
                ParentID = parentID
            };
            return _dal.InsertLog(logM);
        }
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        public List<ApplicationLog> GetLogInfoByCreateTime(DateTime start, DateTime end)
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
        public List<ApplicationLogExceptionView> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end)
        {
            //return _applicationLogExceptionDAL.GetExceptionLogInfoByCreateTime(start, end);
            return null;
        }
        /// <summary>
        /// 根据唯一标识获得日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>日志信息</returns>
        public ApplicationLog GetLogInfoByID(int id)
        {
            //return _dal.GetDBModelInfoByID(id);
            return null;
        }
        /// <summary>
        /// 根据唯一标识获得异常日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>异常日志信息</returns>
        public ApplicationLogExceptionView GetExceptionLogInfoByID(int id)
        {
            //return _applicationLogExceptionDAL.GetDBModelViewInfoByID(id);
            return null;
        }
    }
}
