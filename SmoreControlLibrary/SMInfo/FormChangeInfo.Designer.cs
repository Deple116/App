namespace SmoreControlLibrary.SMInfo
{
    partial class FormChangeInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelClose = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.smButtonCancle = new SmoreControlLibrary.SMButton();
            this.smButtonConfirm = new SmoreControlLibrary.SMButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(10)))));
            this.panel1.Controls.Add(this.panelClose);
            this.panel1.Controls.Add(this.panelLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 32);
            this.panel1.TabIndex = 0;
            // 
            // panelClose
            // 
            this.panelClose.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.close1;
            this.panelClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelClose.Location = new System.Drawing.Point(322, 0);
            this.panelClose.Name = "panelClose";
            this.panelClose.Size = new System.Drawing.Size(32, 32);
            this.panelClose.TabIndex = 1;
            this.panelClose.Click += new System.EventHandler(this.panelClose_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.logo;
            this.panelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(32, 32);
            this.panelLogo.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.smButtonCancle);
            this.panel2.Controls.Add(this.smButtonConfirm);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 204);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(52, 90);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(243, 20);
            this.comboBox2.TabIndex = 4;
            // 
            // smButtonCancle
            // 
            this.smButtonCancle.BackColor = System.Drawing.Color.Red;
            this.smButtonCancle.BtnBackColor = System.Drawing.Color.Red;
            this.smButtonCancle.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonCancle.BtnForeColor = System.Drawing.Color.White;
            this.smButtonCancle.BtnImage = null;
            this.smButtonCancle.BtnText = "取消";
            this.smButtonCancle.ConerRadius = 15;
            this.smButtonCancle.FillColor = System.Drawing.Color.Red;
            this.smButtonCancle.IsRadius = true;
            this.smButtonCancle.IsShowRect = false;
            this.smButtonCancle.IsShowTips = false;
            this.smButtonCancle.Location = new System.Drawing.Point(257, 156);
            this.smButtonCancle.Name = "smButtonCancle";
            this.smButtonCancle.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonCancle.RectWidth = 1;
            this.smButtonCancle.Size = new System.Drawing.Size(78, 28);
            this.smButtonCancle.TabIndex = 3;
            this.smButtonCancle.TabStop = false;
            this.smButtonCancle.TipsText = "";
            this.smButtonCancle.BtnClick += new System.EventHandler(this.smButtonCancle_BtnClick);
            // 
            // smButtonConfirm
            // 
            this.smButtonConfirm.BackColor = System.Drawing.Color.Green;
            this.smButtonConfirm.BtnBackColor = System.Drawing.Color.Green;
            this.smButtonConfirm.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonConfirm.BtnForeColor = System.Drawing.Color.White;
            this.smButtonConfirm.BtnImage = null;
            this.smButtonConfirm.BtnText = "确认";
            this.smButtonConfirm.ConerRadius = 15;
            this.smButtonConfirm.FillColor = System.Drawing.Color.Green;
            this.smButtonConfirm.IsRadius = true;
            this.smButtonConfirm.IsShowRect = false;
            this.smButtonConfirm.IsShowTips = false;
            this.smButtonConfirm.Location = new System.Drawing.Point(150, 156);
            this.smButtonConfirm.Name = "smButtonConfirm";
            this.smButtonConfirm.RectColor = System.Drawing.Color.Green;
            this.smButtonConfirm.RectWidth = 1;
            this.smButtonConfirm.Size = new System.Drawing.Size(78, 28);
            this.smButtonConfirm.TabIndex = 2;
            this.smButtonConfirm.TabStop = false;
            this.smButtonConfirm.TipsText = "";
            this.smButtonConfirm.BtnClick += new System.EventHandler(this.smButtonConfirm_BtnClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(52, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(243, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入料号,组别及批次号，并确认";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(52, 124);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(243, 20);
            this.comboBox3.TabIndex = 5;
            // 
            // FormChangeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 236);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChangeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormChangeInfo";
            this.Load += new System.EventHandler(this.FormChangeInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelClose;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panel2;
        private SMButton smButtonCancle;
        private SMButton smButtonConfirm;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
    }
}