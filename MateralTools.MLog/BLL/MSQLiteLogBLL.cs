using System;
using System.Collections.Generic;
using MateralTools.MEntityFramework.Manager;
using MateralTools.MLog.DAL;
using MateralTools.MLog.Model;
using MateralTools.MVerify;

namespace MateralTools.MLog.BLL
{
    /// <summary>
    /// 应用程序日志业务类
    /// </summary>
    public class MSQLiteLogBLL : EfBaseBll<MSQLiteLogDAL<MSQLiteLogContext>, ApplicationLog>, IMLogBLL
    {
        /// <summary>
        /// 应用程序日志数据操作对象
        /// </summary>
        private readonly MSQLiteExceptionLogDAL<MSQLiteLogContext> _applicationLogExceptionDAL = new MSQLiteExceptionLogDAL<MSQLiteLogContext>();
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
                parentID = WriteLog("发生异常", message, ApplicationLogTypeEnum.Exception, parentID);
                _applicationLogExceptionDAL.Insert(new ApplicationLogException
                {
                    LogID = parentID.Value,
                    StackTrace = ex.StackTrace,
                    Types = ex.GetType().Name
                });
                if (fistID == null)
                {
                    fistID = parentID;
                }
                ex = ex.InnerException;
            } while (ex != null);
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
            return WriteLog(title, message, ApplicationLogTypeEnum.Options, parentID);
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        public int WriteDebugLog(string title, string message, int? parentID = null)
        {
            return WriteLog(title, message, ApplicationLogTypeEnum.Debug, parentID);
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
            var logM = new ApplicationLog
            {
                Types = (byte)types,
                CreateTime = DateTime.Now,
                Title = title,
                Message = message,
                ParentID = parentID
            };
            logM = Dal.Insert(logM);
            return logM.ID;
        }
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        public List<ApplicationLog> GetLogInfoByCreateTime(DateTime start, DateTime end)
        {
            byte[] types = {
                (byte)ApplicationLogTypeEnum.Debug,
                (byte)ApplicationLogTypeEnum.Options
            };
            return Dal.GetLogInfoByCreateTimeAndTypes(start, end, types);
        }
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        public List<ApplicationLogExceptionView> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end)
        {
            return _applicationLogExceptionDAL.GetExceptionLogInfoByCreateTime(start, end);
        }
        /// <summary>
        /// 根据唯一标识获得日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>日志信息</returns>
        public ApplicationLog GetLogInfoByID(int id)
        {
            return Dal.GetDBModelInfoByID(id);
        }
        /// <summary>
        /// 根据唯一标识获得异常日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>异常日志信息</returns>
        public ApplicationLogExceptionView GetExceptionLogInfoByID(int id)
        {
            return _applicationLogExceptionDAL.GetDBModelViewInfoByID(id);
        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="model">验证对象</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        protected override bool Verification(ApplicationLog model, out string msg)
        {
            var listMsg = new List<string>();
            if (model.Message.MIsNullOrEmpty())
            {
                listMsg.Add("日志消息不可以为空");
            }
            if (model.Title.MIsNullOrEmpty())
            {
                listMsg.Add("日志标题不可以为空");
            }
            msg = string.Join(",", listMsg);
            return listMsg.Count <= 0;
        }
    }
}
