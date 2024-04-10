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

namespace SmoreControlLibrary.EquipmentDriver.CameraHIK
{
    public partial class SMCameraHIK : UserControl
    {
        private GigeUsbCamera m_Camera = new GigeUsbCamera();

        private List<string> m_AllCameras;

        public int seze = 0;

        public SMCameraHIK()
        {
            InitializeComponent();

            GigeUsbCamera.listAllDevices(ref m_AllCameras);
            comboBoxCamItems.Items.AddRange(m_AllCameras.ToArray());
            m_Camera.SendImageEvent += getOneImage;
        }

        private void getOneImage(ImagePack pack)
        {
            showPicture(pack);
        }

        private void SMCameraHIK_Load(object sender, EventArgs e)
        {

        }

        private void smButtonOpen_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.openDeviceForName(m_AllCameras[comboBoxCamItems.SelectedIndex]))
                MessageBox.Show("打开相机成功");
            else
                MessageBox.Show("打开相机失败");
        }

        private void smButtonClose_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.closeDevice())
                MessageBox.Show("关闭相机成功");
            else
                MessageBox.Show("关闭相机失败");
        }

        private void smButtonTrrigerOnce_BtnClick(object sender, EventArgs e)
        {
            if (!m_Camera.softWareTriggerOnce())
            {
                MessageBox.Show("触发相机失败");
                return;
            }
        }

        private void smButtonStartGather_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.startGrab())
                MessageBox.Show("采集成功");
            else
                MessageBox.Show("采集失败");
        }

        private void smButtonStopGather_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.stopGrab())
                MessageBox.Show("停止采集成功");
            else
                MessageBox.Show("停止采集失败");
        }

        private void smButtonSoftTrigger_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.setSoftwareTriggerMode())
                MessageBox.Show("设置软触发成功");
            else
                MessageBox.Show("设置软触发失败");
        }

        private void smButtonFreeGather_BtnClick(object sender, EventArgs e)
        {
            if (m_Camera.setFreeRunMode())
                MessageBox.Show("设置自由采集成功");
            else
                MessageBox.Show("设置自由采集失败");
        }

        private void showPicture(ImagePack pack)
        {
            Task task = showImage(pack);
        }

        private async Task showImage(ImagePack pack)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(seze);
                Mat mat = null;
                if (pack.mono)
                    mat = new Mat(pack.height, pack.width, MatType.CV_8UC1, pack.data, pack.width);
                else
                    mat = new Mat(pack.height, pack.width, MatType.CV_8UC3, pack.data, pack.width * 3);
                Bitmap map = Visualize(mat);
                if (pictureBoxShow.Image != null)
                {
                    pictureBoxShow.Image.Dispose();
                }
                pictureBoxShow.Image = map;
            });
        }

        private Bitmap Visualize(Mat mat)
        {
            Bitmap bitmap = new Bitmap(mat.Cols, mat.Rows, (int)mat.Step(), PixelFormat.Format24bppRgb, mat.Data);
            return bitmap;
        }
    }
}
