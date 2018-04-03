using System;

namespace MateralTools.MResult
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class MPagingModel
    {
        /// <summary>
        /// 查询页面
        /// </summary>
        public int PagingIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PagingSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long PagingCount
        {
            get
            {
                if (DataCount % PagingSize > 0)
                {
                    return DataCount / PagingSize + 1;
                }
                else
                {
                    return DataCount / PagingSize;
                }
            }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public long DataCount { get; set; }
        public MPagingModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">当前页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        public MPagingModel(int pagingIndex,int pagingSize)
        {
            if (pagingIndex > 0)
            {
                if (pagingSize > 0)
                {
                    PagingIndex = pagingIndex;
                    PagingSize = pagingSize;
                }
                else
                {
                    throw new MResultException($"参数{nameof(pagingSize)}必须大于0");
                }
            }
            else
            {
                throw new MResultException($"参数{nameof(pagingIndex)}必须大于0");
            }
        }
    }
    /// <summary>
    /// 分页数据模型
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class MPagingData<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public MPagingModel PageInfo { get; set; }
        /// <summary>
        /// 数据信息
        /// </summary>
        public T Data { get; set; }
    }
}
