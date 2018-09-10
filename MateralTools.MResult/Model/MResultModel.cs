using System.ComponentModel;
using MateralTools.MEnum;
using MateralTools.MConvert;
using MateralTools.Base;

namespace MateralTools.MResult
{
    /// <summary>
    /// 返回对象类型
    /// </summary>
    public enum MResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 1
    }
    /// <summary>
    /// 返回对象模型
    /// </summary>
    public class DataResult
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public MResultType ResultType { get; set; }
        /// <summary>
        /// 对象类型文本
        /// </summary>
        public string ResultTypeStr
        {
            get
            {
                return ResultType.MGetDescription();
            }
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataResult()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public DataResult(MResultType resultType, string message = "")
        {
            ResultType = resultType;
            Message = message;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static DataResult Success(string message = "")
        {
            return new DataResult(MResultType.Success, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static DataResult Fail(string message = "")
        {
            return new DataResult(MResultType.Fail, message);
        }
        /// <summary>
        /// 返回Json字符串
        /// </summary>
        /// <returns>对象Json字符串</returns>
        public string GetJsonStr()
        {
            return this.MToJson();
        }
    }
    /// <summary>
    /// 携带数据的返回对象模型
    /// </summary>
    /// <typeparam name="T">保存数据类型</typeparam>
    public class DataResult<T> : DataResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataResult():base()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        public DataResult(MResultType resultType, T data, string message = "") : base(resultType, message)
        {
            Data = data;
        }
        /// <summary>
        /// 携带数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static DataResult<T> Success(T data, string message = "")
        {
            return new DataResult<T>(MResultType.Success, data, message);
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public new static DataResult<T> Success(string message = "")
        {
            return new DataResult<T>(MResultType.Success, default(T), message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static DataResult<T> Fail(T data = default(T), string message = "")
        {
            return new DataResult<T>(MResultType.Fail, data, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public new static DataResult<T> Fail(string message = "")
        {
            return new DataResult<T>(MResultType.Fail, default(T), message);
        }
    }
    /// <summary>
    /// 携带分页数据的返回对象
    /// </summary>
    /// <typeparam name="T">保存数据类型</typeparam>
    public class PagedResult<T> : DataResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedResult() : base()
        {
        }
        /// <summary>
        /// 分页信息
        /// </summary>
        public MPageModel PageInfo { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="resultType">返回对象类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        public PagedResult(MResultType resultType, T data, MPageModel pagingM, string message = "") : base(resultType, data, message)
        {
            PageInfo = pagingM;
        }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static PagedResult<T> Success(T data, int dataCount, string message = "")
        {
            return new PagedResult<T>(MResultType.Success, data, new MPageModel(1, 1, dataCount), message);
        }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static PagedResult<T> Success(T data, MPageModel pagingM, string message = "")
        {
            return new PagedResult<T>(MResultType.Success, data, pagingM, message);
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static PagedResult<T> Success(MPageData<T> pagingM, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new PagedResult<T>(MResultType.Success, pagingM.Data, pagingM.PageInfo, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static PagedResult<T> Fail(T data = default(T), MPageModel pagingM = null, string message = "")
        {
            return new PagedResult<T>(MResultType.Fail, data, pagingM, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static PagedResult<T> Fail(MPageData<T> pagingM = null, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new PagedResult<T>(MResultType.Fail, pagingM.Data, pagingM.PageInfo, message);
        }
        /// <summary>
        /// 获得一个错误返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>错误返回对象</returns>
        public static PagedResult<T> Error(MPageData<T> pagingM = null, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new PagedResult<T>(MResultType.Fail, pagingM.Data, pagingM.PageInfo, message);
        }
    }
}
