using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMLogControlLibrary
{
   public enum LogLevel
    {
        Error,
        Debug,
        Fatal,
        Warn,
        Info,
       
    }

    public class LogInfo
    {
        public Color tag_color;
        public string tag_info;
        public string tag_fullinfo;
        public DateTime tag_dateTime;
        public bool tag_bshow;
        public LogInfo(Color color, string info, string fullinfo,DateTime dateTime,bool bshow)
        {
            tag_color = color;
            tag_info = info;
            tag_fullinfo = fullinfo;
            tag_dateTime = dateTime;
            tag_bshow = bshow;
        }
    }

    /// <summary>
    /// log静态方法调用
    /// </summary>
    public class Log
    {
        public static string fileType = ".txt";
      

        public static void Add(string info, Color color,bool bshow=false, LogLevel loglevel = LogLevel.Info)
        {
            IDPLog.GetInstance.Add(info, color,loglevel,bshow);
        }

        public static LogInfo GetLog()
        {
            return IDPLog.GetInstance.GetLog();
        }

        public static int GetLogCount()
        {
            return IDPLog.GetInstance.GetQueueCount();
        }


    }


   



}
