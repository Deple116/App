namespace SmoreControlLibrary.SMForm
{
    partial class FormLogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.smButtonChangePassword = new SmoreControlLibrary.SMButton();
            this.smButtonQuit = new SmoreControlLibrary.SMButton();
            this.smButtonLogin = new SmoreControlLibrary.SMButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(47, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "密码：";
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(150)))));
            this.textBoxPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassWord.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPassWord.Location = new System.Drawing.Point(97, 60);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.Size = new System.Drawing.Size(146, 22);
            this.textBoxPassWord.TabIndex = 1;
            this.textBoxPassWord.UseSystemPasswordChar = true;
            this.textBoxPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassWord_KeyDown);
            // 
            // smButtonChangePassword
            // 
            this.smButtonChangePassword.BackColor = System.Drawing.Color.White;
            this.smButtonChangePassword.BackColorShow = false;
            this.smButtonChangePassword.BtnBackColor = System.Drawing.Color.White;
            this.smButtonChangePassword.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonChangePassword.BtnForeColor = System.Drawing.Color.White;
            this.smButtonChangePassword.BtnImage = null;
            this.smButtonChangePassword.BtnText = "修改密码";
            this.smButtonChangePassword.ConerRadius = 10;
            this.smButtonChangePassword.FillColor = System.Drawing.Color.Gray;
            this.smButtonChangePassword.IsRadius = true;
            this.smButtonChangePassword.IsShowRect = true;
            this.smButtonChangePassword.IsShowTips = false;
            this.smButtonChangePassword.Location = new System.Drawing.Point(179, 111);
            this.smButtonChangePassword.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonChangePassword.Name = "smButtonChangePassword";
            this.smButtonChangePassword.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonChangePassword.RectWidth = 1;
            this.smButtonChangePassword.Size = new System.Drawing.Size(131, 21);
            this.smButtonChangePassword.TabIndex = 4;
            this.smButtonChangePassword.TabStop = false;
            this.smButtonChangePassword.TipsText = "";
            this.smButtonChangePassword.BtnClick += new System.EventHandler(this.smButtonChangePassword_BtnClick);
            // 
            // smButtonQuit
            // 
            this.smButtonQuit.BackColor = System.Drawing.Color.White;
            this.smButtonQuit.BackColorShow = false;
            this.smButtonQuit.BtnBackColor = System.Drawing.Color.White;
            this.smButtonQuit.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonQuit.BtnForeColor = System.Drawing.Color.White;
            this.smButtonQuit.BtnImage = null;
            this.smButtonQuit.BtnText = "取消";
            this.smButtonQuit.ConerRadius = 10;
            this.smButtonQuit.FillColor = System.Drawing.Color.Red;
            this.smButtonQuit.IsRadius = true;
            this.smButtonQuit.IsShowRect = true;
            this.smButtonQuit.IsShowTips = false;
            this.smButtonQuit.Location = new System.Drawing.Point(254, 149);
            this.smButtonQuit.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonQuit.Name = "smButtonQuit";
            this.smButtonQuit.RectColor = System.Drawing.Color.Red;
            this.smButtonQuit.RectWidth = 1;
            this.smButtonQuit.Size = new System.Drawing.Size(56, 28);
            this.smButtonQuit.TabIndex = 3;
            this.smButtonQuit.TabStop = false;
            this.smButtonQuit.TipsText = "";
            this.smButtonQuit.BtnClick += new System.EventHandler(this.smButtonQuit_BtnClick);
            // 
            // smButtonLogin
            // 
            this.smButtonLogin.BackColor = System.Drawing.Color.White;
            this.smButtonLogin.BackColorShow = false;
            this.smButtonLogin.BtnBackColor = System.Drawing.Color.White;
            this.smButtonLogin.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonLogin.BtnForeColor = System.Drawing.Color.White;
            this.smButtonLogin.BtnImage = null;
            this.smButtonLogin.BtnText = "登入";
            this.smButtonLogin.ConerRadius = 10;
            this.smButtonLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(61)))));
            this.smButtonLogin.IsRadius = true;
            this.smButtonLogin.IsShowRect = true;
            this.smButtonLogin.IsShowTips = false;
            this.smButtonLogin.Location = new System.Drawing.Point(179, 149);
            this.smButtonLogin.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonLogin.Name = "smButtonLogin";
            this.smButtonLogin.RectColor = System.Drawing.Color.Green;
            this.smButtonLogin.RectWidth = 1;
            this.smButtonLogin.Size = new System.Drawing.Size(56, 28);
            this.smButtonLogin.TabIndex = 2;
            this.smButtonLogin.TabStop = false;
            this.smButtonLogin.TipsText = "";
            this.smButtonLogin.BtnClick += new System.EventHandler(this.smButtonLogin_BtnClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(47, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "账号：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "操作员",
            "工程师",
            "管理员"});
            this.comboBox1.Location = new System.Drawing.Point(97, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FormLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(322, 205);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.smButtonChangePassword);
            this.Controls.Add(this.smButtonQuit);
            this.Controls.Add(this.smButtonLogin);
            this.Controls.Add(this.textBoxPassWord);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormLogIn";
            this.Load += new System.EventHandler(this.FormLogIn_Load);
            this.Shown += new System.EventHandler(this.FormLogIn_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private SMButton smButtonLogin;
        private SMButton smButtonQuit;
        private SMButton smButtonChangePassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}