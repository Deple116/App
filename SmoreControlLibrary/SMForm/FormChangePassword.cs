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
    public partial class FormChangePassword : Form
    {
        public string UserPasswordParseFilePath { get; set; }

        private UserPasswordParse m_UserPasswordParse;

        private IDName m_IDName;

        string LastError = "";

        public FormChangePassword(IDName idname,UserPasswordParse _userPasswordParse,string _passwordConfigFilePath)
        {
            InitializeComponent();
            m_IDName= idname;
            m_UserPasswordParse = _userPasswordParse;
            UserPasswordParseFilePath = _passwordConfigFilePath;
        }

        private void smButtonQuit_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void smButtonLogin_BtnClick(object sender, EventArgs e)
        {
            try
            {
                string newPassword = textBoxNewPassword.Text;
                switch (m_IDName)
                {
                    case IDName.Operator:
                        OperatorProcess(newPassword);
                        break;
                    case IDName.Engineer:
                        EngineerProcess(newPassword);
                        break;
                    case IDName.Admin:
                        AdminProcess(newPassword);
                        break;
                
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改密码失败,{ex.ToString()}!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool OperatorProcess(string _newPassword)
        {
            try {

                if (GenerateMD5(textBoxOldPassword.Text).Equals(m_UserPasswordParse.OperatorPassword.Password))
                {
                    if (!_newPassword.Equals(""))
                    {
                        if (textBoxNewPassword.Text.Equals(textBoxNewPasswordAgain.Text))
                        {
                            m_UserPasswordParse.OperatorPassword.Password = GenerateMD5(_newPassword);
                            XMLSerialize.SerializeToXml<UserPasswordParse>(UserPasswordParseFilePath, m_UserPasswordParse, ref LastError);
                            MessageBox.Show("修改密码成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("修改密码失败,两次设置不一致!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"密码不能为空!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"老密码不正确!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return true; 
            }
            catch(Exception ex) {

                MessageBox.Show($"{ex.ToString()}");
                return false;   
            }
        }

        private bool EngineerProcess(string _newPassword)
        {
            try
            {

                if (GenerateMD5(textBoxOldPassword.Text).Equals(m_UserPasswordParse.EngineerPassword.Password))
                {
                    if (!_newPassword.Equals(""))
                    {
                        if (textBoxNewPassword.Text.Equals(textBoxNewPasswordAgain.Text))
                        {
                            m_UserPasswordParse.EngineerPassword.Password = GenerateMD5(_newPassword);
                            XMLSerialize.SerializeToXml<UserPasswordParse>(UserPasswordParseFilePath, m_UserPasswordParse, ref LastError);
                            MessageBox.Show("修改密码成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("修改密码失败,两次设置不一致!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"密码不能为空!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"老密码不正确!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.ToString()}");
                return false;
            }
        }

        private bool AdminProcess(string _newPassword)
        {
            try
            {

                if (GenerateMD5(textBoxOldPassword.Text).Equals(m_UserPasswordParse.AdminPassword.Password))
                {
                    if (!_newPassword.Equals(""))
                    {
                        if (textBoxNewPassword.Text.Equals(textBoxNewPasswordAgain.Text))
                        {
                            m_UserPasswordParse.AdminPassword.Password = GenerateMD5(_newPassword);
                            XMLSerialize.SerializeToXml<UserPasswordParse>(UserPasswordParseFilePath, m_UserPasswordParse, ref LastError);
                            MessageBox.Show("修改密码成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("修改密码失败,两次设置不一致!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"密码不能为空!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"老密码不正确!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.ToString()}");
                return false;
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
    }
}
