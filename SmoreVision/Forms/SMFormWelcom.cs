using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreVision
{

    public delegate void ShowLoadMsg(string msg, int ipos);
    public partial class SMFormWelcom : Form
    {
        public static SMFormWelcom instance;
        public static FormMain form_Main;
        public static bool frmLoadingOpen = false;
        public static ShowLoadMsg LoadingMsg;

       
        public static SMFormWelcom Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public SMFormWelcom()
        {
            InitializeComponent();

            LoadingMsg += new ShowLoadMsg(LogMsg);
            frmLoadingOpen = true;

            Thread th = new Thread(new ThreadStart(FormLoading));
           // th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        /// <summary>
        /// 打开进度条界面
        /// </summary>
        private void FormLoading()
        {
            // Form1 frmloading;
            form_Main = new FormMain();
            //frmloading.ShowDialog();

            //form_Main.TopLevel = true;
             //form_Main.TopMost = true;
             //form_Main.ShowInTaskbar = false;
            //form_Main.WindowState = FormWindowState.Normal;
            Application.Run(form_Main);
        }

        #region 信息显示
        private delegate void LogMsgCallBack(string msg, int ipos);
        private void LogMsg(string msg, int ipos)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg, ipos };
                this.lbLoadMsg.BeginInvoke(new LogMsgCallBack(AddLogMsg), pList);
            }
            else
            {
                AddLogMsg(msg, ipos);
            }
        }
        private void AddLogMsg(string msg, int ipos)
        {
            this.myProgressBar.Value = ipos;
            this.lbLoadMsg.Text = msg;
            if (ipos == 100)
                this.Close();
        }
        #endregion

        public static void ShowSplashScreen()
        {
            instance = new SMFormWelcom();
            instance.ShowDialog();
        }

        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }

        private void FormWelcom_Load(object sender, EventArgs e)
        {
          
        }

        private void SMFormWelcom_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLoadingOpen = false;
        }
    }

}
