using MateralTools.MKeyWord.Model;

namespace MateralTools.MKeyWord.Interface
{

    /// <summary>
    /// 关键词管理器接口
    /// </summary>
    public interface IKeyWordManager
    {
        /// <summary>
        /// 关键词列表
        /// </summary>
        string[] Keywords { get; set; }
        /// <summary>
        /// 搜索所有的关键词
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>搜索到的对象</returns>
        KeyWordModel[] FindAll(string text);
        /// <summary>
        /// 搜索第一个关键词
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>搜索到的对象</returns>
        KeyWordModel FindFirst(string text);
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>是否包含关键词</returns>
        bool ContainsAny(string text);
    }
}
