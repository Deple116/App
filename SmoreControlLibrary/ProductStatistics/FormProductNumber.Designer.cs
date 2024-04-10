
namespace SmoreControlLibrary.ProductStatistics
{
    partial class FormProductNumber
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProductNumber));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimeEnd = new DateSVN.Controls.MyTime();
            this.dateTimeStart = new DateSVN.Controls.MyTime();
            this.gBselect = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radionButtonDay = new System.Windows.Forms.RadioButton();
            this.radioButtonHour = new System.Windows.Forms.RadioButton();
            this.smButton1 = new SmoreControlLibrary.SMButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.gBselect.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 82);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 552);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.chart2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.chart1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(823, 552);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.tableLayoutPanel2.SetColumnSpan(this.chart2, 2);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(2, 278);
            this.chart2.Margin = new System.Windows.Forms.Padding(2);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Label = "#VAL{P1}";
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 2;
            series1.MarkerSize = 3;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Current";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(819, 272);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            title1.BackColor = System.Drawing.Color.Transparent;
            title1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Yield";
            this.chart2.Titles.Add(title1);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.tableLayoutPanel2.SetColumnSpan(this.chart1, 2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(2, 2);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Label = "#VAL";
            series2.Legend = "Legend1";
            series2.Name = "Total";
            series3.ChartArea = "ChartArea1";
            series3.Label = "#VAL";
            series3.Legend = "Legend1";
            series3.Name = "OK";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "NG";
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(819, 272);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            title2.BackColor = System.Drawing.Color.Transparent;
            title2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "UPH";
            this.chart1.Titles.Add(title2);
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimeEnd);
            this.panel1.Controls.Add(this.dateTimeStart);
            this.panel1.Controls.Add(this.gBselect);
            this.panel1.Controls.Add(this.smButton1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 76);
            this.panel1.TabIndex = 2;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.dateTimeEnd.BorderThickness = 2;
            this.dateTimeEnd.DateFormat = "yyyy-MM-dd HH:mm";
            this.dateTimeEnd.Location = new System.Drawing.Point(210, 27);
            this.dateTimeEnd.Margin = new System.Windows.Forms.Padding(0);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.PictrueBoxImage = ((System.Drawing.Image)(resources.GetObject("dateTimeEnd.PictrueBoxImage")));
            this.dateTimeEnd.Size = new System.Drawing.Size(158, 25);
            this.dateTimeEnd.StartDatePosition = 1;
            this.dateTimeEnd.TabIndex = 18;
            this.dateTimeEnd.TextFont = new System.Drawing.Font("微软雅黑", 10.5F);
            this.dateTimeEnd.TextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(103)))), ((int)(((byte)(103)))));
            this.dateTimeEnd.Value = new System.DateTime(2023, 6, 25, 16, 0, 0, 0);
            this.dateTimeEnd.WaterText = "";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.dateTimeStart.BorderThickness = 2;
            this.dateTimeStart.DateFormat = "yyyy-MM-dd HH:mm";
            this.dateTimeStart.Location = new System.Drawing.Point(11, 27);
            this.dateTimeStart.Margin = new System.Windows.Forms.Padding(0);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.PictrueBoxImage = ((System.Drawing.Image)(resources.GetObject("dateTimeStart.PictrueBoxImage")));
            this.dateTimeStart.Size = new System.Drawing.Size(158, 25);
            this.dateTimeStart.StartDatePosition = 1;
            this.dateTimeStart.TabIndex = 17;
            this.dateTimeStart.TextFont = new System.Drawing.Font("微软雅黑", 10.5F);
            this.dateTimeStart.TextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(103)))), ((int)(((byte)(103)))));
            this.dateTimeStart.Value = new System.DateTime(2023, 6, 25, 16, 0, 0, 0);
            this.dateTimeStart.WaterText = "";
            // 
            // gBselect
            // 
            this.gBselect.Controls.Add(this.label2);
            this.gBselect.Controls.Add(this.label1);
            this.gBselect.Controls.Add(this.radionButtonDay);
            this.gBselect.Controls.Add(this.radioButtonHour);
            this.gBselect.Dock = System.Windows.Forms.DockStyle.Right;
            this.gBselect.Location = new System.Drawing.Point(559, 0);
            this.gBselect.Name = "gBselect";
            this.gBselect.Size = new System.Drawing.Size(264, 76);
            this.gBselect.TabIndex = 16;
            this.gBselect.TabStop = false;
            this.gBselect.Text = "select";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(167, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "（实时刷新）";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(128, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "（不实时刷新）";
            // 
            // radionButtonDay
            // 
            this.radionButtonDay.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radionButtonDay.AutoSize = true;
            this.radionButtonDay.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radionButtonDay.Location = new System.Drawing.Point(32, 44);
            this.radionButtonDay.Margin = new System.Windows.Forms.Padding(2);
            this.radionButtonDay.Name = "radionButtonDay";
            this.radionButtonDay.Size = new System.Drawing.Size(89, 19);
            this.radionButtonDay.TabIndex = 9;
            this.radionButtonDay.Text = "按天显示";
            this.radionButtonDay.UseVisualStyleBackColor = true;
            // 
            // radioButtonHour
            // 
            this.radioButtonHour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radioButtonHour.AutoSize = true;
            this.radioButtonHour.Checked = true;
            this.radioButtonHour.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonHour.Location = new System.Drawing.Point(32, 11);
            this.radioButtonHour.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonHour.Name = "radioButtonHour";
            this.radioButtonHour.Size = new System.Drawing.Size(139, 19);
            this.radioButtonHour.TabIndex = 8;
            this.radioButtonHour.TabStop = true;
            this.radioButtonHour.Text = "最近24小时显示";
            this.radioButtonHour.UseVisualStyleBackColor = true;
            // 
            // smButton1
            // 
            this.smButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smButton1.BackColor = System.Drawing.Color.Green;
            this.smButton1.BackColorShow = false;
            this.smButton1.BtnBackColor = System.Drawing.Color.Green;
            this.smButton1.BtnFont = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton1.BtnForeColor = System.Drawing.Color.White;
            this.smButton1.BtnImage = null;
            this.smButton1.BtnText = "刷新";
            this.smButton1.ConerRadius = 20;
            this.smButton1.FillColor = System.Drawing.Color.Transparent;
            this.smButton1.IsRadius = true;
            this.smButton1.IsShowRect = false;
            this.smButton1.IsShowTips = false;
            this.smButton1.Location = new System.Drawing.Point(395, 9);
            this.smButton1.Margin = new System.Windows.Forms.Padding(4);
            this.smButton1.MouseUpColor = System.Drawing.Color.Empty;
            this.smButton1.Name = "smButton1";
            this.smButton1.RectColor = System.Drawing.Color.Green;
            this.smButton1.RectWidth = 1;
            this.smButton1.Size = new System.Drawing.Size(129, 58);
            this.smButton1.TabIndex = 13;
            this.smButton1.TabStop = false;
            this.smButton1.TipsText = "";
            this.smButton1.BtnClick += new System.EventHandler(this.smButton1_BtnClick);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(181, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "至";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 636);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FormProductNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormProductNumber";
            this.Size = new System.Drawing.Size(827, 636);
            this.Load += new System.EventHandler(this.FormProductNumber_Load);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gBselect.ResumeLayout(false);
            this.gBselect.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radionButtonDay;
        private System.Windows.Forms.RadioButton radioButtonHour;
        private System.Windows.Forms.Label label1;
        private SMButton smButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gBselect;
        private DateSVN.Controls.MyTime dateTimeStart;
        private DateSVN.Controls.MyTime dateTimeEnd;
    }
}