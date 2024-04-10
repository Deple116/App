using log4net;
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
    public class log4netHelper : IDPLog
    {
        LogLevel m_loglevel;
        public override void Add(string info, Color color, LogLevel loglevel, bool bshow)
        {
            DateTime dateTime = DateTime.Now;
            m_loglevel = loglevel;
            string fullinfo = PrintStackTrance() + "][thread:" + Thread.CurrentThread.ManagedThreadId.ToString() + "][msg:" + info + "]";
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

                Save(ret, m_loglevel);
            }

            return ret;
        }

        public override int GetQueueCount()
        {
            return tag_Log.Count();
        }


        private void Save(LogInfo log, LogLevel loglevel)
        {
            try
            {
                switch (loglevel)
                {
                    case LogLevel.Debug:
                        LogHelper.Debug(log.tag_fullinfo);
                        break;
                    case LogLevel.Error:
                        LogHelper.Error(log.tag_fullinfo);
                        break;
                    case LogLevel.Fatal:
                        LogHelper.Fatal(log.tag_fullinfo);
                        break;
                    case LogLevel.Info:
                        LogHelper.Info(log.tag_fullinfo);
                        break;
                    case LogLevel.Warn:
                        LogHelper.WARN(log.tag_fullinfo);
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private string PrintStackTrance()
        {
            //https://www.cnblogs.com/bruce1992/p/16897817.html
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace);//当前命名空间名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);//当前全限类名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name); //当前类名
            //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);//当前方法名


            //StackTrace st = new StackTrace(new StackFrame(true));
            StackTrace st = new StackTrace(true);
            //Console.WriteLine(" Stack trace for current level: {0}", st.ToString());
            StackFrame sf = st.GetFrame(3);// 0为本身的方法；1为调用方法
                                           //(new StackTrace()).GetFrame(1) // 0为本身的方法；1为调用方法
                                           //Console.WriteLine(" File: {0}", sf.GetFileName());
                                           //Console.WriteLine(" Method: {0}", sf.GetMethod().Name);
                                           //Console.WriteLine(" Line Number: {0}", sf.GetFileLineNumber());
                                           //Console.WriteLine(" Column Number: {0}", sf.GetFileColumnNumber());

            string[] strData = sf.GetFileName().Split('\\');
            string temp = strData[strData.Length - 1] + "::" + sf.GetMethod().Name + "::" + sf.GetFileLineNumber() + "::";

            return temp;
        }

    }



    public class LogHelper
    {
        #region  log4net
        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            ILog log = LogManager.GetLogger("Info");
            if (log.IsInfoEnabled)
            {
                log.Info( message);
            }
        }

        /// <summary>
        /// 错误日志带异常
        /// </summary>
        /// <param name="message">错误日志</param>
        public static void Error(string message, Exception ex)
        {
            ILog log = LogManager.GetLogger("Error");
            if (log.IsErrorEnabled)
            {
                log.Error( message, ex);
            }
        }

        /// <summary>
        /// 错误日志不带异常
        /// </summary>
        /// <param name="message">错误日志</param>
        public static void Error(string message)
        {
            ILog log = LogManager.GetLogger("Error");
            if (log.IsErrorEnabled)
            {
                log.Error( message);
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">Debug日志</param>
        public static void Debug(string message)
        {
            ILog log = LogManager.GetLogger("Debug");
            if (log.IsDebugEnabled)
            {
                log.Debug( message);
            }
        }

        /// <summary>
        /// 致命错误日志
        /// </summary>
        /// <param name="message">Fatal日志</param>
        public static void Fatal(string message)
        {
            ILog log = LogManager.GetLogger("Fatal");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message">warn日志</param>
        public static void WARN(string message)
        {
            ILog log = LogManager.GetLogger("Warn");
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        #endregion

        

    }

}
