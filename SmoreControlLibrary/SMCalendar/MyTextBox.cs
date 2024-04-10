using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DateSVN.Controls
{
    public partial class MyTextBox : UserControl
    {
        #region 变量

        private int _borderThickness = 2;

        private Image _pictrueBoxImage = null;

        private Font _textFont = new System.Drawing.Font("微软雅黑", 10.5F);

        private Color _textForeColor = Color.FromArgb(0x67, 0x67, 0x67);

        private int _borderLineWidth = 0;

        private Color _borderLineColor = Color.FromArgb(0X56, 0xC4, 0X7F);

        private string _waterText = string.Empty;

        private string _preValue = string.Empty;

        #endregion 变量

        #region 属性

        /// <summary>
        /// 文本框控件
        /// </summary>
        public WaterTextBox WaterTextBox
        {
            get
            {
                return waterTextBox1;
            }
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        public int BorderThickness
        {
            get
            {
                return _borderThickness;
            }
            set
            {
                _borderThickness = value;
                Invalidate();
            }
        }

        public Image PictrueBoxImage
        {
            get
            {
                return _pictrueBoxImage;
            }
            set
            {
                _pictrueBoxImage = value;
                pictureBox1.BackgroundImage = value;

                if (value != null)
                {
                    pictureBox1.Width = value.Width;
                }
                else
                {
                    pictureBox1.Width = 0;
                }

                Invalidate();
            }
        }

        public Font TextFont
        {
            get
            {
                return _textFont;
            }
            set
            {
                _textFont = value;
                waterTextBox1.Font = value;
                Invalidate();
            }
        }

        public Color TextForeColor
        {
            get
            {
                return _textForeColor;
            }
            set
            {
                _textForeColor = value;
                waterTextBox1.ForeColor = value;
            }
        }

        [Browsable(false)]
        public new Font Font
        {
            get;
            set;
        }

        [Browsable(false)]
        public new Color ForeColor
        {
            get;
            set;
        }

        [Browsable(false)]
        public string Text
        {
            get
            {
                return this.waterTextBox1.Text;
            }
            set
            {
                this.waterTextBox1.Text = value;
            }
        }

        /// <summary>
        /// 边框，为0表示没有边框
        /// </summary>
        private int BorderLineWidth
        {
            get
            {
                return _borderLineWidth;
            }
            set
            {
                _borderLineWidth = value;
                myFlowLayoutPanel1.BorderLineWidth = value;
                Invalidate();
            }
        }

        private Color BorderLineColor
        {
            get
            {
                return _borderLineColor;
            }
            set
            {
                _borderLineColor = value;
                myFlowLayoutPanel1.BorderLineColor = value;
            }
        }

        /// <summary>
        /// 水印
        /// </summary>
        public string WaterText
        {
            get
            {
                return _waterText;
            }
            set
            {
                _waterText = value;
                waterTextBox1.WaterText = value;
            }
        }

        /// <summary>
        /// 密保
        /// </summary>
        public char PasswordChar
        {
            get
            {
                return waterTextBox1.PasswordChar;
            }

            set
            {
                waterTextBox1.PasswordChar = value;
            }
        }

        /// <summary>
        /// 获取值是否变更(聚焦前的值和失去焦点之后的值比较)
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (!string.Equals(_preValue, Text, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion 属性

        public MyTextBox()
        {
            InitializeComponent();
            waterTextBox1.GotFocus += new EventHandler(TxtGotFocus);
            waterTextBox1.LostFocus += new EventHandler(TxtLostFocus);
            waterTextBox1.KeyUp += new KeyEventHandler(TxtKeyUp);
            BorderLineColor = Color.FromArgb(0Xdd, 0xe2, 0Xe1);
            BorderLineWidth = 1;
        }

        private void TxtKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void TxtGotFocus(object sender, EventArgs e)
        {
            _preValue = Text;
            BorderLineColor = Color.FromArgb(0X56, 0xC4, 0X7F); 
            myFlowLayoutPanel1.Invalidate();
        }

        private void TxtLostFocus(object sender, EventArgs e)
        {
            BorderLineColor = Color.FromArgb(0Xdd, 0xe2, 0Xe1);
            myFlowLayoutPanel1.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int borderWidth = BorderThickness + BorderLineWidth;

            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.Margin = new Padding(3 + BorderLineWidth, borderWidth, 0, borderWidth);
            }
            else
            {
                pictureBox1.Margin = new Padding(BorderLineWidth, 0, 0, 0);
            }

            waterTextBox1.Margin = new Padding(6, borderWidth, 3 + BorderLineWidth, borderWidth);
            int txtWidth = Size.Width - pictureBox1.Width - pictureBox1.Margin.Left
                                        - waterTextBox1.Margin.Left - waterTextBox1.Margin.Right;

            waterTextBox1.Width = txtWidth;

            int controlHeight = waterTextBox1.Margin.Top + waterTextBox1.Height + waterTextBox1.Margin.Bottom;
            Height = controlHeight;
            myFlowLayoutPanel1.Height = controlHeight;
            pictureBox1.Height = waterTextBox1.Height;

            base.OnPaint(e);
        }
    }
}
