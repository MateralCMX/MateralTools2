using System.Collections;

namespace MateralTools.MKeyWord.Model
{
    /// <summary>
    /// 关键词树节点
    /// </summary>
    public class KeyWordTreeNode
    {
        #region 成员属性

        private readonly ArrayList _results;
        private readonly Hashtable _transHash;

        /// <summary>
        /// 字符
        /// </summary>
        public char Char { get; }

        /// <summary>
        /// 父节点
        /// </summary>
        public KeyWordTreeNode Parent { get; }

        /// <summary>
        /// 检测失败后获取的节点
        /// </summary>
        public KeyWordTreeNode Failure { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public KeyWordTreeNode[] Transitions { get; private set; }

        /// <summary>
        /// 返回列表
        /// </summary>
        public string[] Results { get; private set; }

        #endregion
        #region 方法
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="c">字符</param>
        public KeyWordTreeNode(KeyWordTreeNode parent, char c)
        {
            Char = c; Parent = parent;
            _results = new ArrayList();
            Results = new string[] { };
            Transitions = new KeyWordTreeNode[] { };
            _transHash = new Hashtable();
        }
        /// <summary>
        /// 再返回结果中添加字符串
        /// </summary>
        /// <param name="result">要添加的字符串</param>
        public void AddResult(string result)
        {
            if (_results.Contains(result)) return;
            _results.Add(result);
            Results = (string[])_results.ToArray(typeof(string));
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">过度的几点</param>
        public void AddTransition(KeyWordTreeNode node)
        {
            _transHash.Add(node.Char, node);
            var ar = new KeyWordTreeNode[_transHash.Values.Count];
            _transHash.Values.CopyTo(ar, 0);
            Transitions = ar;
        }
        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>节点或者null</returns>
        public KeyWordTreeNode GetTransition(char c)
        {
            return (KeyWordTreeNode)_transHash[c];
        }
        /// <summary>
        /// 字符是否包含在节点中
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>是否包含</returns>
        public bool ContainsTransition(char c)
        {
            return GetTransition(c) != null;
        }
        #endregion
    }
}