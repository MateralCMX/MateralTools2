using MateralTools.MIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.UI
{
    public class ExportNugetPackageFileManager: ExportFileManager
    {
        /// <summary>
        /// 导出nuget包文件
        /// </summary>
        /// <param name="targetPath">目标目录</param>
        /// <param name="dis">文件夹信息</param>
        public void ExportNuGetPackFile(string targetPath, DirectoryInfo[] dis)
        {
            FileInfo[] fis = null;
            DirectoryInfo[] subDis = null;
            foreach (DirectoryInfo di in dis)
            {
                fis = di.GetFiles("*.nupkg");
                foreach (FileInfo fi in fis)
                {
                    if (fi.Name.Contains(ProjectName))
                    {
                        fi.CopyTo(targetPath + fi.Name, true);
                    }
                }
                subDis = di.GetDirectories();
                if (subDis != null)
                {
                    ExportNuGetPackFile(targetPath, subDis);
                }
            }
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        public override void ExportFile(string targetPath)
        {
            char lastChar = targetPath[targetPath.Length - 1];
            if (lastChar != '\\')
            {
                targetPath += "\\";
            }
            try
            {
                ExportInit(targetPath);
                DirectoryInfo di = null;
                string[] names = Directory.GetDirectories(FilePath);
                DirectoryInfo[] subDis = null;
                foreach (string name in names)
                {
                    di = new DirectoryInfo(name);
                    subDis = di.GetDirectories();
                    if (subDis != null)
                    {
                        ExportNuGetPackFile(targetPath, subDis);
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
