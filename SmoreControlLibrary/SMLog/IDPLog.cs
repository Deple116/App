using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SmoreControlLibrary.SMLog
{
    /// <summary>
    /// log父类
    /// </summary>
    public abstract class IDPLog
    {

        private static readonly IDPLog instance = new MyLog();

        //private static readonly IDPLog instance = new log4netHelper();

        public static IDPLog GetInstance { get { return instance; } }

        public ConcurrentQueue<LogInfo> tag_Log = new ConcurrentQueue<LogInfo>();

        public abstract void Add(string info, Color color, LogLevel loglevel = LogLevel.Info,bool bshow=false);

        public abstract LogInfo GetLog();
        public abstract int GetQueueCount();

    }
}
