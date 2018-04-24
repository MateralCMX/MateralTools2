using MateralTools.Base.MIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.UI
{
    /// <summary>
    /// 导出项目文件成功委托
    /// </summary>
    public delegate void ExportProjectFileDelegate();
    /// <summary>
    /// 导出项目文件管理器
    /// </summary>
    public class ExportProjectFileManager
    {
        /// <summary>
        /// 导出成功后事件
        /// </summary>
        public ExportProjectFileDelegate ExportSuccess;
        /// <summary>
        /// 导出失败后事件
        /// </summary>
        public ExportProjectFileDelegate ExportFail;
        /// <summary>
        /// 导出完成后是否打开资源管理器
        /// </summary>
        public bool IsOpenExplorer { get; set; }
        /// <summary>
        /// 代码路径
        /// </summary>
        public string CodePath { get; set; }
        /// <summary>
        /// 排除文件夹名称
        /// </summary>
        public string[] ExcludeDirectoryName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 导出前初始化
        /// </summary>
        /// <param name="targetPath"></param>
        private void ExportInit(string targetPath)
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
        /// 导出代码文件
        /// </summary>
        /// <param name="targetPath"></param>
        public void ExportCodeFile(string targetPath)
        {
            try
            {
                ExportInit(targetPath);
                DirectoryInfo di = null;
                string newDirectoryName = string.Empty;
                string[] subNames = null;
                string[] names = Directory.GetDirectories(CodePath);
                foreach (string name in names)
                {
                    di = new DirectoryInfo(name);
                    if (di.Name.Contains(ProjectName) && !di.Name.Contains("Tests"))
                    {
                        bool isWebLib = di.Name.Contains("WebLib");
                        newDirectoryName = string.Format(@"{0}\{1}\{2}", targetPath, ProjectName, di.Name.Substring(ProjectName.Length + 1));
                        Directory.CreateDirectory(newDirectoryName);
                        subNames = Directory.GetDirectories(name);
                        foreach (string subName in subNames)
                        {
                            di = new DirectoryInfo(subName);
                            if (!isWebLib)
                            {
                                if (!ExcludeDirectoryName.Contains(di.Name))
                                {
                                    IOManager.CopyDirectory(di.FullName, newDirectoryName, true);
                                }
                            }
                            else
                            {
                                if (di.Name.Contains("m-"))
                                {
                                    IOManager.CopyDirectory(di.FullName, newDirectoryName, true);
                                }
                            }
                        }
                    }
                }
                if (IsOpenExplorer)
                {
                    IOManager.OpenExplorer(targetPath);
                }
                ExportSuccess();
            }
            catch (Exception e)
            {
                ExportFail();
                throw new MUIException("导出失败", e);
            }
        }
    }
}
