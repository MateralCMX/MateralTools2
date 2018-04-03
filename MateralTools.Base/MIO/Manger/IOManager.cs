using System.Diagnostics;
using System.IO;

namespace MateralTools.Base.MIO
{

    /// <summary>
    /// IO管理类
    /// </summary>
    public class IOManager
    {
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceFolderName">源文件夹目录</param>
        /// <param name="destFolderName">目标文件夹目录</param>
        /// <param name="overwrite">允许覆盖文件</param>
        public static void CopyDirectory(string sourceFolderName, string destFolderName, bool overwrite)
        {
            var sourceFilesPath = Directory.GetFileSystemEntries(sourceFolderName);
            for (int i = 0; i < sourceFilesPath.Length; i++)
            {
                string sourceFilePath = sourceFilesPath[i];
                string directoryName = Path.GetDirectoryName(sourceFilePath);
                string[] forlders = directoryName.Split('\\');
                string lastDirectory = forlders[forlders.Length - 1];
                string dest = Path.Combine(destFolderName, lastDirectory);
                if (File.Exists(sourceFilePath))
                {
                    string sourceFileName = Path.GetFileName(sourceFilePath);
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }
                    File.Copy(sourceFilePath, Path.Combine(dest, sourceFileName), overwrite);
                }
                else
                {
                    CopyDirectory(sourceFilePath, dest, overwrite);
                }
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="targetPath">目标文件夹目录</param>
        /// <param name="deleteSelf">是否删除自身</param>
        public static void DeleteDirectory(string targetPath, bool deleteSelf = false)
        {
            DirectoryInfo dir = new DirectoryInfo(targetPath);
            if (!deleteSelf)
            {
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(i.FullName);
                    }
                }
            }
            else
            {
                dir.Delete(true);
            }
        }
        /// <summary>
        /// 打开资源管理器
        /// </summary>
        /// <param name="targetPath">目标文件夹目录</param>
        public static void OpenExplorer(string targetPath)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            proc.StartInfo.Arguments = @"/select," + targetPath;
            proc.Start();
        }
    }
}
