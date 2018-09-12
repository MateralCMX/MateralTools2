using System.Diagnostics;
using System.IO;

namespace MateralTools.MIO.Manger
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
            foreach (var sourceFilePath in sourceFilesPath)
            {
                var directoryName = Path.GetDirectoryName(sourceFilePath);
                if (directoryName == null) continue;
                var forlders = directoryName.Split('\\');
                var lastDirectory = forlders[forlders.Length - 1];
                var dest = Path.Combine(destFolderName, lastDirectory);
                if (File.Exists(sourceFilePath))
                {
                    var sourceFileName = Path.GetFileName(sourceFilePath);
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
            var dir = new DirectoryInfo(targetPath);
            if (!deleteSelf)
            {
                var fileinfo = dir.GetFileSystemInfos();
                foreach (var item in fileinfo)
                {
                    var subdir = new DirectoryInfo(item.FullName);
                    if (item is DirectoryInfo)
                    {
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(item.FullName);
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
            var proc = new Process {StartInfo = {FileName = "explorer", Arguments = @"/select," + targetPath}};
            proc.Start();
        }
    }
}
