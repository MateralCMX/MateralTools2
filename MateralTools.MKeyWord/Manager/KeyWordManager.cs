using MateralTools.MKeyWord.Interface;
using MateralTools.MKeyWord.Model;
using System.Collections;
using System.Linq;

namespace MateralTools.MKeyWord.Manager
{
    /// <inheritdoc />
    /// <summary>
    /// 关键词管理器
    /// 实现思路：生成一个关键词树来进行筛选
    /// </summary>
    public class KeyWordManager : IKeyWordManager
    {
        /// <summary>
        /// 关键词列表
        /// </summary>
        private string[] _keywords;
        public string[] Keywords
        {
            get => _keywords;
            set
            {
                _keywords = value;
                BuildTree();
            }
        }

        /// <summary>
        /// 关键词树根节点
        /// </summary>
        public KeyWordTreeNode Root { get; set; }

        /// <summary>
        /// 生成树
        /// </summary>
        private void BuildTree()
        {
            Root = new KeyWordTreeNode(null, ' ');
            #region 生成树
            foreach (var p in _keywords)
            {
                var nd = Root;
                foreach (var c in p)
                {
                    var ndNew = nd.Transitions.FirstOrDefault(trans => trans.Char == c);
                    if (ndNew == null)
                    {
                        ndNew = new KeyWordTreeNode(nd, c);
                        nd.AddTransition(ndNew);
                    }
                    nd = ndNew;
                }
                nd.AddResult(p);
            }
            #endregion
            var nodes = new ArrayList();
            //第一层失败节点
            foreach (var nd in Root.Transitions)
            {
                nd.Failure = Root;
                foreach (var trans in nd.Transitions)
                {
                    nodes.Add(trans);
                }
            }
            //下级失败节点
            while (nodes.Count != 0)
            {
                var newNodes = new ArrayList();
                foreach (KeyWordTreeNode nd in nodes)
                {
                    var r = nd.Parent.Failure;
                    var c = nd.Char;
                    while (r != null && !r.ContainsTransition(c))
                    {
                        r = r.Failure;
                    }
                    if (r == null)
                    {
                        nd.Failure = Root;
                    }
                    else
                    {
                        nd.Failure = r.GetTransition(c);
                        foreach (var result in nd.Failure.Results)
                        {
                            nd.AddResult(result);
                        }
                    }
                    //添加子节点在失败节点中
                    foreach (var child in nd.Transitions)
                    {
                        newNodes.Add(child);
                    }
                }
                nodes = newNodes;
            }
            Root.Failure = Root;
        }
        /// <summary>
        /// 搜索所有的关键词
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>搜索到的对象</returns>
        public KeyWordModel[] FindAll(string text)
        {
            var ret = new ArrayList();
            var ptr = Root;
            for (var i = 0; i < text.Length; i++)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(text[i]);
                    if (ptr == Root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }

                if (trans == null) continue;
                ptr = trans;
                foreach (var found in ptr.Results)
                {
                    ret.Add(new KeyWordModel(i - found.Length + 1, found));
                }
            }
            return (KeyWordModel[])ret.ToArray(typeof(KeyWordModel));
        }
        /// <summary>
        /// 搜索第一个关键词
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>搜索到的对象</returns>
        public KeyWordModel FindFirst(string text)
        {
            var ptr = Root;
            for (var i = 0; i < text.Length; i++)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(text[i]);
                    if (ptr == Root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }
                if (trans == null) continue;
                ptr = trans;
                foreach (var found in ptr.Results)
                {
                    return new KeyWordModel(i - found.Length + 1, found);
                }
            }
            return KeyWordModel.Empty;
        }
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>是否包含关键词</returns>
        public bool ContainsAny(string text)
        {
            var ptr = Root;
            foreach (var item in text)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(item);
                    if (ptr == Root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }
                if (trans == null) continue;
                ptr = trans;
                if (ptr.Results.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
