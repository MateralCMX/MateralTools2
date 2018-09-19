namespace MateralTools.MResult.Model
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public class MPageRequestModel
    {
        /// <summary>
        /// 查询页面
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 查询页面
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (value < 1)
                {
                    throw new MResultException("PageIndex必须大于等于1");
                }
                _pageIndex = value;
            }
        }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        private int _pageSize = 1;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new MResultException("PageSize必须大于等于1");
                }
                _pageSize = value;
            }
        }
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int Skip
        {
            get
            {
                return PageSize * (PageIndex - 1);
            }
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        public int Take
        {
            get
            {
                return PageSize;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MPageRequestModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">当前页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        public MPageRequestModel(int pagingIndex, int pagingSize)
        {
            PageIndex = pagingIndex;
            PageSize = pagingSize;
        }
    }
    /// <summary>
    /// 分页模型
    /// </summary>
    public class MPageModel : MPageRequestModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (DataCount % PageSize > 0)
                {
                    return DataCount / PageSize + 1;
                }
                else
                {
                    return DataCount / PageSize;
                }
            }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MPageModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">当前页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        public MPageModel(MPageRequestModel pageM, int dataCount) : base(pageM.PageIndex, pageM.PageSize)
        {
            DataCount = dataCount;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">当前页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        public MPageModel(int pagingIndex, int pagingSize, int dataCount):base(pagingIndex, pagingSize)
        {
            DataCount = dataCount;
        }
    }
    /// <summary>
    /// 分页数据模型
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class MPageData<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public MPageModel PageInfo { get; set; }
        /// <summary>
        /// 数据信息
        /// </summary>
        public T Data { get; set; }
    }
}
