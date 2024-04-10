using SmoreControlLibrary.SMForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmoreControlLibrary
{
    public partial class FormMainBase : Form
    {
        public const int ERR_OK = 0;
       
        public const int ERR_FAILED = -1;

        private const string CONFIG_FILE_TYPE = "xml";

        private bool CHANGELANGUAGETOEN = false;

        public static bool BtnRunState = false;

        private string _projectName;

        public IDName idname=IDName.Operator;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; labelHeadName.Text = value; }
        }

        private FormWindowState _winState;

        public FormWindowState WinState
        {
            get { return _winState; }
            set { _winState = value; this.WindowState = value; }
        }

        private Point m_Point;


        public static FormMainBase formMainBase;
        public FormMainBase()
        {
            InitializeComponent();
            formMainBase = this;
        }

        private void FormMainBase_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.smButtonMaxAndNormal.BtnImage = global::SmoreControlLibrary.Properties.Resources.max;
            }
            else
            {
                this.smButtonMaxAndNormal.BtnImage = global::SmoreControlLibrary.Properties.Resources.maxNormal;
            }
        }

        private void Head_MouseDown(object sender, MouseEventArgs e)
        {
            m_Point = new Point(e.X, e.Y);
            this.Cursor = Cursors.Hand;
        }

        private void Head_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(this.Location.X + e.X - m_Point.X, this.Location.Y + e.Y - m_Point.Y);
        }

        private void Head_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void WindowStateChange(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                this.smButtonMaxAndNormal.BtnImage = global::SmoreControlLibrary.Properties.Resources.maxNormal;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.smButtonMaxAndNormal.BtnImage = global::SmoreControlLibrary.Properties.Resources.max;
            }
        }

        private void smButtonClose_BtnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void smButtonMaxAndNormal_BtnClick(object sender, EventArgs e)
        {
            WindowStateChange(sender, e);
        }

        private void smButtonMin_BtnClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void smButtonHelper_BtnClick(object sender, EventArgs e)
        {
            FormHelper formHelper = new FormHelper();
            formHelper.ShowDialog();
        }

        private void smButtonChangeLanguage_BtnClick(object sender, EventArgs e)
        {
            if (!CHANGELANGUAGETOEN)
            {
                smButtonChangeLanguage.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.English;
            }
            else
            {
                smButtonChangeLanguage.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.Chinese;
            }
            CHANGELANGUAGETOEN = !CHANGELANGUAGETOEN;
        }

        private void FormMainBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否关闭软件?", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
        }

        private void smButtonSet_BtnClick(object sender, EventArgs e)
        {
            if(!BtnRunState)
            {
                FormLogIn formLogIn = new FormLogIn();
                formLogIn.idname = idname;
                formLogIn.Init();
                formLogIn.ShowDialog();
                idname = formLogIn.idname;
                UpDateConfig();
            }
            else
            {
                MessageBox.Show($"运行状态下无法更改相应参数", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        public virtual void UpDateConfig()
        {

        }
    }
}
