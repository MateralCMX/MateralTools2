namespace MateralTools.MKeyWord.Model
{
    /// <summary>
    /// 关键字模型
    /// </summary>
    public class KeyWordModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">文本索引位置</param>
        /// <param name="keyword">找到的文本</param>
        public KeyWordModel(int index, string keyword)
        {
            Index = index;
            Keyword = keyword;
        }
        /// <summary>
        /// 索引位置
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 找到的文字
        /// </summary>
        public string Keyword { get; }

        /// <summary>
        /// 初始对象
        /// </summary>
        public static KeyWordModel Empty => new KeyWordModel(-1, "");
    }
}
