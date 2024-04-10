namespace SmoreControlLibrary.EquipmentDriver.CameraHIK
{
    partial class SMCameraHIK
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
            this.smButtonOpen = new SmoreControlLibrary.SMButton();
            this.smButtonClose = new SmoreControlLibrary.SMButton();
            this.smButtonTrrigerOnce = new SmoreControlLibrary.SMButton();
            this.smButtonStartGather = new SmoreControlLibrary.SMButton();
            this.smButtonStopGather = new SmoreControlLibrary.SMButton();
            this.smButtonSoftTrigger = new SmoreControlLibrary.SMButton();
            this.smButtonFreeGather = new SmoreControlLibrary.SMButton();
            this.comboBoxCamItems = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.pictureBoxShow = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxCamItems);
            this.panel1.Controls.Add(this.smButtonFreeGather);
            this.panel1.Controls.Add(this.smButtonSoftTrigger);
            this.panel1.Controls.Add(this.smButtonStopGather);
            this.panel1.Controls.Add(this.smButtonStartGather);
            this.panel1.Controls.Add(this.smButtonTrrigerOnce);
            this.panel1.Controls.Add(this.smButtonClose);
            this.panel1.Controls.Add(this.smButtonOpen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 440);
            this.panel1.TabIndex = 0;
            // 
            // smButtonOpen
            // 
            this.smButtonOpen.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonOpen.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonOpen.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonOpen.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonOpen.BtnImage = null;
            this.smButtonOpen.BtnText = "打开设备";
            this.smButtonOpen.ConerRadius = 20;
            this.smButtonOpen.FillColor = System.Drawing.Color.Transparent;
            this.smButtonOpen.IsRadius = true;
            this.smButtonOpen.IsShowRect = false;
            this.smButtonOpen.IsShowTips = false;
            this.smButtonOpen.Location = new System.Drawing.Point(14, 65);
            this.smButtonOpen.Name = "smButtonOpen";
            this.smButtonOpen.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonOpen.RectWidth = 1;
            this.smButtonOpen.Size = new System.Drawing.Size(121, 29);
            this.smButtonOpen.TabIndex = 0;
            this.smButtonOpen.TabStop = false;
            this.smButtonOpen.TipsText = "";
            this.smButtonOpen.BtnClick += new System.EventHandler(this.smButtonOpen_BtnClick);
            // 
            // smButtonClose
            // 
            this.smButtonClose.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonClose.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonClose.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonClose.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonClose.BtnImage = null;
            this.smButtonClose.BtnText = "关闭设备";
            this.smButtonClose.ConerRadius = 20;
            this.smButtonClose.FillColor = System.Drawing.Color.Transparent;
            this.smButtonClose.IsRadius = true;
            this.smButtonClose.IsShowRect = false;
            this.smButtonClose.IsShowTips = false;
            this.smButtonClose.Location = new System.Drawing.Point(14, 119);
            this.smButtonClose.Name = "smButtonClose";
            this.smButtonClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonClose.RectWidth = 1;
            this.smButtonClose.Size = new System.Drawing.Size(121, 29);
            this.smButtonClose.TabIndex = 1;
            this.smButtonClose.TabStop = false;
            this.smButtonClose.TipsText = "";
            this.smButtonClose.BtnClick += new System.EventHandler(this.smButtonClose_BtnClick);
            // 
            // smButtonTrrigerOnce
            // 
            this.smButtonTrrigerOnce.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonTrrigerOnce.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonTrrigerOnce.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonTrrigerOnce.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonTrrigerOnce.BtnImage = null;
            this.smButtonTrrigerOnce.BtnText = "触发一次";
            this.smButtonTrrigerOnce.ConerRadius = 20;
            this.smButtonTrrigerOnce.FillColor = System.Drawing.Color.Transparent;
            this.smButtonTrrigerOnce.IsRadius = true;
            this.smButtonTrrigerOnce.IsShowRect = false;
            this.smButtonTrrigerOnce.IsShowTips = false;
            this.smButtonTrrigerOnce.Location = new System.Drawing.Point(14, 173);
            this.smButtonTrrigerOnce.Name = "smButtonTrrigerOnce";
            this.smButtonTrrigerOnce.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonTrrigerOnce.RectWidth = 1;
            this.smButtonTrrigerOnce.Size = new System.Drawing.Size(121, 29);
            this.smButtonTrrigerOnce.TabIndex = 2;
            this.smButtonTrrigerOnce.TabStop = false;
            this.smButtonTrrigerOnce.TipsText = "";
            this.smButtonTrrigerOnce.BtnClick += new System.EventHandler(this.smButtonTrrigerOnce_BtnClick);
            // 
            // smButtonStartGather
            // 
            this.smButtonStartGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStartGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStartGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonStartGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonStartGather.BtnImage = null;
            this.smButtonStartGather.BtnText = "开始采集";
            this.smButtonStartGather.ConerRadius = 20;
            this.smButtonStartGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonStartGather.IsRadius = true;
            this.smButtonStartGather.IsShowRect = false;
            this.smButtonStartGather.IsShowTips = false;
            this.smButtonStartGather.Location = new System.Drawing.Point(14, 227);
            this.smButtonStartGather.Name = "smButtonStartGather";
            this.smButtonStartGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonStartGather.RectWidth = 1;
            this.smButtonStartGather.Size = new System.Drawing.Size(121, 29);
            this.smButtonStartGather.TabIndex = 3;
            this.smButtonStartGather.TabStop = false;
            this.smButtonStartGather.TipsText = "";
            this.smButtonStartGather.BtnClick += new System.EventHandler(this.smButtonStartGather_BtnClick);
            // 
            // smButtonStopGather
            // 
            this.smButtonStopGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStopGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStopGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonStopGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonStopGather.BtnImage = null;
            this.smButtonStopGather.BtnText = "停止采集";
            this.smButtonStopGather.ConerRadius = 20;
            this.smButtonStopGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonStopGather.IsRadius = true;
            this.smButtonStopGather.IsShowRect = false;
            this.smButtonStopGather.IsShowTips = false;
            this.smButtonStopGather.Location = new System.Drawing.Point(14, 281);
            this.smButtonStopGather.Name = "smButtonStopGather";
            this.smButtonStopGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonStopGather.RectWidth = 1;
            this.smButtonStopGather.Size = new System.Drawing.Size(121, 29);
            this.smButtonStopGather.TabIndex = 4;
            this.smButtonStopGather.TabStop = false;
            this.smButtonStopGather.TipsText = "";
            this.smButtonStopGather.BtnClick += new System.EventHandler(this.smButtonStopGather_BtnClick);
            // 
            // smButtonSoftTrigger
            // 
            this.smButtonSoftTrigger.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonSoftTrigger.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonSoftTrigger.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonSoftTrigger.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonSoftTrigger.BtnImage = null;
            this.smButtonSoftTrigger.BtnText = "设置为软触发";
            this.smButtonSoftTrigger.ConerRadius = 20;
            this.smButtonSoftTrigger.FillColor = System.Drawing.Color.Transparent;
            this.smButtonSoftTrigger.IsRadius = true;
            this.smButtonSoftTrigger.IsShowRect = false;
            this.smButtonSoftTrigger.IsShowTips = false;
            this.smButtonSoftTrigger.Location = new System.Drawing.Point(14, 335);
            this.smButtonSoftTrigger.Name = "smButtonSoftTrigger";
            this.smButtonSoftTrigger.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonSoftTrigger.RectWidth = 1;
            this.smButtonSoftTrigger.Size = new System.Drawing.Size(121, 29);
            this.smButtonSoftTrigger.TabIndex = 5;
            this.smButtonSoftTrigger.TabStop = false;
            this.smButtonSoftTrigger.TipsText = "";
            this.smButtonSoftTrigger.BtnClick += new System.EventHandler(this.smButtonSoftTrigger_BtnClick);
            // 
            // smButtonFreeGather
            // 
            this.smButtonFreeGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonFreeGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonFreeGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonFreeGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonFreeGather.BtnImage = null;
            this.smButtonFreeGather.BtnText = "自由采集";
            this.smButtonFreeGather.ConerRadius = 20;
            this.smButtonFreeGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonFreeGather.IsRadius = true;
            this.smButtonFreeGather.IsShowRect = false;
            this.smButtonFreeGather.IsShowTips = false;
            this.smButtonFreeGather.Location = new System.Drawing.Point(14, 389);
            this.smButtonFreeGather.Name = "smButtonFreeGather";
            this.smButtonFreeGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonFreeGather.RectWidth = 1;
            this.smButtonFreeGather.Size = new System.Drawing.Size(121, 29);
            this.smButtonFreeGather.TabIndex = 6;
            this.smButtonFreeGather.TabStop = false;
            this.smButtonFreeGather.TipsText = "";
            this.smButtonFreeGather.BtnClick += new System.EventHandler(this.smButtonFreeGather_BtnClick);
            // 
            // comboBoxCamItems
            // 
            this.comboBoxCamItems.FormattingEnabled = true;
            this.comboBoxCamItems.Location = new System.Drawing.Point(14, 20);
            this.comboBoxCamItems.Name = "comboBoxCamItems";
            this.comboBoxCamItems.Size = new System.Drawing.Size(121, 20);
            this.comboBoxCamItems.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 30);
            this.panel2.TabIndex = 1;
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.Black;
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(668, 30);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "HIK相机";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxShow
            // 
            this.pictureBoxShow.BackColor = System.Drawing.Color.Navy;
            this.pictureBoxShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxShow.Location = new System.Drawing.Point(150, 30);
            this.pictureBoxShow.Name = "pictureBoxShow";
            this.pictureBoxShow.Size = new System.Drawing.Size(518, 440);
            this.pictureBoxShow.TabIndex = 2;
            this.pictureBoxShow.TabStop = false;
            // 
            // SMCameraHIK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pictureBoxShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "SMCameraHIK";
            this.Size = new System.Drawing.Size(668, 470);
            this.Load += new System.EventHandler(this.SMCameraHIK_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private SMButton smButtonOpen;
        private SMButton smButtonFreeGather;
        private SMButton smButtonSoftTrigger;
        private SMButton smButtonStopGather;
        private SMButton smButtonStartGather;
        private SMButton smButtonTrrigerOnce;
        private SMButton smButtonClose;
        private System.Windows.Forms.ComboBox comboBoxCamItems;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbl;
        private System.Windows.Forms.PictureBox pictureBoxShow;
    }
}
