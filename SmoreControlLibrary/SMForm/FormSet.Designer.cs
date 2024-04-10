namespace SmoreControlLibrary.SMForm
{
    partial class FormSet
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
            this.panelHead = new System.Windows.Forms.Panel();
            this.smButton2 = new SmoreControlLibrary.SMButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.buttonCountSet = new System.Windows.Forms.Button();
            this.buttonProductInfoSet = new System.Windows.Forms.Button();
            this.buttonLightSet = new System.Windows.Forms.Button();
            this.buttonThresholdSet = new System.Windows.Forms.Button();
            this.buttonSaveSet = new System.Windows.Forms.Button();
            this.buttonSystemSet = new System.Windows.Forms.Button();
            this.panelFormSetHome = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.panelHead.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panelHead.Controls.Add(this.lbl);
            this.panelHead.Controls.Add(this.smButton2);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(1030, 31);
            this.panelHead.TabIndex = 0;
            // 
            // smButton2
            // 
            this.smButton2.BackColor = System.Drawing.Color.Transparent;
            this.smButton2.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButton2.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton2.BtnForeColor = System.Drawing.Color.Black;
            this.smButton2.BtnImage = global::SmoreControlLibrary.Properties.Resources.close1;
            this.smButton2.BtnText = "";
            this.smButton2.ConerRadius = 24;
            this.smButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.smButton2.FillColor = System.Drawing.Color.Transparent;
            this.smButton2.IsRadius = false;
            this.smButton2.IsShowRect = false;
            this.smButton2.IsShowTips = false;
            this.smButton2.Location = new System.Drawing.Point(998, 0);
            this.smButton2.Name = "smButton2";
            this.smButton2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButton2.RectWidth = 1;
            this.smButton2.Size = new System.Drawing.Size(32, 31);
            this.smButton2.TabIndex = 3;
            this.smButton2.TabStop = false;
            this.smButton2.TipsText = "";
            this.smButton2.BtnClick += new System.EventHandler(this.panelClose_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(231)))));
            this.panelLeft.Controls.Add(this.buttonCountSet);
            this.panelLeft.Controls.Add(this.buttonProductInfoSet);
            this.panelLeft.Controls.Add(this.buttonLightSet);
            this.panelLeft.Controls.Add(this.buttonThresholdSet);
            this.panelLeft.Controls.Add(this.buttonSaveSet);
            this.panelLeft.Controls.Add(this.buttonSystemSet);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 31);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(127, 651);
            this.panelLeft.TabIndex = 1;
            // 
            // buttonCountSet
            // 
            this.buttonCountSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonCountSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCountSet.FlatAppearance.BorderSize = 0;
            this.buttonCountSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCountSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCountSet.Image = global::SmoreControlLibrary.Properties.Resources.统计;
            this.buttonCountSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCountSet.Location = new System.Drawing.Point(0, 175);
            this.buttonCountSet.Name = "buttonCountSet";
            this.buttonCountSet.Size = new System.Drawing.Size(127, 35);
            this.buttonCountSet.TabIndex = 8;
            this.buttonCountSet.Text = "统计信息";
            this.buttonCountSet.UseVisualStyleBackColor = false;
            this.buttonCountSet.Click += new System.EventHandler(this.buttonCountSet_Click);
            // 
            // buttonProductInfoSet
            // 
            this.buttonProductInfoSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonProductInfoSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProductInfoSet.FlatAppearance.BorderSize = 0;
            this.buttonProductInfoSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProductInfoSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonProductInfoSet.Image = global::SmoreControlLibrary.Properties.Resources.物料;
            this.buttonProductInfoSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProductInfoSet.Location = new System.Drawing.Point(0, 140);
            this.buttonProductInfoSet.Name = "buttonProductInfoSet";
            this.buttonProductInfoSet.Size = new System.Drawing.Size(127, 35);
            this.buttonProductInfoSet.TabIndex = 7;
            this.buttonProductInfoSet.Text = "物料信息";
            this.buttonProductInfoSet.UseVisualStyleBackColor = false;
            this.buttonProductInfoSet.Click += new System.EventHandler(this.buttonProductInfoSet_Click);
            // 
            // buttonLightSet
            // 
            this.buttonLightSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonLightSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLightSet.FlatAppearance.BorderSize = 0;
            this.buttonLightSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLightSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLightSet.Image = global::SmoreControlLibrary.Properties.Resources.点光源;
            this.buttonLightSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLightSet.Location = new System.Drawing.Point(0, 105);
            this.buttonLightSet.Name = "buttonLightSet";
            this.buttonLightSet.Size = new System.Drawing.Size(127, 35);
            this.buttonLightSet.TabIndex = 6;
            this.buttonLightSet.Text = "光源设置";
            this.buttonLightSet.UseVisualStyleBackColor = false;
            this.buttonLightSet.Click += new System.EventHandler(this.buttonLightSet_Click);
            // 
            // buttonThresholdSet
            // 
            this.buttonThresholdSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonThresholdSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonThresholdSet.FlatAppearance.BorderSize = 0;
            this.buttonThresholdSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonThresholdSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonThresholdSet.Image = global::SmoreControlLibrary.Properties.Resources.默认_阈值告警;
            this.buttonThresholdSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonThresholdSet.Location = new System.Drawing.Point(0, 70);
            this.buttonThresholdSet.Name = "buttonThresholdSet";
            this.buttonThresholdSet.Size = new System.Drawing.Size(127, 35);
            this.buttonThresholdSet.TabIndex = 5;
            this.buttonThresholdSet.Text = "阈值设置";
            this.buttonThresholdSet.UseVisualStyleBackColor = false;
            this.buttonThresholdSet.Click += new System.EventHandler(this.buttonThresholdSet_Click);
            // 
            // buttonSaveSet
            // 
            this.buttonSaveSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonSaveSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSaveSet.FlatAppearance.BorderSize = 0;
            this.buttonSaveSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveSet.Image = global::SmoreControlLibrary.Properties.Resources.保存;
            this.buttonSaveSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveSet.Location = new System.Drawing.Point(0, 35);
            this.buttonSaveSet.Name = "buttonSaveSet";
            this.buttonSaveSet.Size = new System.Drawing.Size(127, 35);
            this.buttonSaveSet.TabIndex = 3;
            this.buttonSaveSet.Text = "保存设置";
            this.buttonSaveSet.UseVisualStyleBackColor = false;
            this.buttonSaveSet.Click += new System.EventHandler(this.buttonSaveSet_Click);
            // 
            // buttonSystemSet
            // 
            this.buttonSystemSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(167)))), ((int)(((byte)(182)))));
            this.buttonSystemSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSystemSet.FlatAppearance.BorderSize = 0;
            this.buttonSystemSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSystemSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSystemSet.Image = global::SmoreControlLibrary.Properties.Resources.系统设置;
            this.buttonSystemSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSystemSet.Location = new System.Drawing.Point(0, 0);
            this.buttonSystemSet.Name = "buttonSystemSet";
            this.buttonSystemSet.Size = new System.Drawing.Size(127, 35);
            this.buttonSystemSet.TabIndex = 2;
            this.buttonSystemSet.Text = "系统设置";
            this.buttonSystemSet.UseVisualStyleBackColor = false;
            this.buttonSystemSet.Click += new System.EventHandler(this.buttonSystemSet_Click);
            // 
            // panelFormSetHome
            // 
            this.panelFormSetHome.BackColor = System.Drawing.Color.Silver;
            this.panelFormSetHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormSetHome.Location = new System.Drawing.Point(127, 31);
            this.panelFormSetHome.Name = "panelFormSetHome";
            this.panelFormSetHome.Size = new System.Drawing.Size(903, 651);
            this.panelFormSetHome.TabIndex = 2;
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.Black;
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(998, 31);
            this.lbl.TabIndex = 4;
            this.lbl.Text = "设置";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 682);
            this.Controls.Add(this.panelFormSetHome);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSet";
            this.Load += new System.EventHandler(this.FormSet_Load);
            this.panelHead.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button buttonSystemSet;
        private System.Windows.Forms.Button buttonCountSet;
        private System.Windows.Forms.Button buttonProductInfoSet;
        private System.Windows.Forms.Button buttonLightSet;
        private System.Windows.Forms.Button buttonThresholdSet;
        private System.Windows.Forms.Button buttonSaveSet;
        private System.Windows.Forms.Panel panelFormSetHome;
        private SMButton smButton1;
        private SMButton smButton2;
        public System.Windows.Forms.Label lbl;
    }
}