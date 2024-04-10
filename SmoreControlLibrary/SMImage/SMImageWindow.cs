using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using SmoreControlLibrary.SMData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmoreControlLibrary.SMImage
{

    public delegate void SetDateMsg(string[] info);

    public partial class SMImageWindow : UserControl
    {

        private string _iamgeWindowType;
        [Category("SmoreControl"), Description("窗体算法类型")]
        public string IamgeWindowType
        {
            get { return _iamgeWindowType; }
            set { _iamgeWindowType = value; }
        }


        private string _imageWindowName;
        [Category("SmoreControl"), Description("窗体名称")]
        public string ImageWindowName
        {
            get { return _imageWindowName; }
            set { _imageWindowName = value; this.labelImageWindowName.Text = $"{value}";}
        }
        private string _iamgeWindowCT;
        [Category("SmoreControl"), Description("推理CT")]
        public string IamgeWindowCT
        {
            get { return _iamgeWindowCT; }
            set { _iamgeWindowCT = value; this.labelIamgeWindowCT.Text = $"{value}";}
        }

        private int _imageWindowOK;
        [Category("SmoreControl"), Description("OK统计")]
        public int ImageWindowOK
        {
            get { return _imageWindowOK; }
            set { _imageWindowOK = value; this.labelOK.Text = $"OK：{value}"; }
        }

        private int _imageWindowNG;
        [Category("SmoreControl"), Description("NG统计")]
        public int ImageWindowNG
        {
            get { return _imageWindowNG; }
            set { _imageWindowNG = value; this.labelNG.Text = $"NG：{value}"; }
        }

        private string _iamgeWindowTotal;
        [Category("SmoreControl"), Description("总数统计")]
        public string IamgeWindowTotal
        {
            get { return _iamgeWindowTotal; }
            set { _iamgeWindowTotal = value; this.labelTotal.Text = $"Total：{value}";}
        }

        private string _iamgeWindowRatio;
        [Category("SmoreControl"), Description("良率统计")]
        public string IamgeWindowRatio
        {
            get { return _iamgeWindowRatio; }
            set { _iamgeWindowRatio = value; this.labelRatio.Text = $"Ratio：{value}";}
        }

        private bool _showManualButton = true;
        [Category("SmoreControl"), Description("是否显示手动按钮")]
        public bool ShowManualButton
        {
            get { return _showManualButton; }
            set {
                    _showManualButton = value;
                    panel4.Visible = _showManualButton;
                    panel5.Visible = _showManualButton;
                    panel6.Visible = _showManualButton;
                   // tableLayoutPanel3.Visible = _showManualButton;

                    //contextMenuStrip1.Enabled = _showManualButton;
            }
        }


        /// <summary>
        /// 手动设置日期
        /// </summary>
        [Description("手动设置日期按钮点击事件"), Category("SmoreControl")]
        public SetDateMsg m_setDateMsg;


        /// <summary>
        /// 按钮点击事件推理单张图片
        /// </summary>
        [Description("推理单张图按钮点击事件"), Category("SmoreControl")]
        public event EventHandler BtnRunSinglePicClick;

        /// <summary>
        /// 按钮点击事件推理整个文件夹中的图片
        /// </summary>
        [Description("推理文件夹中图按钮点击事件"), Category("SmoreControl")]
        public event EventHandler BtnRunFolderPicClick;

        /// <summary>
        /// 手动触发相机事件
        /// </summary>
        [Category("SmoreControl"), Description("手动触发相机时间")]
        public event EventHandler TriggerCamera;

        private static object tag_locker;
        private static string LastError = "";
        private static  string configCLSPath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + $"CLSData.xml";
        private static  string configOCRPath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + $"OCRData.xml";
        private static  CLSDataParse m_CLSDataParse = new CLSDataParse();
        private static  OCRDataParse m_OCRDataParse = new OCRDataParse();

        public static object obj = new object();
        private bool BtnState = false;

        public SMImageWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void SMImageWindow_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(panel4, "单帧采图");
            toolTip1.SetToolTip(panel5, "单张图片测试");
            toolTip1.SetToolTip(panel6, "文件夹批量图片测试");
        }

        public  void InitData()
        {
            try
            {
                if (IamgeWindowType == "CLS")
                {
                    GetCLSData();
                    labelTotal.Text = "Total：" + m_CLSDataParse.CLSData.Total.ToString();
                    labelOK.Text = "OK：" + m_CLSDataParse.CLSData.OK.ToString();
                    labelNG.Text = "NG：" + m_CLSDataParse.CLSData.NG.ToString();
                    labelRatio.Text = "Ratio：" + (m_CLSDataParse.CLSData.Total == 0 ?
                    "0" : ((float)m_CLSDataParse.CLSData.OK / (float)m_CLSDataParse.CLSData.Total).ToString("P"));
                }
                else if (IamgeWindowType == "OCR")
                {
                    GetOCRData();
                    labelTotal.Text = "Total：" + m_OCRDataParse.OCRData.Total.ToString();
                    labelOK.Text = "OK：" + m_OCRDataParse.OCRData.OK.ToString();
                    labelNG.Text = "NG：" + m_OCRDataParse.OCRData.NG.ToString();
                    labelRatio.Text = "Ratio：" + (m_OCRDataParse.OCRData.Total == 0 ?
                    "0" : ((float)m_OCRDataParse.OCRData.OK / (float)m_OCRDataParse.OCRData.Total).ToString("P"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public int GetCLSData()
        {
            return XMLSerialize.DeserializeFromXml<CLSDataParse>(configCLSPath, ref m_CLSDataParse, ref LastError);
        }

        public int GetOCRData()
        {
            return XMLSerialize.DeserializeFromXml<OCRDataParse>(configOCRPath, ref m_OCRDataParse, ref LastError);
        }

        public int  ClearProduceData()
        {
            if (IamgeWindowType == "CLS")
            {
                labelNG.Text = "NG：0";
                labelOK.Text = "OK：0";
                labelTotal.Text = "Total：0";
                labelRatio.Text = "Ratio：0.00%";
                m_CLSDataParse.CLSData.Total = 0;
                m_CLSDataParse.CLSData.OK = 0;
                m_CLSDataParse.CLSData.NG = 0;
                return XMLSerialize.WriteToXML(configCLSPath, m_CLSDataParse, ref LastError);
            }
            else if (IamgeWindowType == "OCR")
            {
                labelNG.Text = "NG：0";
                labelOK.Text = "OK：0";
                labelTotal.Text = "Total：0";
                labelRatio.Text = "Ratio：0.00%";
                m_OCRDataParse.OCRData.Total = 0;
                m_OCRDataParse.OCRData.OK = 0;
                m_OCRDataParse.OCRData.NG = 0;
                return XMLSerialize.WriteToXML(configOCRPath, m_OCRDataParse, ref LastError);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///计数
        /// </summary>
        /// <param name="DataResult"></param>
        public void  AddData(bool DataResult)
        {
            if (IamgeWindowType == "CLS")
            {
                try
                {
                    ++m_CLSDataParse.CLSData.Total;

                    if (DataResult)
                    {
                        ++m_CLSDataParse.CLSData.OK;
                    }
                    else
                    {
                        ++m_CLSDataParse.CLSData.NG;
                    }
                    labelTotal.Text = "Total：" + m_CLSDataParse.CLSData.Total.ToString();
                    labelOK.Text = "OK：" + m_CLSDataParse.CLSData.OK.ToString();
                    labelNG.Text = "NG：" + m_CLSDataParse.CLSData.NG.ToString();
                    labelRatio.Text = "Ratio：" + (m_CLSDataParse.CLSData.Total == 0 ?
                    "0" : ((float)m_CLSDataParse.CLSData.OK / (float)m_CLSDataParse.CLSData.Total).ToString("P"));
                    XMLSerialize.SerializeToXml<CLSDataParse>(configCLSPath, m_CLSDataParse, ref LastError);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    ++m_OCRDataParse.OCRData.Total;

                    if (DataResult)
                    {
                        ++m_OCRDataParse.OCRData.OK;
                    }
                    else
                    {
                        ++m_OCRDataParse.OCRData.NG;
                    }
                    labelTotal.Text = "Total：" + m_OCRDataParse.OCRData.Total.ToString();
                    labelOK.Text = "OK：" + m_OCRDataParse.OCRData.OK.ToString();
                    labelNG.Text = "NG：" + m_OCRDataParse.OCRData.NG.ToString();
                    labelRatio.Text = "Ratio：" + (m_OCRDataParse.OCRData.Total == 0 ?
                    "0" : ((float)m_OCRDataParse.OCRData.OK / (float)m_OCRDataParse.OCRData.Total).ToString("P"));
                    XMLSerialize.SerializeToXml<OCRDataParse>(configOCRPath, m_OCRDataParse, ref LastError);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="mat"></param>
        public void ImageShow(Mat mat)
        {
            lock(obj)
            {
                pictureBoxImgShow.Image = new Bitmap(mat.ToMemoryStream());
            }
           
            GC.Collect();
        }

        /// <summary>
        /// 显示结果
        /// </summary>
        /// <param name="result"></param>
        public void ResultShow(bool result)
        {
            if (result)
            {
                AddData(result);
                plResult.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.OK;
            }
            else
            {
                AddData(result);
                plResult.BackgroundImage = global::SmoreControlLibrary.Properties.Resources.NG;
            }
        }

        /// <summary>
        /// 显示结果
        /// </summary>
        /// <param name="_result"></param>
        public void HeartResultShow(Color _color)
        {
            plHeartSignal.BackColor = _color;
        }

        /// <summary>
        /// 推理单张图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel5_Click(object sender, EventArgs e)
        {
            if (this.BtnRunSinglePicClick != null)
                BtnRunSinglePicClick(this, e);
        }

        /// <summary>
        /// 推理文件夹中的所有图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel6_Click(object sender, EventArgs e)
        {
            if (this.BtnRunFolderPicClick != null)
                BtnRunFolderPicClick(this, e);
        }

        /// <summary>
        /// 右键软触发相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 触发相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TriggerCamera != null)
                TriggerCamera(this, e);
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            if (this.TriggerCamera != null)
                TriggerCamera(this, e);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
