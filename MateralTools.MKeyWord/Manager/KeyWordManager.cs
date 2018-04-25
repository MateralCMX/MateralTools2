using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MKeyWord
{
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
        /// <summary>
        /// 关键词列表
        /// </summary>
        public string[] Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                BuildTree();
            }
        }
        /// <summary>
        /// 关键词树根节点
        /// </summary>
        private KeyWordTreeNode _root;
        /// <summary>
        /// 关键词树根节点
        /// </summary>
        public KeyWordTreeNode Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }
        /// <summary>
        /// 生成树
        /// </summary>
        private void BuildTree()
        {
            _root = new KeyWordTreeNode(null, ' ');
            #region 生成树
            foreach (string p in _keywords)
            {
                KeyWordTreeNode nd = _root;
                foreach (char c in p)
                {
                    KeyWordTreeNode ndNew = null;
                    foreach (KeyWordTreeNode trans in nd.Transitions)
                    {
                        if (trans.Char == c)
                        {
                            ndNew = trans;
                            break;
                        }
                    }
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
            ArrayList nodes = new ArrayList();
            //第一层失败节点
            foreach (KeyWordTreeNode nd in _root.Transitions)
            {
                nd.Failure = _root;
                foreach (KeyWordTreeNode trans in nd.Transitions)
                {
                    nodes.Add(trans);
                }
            }
            //下级失败节点
            while (nodes.Count != 0)
            {
                ArrayList newNodes = new ArrayList();
                foreach (KeyWordTreeNode nd in nodes)
                {
                    KeyWordTreeNode r = nd.Parent.Failure;
                    char c = nd.Char;
                    while (r != null && !r.ContainsTransition(c))
                    {
                        r = r.Failure;
                    }
                    if (r == null)
                    {
                        nd.Failure = _root;
                    }
                    else
                    {
                        nd.Failure = r.GetTransition(c);
                        foreach (string result in nd.Failure.Results)
                        {
                            nd.AddResult(result);
                        }
                    }
                    //添加子节点在失败节点中
                    foreach (KeyWordTreeNode child in nd.Transitions)
                    {
                        newNodes.Add(child);
                    }
                }
                nodes = newNodes;
            }
            _root.Failure = _root;
        }
        /// <summary>
        /// 搜索所有的关键词
        /// </summary>
        /// <param name="text">要搜索的文本</param>
        /// <returns>搜索到的对象</returns>
        public KeyWordModel[] FindAll(string text)
        {
            ArrayList ret = new ArrayList();
            KeyWordTreeNode ptr = _root;
            for (int i = 0; i < text.Length; i++)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(text[i]);
                    if (ptr == _root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }
                if (trans != null)
                {
                    ptr = trans;
                    foreach (string found in ptr.Results)
                    {
                        ret.Add(new KeyWordModel(i - found.Length + 1, found));
                    }
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
            ArrayList ret = new ArrayList();
            KeyWordTreeNode ptr = _root;
            for (int i = 0; i < text.Length; i++)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(text[i]);
                    if (ptr == _root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }
                if (trans != null)
                {
                    ptr = trans;
                    foreach (string found in ptr.Results)
                    {
                        return new KeyWordModel(i - found.Length + 1, found);
                    }
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
            KeyWordTreeNode ptr = _root;
            for (int i = 0; i < text.Length; i++)
            {
                KeyWordTreeNode trans = null;
                while (trans == null)
                {
                    trans = ptr.GetTransition(text[i]);
                    if (ptr == _root)
                    {
                        break;
                    }
                    if (trans == null)
                    {
                        ptr = ptr.Failure;
                    }
                }
                if (trans != null)
                {
                    ptr = trans;
                    if (ptr.Results.Length > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
