namespace SmoreControlLibrary.SMImage
{
    partial class SMImageWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMImageWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelNG = new System.Windows.Forms.Label();
            this.plResult = new System.Windows.Forms.Panel();
            this.labelOK = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plHeartSignal = new System.Windows.Forms.Panel();
            this.labelIamgeWindowCT = new System.Windows.Forms.Label();
            this.labelImageWindowName = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBoxImgShow = new SmoreControlLibrary.SMImage.SMPictureEdit();
            this.panel8 = new System.Windows.Forms.Panel();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelRatio = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 471);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(231)))));
            this.panel2.Controls.Add(this.labelNG);
            this.panel2.Controls.Add(this.plResult);
            this.panel2.Controls.Add(this.labelOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(9);
            this.panel2.Size = new System.Drawing.Size(350, 35);
            this.panel2.TabIndex = 5;
            // 
            // labelNG
            // 
            this.labelNG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNG.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNG.ForeColor = System.Drawing.Color.Red;
            this.labelNG.Location = new System.Drawing.Point(116, 9);
            this.labelNG.Name = "labelNG";
            this.labelNG.Size = new System.Drawing.Size(106, 18);
            this.labelNG.TabIndex = 4;
            this.labelNG.Text = "NG：0";
            this.labelNG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // plResult
            // 
            this.plResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plResult.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.OK;
            this.plResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.plResult.Location = new System.Drawing.Point(263, 0);
            this.plResult.Margin = new System.Windows.Forms.Padding(0);
            this.plResult.Name = "plResult";
            this.plResult.Size = new System.Drawing.Size(87, 35);
            this.plResult.TabIndex = 1;
            // 
            // labelOK
            // 
            this.labelOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOK.ForeColor = System.Drawing.Color.Green;
            this.labelOK.Location = new System.Drawing.Point(9, 9);
            this.labelOK.Name = "labelOK";
            this.labelOK.Size = new System.Drawing.Size(106, 18);
            this.labelOK.TabIndex = 4;
            this.labelOK.Text = "OK：0";
            this.labelOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panel1.Controls.Add(this.plHeartSignal);
            this.panel1.Controls.Add(this.labelIamgeWindowCT);
            this.panel1.Controls.Add(this.labelImageWindowName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(350, 40);
            this.panel1.TabIndex = 2;
            // 
            // plHeartSignal
            // 
            this.plHeartSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plHeartSignal.BackColor = System.Drawing.Color.Yellow;
            this.plHeartSignal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plHeartSignal.Location = new System.Drawing.Point(68, 12);
            this.plHeartSignal.Margin = new System.Windows.Forms.Padding(2);
            this.plHeartSignal.Name = "plHeartSignal";
            this.plHeartSignal.Size = new System.Drawing.Size(16, 16);
            this.plHeartSignal.TabIndex = 7;
            // 
            // labelIamgeWindowCT
            // 
            this.labelIamgeWindowCT.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelIamgeWindowCT.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIamgeWindowCT.ForeColor = System.Drawing.Color.White;
            this.labelIamgeWindowCT.Location = new System.Drawing.Point(260, 9);
            this.labelIamgeWindowCT.Name = "labelIamgeWindowCT";
            this.labelIamgeWindowCT.Size = new System.Drawing.Size(81, 22);
            this.labelIamgeWindowCT.TabIndex = 1;
            this.labelIamgeWindowCT.Text = "0ms";
            this.labelIamgeWindowCT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelIamgeWindowCT.Visible = false;
            // 
            // labelImageWindowName
            // 
            this.labelImageWindowName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelImageWindowName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelImageWindowName.ForeColor = System.Drawing.Color.White;
            this.labelImageWindowName.Location = new System.Drawing.Point(9, 9);
            this.labelImageWindowName.Margin = new System.Windows.Forms.Padding(0);
            this.labelImageWindowName.Name = "labelImageWindowName";
            this.labelImageWindowName.Size = new System.Drawing.Size(81, 22);
            this.labelImageWindowName.TabIndex = 0;
            this.labelImageWindowName.Text = "CCD1";
            this.labelImageWindowName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pictureBoxImgShow);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 110);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(350, 361);
            this.panel7.TabIndex = 6;
            // 
            // pictureBoxImgShow
            // 
            this.pictureBoxImgShow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxImgShow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxImgShow.BackgroundImage")));
            this.pictureBoxImgShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxImgShow.CustomBorderColor = System.Drawing.Color.Black;
            this.pictureBoxImgShow.CustomBorderWidth = 1;
            this.pictureBoxImgShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImgShow.Image = null;
            this.pictureBoxImgShow.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImgShow.MinimumSize = new System.Drawing.Size(10, 10);
            this.pictureBoxImgShow.Name = "pictureBoxImgShow";
            this.pictureBoxImgShow.Size = new System.Drawing.Size(350, 361);
            this.pictureBoxImgShow.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(231)))));
            this.panel8.Controls.Add(this.labelTotal);
            this.panel8.Controls.Add(this.labelRatio);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 75);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(350, 35);
            this.panel8.TabIndex = 7;
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTotal.Location = new System.Drawing.Point(9, 10);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(106, 18);
            this.labelTotal.TabIndex = 3;
            this.labelTotal.Text = "Total：0";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRatio
            // 
            this.labelRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRatio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRatio.ForeColor = System.Drawing.Color.Black;
            this.labelRatio.Location = new System.Drawing.Point(116, 10);
            this.labelRatio.Name = "labelRatio";
            this.labelRatio.Size = new System.Drawing.Size(106, 18);
            this.labelRatio.TabIndex = 2;
            this.labelRatio.Text = "Ratio：0.00%";
            this.labelRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(245, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(105, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            this.panel6.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.group_color;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(40, 5);
            this.panel6.Margin = new System.Windows.Forms.Padding(5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(25, 26);
            this.panel6.TabIndex = 2;
            this.panel6.Visible = false;
            this.panel6.Click += new System.EventHandler(this.panel6_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.camera_color;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(5, 5);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(25, 26);
            this.panel4.TabIndex = 0;
            this.panel4.Click += new System.EventHandler(this.panel4_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightGray;
            this.panel5.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.file_color;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(75, 5);
            this.panel5.Margin = new System.Windows.Forms.Padding(5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(25, 26);
            this.panel5.TabIndex = 1;
            this.panel5.Visible = false;
            this.panel5.Click += new System.EventHandler(this.panel5_Click);
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 471);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 35);
            this.panel3.TabIndex = 3;
            // 
            // SMImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Name = "SMImageWindow";
            this.Size = new System.Drawing.Size(350, 506);
            this.Load += new System.EventHandler(this.SMImageWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelImageWindowName;
        private System.Windows.Forms.Label labelIamgeWindowCT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label labelRatio;
        private System.Windows.Forms.Panel plResult;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelNG;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Panel plHeartSignal;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private SMPictureEdit pictureBoxImgShow;
    }
}
