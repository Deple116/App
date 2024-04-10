using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileControlLibrary
{
    public class FileControl
    {

        public FileControl() { }

        /// <summary>
        /// 获取文件夹及子目录文件大小
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="directoryLength"></param>
        /// <returns></returns>
        public static bool GetDirectorySize(string directoryPath, out long directoryLength)
        {
            directoryLength = -1;
            if (Directory.Exists(directoryPath))
            {
                //一级目录
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                foreach (var item in directoryInfo.GetFiles())
                {
                    directoryLength += item.Length;
                }
                //子目录
                foreach (var item in directoryInfo.GetDirectories())
                {
                    if (GetDirectorySize(item.FullName, out long dirLength))
                    {
                        directoryLength += dirLength;
                    }
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public static bool GetFileSize(string filePath, out long fileLength)
        {
            fileLength = -1;
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileLength = fileInfo.Length;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 文件夹删除
        /// </summary>
        /// <param name="logpath"></param>
        /// <param name="uDays"></param>
        public static void DellogsDir(string logpath, uint uDays)
        {

            Task.Factory.StartNew(() =>
            {
                var path = logpath;  //文件夹路径
                if (!Directory.Exists(path)) return;

                var dyInfo = new DirectoryInfo(path);
                //foreach (var feInfo in dyInfo.GetFiles("*.log"))
                //{
                //    if (feInfo.LastWriteTime < DateTime.Now.AddDays(-7)) feInfo.Delete();
                //}


                foreach (var feInfo in dyInfo.GetDirectories())
                {
                    if (feInfo.LastWriteTime < DateTime.Now.AddDays(-uDays))
                    {
                        //删除文件夹
                        if (Directory.Exists(feInfo.FullName))
                            Directory.Delete(feInfo.FullName, true);
                    }
                    else
                    {

                    }
                }
                Thread.Sleep(1000 * 60 * 60 * 24);//24小时执行一次
                DellogsDir(logpath, uDays);//递归
            });
        }
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="logpath"></param>
        /// <param name="uDays"></param>
        public static void DellogsFile(string logpath, uint uDays,string filetype=null)
        {

            Task.Factory.StartNew(() =>
            {
                var path = logpath;  //文件夹路径
                //Console.WriteLine(path);
                if (!Directory.Exists(path)) return;

                var dyInfo = new DirectoryInfo(path);
                foreach (var feInfo in dyInfo.GetFiles(filetype==null?"*.*": $"*.{filetype}"))
                {
                    if (feInfo.LastWriteTime < DateTime.Now.AddDays(-uDays)) feInfo.Delete();
                }

                Thread.Sleep(1000 * 60 * 60 * 24);//24小时执行一次
                DellogsFile(logpath, uDays);//递归
            });
        }
    }
}
