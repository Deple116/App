namespace SmoreVision
{
    partial class SMFormWelcom
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbLoadMsg = new System.Windows.Forms.Label();
            this.myProgressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbLoadMsg, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.myProgressBar, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 397);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(924, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbLoadMsg
            // 
            this.lbLoadMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLoadMsg.Location = new System.Drawing.Point(3, 0);
            this.lbLoadMsg.Name = "lbLoadMsg";
            this.lbLoadMsg.Size = new System.Drawing.Size(918, 50);
            this.lbLoadMsg.TabIndex = 1;
            this.lbLoadMsg.Text = "label1";
            this.lbLoadMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myProgressBar
            // 
            this.myProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myProgressBar.Location = new System.Drawing.Point(3, 53);
            this.myProgressBar.Name = "myProgressBar";
            this.myProgressBar.Size = new System.Drawing.Size(918, 44);
            this.myProgressBar.TabIndex = 2;
            // 
            // SMFormWelcom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(924, 497);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SMFormWelcom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMFormWelcom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SMFormWelcom_FormClosed);
            this.Load += new System.EventHandler(this.FormWelcom_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbLoadMsg;
        private System.Windows.Forms.ProgressBar myProgressBar;
    }
}