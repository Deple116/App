using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreControlLibrary.SMForm
{
    public partial class FormLogIn : Form
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        private string LastError = "";
        private static string configPath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + "Producepassword.xml";
        private  static UserPasswordParse m_UserPasswordParse = new UserPasswordParse();

        public IDName idname = IDName.Operator;

        private IDName previousID = IDName.Operator;
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
            int returnVale = GetProducePassword();
            if (returnVale != ERROR_OK)
            {
                MessageBox.Show($"加载配置文件错误，{LastError}!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void smButtonQuit_BtnClick(object sender, EventArgs e)
        {
            idname= previousID;
            this.Close();
        }

        private void smButtonChangePassword_BtnClick(object sender, EventArgs e)
        {
            this.Close();
            FormChangePassword formChangePassword = new FormChangePassword(idname,m_UserPasswordParse, configPath);
            formChangePassword.ShowDialog();
        }

        private void smButtonLogin_BtnClick(object sender, EventArgs e)
        {
            if (textBoxPassWord.Text.Equals(""))
            {
                MessageBox.Show("密码不能为空!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //switch (comboBox1.SelectedItem.ToString())
            //{
            //    case "操作员":
            //        idname = IDName.Operator;
            //        break;
            //    case "工程师":
            //        idname= IDName.Engineer;
            //        break;
            //    case "管理员":
            //        idname = IDName.Admin;
            //        break;
            //}

           
            if (UserLogOn(idname,textBoxPassWord.Text))
            {
                this.Close();
                if(idname==IDName.Admin)
                {
                    previousID = idname;
                    FormSet formSet = new FormSet();
                    formSet.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show($"密码错误!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool UserLogOn(IDName Name,string _password)
        {
            try
            {
                bool bRes = false;
                switch (Name)
                {
                    case IDName.Operator:
                        if (GenerateMD5(_password) == m_UserPasswordParse.OperatorPassword.Password)
                        {
                            return true;
                        }
                        break;
                    case IDName.Engineer:
                        if (GenerateMD5(_password) == m_UserPasswordParse.EngineerPassword.Password)
                        {
                            return true;
                        }
                        break;
                    case IDName.Admin:
                        if (GenerateMD5(_password) == m_UserPasswordParse.AdminPassword.Password)
                        {
                            return true;
                        }
                        break;
                }

                return bRes;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        

        public int GetProducePassword()
        {
            return XMLSerialize.DeserializeFromXml<UserPasswordParse>(configPath, ref m_UserPasswordParse, ref LastError);
        }

        private void textBoxPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        smButtonLogin_BtnClick(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "操作员":
                    idname = IDName.Operator;
                    break;
                case "工程师":
                    idname = IDName.Engineer;
                    break;
                case "管理员":
                    idname = IDName.Admin;
                    break;
            }
        }

        private void FormLogIn_Shown(object sender, EventArgs e)
        {
            
        }

        public void  Init()
        {
            comboBox1.SelectedIndex = (int)idname;
            previousID = idname;
        }
    }
}
