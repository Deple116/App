namespace LogControlLibrary
{
    partial class SMButton
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
            this.lbl = new System.Windows.Forms.Label();
            this.lblTips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(209, 56);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "自定义按钮";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseClick);
            this.lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDown);
            this.lbl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseUp);
            // 
            // lblTips
            // 
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTips.BackColor = System.Drawing.Color.Transparent;
            this.lblTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips.ForeColor = System.Drawing.Color.White;
            this.lblTips.ImageIndex = 0;
            this.lblTips.Location = new System.Drawing.Point(177, 0);
            this.lblTips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(32, 30);
            this.lblTips.TabIndex = 2;
            this.lblTips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTips.Visible = false;
            // 
            // SMButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.lbl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SMButton";
            this.Size = new System.Drawing.Size(209, 56);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblTips;
    }
}
