using MateralTools.MEntityFramework.Manager;
using MateralTools.MLog.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MateralTools.MLog.DAL
{
    /// <inheritdoc />
    /// <summary>
    /// MateralToolsLog连接
    /// </summary>
    public class MSQLiteLogContext : DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public const string SQLConnectionStr = "Data Source=Data/MateralToolsLogDB.db";
        /// <summary>
        /// 日志表
        /// </summary>
        public DbSet<ApplicationLog> ApplicationLog { get; set; }
        /// <summary>
        /// 异常日志表
        /// </summary>
        public DbSet<ApplicationLogException> ApplicationLogException { get; set; }
        /// <summary>
        /// 异常日志视图
        /// </summary>
        public DbSet<ApplicationLogExceptionView> ApplicationLogExceptionView { get; set; }
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
    public sealed class MSQLiteLogDAL<TDB> : EFBaseDAL<TDB, ApplicationLog> where TDB : MSQLiteLogContext
    {
        /// <summary>
        /// 根据创建时间和类型获得日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="types">类型</param>
        /// <returns>日志列表信息</returns>
        public List<ApplicationLog> GetLogInfoByCreateTimeAndTypes(DateTime start, DateTime end, byte[] types)
        {
            var listM = _DB.ApplicationLog.Where(m => m.CreateTime >= start && m.CreateTime <= end && types.Contains(m.Types)).ToList();
            return listM;
        }
    }
    /// <summary>
    /// 异常日志数据访问类
    /// </summary>
    public sealed class MSQLiteExceptionLogDAL<TDB> : EFBaseDAL<TDB, ApplicationLogException, ApplicationLogExceptionView> where TDB : MSQLiteLogContext
    {
        /// <summary>
        /// 根据创建时间获得异常日志信息
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>异常日志列表信息</returns>
        public List<ApplicationLogExceptionView> GetExceptionLogInfoByCreateTime(DateTime start, DateTime end)
        {
            var listM = _DB.ApplicationLogExceptionView.Where(m => m.CreateTime >= start && m.CreateTime <= end).ToList();
            return listM;
        }
    }
}
