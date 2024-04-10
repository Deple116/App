namespace SmoreControlLibrary.SMForm
{
    partial class SMSystemSet
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.smButtonSaveSystemSet = new SmoreControlLibrary.SMButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.smButtonGetModelInfo = new SmoreControlLibrary.SMButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxSystemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxSystemInfo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.smButtonSaveSystemSet);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 35);
            this.panel1.TabIndex = 2;
            // 
            // smButtonSaveSystemSet
            // 
            this.smButtonSaveSystemSet.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smButtonSaveSystemSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.smButtonSaveSystemSet.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.smButtonSaveSystemSet.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonSaveSystemSet.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonSaveSystemSet.BtnImage = null;
            this.smButtonSaveSystemSet.BtnText = "保存";
            this.smButtonSaveSystemSet.ConerRadius = 8;
            this.smButtonSaveSystemSet.FillColor = System.Drawing.Color.DarkGray;
            this.smButtonSaveSystemSet.IsRadius = true;
            this.smButtonSaveSystemSet.IsShowRect = true;
            this.smButtonSaveSystemSet.IsShowTips = false;
            this.smButtonSaveSystemSet.Location = new System.Drawing.Point(743, 6);
            this.smButtonSaveSystemSet.Name = "smButtonSaveSystemSet";
            this.smButtonSaveSystemSet.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonSaveSystemSet.RectWidth = 1;
            this.smButtonSaveSystemSet.Size = new System.Drawing.Size(60, 23);
            this.smButtonSaveSystemSet.TabIndex = 1;
            this.smButtonSaveSystemSet.TabStop = false;
            this.smButtonSaveSystemSet.TipsText = "";
            this.smButtonSaveSystemSet.BtnClick += new System.EventHandler(this.smButtonSaveSystemSet_BtnClick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 515);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panel3.Controls.Add(this.smButtonGetModelInfo);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 35);
            this.panel3.TabIndex = 1;
            // 
            // smButtonGetModelInfo
            // 
            this.smButtonGetModelInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smButtonGetModelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.smButtonGetModelInfo.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.smButtonGetModelInfo.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonGetModelInfo.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonGetModelInfo.BtnImage = null;
            this.smButtonGetModelInfo.BtnText = "获取";
            this.smButtonGetModelInfo.ConerRadius = 8;
            this.smButtonGetModelInfo.FillColor = System.Drawing.Color.DarkGray;
            this.smButtonGetModelInfo.IsRadius = true;
            this.smButtonGetModelInfo.IsShowRect = true;
            this.smButtonGetModelInfo.IsShowTips = false;
            this.smButtonGetModelInfo.Location = new System.Drawing.Point(743, 6);
            this.smButtonGetModelInfo.Name = "smButtonGetModelInfo";
            this.smButtonGetModelInfo.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonGetModelInfo.RectWidth = 1;
            this.smButtonGetModelInfo.Size = new System.Drawing.Size(60, 23);
            this.smButtonGetModelInfo.TabIndex = 2;
            this.smButtonGetModelInfo.TabStop = false;
            this.smButtonGetModelInfo.TipsText = "";
            this.smButtonGetModelInfo.BtnClick += new System.EventHandler(this.smButtonGetModelInfo_BtnClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "系统信息";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.textBoxSystemName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 40);
            this.panel2.TabIndex = 0;
            // 
            // textBoxSystemName
            // 
            this.textBoxSystemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.textBoxSystemName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSystemName.Location = new System.Drawing.Point(76, 11);
            this.textBoxSystemName.Name = "textBoxSystemName";
            this.textBoxSystemName.Size = new System.Drawing.Size(347, 19);
            this.textBoxSystemName.TabIndex = 1;
            this.textBoxSystemName.Text = "舍弗勒OCR AI检测系统";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "系统名称";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.textBoxSystemInfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 75);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(813, 440);
            this.panel4.TabIndex = 2;
            // 
            // textBoxSystemInfo
            // 
            this.textBoxSystemInfo.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSystemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSystemInfo.Location = new System.Drawing.Point(0, 0);
            this.textBoxSystemInfo.Multiline = true;
            this.textBoxSystemInfo.Name = "textBoxSystemInfo";
            this.textBoxSystemInfo.ReadOnly = true;
            this.textBoxSystemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSystemInfo.Size = new System.Drawing.Size(813, 440);
            this.textBoxSystemInfo.TabIndex = 0;
            // 
            // SMSystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "SMSystemSet";
            this.Size = new System.Drawing.Size(813, 550);
            this.Load += new System.EventHandler(this.SMSystemSet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private SMButton smButtonSaveSystemSet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSystemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxSystemInfo;
        private SMButton smButtonGetModelInfo;
    }
}
