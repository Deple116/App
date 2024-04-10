namespace SmoreControlLibrary
{
    partial class SMLogWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelLogForm = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewLog = new System.Windows.Forms.DataGridView();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.smButtonExportLog = new SmoreControlLibrary.SMButton();
            this.panel1.SuspendLayout();
            this.tableLayoutPanelLogForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panel1.Controls.Add(this.smButtonExportLog);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(350, 24);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志分析";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanelLogForm
            // 
            this.tableLayoutPanelLogForm.BackColor = System.Drawing.Color.LightGray;
            this.tableLayoutPanelLogForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tableLayoutPanelLogForm.ColumnCount = 1;
            this.tableLayoutPanelLogForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogForm.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanelLogForm.Controls.Add(this.dataGridViewLog, 0, 1);
            this.tableLayoutPanelLogForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLogForm.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLogForm.Name = "tableLayoutPanelLogForm";
            this.tableLayoutPanelLogForm.RowCount = 2;
            this.tableLayoutPanelLogForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelLogForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogForm.Size = new System.Drawing.Size(350, 323);
            this.tableLayoutPanelLogForm.TabIndex = 0;
            // 
            // dataGridViewLog
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLog.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridViewLog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTime,
            this.ColumnInfo});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewLog.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLog.GridColor = System.Drawing.Color.DimGray;
            this.dataGridViewLog.Location = new System.Drawing.Point(0, 24);
            this.dataGridViewLog.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewLog.Name = "dataGridViewLog";
            this.dataGridViewLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewLog.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewLog.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewLog.RowTemplate.Height = 23;
            this.dataGridViewLog.Size = new System.Drawing.Size(350, 299);
            this.dataGridViewLog.TabIndex = 2;
            this.dataGridViewLog.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLog_CellMouseEnter);
            this.dataGridViewLog.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLog_CellMouseLeave);
            this.dataGridViewLog.MouseHover += new System.EventHandler(this.dataGridViewLog_MouseHover);
            // 
            // ColumnTime
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ColumnTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnTime.HeaderText = "时间";
            this.ColumnTime.Name = "ColumnTime";
            // 
            // ColumnInfo
            // 
            this.ColumnInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ColumnInfo.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnInfo.HeaderText = "信息";
            this.ColumnInfo.Name = "ColumnInfo";
            this.ColumnInfo.ReadOnly = true;
            // 
            // smButtonExportLog
            // 
            this.smButtonExportLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smButtonExportLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.smButtonExportLog.BackColorShow = false;
            this.smButtonExportLog.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.smButtonExportLog.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonExportLog.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonExportLog.BtnImage = null;
            this.smButtonExportLog.BtnText = "导出";
            this.smButtonExportLog.ConerRadius = 24;
            this.smButtonExportLog.FillColor = System.Drawing.Color.Transparent;
            this.smButtonExportLog.IsRadius = false;
            this.smButtonExportLog.IsShowRect = false;
            this.smButtonExportLog.IsShowTips = false;
            this.smButtonExportLog.Location = new System.Drawing.Point(293, 4);
            this.smButtonExportLog.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonExportLog.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonExportLog.Name = "smButtonExportLog";
            this.smButtonExportLog.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonExportLog.RectWidth = 1;
            this.smButtonExportLog.Size = new System.Drawing.Size(52, 16);
            this.smButtonExportLog.TabIndex = 1;
            this.smButtonExportLog.TabStop = false;
            this.smButtonExportLog.TipsText = "";
            this.smButtonExportLog.BtnClick += new System.EventHandler(this.smButtonExportLog_BtnClick);
            // 
            // SMLogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelLogForm);
            this.Name = "SMLogWindow";
            this.Size = new System.Drawing.Size(350, 323);
            this.Load += new System.EventHandler(this.SMLogWindow_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanelLogForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLogForm;
        private System.Windows.Forms.DataGridView dataGridViewLog;
        private SMButton smButtonExportLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInfo;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
