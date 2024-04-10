using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmoreControlLibrary.Properties;

namespace DateSVN.Controls
{
    public partial class MyTime : UserControl
    {
        #region 变量

        /// <summary>
        /// 边框厚度
        /// </summary>
        private int _borderThickness = 2;

        /// <summary>
        /// 图标
        /// </summary>
        private Image _pictrueBoxImage = Resources.datePicker;

        /// <summary>
        /// 字体大小
        /// </summary>
        private Font _textFont = new System.Drawing.Font("微软雅黑", 10.5F);

        /// <summary>
        /// 字体颜色
        /// </summary>
        private Color _textForeColor = Color.FromArgb(0x67, 0x67, 0x67);

        /// <summary>
        /// 边框线厚度
        /// </summary>
        private int _borderLineWidth = 0;

        /// <summary>
        /// 边框线颜色
        /// </summary>
        private Color _borderLineColor = Color.FromArgb(0X56, 0xC4, 0X7F);

        /// <summary>
        /// 水印
        /// </summary>
        private string _waterText = string.Empty;

        private DateTime? _value;

        private MyCalanderTime calander;

        #endregion 变量

        #region 属性

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

        /// <summary>
        /// 图标
        /// </summary>
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

        /// <summary>
        /// 字号
        /// </summary>
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

        /// <summary>
        /// 字体颜色
        /// </summary>
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

        /// <summary>
        /// 文本框内容
        /// </summary>
        public DateTime Value
        {
            get
            {
                if (_value == null)
                {
                    return DateTime.Now;
                }

                return _value.Value;
            }

            set
            {
                _value = value;
                calander.SelectedDateTime = value;
                waterTextBox1.Text = value.ToString(DateFormat);              
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

        /// <summary>
        /// 边框线颜色
        /// </summary>
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

        private string _dateFormat = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 时间格式
        /// </summary>
        public string DateFormat
        {
            get
            {
                return _dateFormat;
            }

            set
            {
                _dateFormat = value;
            }
        }

        private int _startDatePosition = 1;

        /// <summary>
        /// 时间开始的位置，为空默认从第一位开始
        /// </summary>
        public int StartDatePosition
        {
            get
            {
                return _startDatePosition;
            }

            set
            {
                if (value > DateFormat.Length)
                {
                    value = 1;
                }

                _startDatePosition = value;
            }
        }

        #endregion 属性

        public MyTime()
        {
            InitializeComponent();
            waterTextBox1.GotFocus += new EventHandler(TxtGotFocus);
            waterTextBox1.LostFocus += new EventHandler(TxtLostFocus);
            
            waterTextBox1.KeyUp += new KeyEventHandler(TxtKeyUp);

            BorderLineColor = Color.FromArgb(0Xdd, 0xe2, 0Xe1);
            BorderLineWidth = 1;
            // 
            // calander
            // 
            calander = new MyCalanderTime();
          
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ParentForm != null)
            {
                calander.BackColor = Color.FromArgb(249, 249, 249);
                calander.Font = new Font("微软雅黑", 10.5F);
                calander.Margin = new Padding(0);
                calander.Name = "calander";
                calander.TabStop = false;
                calander.Visible = false;
                calander.VisibleChanged += new EventHandler(this.calander_VisibleChanged);
                calander.Leave += new EventHandler(this.calander_Leave);

                ParentForm.Controls.Add(calander);
                Point pos = ParentForm.PointToClient(this.PointToScreen(Point.Empty));
                //calander.Size = new Size(this.Width, 201);
                calander.Left = pos.X;
                calander.Top = pos.Y + this.Height;
            }
        }

        private void calander_Leave(object sender, EventArgs e)
        {
            if (calander != null)
            {
                calander.Visible = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            myFlowLayoutPanel1.Height = waterTextBox1.Margin.Top + waterTextBox1.Height + waterTextBox1.Margin.Bottom;
            pictureBox1.Height = waterTextBox1.Height;
            Height = myFlowLayoutPanel1.Height;

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
            base.OnPaint(e);
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
            BorderLineColor = Color.FromArgb(0X56, 0xC4, 0X7F); 
            myFlowLayoutPanel1.Invalidate();
        }

        private void TxtLostFocus(object sender, EventArgs e)
        {
            BorderLineColor = Color.FromArgb(0Xdd, 0xe2, 0Xe1);  
            myFlowLayoutPanel1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (calander != null && !calander.Visible)
            {
                calander.Visible = true;
                this.BringToFront();
                calander.BringToFront();
            }
        }

        private void calander_VisibleChanged(object sender, EventArgs e)
        {
            if (calander != null && !calander.Visible)
            {
                _value = calander.SelectedDateTime;

                if (_value == null)
                {
                    waterTextBox1.Text = string.Empty;
                }
                else
                {
                    waterTextBox1.Text = _value.Value.ToString(DateFormat);
                }

                calander.SendToBack();
            }
        }

        private void waterTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (calander != null && !calander.Visible)
            {
                calander.Visible = true;
                calander.BringToFront();
            }
        }
    }
}
