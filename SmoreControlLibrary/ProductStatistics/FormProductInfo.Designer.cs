
namespace SmoreControlLibrary.ProductStatistics
{
    
    partial class FormProductInfo
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
            this.smCountSet1 = new SmoreControlLibrary.SMForm.SMCountSet();
            this.SuspendLayout();
            // 
            // smCountSet1
            // 
            this.smCountSet1.AutoSize = true;
            this.smCountSet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smCountSet1.Location = new System.Drawing.Point(0, 0);
            this.smCountSet1.Name = "smCountSet1";
            this.smCountSet1.Size = new System.Drawing.Size(840, 431);
            this.smCountSet1.TabIndex = 0;
            this.smCountSet1.Load += new System.EventHandler(this.smCountSet1_Load);
            // 
            // FormProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 431);
            this.Controls.Add(this.smCountSet1);
            this.Name = "FormProductInfo";
            this.Text = "生产数据";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProductInfo_FormClosing);
            this.Load += new System.EventHandler(this.FormProductInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
    }
}