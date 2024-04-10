using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmoreControlLibrary.SMLog
{
    public class FileDel
    {
        //文件夹删除
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

        //文件删除
        public static void DellogsFile(string logpath, uint uDays)
        {

            Task.Factory.StartNew(() =>
            {
                var path = logpath;  //文件夹路径
                //Console.WriteLine(path);
                if (!Directory.Exists(path)) return;

                var dyInfo = new DirectoryInfo(path);
                foreach (var feInfo in dyInfo.GetFiles("*"+Log.fileType))
                {
                    if (feInfo.LastWriteTime < DateTime.Now.AddDays(-uDays)) feInfo.Delete();
                }

                Thread.Sleep(1000 * 60 * 60 * 24);//24小时执行一次
                DellogsFile(logpath, uDays);//递归
            });
        }

    }

}
