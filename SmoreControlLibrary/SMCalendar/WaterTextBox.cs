using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DateSVN.Controls
{
    public partial class WaterTextBox : TextBox
    {
        private readonly Label lblwaterText = new Label();

        public WaterTextBox()
        {
            InitializeComponent();
            lblwaterText.BorderStyle = BorderStyle.None;
            lblwaterText.Enabled = false;
            lblwaterText.BackColor = Color.White;
            lblwaterText.AutoSize = false;
            lblwaterText.Top = 1;
            lblwaterText.Left = 2;
            lblwaterText.FlatStyle = FlatStyle.System;
            Controls.Add(lblwaterText);
        }

        [Category("扩展属性"), Description("显示的提示信息"), DefaultValue("")]
        public string WaterText
        {
            get { return lblwaterText.Text; }
            set { lblwaterText.Text = value; }
        }

        public override string Text
        {
            set
            {
                lblwaterText.Visible = value == string.Empty;
                base.Text = value;
            }

            get
            {
                return base.Text;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Multiline && (ScrollBars == ScrollBars.Vertical || ScrollBars == ScrollBars.Both))
            {
                lblwaterText.Width = Width - 20;
            }
            else
            {
                lblwaterText.Width = Width;
            }

            lblwaterText.Height = Height - 2;
            base.OnSizeChanged(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            lblwaterText.Visible = base.Text == string.Empty;
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            lblwaterText.Visible = false;
            base.OnGotFocus(e);
        }
    }
}
