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
using System.Drawing.Imaging;

namespace CameraControlLibrary
{
    public partial class SMCameraBasler : UserControl
    {
        private const int ERR_OK = 0;
        private const int ERR_FAIL = -1;

        private long count1 = 0;

        private BaslerCam m_BaslerCam;
        private List<string> m_AllCameras;

        public SMCameraBasler()
        {
            InitializeComponent();
        }

        private void SMCameraBasler_Load(object sender, EventArgs e)
        {
            m_BaslerCam = new BaslerCam();
            m_BaslerCam.listAllDevices(ref m_AllCameras);
            comboBoxCamItems.Items.AddRange(m_AllCameras.ToArray());
            m_BaslerCam.eventProcessImage += processHImage;         // 注册显示回调函数
            m_BaslerCam.eventComputeGrabTime += computeGrabTime;    // 注册计算采集图像时间回调函数
        }

        private void smButtonOpen_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.openDeviceForName(m_AllCameras[comboBoxCamItems.SelectedIndex]))
                MessageBox.Show("打开相机成功");
            else
                MessageBox.Show("打开相机失败");
        }

        private void smButtonClose_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.CloseCam())
                MessageBox.Show("关闭设备成功");
            else
                MessageBox.Show("关闭设备失败");
        }

        private void smButtonTrrigerOnce_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.SetSoftwareTrigger())
                MessageBox.Show("设置为软触发成功");
            else
                MessageBox.Show("设置为软触发失败");
        }

        private void smButtonStartGather_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.SetExternTrigger())
                MessageBox.Show("设置为外部触发成功");
            else
                MessageBox.Show("设置为外部触发失败");
        }

        private void smButtonStopGather_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.StartGrabbing())
                MessageBox.Show("设置为连续采集成功");
            else
                MessageBox.Show("设置为连续采集失败");
        }

        private void smButtonSoftTrigger_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.StartGrabbing())
                MessageBox.Show("设置为单张采集成功");
            else
                MessageBox.Show("设置为单张采集失败");
        }

        private void smButtonFreeGather_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.StopGrabbing())
                MessageBox.Show("停止采集成功");
            else
                MessageBox.Show("停止采集失败");
        }

        private void smButton1_BtnClick(object sender, EventArgs e)
        {
            if (m_BaslerCam.SendSoftwareExecute())
                MessageBox.Show("触发采集成功");
            else
                MessageBox.Show("触发采集失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isColor">是否是彩色图</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="frameAddress"></param>
        private void processHImage(Boolean isColor, int width, int height, IntPtr frameAddress)
        {
            Mat mat = null;
            if (isColor)
            {

                mat = new Mat(height, width, MatType.CV_8UC3, frameAddress, width * 3);
            }
            else
            {
                mat = new Mat(height, width, MatType.CV_8UC1, frameAddress, width);
            }

            pictureBoxShow.Image = Visualize(mat);
        }

        private Bitmap Visualize(Mat mat)
        {
            Bitmap bitmap = new Bitmap(mat.Cols, mat.Rows, (int)mat.Step(), PixelFormat.Format24bppRgb, mat.Data);
            return bitmap;
        }

        // 计算相机采集图像时间
        private void computeGrabTime(long time)
        {
            ++count1;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(()=> { label1.Text = "[  Count : " + count1 + "  ]"; }));
                this.Invoke(new Action(() => { label2.Text = "[  Time : " + time + "  ]"; }));
            }
        }
    }
}
