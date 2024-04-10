
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmoreControlLibrary
{
    [DefaultEvent("BtnClick")]
    public partial class SMButton : SMControlBase
    {
        #region 字段属性

        [Description("是否显示角标"), Category("SmoreControl")]
        public bool IsShowTips
        {
            get
            {
                return this.lblTips.Visible;
            }
            set
            {
                this.lblTips.Visible = value;
            }
        }

        [Description("角标文字"), Category("SmoreControl")]
        public string TipsText
        {
            get
            {
                return this.lblTips.Text;
            }
            set
            {
                this.lblTips.Text = value;
            }
        }

        [Description("按钮背景色"), Category("SmoreControl")]
        public Color BtnBackColor
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
            }
        }

        [Description("鼠标Up时的颜色"), Category("SmoreControl")]
        private Color _mouseUpColor;

        public Color MouseUpColor
        {
            get { return _mouseUpColor; }
            set { _mouseUpColor = value; }
        }


        private Color _btnForeColor = Color.Black;
        /// <summary>
        /// 按钮字体颜色
        /// </summary>
        [Description("按钮字体颜色"), Category("SmoreControl")]
        public Color BtnForeColor
        {
            get { return _btnForeColor; }
            set
            {
                _btnForeColor = value;
                this.lbl.ForeColor = value;
            }
        }

        private Font _btnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        /// <summary>
        /// 按钮字体
        /// </summary>
        [Description("按钮字体"), Category("SmoreControl")]
        public Font BtnFont
        {
            get { return _btnFont; }
            set
            {
                _btnFont = value;
                this.lbl.Font = value;
            }
        }

        [Description("鼠标按下是否显示背景颜色"), Category("SmoreControl")]
        private bool _backColorShow;

        public bool BackColorShow
        {
            get { return _backColorShow; }
            set { _backColorShow = value; }
        }


        /// <summary>
        /// 按钮点击事件
        /// </summary>
        [Description("按钮点击事件"), Category("SmoreControl")]
        public event EventHandler BtnClick;

        private string _btnText;
        /// <summary>
        /// 按钮文字
        /// </summary>
        [Description("按钮文字"), Category("SmoreControl")]
        public string BtnText
        {
            get { return _btnText; }
            set
            {
                _btnText = value;
                lbl.Text = value;
            }
        }

        private Image _btnImage;
        [Description("按钮图片"), Category("SmoreControl")]
        public Image BtnImage
        {
            get { return _btnImage; }
            set { _btnImage = value; lbl.Image = value; }
        }

        [Description("鼠标效果生效时发生，需要和MouseEffected同时使用，否则无效"), Category("自定义")]
        public event EventHandler MouseEffecting;
        [Description("鼠标效果结束时发生，需要和MouseEffecting同时使用，否则无效"), Category("自定义")]
        public event EventHandler MouseEffected;

        #endregion
        public SMButton()
        {
            InitializeComponent();
            this.TabStop = false;
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (BackColorShow)
            {
                BtnBackColor = Color.LightGray;
            }

            if (this.BtnClick != null)
            {

                BtnClick(this, e);
            }
        }
        private void lbl_MouseUp(object sender, MouseEventArgs e)
        {
            if (BackColorShow)
                BtnBackColor = MouseUpColor;
        }
        private void lbl_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}