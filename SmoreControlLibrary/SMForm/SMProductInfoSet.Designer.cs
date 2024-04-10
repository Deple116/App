namespace SmoreControlLibrary.SMForm
{
    partial class SMProductInfoSet
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
            this.smBtnSave = new SmoreControlLibrary.SMButton();
            this.smBtnDelete = new SmoreControlLibrary.SMButton();
            this.smBtnAdd = new SmoreControlLibrary.SMButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.smProductInfoSetTitle2 = new SmoreControlLibrary.SMForm.SMProductInfoSetTitle();
            this.plProductSetHome = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.panel1.TabIndex = 2;
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
            this.label1.Text = "物料信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.smBtnSave);
            this.panel2.Controls.Add(this.smBtnDelete);
            this.panel2.Controls.Add(this.smBtnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 38);
            this.panel2.TabIndex = 3;
            // 
            // smBtnSave
            // 
            this.smBtnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smBtnSave.BackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnSave.BtnBackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnSave.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smBtnSave.BtnForeColor = System.Drawing.Color.Black;
            this.smBtnSave.BtnImage = null;
            this.smBtnSave.BtnText = "保存";
            this.smBtnSave.ConerRadius = 15;
            this.smBtnSave.FillColor = System.Drawing.Color.Transparent;
            this.smBtnSave.IsRadius = true;
            this.smBtnSave.IsShowRect = false;
            this.smBtnSave.IsShowTips = false;
            this.smBtnSave.Location = new System.Drawing.Point(680, 6);
            this.smBtnSave.Name = "smBtnSave";
            this.smBtnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smBtnSave.RectWidth = 1;
            this.smBtnSave.Size = new System.Drawing.Size(58, 25);
            this.smBtnSave.TabIndex = 1;
            this.smBtnSave.TabStop = false;
            this.smBtnSave.TipsText = "";
            this.smBtnSave.BtnClick += new System.EventHandler(this.smBtnSave_BtnClick);
            // 
            // smBtnDelete
            // 
            this.smBtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smBtnDelete.BackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnDelete.BtnBackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnDelete.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smBtnDelete.BtnForeColor = System.Drawing.Color.Black;
            this.smBtnDelete.BtnImage = null;
            this.smBtnDelete.BtnText = "删除";
            this.smBtnDelete.ConerRadius = 15;
            this.smBtnDelete.FillColor = System.Drawing.Color.Transparent;
            this.smBtnDelete.IsRadius = true;
            this.smBtnDelete.IsShowRect = false;
            this.smBtnDelete.IsShowTips = false;
            this.smBtnDelete.Location = new System.Drawing.Point(746, 6);
            this.smBtnDelete.Name = "smBtnDelete";
            this.smBtnDelete.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smBtnDelete.RectWidth = 1;
            this.smBtnDelete.Size = new System.Drawing.Size(58, 25);
            this.smBtnDelete.TabIndex = 2;
            this.smBtnDelete.TabStop = false;
            this.smBtnDelete.TipsText = "";
            this.smBtnDelete.BtnClick += new System.EventHandler(this.smBtnDelete_BtnClick);
            // 
            // smBtnAdd
            // 
            this.smBtnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.smBtnAdd.BackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnAdd.BtnBackColor = System.Drawing.SystemColors.ControlDark;
            this.smBtnAdd.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smBtnAdd.BtnForeColor = System.Drawing.Color.Black;
            this.smBtnAdd.BtnImage = null;
            this.smBtnAdd.BtnText = "新增物料";
            this.smBtnAdd.ConerRadius = 15;
            this.smBtnAdd.FillColor = System.Drawing.Color.Transparent;
            this.smBtnAdd.IsRadius = true;
            this.smBtnAdd.IsShowRect = false;
            this.smBtnAdd.IsShowTips = false;
            this.smBtnAdd.Location = new System.Drawing.Point(606, 6);
            this.smBtnAdd.Name = "smBtnAdd";
            this.smBtnAdd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smBtnAdd.RectWidth = 1;
            this.smBtnAdd.Size = new System.Drawing.Size(66, 25);
            this.smBtnAdd.TabIndex = 0;
            this.smBtnAdd.TabStop = false;
            this.smBtnAdd.TipsText = "";
            this.smBtnAdd.BtnClick += new System.EventHandler(this.smBtnAdd_BtnClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.smProductInfoSetTitle2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 38);
            this.panel3.TabIndex = 4;
            // 
            // smProductInfoSetTitle2
            // 
            this.smProductInfoSetTitle2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.smProductInfoSetTitle2.Checked = false;
            this.smProductInfoSetTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smProductInfoSetTitle2.Location = new System.Drawing.Point(0, 0);
            this.smProductInfoSetTitle2.Name = "smProductInfoSetTitle2";
            this.smProductInfoSetTitle2.Size = new System.Drawing.Size(813, 38);
            this.smProductInfoSetTitle2.TabIndex = 0;
            this.smProductInfoSetTitle2.CheckedChangedEvent += new System.EventHandler(this.smProductInfoSetTitle2_CheckedChangedEvent);
            // 
            // plProductSetHome
            // 
            this.plProductSetHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.plProductSetHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plProductSetHome.Location = new System.Drawing.Point(0, 111);
            this.plProductSetHome.Name = "plProductSetHome";
            this.plProductSetHome.Size = new System.Drawing.Size(813, 439);
            this.plProductSetHome.TabIndex = 5;
            // 
            // SMProductInfoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plProductSetHome);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SMProductInfoSet";
            this.Size = new System.Drawing.Size(813, 550);
            this.Load += new System.EventHandler(this.SMProductInfoSet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel plProductSetHome;
        private SMButton smBtnAdd;
        private SMButton smBtnSave;
        private SMButton smBtnDelete;
        private SMProductInfoSetTitle smProductInfoSetTitle2;
    }
}
