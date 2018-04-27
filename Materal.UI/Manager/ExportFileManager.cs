using MateralTools.MIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.UI
{
    /// <summary>
    /// 导出后文件委托
    /// </summary>
    public delegate void ExportFileDelegate();
    public abstract class ExportFileManager
    {
        /// <summary>
        /// 导出成功后事件
        /// </summary>
        public ExportFileDelegate ExportSuccess;
        /// <summary>
        /// 导出失败后事件
        /// </summary>
        public ExportFileDelegate ExportFail;
        /// <summary>
        /// 导出完成后是否打开资源管理器
        /// </summary>
        public bool IsOpenExplorer { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 导出前初始化
        /// </summary>
        /// <param name="targetPath"></param>
        protected virtual void ExportInit(string targetPath)
        {
            if (Directory.Exists(targetPath))
            {
                IOManager.DeleteDirectory(targetPath);
            }
            else
            {
                Directory.CreateDirectory(targetPath);
            }
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        public abstract void ExportFile(string targetPath);
    }
}
