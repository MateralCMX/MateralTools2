using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLog
{
    interface IMLogBLL
    {
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        int WriteExceptionLog(Exception ex);
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        int WriteOptionsLog(string title, string message, int? parentID = null);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="parentID">父级ID</param>
        int WriteDebugLog(string title, string message, int? parentID = null);
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        List<T_ApplicationLog> GetLogInfoByCreateTime(DateTime start, DateTime end);
        /// <summary>
        /// 根据时间获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>日志信息</returns>
        List<V_ApplicationLog_Exception> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end);
        /// <summary>
        /// 根据唯一标识获得日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>日志信息</returns>
        T_ApplicationLog GetLogInfoByID(int id);
        /// <summary>
        /// 根据唯一标识获得异常日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>异常日志信息</returns>
        V_ApplicationLog_Exception GetExceptionLogInfoByID(int id);
    }
}
