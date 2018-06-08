using MateralTools.MLinQ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using MateralTools.MEntityFramework;

namespace MateralTools.MLog
{
    /// <summary>
    /// MateralToolsLog连接
    /// </summary>
    public partial class MSQLiteLogContext : DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public const string SQLConnectionStr = "Data Source=Data/MateralToolsLogDB.db";
        /// <summary>
        /// 日志表
        /// </summary>
        public DbSet<T_ApplicationLog> T_ApplicationLog { get; set; }
        /// <summary>
        /// 异常日志表
        /// </summary>
        public DbSet<T_ApplicationLog_Exception> T_ApplicationLog_Exception { get; set; }
        /// <summary>
        /// 异常日志视图
        /// </summary>
        public DbSet<V_ApplicationLog_Exception> V_ApplicationLog_Exception { get; set; }
        /// <summary>
        /// 配置事件
        /// </summary>
        /// <param name="optionsBuilder">数据库连接配置对象</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SQLConnectionStr);
        }
    }
    /// <summary>
    /// 日志数据访问类
    /// </summary>
    public sealed class MSQLiteLogDAL<TDB> : EFBaseDAL<TDB, T_ApplicationLog> where TDB : MSQLiteLogContext
    {
        /// <summary>
        /// 根据创建时间和类型获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="types">类型</param>
        /// <returns>日志列表信息</returns>
        public List<T_ApplicationLog> GetLogInfoByCreateTimeAndTypes(DateTime start, DateTime end, byte[] types)
        {
            List<T_ApplicationLog> listM = _DB.T_ApplicationLog.Where(m => m.CreateTime >= start && m.CreateTime <= end && types.Contains(m.Types)).ToList();
            return listM;
        }
    }
    /// <summary>
    /// 异常日志数据访问类
    /// </summary>
    public sealed class MSQLiteExceptionLogDAL<TDB> : EFBaseDAL<TDB, T_ApplicationLog_Exception, V_ApplicationLog_Exception> where TDB : MSQLiteLogContext
    {
        /// <summary>
        /// 根据创建时间获得异常日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>异常日志列表信息</returns>
        public List<V_ApplicationLog_Exception> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end)
        {
            List<V_ApplicationLog_Exception> listM = _DB.V_ApplicationLog_Exception.Where(m => m.CreateTime >= start && m.CreateTime <= end).ToList();
            return listM;
        }
    }
}
