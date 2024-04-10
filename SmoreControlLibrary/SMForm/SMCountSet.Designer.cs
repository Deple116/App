namespace SmoreControlLibrary.SMForm
{
    partial class SMCountSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.smButtonRefresh = new SmoreControlLibrary.SMButton();
            this.smButtonSwitch = new SmoreControlLibrary.SMButton();
            this.smButton2 = new SmoreControlLibrary.SMButton();
            this.smButton1 = new SmoreControlLibrary.SMButton();
            this.cmProductModel = new System.Windows.Forms.ComboBox();
            this.cmBatchNo = new System.Windows.Forms.ComboBox();
            this.cmTestResults = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 35);
            this.panel1.TabIndex = 1;
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
            this.label1.Text = "统计信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.smButtonRefresh);
            this.panel2.Controls.Add(this.smButtonSwitch);
            this.panel2.Controls.Add(this.smButton2);
            this.panel2.Controls.Add(this.smButton1);
            this.panel2.Controls.Add(this.cmProductModel);
            this.panel2.Controls.Add(this.cmBatchNo);
            this.panel2.Controls.Add(this.cmTestResults);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dateTimeEnd);
            this.panel2.Controls.Add(this.dateTimeStart);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 115);
            this.panel2.TabIndex = 2;
            // 
            // smButtonRefresh
            // 
            this.smButtonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.smButtonRefresh.BackColorShow = false;
            this.smButtonRefresh.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.刷新;
            this.smButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.smButtonRefresh.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.smButtonRefresh.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonRefresh.BtnForeColor = System.Drawing.Color.White;
            this.smButtonRefresh.BtnImage = null;
            this.smButtonRefresh.BtnText = "";
            this.smButtonRefresh.ConerRadius = 20;
            this.smButtonRefresh.FillColor = System.Drawing.Color.Transparent;
            this.smButtonRefresh.IsRadius = true;
            this.smButtonRefresh.IsShowRect = false;
            this.smButtonRefresh.IsShowTips = false;
            this.smButtonRefresh.Location = new System.Drawing.Point(513, 75);
            this.smButtonRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smButtonRefresh.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonRefresh.Name = "smButtonRefresh";
            this.smButtonRefresh.RectColor = System.Drawing.Color.Green;
            this.smButtonRefresh.RectWidth = 1;
            this.smButtonRefresh.Size = new System.Drawing.Size(104, 31);
            this.smButtonRefresh.TabIndex = 15;
            this.smButtonRefresh.TabStop = false;
            this.smButtonRefresh.TipsText = "";
            this.smButtonRefresh.BtnClick += new System.EventHandler(this.smButtonRefresh_BtnClick);
            // 
            // smButtonSwitch
            // 
            this.smButtonSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.smButtonSwitch.BackColorShow = false;
            this.smButtonSwitch.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.切换;
            this.smButtonSwitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.smButtonSwitch.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.smButtonSwitch.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonSwitch.BtnForeColor = System.Drawing.Color.White;
            this.smButtonSwitch.BtnImage = null;
            this.smButtonSwitch.BtnText = "";
            this.smButtonSwitch.ConerRadius = 20;
            this.smButtonSwitch.FillColor = System.Drawing.Color.Transparent;
            this.smButtonSwitch.IsRadius = true;
            this.smButtonSwitch.IsShowRect = false;
            this.smButtonSwitch.IsShowTips = false;
            this.smButtonSwitch.Location = new System.Drawing.Point(658, 75);
            this.smButtonSwitch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smButtonSwitch.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonSwitch.Name = "smButtonSwitch";
            this.smButtonSwitch.RectColor = System.Drawing.Color.Green;
            this.smButtonSwitch.RectWidth = 1;
            this.smButtonSwitch.Size = new System.Drawing.Size(104, 31);
            this.smButtonSwitch.TabIndex = 14;
            this.smButtonSwitch.TabStop = false;
            this.smButtonSwitch.TipsText = "";
            this.smButtonSwitch.Visible = false;
            // 
            // smButton2
            // 
            this.smButton2.BackColor = System.Drawing.Color.Green;
            this.smButton2.BackColorShow = false;
            this.smButton2.BtnBackColor = System.Drawing.Color.Green;
            this.smButton2.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton2.BtnForeColor = System.Drawing.Color.White;
            this.smButton2.BtnImage = null;
            this.smButton2.BtnText = "导出";
            this.smButton2.ConerRadius = 20;
            this.smButton2.FillColor = System.Drawing.Color.Transparent;
            this.smButton2.IsRadius = true;
            this.smButton2.IsShowRect = false;
            this.smButton2.IsShowTips = false;
            this.smButton2.Location = new System.Drawing.Point(658, 6);
            this.smButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smButton2.MouseUpColor = System.Drawing.Color.Empty;
            this.smButton2.Name = "smButton2";
            this.smButton2.RectColor = System.Drawing.Color.Green;
            this.smButton2.RectWidth = 1;
            this.smButton2.Size = new System.Drawing.Size(104, 31);
            this.smButton2.TabIndex = 13;
            this.smButton2.TabStop = false;
            this.smButton2.TipsText = "";
            // 
            // smButton1
            // 
            this.smButton1.BackColor = System.Drawing.Color.Green;
            this.smButton1.BackColorShow = false;
            this.smButton1.BtnBackColor = System.Drawing.Color.Green;
            this.smButton1.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton1.BtnForeColor = System.Drawing.Color.White;
            this.smButton1.BtnImage = null;
            this.smButton1.BtnText = "搜索";
            this.smButton1.ConerRadius = 20;
            this.smButton1.FillColor = System.Drawing.Color.Transparent;
            this.smButton1.IsRadius = true;
            this.smButton1.IsShowRect = false;
            this.smButton1.IsShowTips = false;
            this.smButton1.Location = new System.Drawing.Point(513, 6);
            this.smButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smButton1.MouseUpColor = System.Drawing.Color.Empty;
            this.smButton1.Name = "smButton1";
            this.smButton1.RectColor = System.Drawing.Color.Green;
            this.smButton1.RectWidth = 1;
            this.smButton1.Size = new System.Drawing.Size(104, 31);
            this.smButton1.TabIndex = 12;
            this.smButton1.TabStop = false;
            this.smButton1.TipsText = "";
            this.smButton1.BtnClick += new System.EventHandler(this.smButton1_BtnClick);
            // 
            // cmProductModel
            // 
            this.cmProductModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.cmProductModel.FormattingEnabled = true;
            this.cmProductModel.Items.AddRange(new object[] {
            "ALL"});
            this.cmProductModel.Location = new System.Drawing.Point(85, 88);
            this.cmProductModel.Name = "cmProductModel";
            this.cmProductModel.Size = new System.Drawing.Size(342, 20);
            this.cmProductModel.TabIndex = 10;
            this.cmProductModel.Text = "ALL";
            // 
            // cmBatchNo
            // 
            this.cmBatchNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.cmBatchNo.FormattingEnabled = true;
            this.cmBatchNo.Items.AddRange(new object[] {
            "ALL"});
            this.cmBatchNo.Location = new System.Drawing.Point(85, 60);
            this.cmBatchNo.Name = "cmBatchNo";
            this.cmBatchNo.Size = new System.Drawing.Size(342, 20);
            this.cmBatchNo.TabIndex = 9;
            this.cmBatchNo.Text = "ALL";
            // 
            // cmTestResults
            // 
            this.cmTestResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.cmTestResults.FormattingEnabled = true;
            this.cmTestResults.Items.AddRange(new object[] {
            "ALL"});
            this.cmTestResults.Location = new System.Drawing.Point(85, 32);
            this.cmTestResults.Name = "cmTestResults";
            this.cmTestResults.Size = new System.Drawing.Size(342, 20);
            this.cmTestResults.TabIndex = 8;
            this.cmTestResults.Text = "ALL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(246, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "至";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CalendarFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(270, 4);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(157, 21);
            this.dateTimeEnd.TabIndex = 6;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CalendarFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(85, 4);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(157, 21);
            this.dateTimeStart.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(11, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "产品型号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "批次号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(11, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "检测结果：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "时间：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelChart);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 150);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 400);
            this.panel3.TabIndex = 3;
            // 
            // panelChart
            // 
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 0);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(813, 400);
            this.panelChart.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(813, 400);
            this.dataGridView1.TabIndex = 0;
            // 
            // SMCountSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SMCountSet";
            this.Size = new System.Drawing.Size(813, 550);
            this.Load += new System.EventHandler(this.SMCountSet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.ComboBox cmProductModel;
        private System.Windows.Forms.ComboBox cmBatchNo;
        private System.Windows.Forms.ComboBox cmTestResults;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private SMButton smButton2;
        private SMButton smButton1;
        private SMButton smButtonSwitch;
        private System.Windows.Forms.Panel panelChart;
        private SMButton smButtonRefresh;
    }
}
