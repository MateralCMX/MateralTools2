﻿using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;
using MateralTools.MLinQ.Manager;
using MateralTools.MResult.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MateralTools.MEntityFramework.Manager
{
    /// <summary>
    /// 数据操作类父类
    /// </summary>
    /// <typeparam name="TDB">数据连接对象</typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="VModel"></typeparam>
    public class EFBaseDAL<TDB, TModel, VModel> : EFBaseDAL<TDB, TModel> where TDB : DbContext where TModel : class where VModel : class
    {
        /// <summary>
        /// 根据条件获得数据库视图信息
        /// </summary>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IQueryable<VModel> GetDBModelViewInfoByWhere(params FilterInfo<VModel>[] filters)
        {
            DbSet<VModel> dbSet = GetDBViewSet();
            IQueryable<VModel> listM = dbSet.Where(filters);
            return listM;
        }
        /// <summary>
        /// 根据条件获得数据库视图信息
        /// </summary>
        /// <param name="pageM">分页对象</param>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IQueryable<VModel> GetDBModelViewPageInfoByWhere(MPageModel pageM, params FilterInfo<VModel>[] filters)
        {
            IQueryable<VModel> listM = GetDBModelViewInfoByWhere(filters);
            pageM.DataCount = listM.Count();
            listM = listM.Paging(pageM.PageIndex, pageM.PageSize);
            return listM;
        }
        /// <summary>
        /// 根据条件获得数据库信息(异步)
        /// </summary>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IAsyncEnumerable<VModel> GetDBModelViewInfoByWhereAsync(params FilterInfo<VModel>[] filters)
        {
            DbSet<VModel> dbSet = GetDBViewSet();
            IQueryable<VModel> listM = dbSet.Where(filters);
            return listM.ToAsyncEnumerable();
        }
        /// <summary>
        /// 根据条件获得数据库视图分页信息(异步)
        /// </summary>
        /// <param name="pageM">分页对象</param>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual async Task<IAsyncEnumerable<VModel>> GetDBModelViewPageInfoByWhereAsync(MPageModel pageM, params FilterInfo<VModel>[] filters)
        {
            IAsyncEnumerable<VModel> listM = GetDBModelViewInfoByWhereAsync(filters);
            pageM.DataCount = await listM.Count();
            listM = listM.Paging(pageM.PageIndex, pageM.PageSize);
            return listM;
        }
        /// <summary>
        /// 根据主键获得数据模型对象信息
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>模型对象信息</returns>
        public virtual VModel GetDBModelViewInfoByID(object id)
        {
            IEnumerable<VModel> where = GetModeWhereByID(GetDBViewSet(), id);
            return where.FirstOrDefault();
        }
        /// <summary>
        /// 根据主键获得数据模型对象信息(异步)
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>模型对象信息</returns>
        public virtual async Task<VModel> GetDBModelViewInfoByIDAsync(object id)
        {
            IEnumerable<VModel> where = GetModeWhereByID(GetDBViewSet(), id);
            return await where.ToAsyncEnumerable().FirstOrDefault();
        }
        /// <summary>
        /// 获得数据模型属性信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MEntityFrameworkException"></exception>
        private DbSet<VModel> GetDBViewSet()
        {
            PropertyInfo dbPro = _DB.GetType().GetProperty(typeof(VModel).Name);
            if (dbPro != null)
            {
                DbSet<VModel> dbSet = dbPro.GetValue(_DB) as DbSet<VModel>;
                return dbSet;
            }
            else
            {
                throw new MEntityFrameworkException($"类型{typeof(VModel).Name}错误。");
            }
        }
    }
    /// <summary>
    /// 数据操作类父类
    /// </summary>
    /// <typeparam name="TDB">数据连接对象</typeparam>
    /// <typeparam name="TModel">要操作的主要类型</typeparam>
    public class EFBaseDAL<TDB, TModel> : EFBaseDAL<TDB> where TDB : DbContext where TModel : class
    {
        /// <summary>
        /// 根据主键获得数据模型对象信息
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>模型对象信息</returns>
        public virtual TModel GetDBModelInfoByID(object id)
        {
            IEnumerable<TModel> where = GetModeWhereByID(GetDBSet(), id);
            return where.FirstOrDefault();
        }
        /// <summary>
        /// 根据主键获得数据模型对象信息(异步)
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>模型对象信息</returns>
        public virtual async Task<TModel> GetDBModelInfoByIDAsync(object id)
        {
            IEnumerable<TModel> where = GetModeWhereByID(GetDBSet(), id);
            return await where.ToAsyncEnumerable().FirstOrDefault();
        }
        /// <summary>
        /// 根据条件获得数据库信息
        /// </summary>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IQueryable<TModel> GetDBModelInfoByWhere(params FilterInfo<TModel>[] filters)
        {
            DbSet<TModel> dbSet = GetDBSet();
            IQueryable<TModel> listM = dbSet.Where(filters);
            return listM;
        }
        /// <summary>
        /// 根据条件获得数据库分页信息
        /// </summary>
        /// <param name="pageM">分页对象</param>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IQueryable<TModel> GetDBModelPageInfoByWhere(MPageModel pageM, params FilterInfo<TModel>[] filters)
        {
            IQueryable<TModel> listM = GetDBModelInfoByWhere(filters);
            pageM.DataCount = listM.Count();
            listM = listM.Paging(pageM.PageIndex, pageM.PageSize);
            return listM;
        }
        /// <summary>
        /// 根据条件获得数据库信息(异步)
        /// </summary>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual IAsyncEnumerable<TModel> GetDBModelInfoByWhereAsync(params FilterInfo<TModel>[] filters)
        {
            DbSet<TModel> dbSet = GetDBSet();
            IQueryable<TModel> listM = dbSet.Where(filters);
            return listM.ToAsyncEnumerable();
        }
        /// <summary>
        /// 根据条件获得数据库分页信息(异步)
        /// </summary>
        /// <param name="pageM">分页对象</param>
        /// <param name="filters">条件</param>
        /// <returns>数据库信息</returns>
        public virtual async Task<IAsyncEnumerable<TModel>> GetDBModelPageInfoByWhereAsync(MPageModel pageM, params FilterInfo<TModel>[] filters)
        {
            IAsyncEnumerable<TModel> listM = GetDBModelInfoByWhereAsync(filters);
            pageM.DataCount = await listM.Count();
            listM = listM.Paging(pageM.PageIndex, pageM.PageSize);
            return listM;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">要添加的对象</param>
        public virtual TModel Insert(TModel model)
        {
            model = GetBeforeInsertModel(model);
            DbSet<TModel> dbSet = GetDBSet();
            dbSet.Add(model);
            _DB.SaveChanges();
            return model;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">要删除的模型主键或者模型对象</param>
        /// <exception cref="MEntityFrameworkException"></exception>
        public virtual void Delete(object id)
        {
            TModel model;
            if (id.GetType().GUID == typeof(TModel).GUID)
            {
                model = (TModel)id;
            }
            else
            {
                model = GetDBModelInfoByID(id);
                if (model == default(TModel))
                {
                    throw new MEntityFrameworkException($"参数{nameof(id)}错误。");
                }
            }
            DbSet<TModel> dbSet = GetDBSet();
            dbSet.Remove(model);
            _DB.SaveChanges();
        }
        /// <summary>
        /// 获得数据模型属性信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MEntityFrameworkException"></exception>
        private DbSet<TModel> GetDBSet()
        {
            PropertyInfo dbPro = _DB.GetType().GetProperty(typeof(TModel).Name);
            if (dbPro != null)
            {
                DbSet<TModel> dbSet = dbPro.GetValue(_DB) as DbSet<TModel>;
                return dbSet;
            }
            else
            {
                throw new MEntityFrameworkException($"类型{typeof(TModel).Name}错误。");
            }
        }
    }
    /// <summary>
    /// 数据操作类父类
    /// </summary>
    /// <typeparam name="TDB">数据连接对象</typeparam>
    public class EFBaseDAL<TDB>:EFBaseDAL where TDB : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EFBaseDAL()
        {
            _DB = _DB.MGetDefultObject<TDB>();
        }
        /// <summary>
        /// 数据连接对象
        /// </summary>
        protected readonly TDB _DB;
        /// <summary>
        /// 保存更改
        /// </summary>
        public void SaveChange()
        {
            _DB.SaveChanges();
        }
    }
    /// <summary>
    /// 数据操作类父类
    /// </summary>
    public class EFBaseDAL
    {
        /// <summary>
        /// 添加之前
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyPropertyInfo">主键属性</param>
        /// <returns></returns>
        /// <exception cref="MEntityFrameworkException"></exception>
        protected T GetBeforeInsertModel<T>(T model, PropertyInfo keyPropertyInfo)
        {
            Type tType = typeof(T);
            ConstructorInfo ci = tType.GetConstructor(new Type[0]);
            if (ci != null)
            {
                T addModel = (T)ci.Invoke(new object[0]);
                model.MCopyProperties(addModel);
                PropertyInfo[] pis = tType.GetProperties();
                if (keyPropertyInfo != null)
                {
                    Guid piGuid = keyPropertyInfo.PropertyType.GUID;
                    if (piGuid == typeof(Guid).GUID)
                    {
                        if (((Guid)keyPropertyInfo.GetValue(addModel)) == Guid.Empty)
                        {
                            keyPropertyInfo.SetValue(addModel, Guid.NewGuid());
                        }
                    }
                }
                return addModel;
            }
            else
            {
                throw new MEntityFrameworkException($"类型{typeof(T).Name}不支持该方法。");
            }
        }
        /// <summary>
        /// 添加之前
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MEntityFrameworkException"></exception>
        protected T GetBeforeInsertModel<T>(T model)
        {
            PropertyInfo pi = GetKeyPropertyInfo<T>();
            if (pi != null)
            {
                return GetBeforeInsertModel(model, pi);
            }
            else
            {
                throw new MEntityFrameworkException($"类型{typeof(T).Name}不能找到主键属性，请为主键属性添加Key特性。");
            }
        }
        /// <summary>
        /// 获得主键属性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>类型</returns>
        public static PropertyInfo GetKeyPropertyInfo<T>()
        {
            Type tType = typeof(T);
            PropertyInfo[] pis = tType.GetProperties();
            KeyAttribute ka = null;
            PropertyInfo pi = null;
            foreach (PropertyInfo item in pis)
            {
                ka = item.GetCustomAttribute<KeyAttribute>();
                if (ka != null)
                {
                    pi = item;
                    break;
                }
            }
            return pi;
        }
        /// <summary>
        /// 根据主键获取数据库模型查询条件
        /// </summary>
        /// <param name="dbSet">数据源</param>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        /// <exception cref="MEntityFrameworkException"></exception>
        protected static IEnumerable<T> GetModeWhereByID<T>(DbSet<T> dbSet, object id) where T: class
        {
            PropertyInfo modelPro = GetKeyPropertyInfo<T>();
            if (modelPro != null)
            {
                Expression<Func<T, bool>> expression;
                Type proType = modelPro.PropertyType;
                if (proType.GUID == typeof(Int32).GUID)
                {
                    int ID = Convert.ToInt32(id);
                    expression = m => (int)modelPro.GetValue(m) == ID;
                }
                else if (proType.GUID == typeof(Int64).GUID)
                {
                    long ID = Convert.ToInt64(id);
                    expression = m => (long)modelPro.GetValue(m) == ID;
                }
                else if (proType.GUID == typeof(Guid).GUID)
                {
                    Guid ID = (Guid)id;
                    expression = m => (Guid)modelPro.GetValue(m) == ID;
                }
                else if (proType.GUID == typeof(string).GUID)
                {
                    string ID = (string)id;
                    expression = m => (string)modelPro.GetValue(m) == ID;
                }
                else
                {
                    throw new MEntityFrameworkException($"该方法不支持类型{typeof(T).Name}");
                }
                return dbSet.Where(expression.Compile());
            }
            else
            {
                throw new MEntityFrameworkException($"类型{typeof(T).Name}不能找到主键属性，请为主键属性添加Key特性。");
            }
        }
    }
}
