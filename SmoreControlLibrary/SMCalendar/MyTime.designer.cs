﻿namespace DateSVN.Controls
{
    partial class MyTime
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.myFlowLayoutPanel1 = new DateSVN.Controls.MyFlowLayoutPanel();
            this.waterTextBox1 = new DateSVN.Controls.WaterTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.myFlowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // myFlowLayoutPanel1
            // 
            this.myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.myFlowLayoutPanel1.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(196)))), ((int)(((byte)(127)))));
            this.myFlowLayoutPanel1.BorderLineWidth = 3;
            this.myFlowLayoutPanel1.Controls.Add(this.waterTextBox1);
            this.myFlowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myFlowLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.myFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.myFlowLayoutPanel1.Name = "myFlowLayoutPanel1";
            this.myFlowLayoutPanel1.Size = new System.Drawing.Size(487, 26);
            this.myFlowLayoutPanel1.TabIndex = 3;
            // 
            // waterTextBox1
            // 
            this.waterTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.waterTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waterTextBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.waterTextBox1.Location = new System.Drawing.Point(0, 0);
            this.waterTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.waterTextBox1.Name = "waterTextBox1";
            this.waterTextBox1.Size = new System.Drawing.Size(447, 19);
            this.waterTextBox1.TabIndex = 1;
            this.waterTextBox1.WordWrap = false;
            this.waterTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.waterTextBox1_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(447, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 22);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MyTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.myFlowLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MyTime";
            this.Size = new System.Drawing.Size(487, 27);
            this.myFlowLayoutPanel1.ResumeLayout(false);
            this.myFlowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WaterTextBox waterTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MyFlowLayoutPanel myFlowLayoutPanel1;
    }
}
