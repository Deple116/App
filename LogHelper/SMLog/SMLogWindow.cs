using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SMLogControlLibrary
{
    public delegate void delegate_FormMain_Log(string info, int type);

    public partial class SMLogWindow : UserControl
    {
        private string _logPathValue="";
        [Description("Log保存路径"), Category("SmoreControl")]
        public string LogPathValue
        {
            get { return _logPathValue; }
            set { _logPathValue = value; }
        }

        [Description("Title颜色"), Category("SmoreControl")]
        public Color TitleColor
        {
            get { return panel1.BackColor; }
            set { panel1.BackColor = value; }
        }

        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        private const string LOG_TIME_FORMAT_ONE = "HH:mm:ss:ffff";
        private const string LOG_TIME_FORMAT_TWO = "HH:mm:ss";
        private const string LOG_TIME_FORMAT_THREE = "HH-mm-ss";
        public const string TIME_LOG_FORMAT = "yyyy_MM_dd";
        public static string StaticlogPath = "";

        private System.Windows.Forms.Timer tag_ShowLogTimer;

        private static object tag_locker;
        private static int is_showMsg = 500;
        private static int showRowCount = 1000;
        private static string tag_upmsg = null;

        private DataGridViewRow dataGridViewRow = null;
        private DataGridViewTextBoxCell time = null;
        private DataGridViewTextBoxCell info = null;

        public bool bcircle = true;

        public SMLogWindow()
        {
            InitializeComponent();

            StaticlogPath = LogPathValue = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            Task.Factory.StartNew(() => LogOut_Show());
            //保留五天日志
            FileDel.DellogsFile(LogPathValue, 5);
        }

        ~SMLogWindow()
        {
            bcircle = false;
        }
        private void SMLogWindow_Load(object sender, EventArgs e)
        {
            //保存路径
           //if(_logPathValue==""||_logPathValue==null) LogPathValue=Environment.CurrentDirectory + "\\Log\\";

            
            //Console.WriteLine(LogPathValue);
          
            dataGridViewRow = new DataGridViewRow();
            time = new DataGridViewTextBoxCell();
            info = new DataGridViewTextBoxCell();

            tag_locker = new object();
            tag_ShowLogTimer = new System.Windows.Forms.Timer();
            tag_ShowLogTimer.Tick += new EventHandler(UserControl_LogOut_Show);
            tag_ShowLogTimer.Interval = 1;
            //tag_ShowLogTimer.Start();

           

            this.toolTip1.AutomaticDelay = 0;//提示延迟
            this.toolTip1.ShowAlways = true;//是否显示文本
            this.toolTip1.ToolTipTitle = "";//窗口标题
            this.toolTip1.UseAnimation = true;//动画效果
            this.toolTip1.UseFading = true;//淡入淡出效果

        }

        private void smButtonExportLog_BtnClick(object sender, EventArgs e)
        {
            if (dataGridViewLog.Rows.Count > 0)
            {
                DataGridViewToExcel(dataGridViewLog);
            }
            else
            {
                MessageBox.Show($"无可导出Log!", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        public void dataGridViewShow(LogInfo log, DataGridView daview)
        {
            try
            {
                if (log != null)
                {
                    if (daview.Rows.Count < showRowCount)
                    {
                        dataGridViewRow = new DataGridViewRow();
                        time = new DataGridViewTextBoxCell();
                        info = new DataGridViewTextBoxCell();
                        time.Value = log.tag_dateTime.ToString(LOG_TIME_FORMAT_ONE);
                        info.Value = log.tag_info.ToString();
                        dataGridViewRow.DefaultCellStyle.ForeColor = log.tag_color;
                        dataGridViewRow.Cells.Add(time);
                        dataGridViewRow.Cells.Add(info);
                        daview.Rows.Insert(0, dataGridViewRow);
                    }
                    else
                    {
                        daview.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void UserControl_LogOut_Show(object sender, EventArgs e)
        {
            try
            {
                //if (is_showMsg > 0)
                {
                    //is_showMsg--;
                    LogInfo listlog = Log.GetLog();
                    if (listlog != null)
                    {
                        dataGridViewShow(listlog, dataGridViewLog);
                        //Save(listlog);
                    }
                }
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        private  void LogOut_Show()
        {
            while (bcircle)
            {
                try
                {
                  
                    while (Log.GetLogCount()>0)
                    {
                       // int len = Log.GetLogCount();
                        LogInfo listlog = Log.GetLog();
                        if (listlog != null)
                        {
                            if(listlog.tag_bshow)
                            {
                                //Task.Run(()=> {
                                if (this.IsHandleCreated)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {

                                        dataGridViewShow(listlog, dataGridViewLog);

                                    });
                                }
                                    
                               // });
                                
                            }


                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                    // MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                Thread.Sleep(10);
            }
            
        }

        public static void OutLog(string Msg, Color color, LogLevel loglevel = LogLevel.Info, bool bshow = false)
        {

            
            if (tag_locker==null)
            {
                Log.Add(Msg, color, bshow);
                return;
            }
           // lock (tag_locker)
            {
                try
                {
                    if (Msg == tag_upmsg)
                        return;
                    //is_showMsg = 1;
                    Log.Add(Msg, color, bshow, loglevel);
                }
                catch (Exception ex)
                {
                    SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                    MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        //public static string PrintStackTrance()
        //{
        //    //https://www.cnblogs.com/bruce1992/p/16897817.html
        //    //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace);//当前命名空间名
        //    //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);//当前全限类名
        //    //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name); //当前类名
        //    //Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);//当前方法名


        //    //StackTrace st = new StackTrace(new StackFrame(true));
        //    StackTrace st = new StackTrace(true);
        //    //Console.WriteLine(" Stack trace for current level: {0}", st.ToString());
        //    StackFrame sf = st.GetFrame(3);// 0为本身的方法；1为调用方法
        //                                   //(new StackTrace()).GetFrame(1) // 0为本身的方法；1为调用方法
        //                                   //Console.WriteLine(" File: {0}", sf.GetFileName());
        //                                   //Console.WriteLine(" Method: {0}", sf.GetMethod().Name);
        //                                   //Console.WriteLine(" Line Number: {0}", sf.GetFileLineNumber());
        //                                   //Console.WriteLine(" Column Number: {0}", sf.GetFileColumnNumber());

        //    string[] strData = sf.GetFileName().Split('\\');
        //    string temp = "[method:" + strData[strData.Length - 1] + "::" + sf.GetMethod().Name + "::" + sf.GetFileLineNumber() + "::";

        //    return temp;
        //}

        public void Save(LogInfo log)
        {
            try
            {
                string sPath = LogPathValue + DateTime.Now.ToString(TIME_LOG_FORMAT) + "_log"+ Log.fileType;
                //Console.WriteLine(sPath);
                if (!File.Exists(sPath)) Directory.CreateDirectory(LogPathValue);
                File.AppendAllText(sPath, log.tag_dateTime + ":" + log.tag_fullinfo + "\r\n");
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void DataGridViewToExcel(DataGridView dgv)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl files (*.csv)|*.csv";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.CreatePrompt = true;
                dlg.Title = "保存为csv文件";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Stream myStream;
                    myStream = dlg.OpenFile();
                    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                    string columnTitle = "";
                    try
                    {
                        //写入列标题    
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            if (i > 0)
                            {
                                columnTitle += ",";
                            }
                            columnTitle += dgv.Columns[i].HeaderText;
                        }

                        sw.WriteLine(columnTitle);

                        //写入列内容    
                        for (int j = 0; j < dgv.Rows.Count; j++)
                        {
                            string columnValue = "";
                            for (int k = 0; k < dgv.Columns.Count; k++)
                            {
                                if (k > 0)
                                {
                                    columnValue += ",";
                                }
                                if (dgv.Rows[j].Cells[k].Value == null)
                                    columnValue += "";
                                else if (dgv.Rows[j].Cells[k].Value.ToString().Contains(","))
                                {
                                    columnValue += "\"" + dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\"";
                                }
                                else
                                {
                                    columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";
                                }
                            }
                            sw.WriteLine(columnValue);
                        }
                        sw.Close();
                        myStream.Close();
                        MessageBox.Show($"导出Log表格成功!", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                        MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sw.Close();
                        myStream.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"取消导出Log表格操作!", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewLog_MouseHover(object sender, EventArgs e)
        {

        }

        private void dataGridViewLog_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.RowIndex < 0 || e.ColumnIndex < 0)
                //{
                //    return;
                //}
                //else
                //{
                //    this.dataGridViewLog.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = this.dataGridViewLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //}
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel:LogLevel.Error);
            }

        }

        private void dataGridViewLog_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            ////鼠标移出单元格后隐藏提示工具
            this.toolTip1.Hide(this.dataGridViewLog);
        }

        


    }
}
