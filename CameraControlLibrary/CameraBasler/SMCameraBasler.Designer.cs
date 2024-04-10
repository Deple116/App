using CameraControlLibrary.CameraHIK;

namespace CameraControlLibrary
{
    partial class SMCameraBasler
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.smButton1 = new SMButton();
            this.comboBoxCamItems = new System.Windows.Forms.ComboBox();
            this.smButtonFreeGather = new SMButton();
            this.smButtonSoftTrigger = new SMButton();
            this.smButtonStopGather = new SMButton();
            this.smButtonStartGather = new SMButton();
            this.smButtonTrrigerOnce = new SMButton();
            this.smButtonClose = new SMButton();
            this.smButtonOpen = new SMButton();
            this.pictureBoxShow = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.Black;
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(995, 35);
            this.lbl.TabIndex = 3;
            this.lbl.Text = "Basler相机";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.smButton1);
            this.panel1.Controls.Add(this.comboBoxCamItems);
            this.panel1.Controls.Add(this.smButtonFreeGather);
            this.panel1.Controls.Add(this.smButtonSoftTrigger);
            this.panel1.Controls.Add(this.smButtonStopGather);
            this.panel1.Controls.Add(this.smButtonStartGather);
            this.panel1.Controls.Add(this.smButtonTrrigerOnce);
            this.panel1.Controls.Add(this.smButtonClose);
            this.panel1.Controls.Add(this.smButtonOpen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 621);
            this.panel1.TabIndex = 4;
            // 
            // smButton1
            // 
            this.smButton1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButton1.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButton1.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton1.BtnForeColor = System.Drawing.Color.Black;
            this.smButton1.BtnImage = null;
            this.smButton1.BtnText = "执行软触发";
            this.smButton1.ConerRadius = 20;
            this.smButton1.FillColor = System.Drawing.Color.Transparent;
            this.smButton1.IsRadius = true;
            this.smButton1.IsShowRect = false;
            this.smButton1.IsShowTips = false;
            this.smButton1.Location = new System.Drawing.Point(19, 471);
            this.smButton1.Margin = new System.Windows.Forms.Padding(5);
            this.smButton1.Name = "smButton1";
            this.smButton1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButton1.RectWidth = 1;
            this.smButton1.Size = new System.Drawing.Size(161, 36);
            this.smButton1.TabIndex = 8;
            this.smButton1.TabStop = false;
            this.smButton1.TipsText = "";
            this.smButton1.BtnClick += new System.EventHandler(this.smButton1_BtnClick);
            // 
            // comboBoxCamItems
            // 
            this.comboBoxCamItems.FormattingEnabled = true;
            this.comboBoxCamItems.Location = new System.Drawing.Point(19, 25);
            this.comboBoxCamItems.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCamItems.Name = "comboBoxCamItems";
            this.comboBoxCamItems.Size = new System.Drawing.Size(160, 23);
            this.comboBoxCamItems.TabIndex = 7;
            // 
            // smButtonFreeGather
            // 
            this.smButtonFreeGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonFreeGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonFreeGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonFreeGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonFreeGather.BtnImage = null;
            this.smButtonFreeGather.BtnText = "停止采集";
            this.smButtonFreeGather.ConerRadius = 20;
            this.smButtonFreeGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonFreeGather.IsRadius = true;
            this.smButtonFreeGather.IsShowRect = false;
            this.smButtonFreeGather.IsShowTips = false;
            this.smButtonFreeGather.Location = new System.Drawing.Point(19, 414);
            this.smButtonFreeGather.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonFreeGather.Name = "smButtonFreeGather";
            this.smButtonFreeGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonFreeGather.RectWidth = 1;
            this.smButtonFreeGather.Size = new System.Drawing.Size(161, 36);
            this.smButtonFreeGather.TabIndex = 6;
            this.smButtonFreeGather.TabStop = false;
            this.smButtonFreeGather.TipsText = "";
            this.smButtonFreeGather.BtnClick += new System.EventHandler(this.smButtonFreeGather_BtnClick);
            // 
            // smButtonSoftTrigger
            // 
            this.smButtonSoftTrigger.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonSoftTrigger.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonSoftTrigger.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonSoftTrigger.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonSoftTrigger.BtnImage = null;
            this.smButtonSoftTrigger.BtnText = "单张采集";
            this.smButtonSoftTrigger.ConerRadius = 20;
            this.smButtonSoftTrigger.FillColor = System.Drawing.Color.Transparent;
            this.smButtonSoftTrigger.IsRadius = true;
            this.smButtonSoftTrigger.IsShowRect = false;
            this.smButtonSoftTrigger.IsShowTips = false;
            this.smButtonSoftTrigger.Location = new System.Drawing.Point(19, 362);
            this.smButtonSoftTrigger.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonSoftTrigger.Name = "smButtonSoftTrigger";
            this.smButtonSoftTrigger.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonSoftTrigger.RectWidth = 1;
            this.smButtonSoftTrigger.Size = new System.Drawing.Size(161, 36);
            this.smButtonSoftTrigger.TabIndex = 5;
            this.smButtonSoftTrigger.TabStop = false;
            this.smButtonSoftTrigger.TipsText = "";
            this.smButtonSoftTrigger.BtnClick += new System.EventHandler(this.smButtonSoftTrigger_BtnClick);
            // 
            // smButtonStopGather
            // 
            this.smButtonStopGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStopGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStopGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonStopGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonStopGather.BtnImage = null;
            this.smButtonStopGather.BtnText = "连续采集";
            this.smButtonStopGather.ConerRadius = 20;
            this.smButtonStopGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonStopGather.IsRadius = true;
            this.smButtonStopGather.IsShowRect = false;
            this.smButtonStopGather.IsShowTips = false;
            this.smButtonStopGather.Location = new System.Drawing.Point(19, 304);
            this.smButtonStopGather.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonStopGather.Name = "smButtonStopGather";
            this.smButtonStopGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonStopGather.RectWidth = 1;
            this.smButtonStopGather.Size = new System.Drawing.Size(161, 36);
            this.smButtonStopGather.TabIndex = 4;
            this.smButtonStopGather.TabStop = false;
            this.smButtonStopGather.TipsText = "";
            this.smButtonStopGather.BtnClick += new System.EventHandler(this.smButtonStopGather_BtnClick);
            // 
            // smButtonStartGather
            // 
            this.smButtonStartGather.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStartGather.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonStartGather.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonStartGather.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonStartGather.BtnImage = null;
            this.smButtonStartGather.BtnText = "设置为硬触发";
            this.smButtonStartGather.ConerRadius = 20;
            this.smButtonStartGather.FillColor = System.Drawing.Color.Transparent;
            this.smButtonStartGather.IsRadius = true;
            this.smButtonStartGather.IsShowRect = false;
            this.smButtonStartGather.IsShowTips = false;
            this.smButtonStartGather.Location = new System.Drawing.Point(19, 248);
            this.smButtonStartGather.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonStartGather.Name = "smButtonStartGather";
            this.smButtonStartGather.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonStartGather.RectWidth = 1;
            this.smButtonStartGather.Size = new System.Drawing.Size(161, 36);
            this.smButtonStartGather.TabIndex = 3;
            this.smButtonStartGather.TabStop = false;
            this.smButtonStartGather.TipsText = "";
            this.smButtonStartGather.BtnClick += new System.EventHandler(this.smButtonStartGather_BtnClick);
            // 
            // smButtonTrrigerOnce
            // 
            this.smButtonTrrigerOnce.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonTrrigerOnce.BtnBackColor = System.Drawing.Color.DeepSkyBlue;
            this.smButtonTrrigerOnce.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButtonTrrigerOnce.BtnForeColor = System.Drawing.Color.Black;
            this.smButtonTrrigerOnce.BtnImage = null;
            this.smButtonTrrigerOnce.BtnText = "设置为软触发";
            this.smButtonTrrigerOnce.ConerRadius = 20;
            this.smButtonTrrigerOnce.FillColor = System.Drawing.Color.Transparent;
            this.smButtonTrrigerOnce.IsRadius = true;
            this.smButtonTrrigerOnce.IsShowRect = false;
            this.smButtonTrrigerOnce.IsShowTips = false;
            this.smButtonTrrigerOnce.Location = new System.Drawing.Point(19, 190);
            this.smButtonTrrigerOnce.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonTrrigerOnce.Name = "smButtonTrrigerOnce";
            this.smButtonTrrigerOnce.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonTrrigerOnce.RectWidth = 1;
            this.smButtonTrrigerOnce.Size = new System.Drawing.Size(161, 36);
            this.smButtonTrrigerOnce.TabIndex = 2;
            this.smButtonTrrigerOnce.TabStop = false;
            this.smButtonTrrigerOnce.TipsText = "";
            this.smButtonTrrigerOnce.BtnClick += new System.EventHandler(this.smButtonTrrigerOnce_BtnClick);
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
            this.smButtonClose.Location = new System.Drawing.Point(19, 134);
            this.smButtonClose.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonClose.Name = "smButtonClose";
            this.smButtonClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonClose.RectWidth = 1;
            this.smButtonClose.Size = new System.Drawing.Size(161, 36);
            this.smButtonClose.TabIndex = 1;
            this.smButtonClose.TabStop = false;
            this.smButtonClose.TipsText = "";
            this.smButtonClose.BtnClick += new System.EventHandler(this.smButtonClose_BtnClick);
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
            this.smButtonOpen.Location = new System.Drawing.Point(19, 81);
            this.smButtonOpen.Margin = new System.Windows.Forms.Padding(5);
            this.smButtonOpen.Name = "smButtonOpen";
            this.smButtonOpen.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButtonOpen.RectWidth = 1;
            this.smButtonOpen.Size = new System.Drawing.Size(161, 36);
            this.smButtonOpen.TabIndex = 0;
            this.smButtonOpen.TabStop = false;
            this.smButtonOpen.TipsText = "";
            this.smButtonOpen.BtnClick += new System.EventHandler(this.smButtonOpen_BtnClick);
            // 
            // pictureBoxShow
            // 
            this.pictureBoxShow.BackColor = System.Drawing.Color.Navy;
            this.pictureBoxShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxShow.Location = new System.Drawing.Point(200, 35);
            this.pictureBoxShow.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxShow.Name = "pictureBoxShow";
            this.pictureBoxShow.Size = new System.Drawing.Size(795, 621);
            this.pictureBoxShow.TabIndex = 5;
            this.pictureBoxShow.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 565);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // SMCameraBasler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pictureBoxShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl);
            this.Name = "SMCameraBasler";
            this.Size = new System.Drawing.Size(995, 656);
            this.Load += new System.EventHandler(this.SMCameraBasler_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxCamItems;
        private SMButton smButtonFreeGather;
        private SMButton smButtonSoftTrigger;
        private SMButton smButtonStopGather;
        private SMButton smButtonStartGather;
        private SMButton smButtonTrrigerOnce;
        private SMButton smButtonClose;
        private SMButton smButtonOpen;
        private System.Windows.Forms.PictureBox pictureBoxShow;
        private SMButton smButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
