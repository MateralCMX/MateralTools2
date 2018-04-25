namespace MateralTools.MKeyWord
{
    /// <summary>
    /// 关键字模型
    /// </summary>
    public class KeyWordModel
    {
        /// <summary>
        /// 索引位置
        /// </summary>
        private int _index;
        /// <summary>
        /// 关键词
        /// </summary>
        private string _keyword;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">文本索引位置</param>
        /// <param name="keyword">找到的文本</param>
        public KeyWordModel(int index, string keyword)
        {
            _index = index;
            _keyword = keyword;
        }
        /// <summary>
        /// 索引位置
        /// </summary>
        public int Index
        {
            get { return _index; }
        }
        /// <summary>
        /// 找到的文字
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
        }
        /// <summary>
        /// 初始对象
        /// </summary>
        public static KeyWordModel Empty
        {
            get
            {
                return new KeyWordModel(-1, "");
            }
        }
    }
}
