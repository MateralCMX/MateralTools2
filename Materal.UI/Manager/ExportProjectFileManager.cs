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
    /// 导出项目文件管理器
    /// </summary>
    public class ExportProjectFileManager: ExportFileManager
    {
        /// <summary>
        /// 排除文件夹名称
        /// </summary>
        public string[] ExcludeDirectoryName { get; set; }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        public override void ExportFile(string targetPath)
        {
            try
            {
                ExportInit(targetPath);
                DirectoryInfo di = null;
                string newDirectoryName = string.Empty;
                string[] subNames = null;
                string[] names = Directory.GetDirectories(FilePath);
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
