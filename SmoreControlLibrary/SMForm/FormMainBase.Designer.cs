namespace SmoreControlLibrary
{
    partial class FormMainBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainBase));
            this.tableLayoutPanelHead = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelHeadName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.smButtonMaxAndNormal = new SmoreControlLibrary.SMButton();
            this.smButtonClose = new SmoreControlLibrary.SMButton();
            this.smButtonMin = new SmoreControlLibrary.SMButton();
            this.smButtonChangeLanguage = new SmoreControlLibrary.SMButton();
            this.smButtonHelper = new SmoreControlLibrary.SMButton();
            this.smButtonSet = new SmoreControlLibrary.SMButton();
            this.tableLayoutPanelHead.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelHead
            // 
            this.tableLayoutPanelHead.BackColor = System.Drawing.Color.DodgerBlue;
            this.tableLayoutPanelHead.ColumnCount = 5;
            this.tableLayoutPanelHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanelHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanelHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanelHead.Controls.Add(this.panel1, 4, 0);
            this.tableLayoutPanelHead.Controls.Add(this.labelHeadName, 1, 0);
            this.tableLayoutPanelHead.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanelHead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelHead.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelHead.Name = "tableLayoutPanelHead";
            this.tableLayoutPanelHead.RowCount = 1;
            this.tableLayoutPanelHead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHead.Size = new System.Drawing.Size(1350, 30);
            this.tableLayoutPanelHead.TabIndex = 0;
            this.tableLayoutPanelHead.DoubleClick += new System.EventHandler(this.WindowStateChange);
            this.tableLayoutPanelHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Head_MouseDown);
            this.tableLayoutPanelHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Head_MouseMove);
            this.tableLayoutPanelHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Head_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.smButtonMaxAndNormal);
            this.panel1.Controls.Add(this.smButtonClose);
            this.panel1.Controls.Add(this.smButtonMin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1260, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 30);
            this.panel1.TabIndex = 4;
            // 
            // labelHeadName
            // 
            this.labelHeadName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeadName.AutoSize = true;
            this.labelHeadName.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelHeadName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHeadName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHeadName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelHeadName.ForeColor = System.Drawing.Color.White;
            this.labelHeadName.Location = new System.Drawing.Point(243, 0);
            this.labelHeadName.Name = "labelHeadName";
            this.labelHeadName.Size = new System.Drawing.Size(864, 30);
            this.labelHeadName.TabIndex = 3;
            this.labelHeadName.Text = "SmoreVision";
            this.labelHeadName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHeadName.DoubleClick += new System.EventHandler(this.WindowStateChange);
            this.labelHeadName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Head_MouseDown);
            this.labelHeadName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Head_MouseMove);
            this.labelHeadName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Head_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.smButtonChangeLanguage);
            this.panel2.Controls.Add(this.smButtonHelper);
            this.panel2.Controls.Add(this.smButtonSet);
            this.panel2.Location = new System.Drawing.Point(1110, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 30);
            this.panel2.TabIndex = 5;
            // 
            // smButtonMaxAndNormal
            // 
            this.smButtonMaxAndNormal.BackColor = System.Drawing.Color.Transparent;
            this.smButtonMaxAndNormal.BackColorShow = false;
            this.smButtonMaxAndNormal.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonMaxAndNormal.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonMaxAndNormal.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonMaxAndNormal.BtnImage = ((System.Drawing.Image)(resources.GetObject("smButtonMaxAndNormal.BtnImage")));
            this.smButtonMaxAndNormal.BtnText = null;
            this.smButtonMaxAndNormal.ConerRadius = 24;
            this.smButtonMaxAndNormal.FillColor = System.Drawing.Color.Transparent;
            this.smButtonMaxAndNormal.IsRadius = false;
            this.smButtonMaxAndNormal.IsShowRect = false;
            this.smButtonMaxAndNormal.IsShowTips = false;
            this.smButtonMaxAndNormal.Location = new System.Drawing.Point(34, 3);
            this.smButtonMaxAndNormal.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonMaxAndNormal.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonMaxAndNormal.Name = "smButtonMaxAndNormal";
            this.smButtonMaxAndNormal.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonMaxAndNormal.RectWidth = 1;
            this.smButtonMaxAndNormal.Size = new System.Drawing.Size(22, 23);
            this.smButtonMaxAndNormal.TabIndex = 3;
            this.smButtonMaxAndNormal.TabStop = false;
            this.smButtonMaxAndNormal.TipsText = "";
            this.smButtonMaxAndNormal.BtnClick += new System.EventHandler(this.smButtonMaxAndNormal_BtnClick);
            // 
            // smButtonClose
            // 
            this.smButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.smButtonClose.BackColorShow = false;
            this.smButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.smButtonClose.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonClose.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonClose.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonClose.BtnImage = ((System.Drawing.Image)(resources.GetObject("smButtonClose.BtnImage")));
            this.smButtonClose.BtnText = null;
            this.smButtonClose.ConerRadius = 24;
            this.smButtonClose.FillColor = System.Drawing.Color.Transparent;
            this.smButtonClose.IsRadius = false;
            this.smButtonClose.IsShowRect = false;
            this.smButtonClose.IsShowTips = false;
            this.smButtonClose.Location = new System.Drawing.Point(65, 3);
            this.smButtonClose.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonClose.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonClose.Name = "smButtonClose";
            this.smButtonClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonClose.RectWidth = 1;
            this.smButtonClose.Size = new System.Drawing.Size(22, 23);
            this.smButtonClose.TabIndex = 2;
            this.smButtonClose.TabStop = false;
            this.smButtonClose.TipsText = "";
            this.smButtonClose.BtnClick += new System.EventHandler(this.smButtonClose_BtnClick);
            // 
            // smButtonMin
            // 
            this.smButtonMin.BackColor = System.Drawing.Color.Transparent;
            this.smButtonMin.BackColorShow = false;
            this.smButtonMin.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonMin.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonMin.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonMin.BtnImage = ((System.Drawing.Image)(resources.GetObject("smButtonMin.BtnImage")));
            this.smButtonMin.BtnText = null;
            this.smButtonMin.ConerRadius = 24;
            this.smButtonMin.FillColor = System.Drawing.Color.Transparent;
            this.smButtonMin.IsRadius = false;
            this.smButtonMin.IsShowRect = false;
            this.smButtonMin.IsShowTips = false;
            this.smButtonMin.Location = new System.Drawing.Point(3, 3);
            this.smButtonMin.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonMin.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonMin.Name = "smButtonMin";
            this.smButtonMin.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonMin.RectWidth = 1;
            this.smButtonMin.Size = new System.Drawing.Size(22, 23);
            this.smButtonMin.TabIndex = 1;
            this.smButtonMin.TabStop = false;
            this.smButtonMin.TipsText = "";
            this.smButtonMin.BtnClick += new System.EventHandler(this.smButtonMin_BtnClick);
            // 
            // smButtonChangeLanguage
            // 
            this.smButtonChangeLanguage.BackColor = System.Drawing.Color.Transparent;
            this.smButtonChangeLanguage.BackColorShow = false;
            this.smButtonChangeLanguage.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.Chinese;
            this.smButtonChangeLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.smButtonChangeLanguage.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonChangeLanguage.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonChangeLanguage.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonChangeLanguage.BtnImage = null;
            this.smButtonChangeLanguage.BtnText = null;
            this.smButtonChangeLanguage.ConerRadius = 24;
            this.smButtonChangeLanguage.FillColor = System.Drawing.Color.Transparent;
            this.smButtonChangeLanguage.IsRadius = false;
            this.smButtonChangeLanguage.IsShowRect = false;
            this.smButtonChangeLanguage.IsShowTips = false;
            this.smButtonChangeLanguage.Location = new System.Drawing.Point(34, 4);
            this.smButtonChangeLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonChangeLanguage.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonChangeLanguage.Name = "smButtonChangeLanguage";
            this.smButtonChangeLanguage.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonChangeLanguage.RectWidth = 1;
            this.smButtonChangeLanguage.Size = new System.Drawing.Size(23, 23);
            this.smButtonChangeLanguage.TabIndex = 6;
            this.smButtonChangeLanguage.TabStop = false;
            this.smButtonChangeLanguage.TipsText = "";
            this.smButtonChangeLanguage.BtnClick += new System.EventHandler(this.smButtonChangeLanguage_BtnClick);
            // 
            // smButtonHelper
            // 
            this.smButtonHelper.BackColor = System.Drawing.Color.Transparent;
            this.smButtonHelper.BackColorShow = false;
            this.smButtonHelper.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.Help;
            this.smButtonHelper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.smButtonHelper.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonHelper.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonHelper.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonHelper.BtnImage = null;
            this.smButtonHelper.BtnText = null;
            this.smButtonHelper.ConerRadius = 24;
            this.smButtonHelper.FillColor = System.Drawing.Color.Transparent;
            this.smButtonHelper.IsRadius = false;
            this.smButtonHelper.IsShowRect = false;
            this.smButtonHelper.IsShowTips = false;
            this.smButtonHelper.Location = new System.Drawing.Point(65, 4);
            this.smButtonHelper.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonHelper.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonHelper.Name = "smButtonHelper";
            this.smButtonHelper.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonHelper.RectWidth = 1;
            this.smButtonHelper.Size = new System.Drawing.Size(23, 23);
            this.smButtonHelper.TabIndex = 5;
            this.smButtonHelper.TabStop = false;
            this.smButtonHelper.TipsText = "";
            this.smButtonHelper.BtnClick += new System.EventHandler(this.smButtonHelper_BtnClick);
            // 
            // smButtonSet
            // 
            this.smButtonSet.BackColor = System.Drawing.Color.Transparent;
            this.smButtonSet.BackColorShow = false;
            this.smButtonSet.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.Set;
            this.smButtonSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.smButtonSet.BtnBackColor = System.Drawing.Color.Transparent;
            this.smButtonSet.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonSet.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonSet.BtnImage = null;
            this.smButtonSet.BtnText = null;
            this.smButtonSet.ConerRadius = 24;
            this.smButtonSet.FillColor = System.Drawing.Color.Transparent;
            this.smButtonSet.IsRadius = false;
            this.smButtonSet.IsShowRect = false;
            this.smButtonSet.IsShowTips = false;
            this.smButtonSet.Location = new System.Drawing.Point(3, 4);
            this.smButtonSet.Margin = new System.Windows.Forms.Padding(4);
            this.smButtonSet.MouseUpColor = System.Drawing.Color.Empty;
            this.smButtonSet.Name = "smButtonSet";
            this.smButtonSet.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonSet.RectWidth = 1;
            this.smButtonSet.Size = new System.Drawing.Size(23, 23);
            this.smButtonSet.TabIndex = 4;
            this.smButtonSet.TabStop = false;
            this.smButtonSet.TipsText = "";
            this.smButtonSet.BtnClick += new System.EventHandler(this.smButtonSet_BtnClick);
            // 
            // FormMainBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 30);
            this.Controls.Add(this.tableLayoutPanelHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMainBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainBase_FormClosing);
            this.Load += new System.EventHandler(this.FormMainBase_Load);
            this.tableLayoutPanelHead.ResumeLayout(false);
            this.tableLayoutPanelHead.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHead;
        private System.Windows.Forms.Label labelHeadName;
        private SMButton smButtonClose;
        private SMButton smButtonMin;
        private SMButton smButtonMaxAndNormal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SMButton smButtonChangeLanguage;
        private SMButton smButtonHelper;
        private SMButton smButtonSet;
    }
}