namespace SmoreControlLibrary.SMForm
{
    partial class FormChangePassword
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
            this.smButtonQuit = new SmoreControlLibrary.SMButton();
            this.smButtonLogin = new SmoreControlLibrary.SMButton();
            this.textBoxOldPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewPasswordAgain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // smButtonQuit
            // 
            this.smButtonQuit.BackColor = System.Drawing.Color.Red;
            this.smButtonQuit.BtnBackColor = System.Drawing.Color.Red;
            this.smButtonQuit.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonQuit.BtnForeColor = System.Drawing.Color.White;
            this.smButtonQuit.BtnImage = null;
            this.smButtonQuit.BtnText = "取消";
            this.smButtonQuit.ConerRadius = 10;
            this.smButtonQuit.FillColor = System.Drawing.Color.Red;
            this.smButtonQuit.IsRadius = true;
            this.smButtonQuit.IsShowRect = true;
            this.smButtonQuit.IsShowTips = false;
            this.smButtonQuit.Location = new System.Drawing.Point(233, 126);
            this.smButtonQuit.Name = "smButtonQuit";
            this.smButtonQuit.RectColor = System.Drawing.Color.Red;
            this.smButtonQuit.RectWidth = 1;
            this.smButtonQuit.Size = new System.Drawing.Size(56, 28);
            this.smButtonQuit.TabIndex = 7;
            this.smButtonQuit.TabStop = false;
            this.smButtonQuit.TipsText = "";
            this.smButtonQuit.BtnClick += new System.EventHandler(this.smButtonQuit_BtnClick);
            // 
            // smButtonLogin
            // 
            this.smButtonLogin.BackColor = System.Drawing.Color.White;
            this.smButtonLogin.BtnBackColor = System.Drawing.Color.White;
            this.smButtonLogin.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonLogin.BtnForeColor = System.Drawing.Color.White;
            this.smButtonLogin.BtnImage = null;
            this.smButtonLogin.BtnText = "确认";
            this.smButtonLogin.ConerRadius = 10;
            this.smButtonLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(61)))));
            this.smButtonLogin.IsRadius = true;
            this.smButtonLogin.IsShowRect = true;
            this.smButtonLogin.IsShowTips = false;
            this.smButtonLogin.Location = new System.Drawing.Point(158, 126);
            this.smButtonLogin.Name = "smButtonLogin";
            this.smButtonLogin.RectColor = System.Drawing.Color.Green;
            this.smButtonLogin.RectWidth = 1;
            this.smButtonLogin.Size = new System.Drawing.Size(56, 28);
            this.smButtonLogin.TabIndex = 6;
            this.smButtonLogin.TabStop = false;
            this.smButtonLogin.TipsText = "";
            this.smButtonLogin.BtnClick += new System.EventHandler(this.smButtonLogin_BtnClick);
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(150)))));
            this.textBoxOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxOldPassword.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxOldPassword.Location = new System.Drawing.Point(81, 24);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.Size = new System.Drawing.Size(162, 22);
            this.textBoxOldPassword.TabIndex = 5;
            this.textBoxOldPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "旧密码：";
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(150)))));
            this.textBoxNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNewPassword.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNewPassword.Location = new System.Drawing.Point(81, 55);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.Size = new System.Drawing.Size(162, 22);
            this.textBoxNewPassword.TabIndex = 9;
            this.textBoxNewPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "新密码：";
            // 
            // textBoxNewPasswordAgain
            // 
            this.textBoxNewPasswordAgain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(150)))));
            this.textBoxNewPasswordAgain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNewPasswordAgain.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNewPasswordAgain.Location = new System.Drawing.Point(81, 86);
            this.textBoxNewPasswordAgain.Name = "textBoxNewPasswordAgain";
            this.textBoxNewPasswordAgain.Size = new System.Drawing.Size(162, 22);
            this.textBoxNewPasswordAgain.TabIndex = 11;
            this.textBoxNewPasswordAgain.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(19, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "新密码：";
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 166);
            this.Controls.Add(this.textBoxNewPasswordAgain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNewPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.smButtonQuit);
            this.Controls.Add(this.smButtonLogin);
            this.Controls.Add(this.textBoxOldPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMButton smButtonQuit;
        private SMButton smButtonLogin;
        private System.Windows.Forms.TextBox textBoxOldPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNewPasswordAgain;
        private System.Windows.Forms.Label label3;
    }
}