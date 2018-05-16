using System.ComponentModel;
using MateralTools.MEnum;
using MateralTools.MConvert;

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
        Fail = 1,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 2
    }
    /// <summary>
    /// 返回对象模型
    /// </summary>
    public class MResultModel
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public MResultType ResultType { get; private set; }
        /// <summary>
        /// 对象类型文本
        /// </summary>
        public string ResultTypeStr
        {
            get
            {
                return ResultType.GetDescription();
            }
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public MResultModel(MResultType resultType, string message = "")
        {
            ResultType = resultType;
            Message = message;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static MResultModel GetSuccessResultM(string message = "")
        {
            return new MResultModel(MResultType.Success, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static MResultModel GetFailResultM(string message = "")
        {
            return new MResultModel(MResultType.Fail, message);
        }
        /// <summary>
        /// 获得一个错误返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>错误返回对象</returns>
        public static MResultModel GetErrorResultM(string message = "")
        {
            return new MResultModel(MResultType.Error, message);
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
    public class MResultModel<T> : MResultModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        public MResultModel(MResultType resultType, T data, string message = "") : base(resultType, message)
        {
            Data = data;
        }
        /// <summary>
        /// 携带数据
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static MResultModel<T> GetSuccessResultM(T data, string message = "")
        {
            return new MResultModel<T>(MResultType.Success, data, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static MResultModel<T> GetFailResultM(T data = default(T), string message = "")
        {
            return new MResultModel<T>(MResultType.Fail, data, message);
        }
        /// <summary>
        /// 获得一个错误返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>错误返回对象</returns>
        public static MResultModel<T> GetErrorResultM(T data = default(T), string message = "")
        {
            return new MResultModel<T>(MResultType.Error, data, message);
        }
    }
    /// <summary>
    /// 携带分页数据的返回对象
    /// </summary>
    /// <typeparam name="T">保存数据类型</typeparam>
    public class MResultPageModel<T> : MResultModel<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public MPageModel PageInfo { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="resultType">返回对象类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        public MResultPageModel(MResultType resultType, T data, MPageModel pagingM, string message = "") : base(resultType, data, message)
        {
            PageInfo = pagingM;
        }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static MResultPageModel<T> GetSuccessResultM(T data, MPageModel pagingM, string message = "")
        {
            return new MResultPageModel<T>(MResultType.Success, data, pagingM, message);
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static MResultPageModel<T> GetSuccessResultM(MPageData<T> pagingM, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new MResultPageModel<T>(MResultType.Success, pagingM.Data, pagingM.PageInfo, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static MResultPageModel<T> GetFailResultM(T data = default(T), MPageModel pagingM = null, string message = "")
        {
            return new MResultPageModel<T>(MResultType.Fail, data, pagingM, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static MResultPageModel<T> GetFailResultM(MPageData<T> pagingM = null, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new MResultPageModel<T>(MResultType.Fail, pagingM.Data, pagingM.PageInfo, message);
        }
        /// <summary>
        /// 获得一个错误返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pagingM">分页信息</param>
        /// <param name="message">返回消息</param>
        /// <returns>错误返回对象</returns>
        public static MResultPageModel<T> GetErrorResultM(T data = default(T), MPageModel pagingM = null, string message = "")
        {
            return new MResultPageModel<T>(MResultType.Error, data, pagingM, message);
        }
        /// <summary>
        /// 获得一个错误返回对象
        /// </summary>
        /// <param name="pagingM">分页数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>错误返回对象</returns>
        public static MResultPageModel<T> GetErrorResultM(MPageData<T> pagingM = null, string message = "")
        {
            if (pagingM == null)
            {
                pagingM = new MPageData<T>();
            }
            return new MResultPageModel<T>(MResultType.Fail, pagingM.Data, pagingM.PageInfo, message);
        }
    }
}
