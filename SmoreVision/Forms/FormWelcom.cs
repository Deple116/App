using SmoreVision;
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
    public partial class FormWelcom : Form
    {
        public static FormWelcom instance;
        public static FormMain form_Main;

        private int TimeCount = 0;
        
        public delegate void messageEventHandle();
        public static FormWelcom Instance
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

        public FormWelcom()
        {
            InitializeComponent();
        }

        public static void ShowSplashScreen()
        {
            instance = new FormWelcom();
            instance.ShowDialog();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            TimeCount += 1;
            if (TimeCount >= 10)
            {
                form_Main = new FormMain();
                instance.Dispose();
                form_Main.ShowDialog();
            }
        }

        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }

        private void FormWelcom_Load(object sender, EventArgs e)
        {
            timerRefresh.Enabled = true;
        }
    }
}
