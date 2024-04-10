namespace SmoreControlLibrary.SMInfo
{
    partial class SMInfoWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.smButton1 = new SmoreControlLibrary.SMButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbProductModel = new System.Windows.Forms.Label();
            this.lbModelVersion = new System.Windows.Forms.Label();
            this.lbCurUser = new System.Windows.Forms.Label();
            this.lbProjectName = new System.Windows.Forms.Label();
            this.lbDetectPerson = new System.Windows.Forms.Label();
            this.lbDetectDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 420);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(136)))), ((int)(((byte)(149)))));
            this.panel1.Controls.Add(this.smButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(350, 40);
            this.panel1.TabIndex = 2;
            // 
            // smButton1
            // 
            this.smButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.smButton1.BackColorShow = false;
            this.smButton1.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.smButton1.BtnFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smButton1.BtnForeColor = System.Drawing.Color.Black;
            this.smButton1.BtnImage = null;
            this.smButton1.BtnText = "修改";
            this.smButton1.ConerRadius = 24;
            this.smButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smButton1.Enabled = false;
            this.smButton1.FillColor = System.Drawing.Color.Transparent;
            this.smButton1.IsRadius = false;
            this.smButton1.IsShowRect = false;
            this.smButton1.IsShowTips = false;
            this.smButton1.Location = new System.Drawing.Point(286, 9);
            this.smButton1.Margin = new System.Windows.Forms.Padding(4);
            this.smButton1.MouseUpColor = System.Drawing.Color.Empty;
            this.smButton1.Name = "smButton1";
            this.smButton1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.smButton1.RectWidth = 1;
            this.smButton1.Size = new System.Drawing.Size(55, 22);
            this.smButton1.TabIndex = 2;
            this.smButton1.TabStop = false;
            this.smButton1.TipsText = "";
            this.smButton1.Visible = false;
            this.smButton1.BtnClick += new System.EventHandler(this.smButton1_BtnClick);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备信息";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lbDetectDate, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lbDetectPerson, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbProjectName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbProductModel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbModelVersion, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbCurUser, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(350, 380);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lbProductModel
            // 
            this.lbProductModel.AutoSize = true;
            this.lbProductModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbProductModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProductModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProductModel.Location = new System.Drawing.Point(0, 63);
            this.lbProductModel.Margin = new System.Windows.Forms.Padding(0);
            this.lbProductModel.Name = "lbProductModel";
            this.lbProductModel.Size = new System.Drawing.Size(350, 63);
            this.lbProductModel.TabIndex = 0;
            this.lbProductModel.Text = "  产品名称：xxxx";
            this.lbProductModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbModelVersion
            // 
            this.lbModelVersion.AutoSize = true;
            this.lbModelVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbModelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbModelVersion.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModelVersion.Location = new System.Drawing.Point(0, 126);
            this.lbModelVersion.Margin = new System.Windows.Forms.Padding(0);
            this.lbModelVersion.Name = "lbModelVersion";
            this.lbModelVersion.Size = new System.Drawing.Size(350, 63);
            this.lbModelVersion.TabIndex = 2;
            this.lbModelVersion.Text = "  委托单号：xxxx";
            this.lbModelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurUser
            // 
            this.lbCurUser.AutoSize = true;
            this.lbCurUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbCurUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCurUser.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCurUser.Location = new System.Drawing.Point(0, 189);
            this.lbCurUser.Margin = new System.Windows.Forms.Padding(0);
            this.lbCurUser.Name = "lbCurUser";
            this.lbCurUser.Size = new System.Drawing.Size(350, 63);
            this.lbCurUser.TabIndex = 3;
            this.lbCurUser.Text = "  当前用户：操作员";
            this.lbCurUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbProjectName
            // 
            this.lbProjectName.AutoSize = true;
            this.lbProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbProjectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProjectName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProjectName.Location = new System.Drawing.Point(0, 0);
            this.lbProjectName.Margin = new System.Windows.Forms.Padding(0);
            this.lbProjectName.Name = "lbProjectName";
            this.lbProjectName.Size = new System.Drawing.Size(350, 63);
            this.lbProjectName.TabIndex = 4;
            this.lbProjectName.Text = "  项目名称：xxxx";
            this.lbProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDetectPerson
            // 
            this.lbDetectPerson.AutoSize = true;
            this.lbDetectPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbDetectPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDetectPerson.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDetectPerson.Location = new System.Drawing.Point(0, 252);
            this.lbDetectPerson.Margin = new System.Windows.Forms.Padding(0);
            this.lbDetectPerson.Name = "lbDetectPerson";
            this.lbDetectPerson.Size = new System.Drawing.Size(350, 63);
            this.lbDetectPerson.TabIndex = 5;
            this.lbDetectPerson.Text = "  检测人员：xxxx";
            this.lbDetectPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDetectDate
            // 
            this.lbDetectDate.AutoSize = true;
            this.lbDetectDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbDetectDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDetectDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDetectDate.Location = new System.Drawing.Point(0, 315);
            this.lbDetectDate.Margin = new System.Windows.Forms.Padding(0);
            this.lbDetectDate.Name = "lbDetectDate";
            this.lbDetectDate.Size = new System.Drawing.Size(350, 65);
            this.lbDetectDate.TabIndex = 6;
            this.lbDetectDate.Text = "  检测日期：xxxx";
            this.lbDetectDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SMInfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SMInfoWindow";
            this.Size = new System.Drawing.Size(350, 420);
            this.Load += new System.EventHandler(this.SMInfoWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbCurUser;
        private System.Windows.Forms.Label lbModelVersion;
        private System.Windows.Forms.Label lbProductModel;
        private SMButton smButton1;
        private System.Windows.Forms.Label lbProjectName;
        private System.Windows.Forms.Label lbDetectDate;
        private System.Windows.Forms.Label lbDetectPerson;
    }
}
