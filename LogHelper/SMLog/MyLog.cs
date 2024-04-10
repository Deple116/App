using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMLogControlLibrary
{
    /// <summary>
    /// 具体方法类
    /// </summary>
    public class MyLog : IDPLog
    {

        FileStream fs = null;
        StreamWriter sw = null;
        string sPath = "";

        public MyLog()
        {

        }
        ~MyLog()
        {
            //if (sw != null)
            //{
            //    sw.Dispose();
            //    sw.Close();
            //}
          if(fs != null) fs.Close();
        }


        public override void Add(string info, Color color, LogLevel loglevel = LogLevel.Info,bool bshow=false)
        {
            DateTime dateTime = DateTime.Now;
            string fullinfo =PrintStackTrance() + "][tid:" + Thread.CurrentThread.ManagedThreadId.ToString() + "][" + loglevel + "][" + info + "]";
            tag_Log.Enqueue(new LogInfo(color, info, fullinfo, dateTime, bshow));
        }

        public override LogInfo GetLog()
        {
            if (tag_Log.IsEmpty)
            {
                return null;
            }

            tag_Log.TryDequeue(out LogInfo ret);


            if (ret != null)
            {
                 Save(ret);
            }

            return ret;
        }

        public override int GetQueueCount()
        {
           return tag_Log.Count();
        }

        
        private void Save(LogInfo log)
        {
            try
            {
                sPath = SMLogWindow.StaticlogPath + DateTime.Now.ToString(SMLogWindow.TIME_LOG_FORMAT) + "_log" + Log.fileType;
                //Console.WriteLine(sPath);
               if (!File.Exists(sPath)) Directory.CreateDirectory(SMLogWindow.StaticlogPath);
                //if(fs==null) fs = new FileStream(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                if (fs == null)
                {
                    fs = new FileStream(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    if (!fs.Name.Contains(DateTime.Now.ToString(SMLogWindow.TIME_LOG_FORMAT)))
                    {
                        if (sw != null) sw.Close();
                        fs.Close();
                        fs = new FileStream(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        sw = new StreamWriter(fs, Encoding.UTF8);
                        sw.AutoFlush = true;
                    }
                }

                //是否满100M
                FileInfo fileinfo = new FileInfo(fs.Name);              
                if (fileinfo.Length > 100 * 1024 * 1024)
                {
                    //获取指定目录下的所有的子文件
                    string[] files = Directory.GetFiles(SMLogWindow.StaticlogPath, DateTime.Now.ToString(SMLogWindow.TIME_LOG_FORMAT) + "*", SearchOption.TopDirectoryOnly);
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();

                    File.Move(fs.Name, GetPathStr(SMLogWindow.StaticlogPath, string.Format("{0}{1}", DateTime.Now.ToString(SMLogWindow.TIME_LOG_FORMAT) + "_log_" + files.Length, Log.fileType)));

                   
                    fs = new FileStream(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.AutoFlush = true;

                }

                if (sw == null)
                {
                    sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.AutoFlush = true;
                }

                sw.Write(log.tag_dateTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + log.tag_fullinfo + "\r\n");
              
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                //MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 拼接地址串
        /// </summary>
        /// <param name="firstPath"></param>
        /// <param name="secondPath"></param>
        /// <returns></returns>
        private static string GetPathStr(string firstPath, string secondPath)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(firstPath);
            builder.Append("\\");
            builder.Append(secondPath);
            return builder.ToString();
        }
        private string PrintStackTrance()
        {
            try
            {
                //StackTrace st = new StackTrace(new StackFrame(true));
                StackTrace st = new StackTrace(true);
                //Console.WriteLine(" Stack trace for current level: {0}", st.ToString());
                StackFrame sf = st.GetFrame(4);// 0为本身的方法；1为调用方法
                                               //(new StackTrace()).GetFrame(1) // 0为本身的方法；1为调用方法
                                               //Console.WriteLine(" File: {0}", sf.GetFileName());
                                               //Console.WriteLine(" Method: {0}", sf.GetMethod().Name);
                                               //Console.WriteLine(" Line Number: {0}", sf.GetFileLineNumber());
                                               //Console.WriteLine(" Column Number: {0}", sf.GetFileColumnNumber());
                if (sf == null) return "sf_null";
                if (sf.GetFileName()==null) sf = st.GetFrame(3);
                if (sf.GetFileName() == null) sf = st.GetFrame(2);
                if (sf.GetFileName() == null) sf = st.GetFrame(1);
                string temp = "statckInfo_error";
                if (sf.GetFileName()!=null)
                {
                    string[] strData = sf.GetFileName().Split('\\');
                    temp = "[" + strData[strData.Length - 1] + ":" + sf.GetMethod().Name + ":" + sf.GetFileLineNumber();
                }
               

                return temp;
            }
            catch(Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                return ex.ToString();
            }
            //https://www.cnblogs.com/bruce1992/p/16897817.html
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace);//当前命名空间名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);//当前全限类名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name); //当前类名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);//当前方法名


           
        }
    }

}
