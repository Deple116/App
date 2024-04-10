using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SmoreControlLibrary.SMImage
{

    public delegate void SendMsg(object sender, int index);

    public partial class SMPictureEdit : UserControl
    {
        public SMPictureEdit()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(10, 10);
            _oldRectf = GetClientRectangleF();
        }

        public event Action<PointF> GetImageViewPointFEvent;

        #region 公开属性
        [Browsable(true)]
        [Description("图片")]
        public Image Image
        {
            get { return _img;}
            set
            {
                if (_img != null)
                    _img.Dispose();
                _img = value;
                if (_img != null)
                {
                    _img = CloneC(_img);
                    _isAutoFit = true;
                    CalcuAutoSize();
                }
                this.Invalidate();
            }
        }
        private Image _img = null;

        [Browsable(true)]
        [Description("是否允许右键菜单")]
        [DefaultValue(true)]
        public bool AllowRightMenu
        {
            get { return _allowRightMenu; }
            set { _allowRightMenu = value; }
        }
        private bool _allowRightMenu = true;

        [Browsable(true)]
        [Description("自定义边框颜色")]
        public Color CustomBorderColor
        {
            get { return _customBorderColor; }
            set { _customBorderColor = value; }
        }
        private Color _customBorderColor = Color.Black;

        [Browsable(true)]
        [Description("自定义边框宽度")]
        public int CustomBorderWidth
        {
            get { return _customBorderWidth; }
            set { _customBorderWidth = value; }
        }
        private int _customBorderWidth = 1;

        [Browsable(true)]
        [Description("自定义边框可见性")]
        [DefaultValue(false)]
        public bool CustomBorderVisible
        {
            get { return _customBorderVisible; }
            set
            {
                _customBorderVisible = value;
            }
        }
        private bool _customBorderVisible = false;

        /// <summary>
        /// 手动触发相机事件
        /// </summary>
        [Category("SmoreControl"), Description("手动触发相机时间")]
        public event EventHandler TriggerCamera;

        /// <summary>
        /// 手动触发相机事件
        /// </summary>
        [Category("SmoreControl"), Description("手动传统触发相机")]
        public SendMsg TriggerCameraTra;

        /// <summary>
        /// 手动加载单张图片事件
        /// </summary>
        [Category("SmoreControl"), Description("手动加载单张图片事件")]
        public SendMsg AddSinglePicture;

        #endregion

        #region 私有字段
        //源矩形
        private RectangleF _srcRectf;
        //图片缩放矩形
        private RectangleF _zoomRectf;
        //目标矩形
        private RectangleF _desRectf;

        //最小缩放系数
        private int _minZoom = 1;
        //最大缩放系数
        private int _maxZoom = 3200;
        //当前缩放系数
        private float _currentZoom;
        //是否为自适应状态
        private bool _isAutoFit = false;
        #endregion

        #region 事件处理程序

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }

        private RectangleF _oldRectf;
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_img == null)
                return;
            if (_isAutoFit)
            {
                CalcuAutoSize();
            }
            else
            {
                //如果之前图片全在容器内
                if (_oldRectf.Contains(_zoomRectf))
                {
                    var borderWidth = _customBorderVisible ? _customBorderWidth : 0;
                    _zoomRectf.X = (_zoomRectf.X - borderWidth) / _oldRectf.Width * (this.Width - 2 * _customBorderWidth);
                    _zoomRectf.Y = (_zoomRectf.Y - borderWidth) / _oldRectf.Height * (this.Height - 2 * _customBorderWidth);
                }
                _desRectf = GetClientRectangleF();
                _desRectf.Intersect(_zoomRectf);
                CalcuSrcRectangleF();
            }
            _oldRectf = GetClientRectangleF();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_img == null)
                return;
            e.Graphics.DrawImage(_img, _desRectf, _srcRectf, GraphicsUnit.Pixel);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_img == null)
                return;
            if (!this.ClientRectangle.Contains(e.Location))
                return;

            #region zoom
            _isAutoFit = false;
            float zoom;
            if (e.Delta > 0)
            {
                if (_currentZoom < _maxZoom)
                {
                    zoom = _currentZoom * 1.1f;
                }
                else
                {
                    zoom = _maxZoom;
                }
                if (zoom > _maxZoom)
                    zoom = _maxZoom;
            }
            else
            {
                if (_currentZoom > _minZoom)
                    zoom = _currentZoom * 0.9f;
                else
                    zoom = _minZoom;
                if (zoom < _minZoom)
                    zoom = _minZoom;
            }

            #endregion

            #region RectangleF
            //滚轮相对于zoom图片的位置
            PointF px;
            if (_desRectf.Contains(e.Location))
                px = new PointF(e.X, e.Y);
            else
                px = new PointF(_desRectf.X + _desRectf.Width / 2, _desRectf.Y + _desRectf.Height / 2);

            var pxScaleX = (px.X - _zoomRectf.X) / _zoomRectf.Width;
            var pxScaleY = (px.Y - _zoomRectf.Y) / _zoomRectf.Height;
            _zoomRectf.X = _zoomRectf.X - _img.Width / 100f * (zoom - _currentZoom) * pxScaleX;
            _zoomRectf.Y = _zoomRectf.Y - _img.Height / 100f * (zoom - _currentZoom) * pxScaleY;
            _zoomRectf.Width = _img.Width / 100f * zoom;
            _zoomRectf.Height = _img.Height / 100f * zoom;

            _desRectf = GetClientRectangleF();
            _desRectf.Intersect(_zoomRectf);

            CalcuSrcRectangleF();

            #endregion

            _currentZoom = zoom;

            this.Invalidate();
        }

        private bool _mouseDown = false;
        private Point _movePoint;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_desRectf.Contains(e.Location))
            {
                _mouseDown = true;
                _movePoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_desRectf.Contains(e.Location))
            {
                this.Cursor = Cursors.Hand;
                if (_mouseDown)
                {
                    _isAutoFit = false;
                    _zoomRectf.X += e.X - _movePoint.X;
                    _zoomRectf.Y += e.Y - _movePoint.Y;
                    _desRectf = GetClientRectangleF();
                    _desRectf.Intersect(_zoomRectf);
                    CalcuSrcRectangleF();
                    _movePoint = e.Location;
                    this.Invalidate();
                }
            }
            else
                this.Cursor = Cursors.Default;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _mouseDown = false;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_img == null)
                return;
            if (!_desRectf.Contains(e.Location))
                return;
            if (this.GetImageViewPointFEvent != null)
            {
                var pointf = ViewPointToImage(e.Location);
                if (pointf.HasValue)
                    this.GetImageViewPointFEvent(pointf.Value);
            }

        }

        #endregion

        #region 私有方法

        //通过_zoomRectf和desRectf计算_srcRectf
        private void CalcuSrcRectangleF()
        {
            var srcScaleX = (_desRectf.X - _zoomRectf.X) / _zoomRectf.Width;
            var srcScaleY = (_desRectf.Y - _zoomRectf.Y) / _zoomRectf.Height;
            var srcScaleW = _desRectf.Width / _zoomRectf.Width;
            var srcScaleH = _desRectf.Height / _zoomRectf.Height;
            _srcRectf.X = _img.Width * srcScaleX;
            _srcRectf.Y = _img.Height * srcScaleY;
            _srcRectf.Width = _img.Width * srcScaleW;
            _srcRectf.Height = _img.Height * srcScaleH;
        }

        //绘制一张新的图片
        private Image CloneC(Image img, PixelFormat format = PixelFormat.Format32bppArgb)
        {
            var bmp = new Bitmap(img.Width, img.Height, format);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            return bmp;
        }

        //计算自适应的相关参数
        private void CalcuAutoSize()
        {
            if (_img == null)
                return;
            var clientRectf = GetClientRectangleF();
            var scale = 1f;
            if (_img.Width > clientRectf.Width || _img.Height > clientRectf.Height)
            {
                var xscale = _img.Width / clientRectf.Width;
                var yscale = _img.Height / clientRectf.Height;
                scale = xscale > yscale ? xscale : yscale;
            }
            _currentZoom = 100 / scale;
            var autoWidth = _img.Width / scale;
            var autoHeight = _img.Height / scale;
            var zoomx = (clientRectf.Width - autoWidth) / 2;
            var zoomy = (clientRectf.Height - autoHeight) / 2;
            _srcRectf = new RectangleF(0, 0, _img.Width, _img.Height);
            _zoomRectf = new RectangleF(zoomx, zoomy, autoWidth, autoHeight);
            _desRectf = _zoomRectf;
        }

        //获取工作区域（去除边框）
        private RectangleF GetClientRectangleF()
        {
            if (_customBorderVisible)
            {
                return new RectangleF(_customBorderWidth, _customBorderWidth, this.Width - 2 * _customBorderWidth, this.Height - 2 * _customBorderWidth);
            }
            else
            {
                return new RectangleF(0, 0, this.Width, this.Height);
            }
        }

        #endregion

        #region 右键菜单
        //右键菜单弹出前判断
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!this.AllowRightMenu)
            {
                e.Cancel = true;
                return;
            }
            var enable = _img == null ? false : true;
            this.自适应ToolStripMenuItem.Enabled = enable;
            
        }

        private void 自适应ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFit();
        }

        //相机触发
        private void 相机触发toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void AItoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.TriggerCamera != null)
                TriggerCamera(this, e);
        }

        private void 检测1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TriggerCameraTra != null)
            {
               // e.
                TriggerCameraTra(this,1 );
            }
               
        }

        private void 检测2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TriggerCameraTra != null)
                TriggerCameraTra(this, 2);
        }

        private void 检测3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.TriggerCameraTra != null)
                TriggerCameraTra(this, 3);
        }

        private void 检测3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.AddSinglePicture != null)
                AddSinglePicture(this, 3);
        }


        private void aIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.AddSinglePicture != null)
                AddSinglePicture(this, 0);
        }

        private void 检测1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.AddSinglePicture != null)
                AddSinglePicture(this,1);
        }

        private void 检测2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.AddSinglePicture != null)
                AddSinglePicture(this, 2);
        }

        

        


        //加载图片
        private void 加载图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            //var dlg = new OpenFileDialog();
            //dlg.Title = "选择图片";
            //dlg.Filter = "图片文件|*.png;*.jpg;*.bmp";
            //dlg.AutoUpgradeEnabled = true;
            //dlg.CheckFileExists = true;
            //dlg.CheckPathExists = true;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    using (var img = Image.FromFile(dlg.FileName))
            //    {
            //        this.Image = img;
            //    }
            //}

        }

        private void pNG格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToPng();
        }

        private void jPG格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToJpeg();
        }

        private void bmp格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToBmp();
        }

        private void 左转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateToLeft();
        }

        private void 右转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateToRight();
        }


        #endregion

        #region 公用方法

        /// <summary>
        /// 自适应
        /// </summary>
        public void SetFit()
        {
            if (_img == null)
                return;
            _isAutoFit = true;
            CalcuAutoSize();
            this.Invalidate();
        }

        /// <summary>
        /// 根据容器上的点获取相对于图片的点位置
        /// </summary>
        /// <param name="pf">相对于容器的点</param>
        /// <returns></returns>
        public PointF? ViewPointToImage(PointF pf)
        {
            if (_img == null)
                return null;
            if (!_desRectf.Contains(pf))
                return null;
            var x = (pf.X - _zoomRectf.X) / _currentZoom * 100;
            var y = (pf.Y - _zoomRectf.Y) / _currentZoom * 100;
            return new PointF(x, y);
        }

        /// <summary>
        /// 保存至BMP格式
        /// </summary>
        public void ExportToBmp()
        {
            if (_img == null)
                return;
            var dlg = new SaveFileDialog();
            dlg.Title = "保存Bmp格式图片";
            dlg.Filter = "图片|*.bmp";
            dlg.AutoUpgradeEnabled = true;
            dlg.AddExtension = true;
            dlg.DefaultExt = ".bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _img.Save(dlg.FileName, ImageFormat.Bmp);
            }
        }

        /// <summary>
        /// 保存至Png格式
        /// </summary>
        public void ExportToPng()
        {
            if (_img == null)
                return;
            var dlg = new SaveFileDialog();
            dlg.Title = "保存PNG格式图片";
            dlg.Filter = "图片|*.png";
            dlg.AutoUpgradeEnabled = true;
            dlg.AddExtension = true;
            dlg.DefaultExt = ".png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _img.Save(dlg.FileName, ImageFormat.Png);
            }
        }

        /// <summary>
        /// 保存至Jpeg格式
        /// </summary>
        public void ExportToJpeg()
        {
            if (_img == null)
                return;
            var dlg = new SaveFileDialog();
            dlg.Title = "保存Jpeg格式图片";
            dlg.Filter = "图片|*.jpg";
            dlg.AutoUpgradeEnabled = true;
            dlg.AddExtension = true;
            dlg.DefaultExt = ".jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _img.Save(dlg.FileName, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 向右旋转90度
        /// </summary>
        public void RotateToRight()
        {
            _img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            CalcuAutoSize();
            this.Invalidate();
        }

        /// <summary>
        /// 向左旋转90度
        /// </summary>
        public void RotateToLeft()
        {
            _img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            CalcuAutoSize();
            this.Invalidate();
        }

        /// <summary>
        /// 放大图片
        /// </summary>
        public void EnlargeImage()
        {
            if (_img == null)
                return;
            #region zoom
            _isAutoFit = false;
            float zoom;
            if (_currentZoom < _maxZoom)
            {
                zoom = _currentZoom * 1.1f;
            }
            else
            {
                zoom = _maxZoom;
            }
            if (zoom > _maxZoom)
                zoom = _maxZoom;
            #endregion
            #region RectangleF
            PointF px;
            px = new PointF(_desRectf.X + _desRectf.Width / 2, _desRectf.Y + _desRectf.Height / 2);

            var pxScaleX = (px.X - _zoomRectf.X) / _zoomRectf.Width;
            var pxScaleY = (px.Y - _zoomRectf.Y) / _zoomRectf.Height;
            _zoomRectf.X = _zoomRectf.X - _img.Width / 100f * (zoom - _currentZoom) * pxScaleX;
            _zoomRectf.Y = _zoomRectf.Y - _img.Height / 100f * (zoom - _currentZoom) * pxScaleY;
            _zoomRectf.Width = _img.Width / 100f * zoom;
            _zoomRectf.Height = _img.Height / 100f * zoom;
            _desRectf = GetClientRectangleF();
            _desRectf.Intersect(_zoomRectf);
            CalcuSrcRectangleF();
            #endregion
            _currentZoom = zoom;
            this.Invalidate();
        }

        /// <summary>
        /// 缩小图片
        /// </summary>
        public void NarrowImage()
        {
            if (_img == null)
                return;
            #region zoom
            _isAutoFit = false;
            float zoom;
            if (_currentZoom > _minZoom)
                zoom = _currentZoom * 0.9f;
            else
                zoom = _minZoom;
            if (zoom < _minZoom)
                zoom = _minZoom;
            #endregion
            #region RectangleF
            PointF px;
            px = new PointF(_desRectf.X + _desRectf.Width / 2, _desRectf.Y + _desRectf.Height / 2);

            var pxScaleX = (px.X - _zoomRectf.X) / _zoomRectf.Width;
            var pxScaleY = (px.Y - _zoomRectf.Y) / _zoomRectf.Height;
            _zoomRectf.X = _zoomRectf.X - _img.Width / 100f * (zoom - _currentZoom) * pxScaleX;
            _zoomRectf.Y = _zoomRectf.Y - _img.Height / 100f * (zoom - _currentZoom) * pxScaleY;
            _zoomRectf.Width = _img.Width / 100f * zoom;
            _zoomRectf.Height = _img.Height / 100f * zoom;
            _desRectf = GetClientRectangleF();
            _desRectf.Intersect(_zoomRectf);
            CalcuSrcRectangleF();
            #endregion
            _currentZoom = zoom;
            this.Invalidate();
        }

        public void AddImage(string fileName)
        {
            try
            {
                using (var img = Image.FromFile(fileName))
                {
                    this.Image = img;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        #endregion

       
    }
}
