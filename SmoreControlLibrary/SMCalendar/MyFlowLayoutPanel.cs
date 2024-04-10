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
    public partial class MyFlowLayoutPanel : FlowLayoutPanel
    {
        #region 变量

        private int _borderLineWidth = 0;

        private Color _borderLineColor = Color.FromArgb(0X56, 0xC4, 0X7F);

        #endregion 变量

        #region 属性

        /// <summary>
        /// 边框，为0表示没有边框
        /// </summary>
        public int BorderLineWidth
        {
            get
            {
                return _borderLineWidth;
            }
            set
            {
                _borderLineWidth = value;
            }
        }

        public Color BorderLineColor
        {
            get
            {
                return _borderLineColor;
            }
            set
            {
                _borderLineColor = value;
            }
        }

        #endregion 属性

        public MyFlowLayoutPanel()
        {
            InitializeComponent();
            ControlDisplay();
        }

        /// <summary>
        /// 控制显示
        /// </summary>
        private void ControlDisplay()
        {
            Margin = new Padding(0);
            BackColor = System.Drawing.Color.Transparent;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void MyFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            if (BorderLineWidth > 0)
            {
                ControlPaint.DrawBorder(e.Graphics,
                    this.ClientRectangle,
                    BorderLineColor,
                    BorderLineWidth,
                    ButtonBorderStyle.Solid,
                    BorderLineColor,
                    BorderLineWidth,
                    ButtonBorderStyle.Solid,
                    BorderLineColor,
                    BorderLineWidth,
                    ButtonBorderStyle.Solid,
                    BorderLineColor,
                    BorderLineWidth,
                    ButtonBorderStyle.Solid);
            }
        }
    }
}
