using SMLogControlLibrary;

namespace SmoreVision
{
    partial class FormMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.smLogWindow = new SMLogControlLibrary.SMLogWindow();
            this.smInfoWindow1 = new SmoreControlLibrary.SMInfo.SMInfoWindow();
            this.smDataWindow1 = new SmoreControlLibrary.SMData.SMDataWindow();
            this.tableLayoutPanelMainHome = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.smButtonRun = new SmoreControlLibrary.SMButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPImg = new System.Windows.Forms.TabPage();
            this.smImageWindow1 = new SmoreControlLibrary.SMImage.SMImageWindow();
            this.tPDataShow = new System.Windows.Forms.TabPage();
            this.panelDataShow = new System.Windows.Forms.Panel();
            this.tPDataStatistics = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tPChart = new System.Windows.Forms.TabPage();
            this.panelDataStatistics = new System.Windows.Forms.Panel();
            this.tPSql = new System.Windows.Forms.TabPage();
            this.panelSql = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanelMainHome.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tPImg.SuspendLayout();
            this.tPDataShow.SuspendLayout();
            this.tPDataStatistics.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tPChart.SuspendLayout();
            this.tPSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.smLogWindow, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.smInfoWindow1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.smDataWindow1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(805, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 731);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // smLogWindow
            // 
            this.smLogWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smLogWindow.Location = new System.Drawing.Point(3, 514);
            this.smLogWindow.LogPathValue = null;
            this.smLogWindow.Name = "smLogWindow";
            this.smLogWindow.Size = new System.Drawing.Size(321, 214);
            this.smLogWindow.TabIndex = 0;
            this.smLogWindow.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            // 
            // smInfoWindow1
            // 
            this.smInfoWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smInfoWindow1.Location = new System.Drawing.Point(3, 3);
            this.smInfoWindow1.ModelDate = "";
            this.smInfoWindow1.ModelVersion = "V1.0";
            this.smInfoWindow1.Name = "smInfoWindow1";
            this.smInfoWindow1.ProductModel = "";
            this.smInfoWindow1.Size = new System.Drawing.Size(321, 213);
            this.smInfoWindow1.TabIndex = 1;
            // 
            // smDataWindow1
            // 
            this.smDataWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smDataWindow1.Location = new System.Drawing.Point(3, 222);
            this.smDataWindow1.Name = "smDataWindow1";
            this.smDataWindow1.NGNum = 0;
            this.smDataWindow1.OKNum = 0;
            this.smDataWindow1.RateNum = 0D;
            this.smDataWindow1.Size = new System.Drawing.Size(321, 286);
            this.smDataWindow1.TabIndex = 2;
            this.smDataWindow1.TotalNum = 0;
            this.smDataWindow1.ClearData += new System.EventHandler(this.smDataWindow1_ClearData);
            // 
            // tableLayoutPanelMainHome
            // 
            this.tableLayoutPanelMainHome.ColumnCount = 2;
            this.tableLayoutPanelMainHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMainHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMainHome.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanelMainHome.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanelMainHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMainHome.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanelMainHome.Name = "tableLayoutPanelMainHome";
            this.tableLayoutPanelMainHome.RowCount = 3;
            this.tableLayoutPanelMainHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanelMainHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMainHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMainHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMainHome.Size = new System.Drawing.Size(805, 731);
            this.tableLayoutPanelMainHome.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(231)))));
            this.tableLayoutPanelMainHome.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(799, 40);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.smButtonRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(686, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(113, 40);
            this.panel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "自动运行";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // smButtonRun
            // 
            this.smButtonRun.BackColor = System.Drawing.Color.Transparent;
            this.smButtonRun.BackColorShow = false;
            this.smButtonRun.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonRun.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonRun.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonRun.BtnImage = global::SmoreVision.Properties.Resources.auto_off;
            this.smButtonRun.BtnText = null;
            this.smButtonRun.ConerRadius = 24;
            this.smButtonRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.smButtonRun.Enabled = false;
            this.smButtonRun.FillColor = System.Drawing.Color.Transparent;
            this.smButtonRun.IsRadius = false;
            this.smButtonRun.IsShowRect = false;
            this.smButtonRun.IsShowTips = false;
            this.smButtonRun.Location = new System.Drawing.Point(78, 0);
            this.smButtonRun.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonRun.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonRun.Name = "smButtonRun";
            this.smButtonRun.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonRun.RectWidth = 1;
            this.smButtonRun.Size = new System.Drawing.Size(35, 40);
            this.smButtonRun.TabIndex = 0;
            this.smButtonRun.TabStop = false;
            this.smButtonRun.TipsText = "";
            this.smButtonRun.BtnClick += new System.EventHandler(this.smButtonRun_BtnClick);
            // 
            // tabControl1
            // 
            this.tableLayoutPanelMainHome.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tPImg);
            this.tabControl1.Controls.Add(this.tPDataShow);
            this.tabControl1.Controls.Add(this.tPDataStatistics);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 49);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanelMainHome.SetRowSpan(this.tabControl1, 2);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 679);
            this.tabControl1.TabIndex = 5;
            // 
            // tPImg
            // 
            this.tPImg.Controls.Add(this.smImageWindow1);
            this.tPImg.Location = new System.Drawing.Point(4, 22);
            this.tPImg.Name = "tPImg";
            this.tPImg.Padding = new System.Windows.Forms.Padding(3);
            this.tPImg.Size = new System.Drawing.Size(791, 653);
            this.tPImg.TabIndex = 0;
            this.tPImg.Text = "图像窗口";
            this.tPImg.UseVisualStyleBackColor = true;
            // 
            // smImageWindow1
            // 
            this.smImageWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smImageWindow1.IamgeWindowCT = "0ms";
            this.smImageWindow1.IamgeWindowRatio = null;
            this.smImageWindow1.IamgeWindowTotal = null;
            this.smImageWindow1.IamgeWindowType = "CLS";
            this.smImageWindow1.ImageWindowName = "CCD1";
            this.smImageWindow1.ImageWindowNG = 0;
            this.smImageWindow1.ImageWindowOK = 0;
            this.smImageWindow1.Location = new System.Drawing.Point(3, 3);
            this.smImageWindow1.Margin = new System.Windows.Forms.Padding(4);
            this.smImageWindow1.Name = "smImageWindow1";
            this.smImageWindow1.ShowManualButton = false;
            this.smImageWindow1.Size = new System.Drawing.Size(785, 647);
            this.smImageWindow1.TabIndex = 4;
            this.smImageWindow1.BtnRunSinglePicClick += new System.EventHandler(this.smImageWindow1_BtnRunSinglePicClick);
            this.smImageWindow1.BtnRunFolderPicClick += new System.EventHandler(this.smImageWindow1_BtnRunFolderPicClick);
            this.smImageWindow1.TriggerCamera += new System.EventHandler(this.smImageWindow1_TriggerCamera);
            this.smImageWindow1.Load += new System.EventHandler(this.smImageWindow1_Load);
            // 
            // tPDataShow
            // 
            this.tPDataShow.Controls.Add(this.panelDataShow);
            this.tPDataShow.Location = new System.Drawing.Point(4, 22);
            this.tPDataShow.Name = "tPDataShow";
            this.tPDataShow.Padding = new System.Windows.Forms.Padding(3);
            this.tPDataShow.Size = new System.Drawing.Size(791, 653);
            this.tPDataShow.TabIndex = 1;
            this.tPDataShow.Text = "数据显示";
            this.tPDataShow.UseVisualStyleBackColor = true;
            // 
            // panelDataShow
            // 
            this.panelDataShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataShow.Location = new System.Drawing.Point(3, 3);
            this.panelDataShow.Name = "panelDataShow";
            this.panelDataShow.Size = new System.Drawing.Size(785, 647);
            this.panelDataShow.TabIndex = 0;
            // 
            // tPDataStatistics
            // 
            this.tPDataStatistics.Controls.Add(this.tabControl2);
            this.tPDataStatistics.Location = new System.Drawing.Point(4, 22);
            this.tPDataStatistics.Name = "tPDataStatistics";
            this.tPDataStatistics.Size = new System.Drawing.Size(791, 653);
            this.tPDataStatistics.TabIndex = 2;
            this.tPDataStatistics.Text = "数据统计";
            this.tPDataStatistics.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tPSql);
            this.tabControl2.Controls.Add(this.tPChart);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(791, 653);
            this.tabControl2.TabIndex = 1;
            // 
            // tPChart
            // 
            this.tPChart.Controls.Add(this.panelDataStatistics);
            this.tPChart.Location = new System.Drawing.Point(4, 22);
            this.tPChart.Name = "tPChart";
            this.tPChart.Padding = new System.Windows.Forms.Padding(3);
            this.tPChart.Size = new System.Drawing.Size(783, 627);
            this.tPChart.TabIndex = 0;
            this.tPChart.Text = "图表";
            this.tPChart.UseVisualStyleBackColor = true;
            // 
            // panelDataStatistics
            // 
            this.panelDataStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataStatistics.Location = new System.Drawing.Point(3, 3);
            this.panelDataStatistics.Name = "panelDataStatistics";
            this.panelDataStatistics.Size = new System.Drawing.Size(777, 621);
            this.panelDataStatistics.TabIndex = 0;
            // 
            // tPSql
            // 
            this.tPSql.Controls.Add(this.panelSql);
            this.tPSql.Location = new System.Drawing.Point(4, 22);
            this.tPSql.Name = "tPSql";
            this.tPSql.Padding = new System.Windows.Forms.Padding(3);
            this.tPSql.Size = new System.Drawing.Size(783, 627);
            this.tPSql.TabIndex = 1;
            this.tPSql.Text = "数据库";
            this.tPSql.UseVisualStyleBackColor = true;
            // 
            // panelSql
            // 
            this.panelSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSql.Location = new System.Drawing.Point(3, 3);
            this.panelSql.Name = "panelSql";
            this.panelSql.Size = new System.Drawing.Size(777, 621);
            this.panelSql.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1132, 761);
            this.Controls.Add(this.tableLayoutPanelMainHome);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanelMainHome, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanelMainHome.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tPImg.ResumeLayout(false);
            this.tPDataShow.ResumeLayout(false);
            this.tPDataStatistics.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tPChart.ResumeLayout(false);
            this.tPSql.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SMLogWindow smLogWindow;
        private SmoreControlLibrary.SMInfo.SMInfoWindow smInfoWindow1;
        private SmoreControlLibrary.SMData.SMDataWindow smDataWindow1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMainHome;
        private System.Windows.Forms.Panel panel3;
        private SmoreControlLibrary.SMButton smButtonRun;
        private System.Windows.Forms.Label label1;
        private SmoreControlLibrary.SMImage.SMImageWindow smImageWindow1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPImg;
        private System.Windows.Forms.TabPage tPDataShow;
        private System.Windows.Forms.Panel panelDataShow;
        private System.Windows.Forms.TabPage tPDataStatistics;
        private System.Windows.Forms.Panel panelDataStatistics;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tPChart;
        private System.Windows.Forms.TabPage tPSql;
        private System.Windows.Forms.Panel panelSql;
    }
}

