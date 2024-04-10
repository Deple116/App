using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThridLibray;
using OpenCvSharp;

namespace CameraControlLibrary.CameraDahua
{
    public partial class SMCameraDahua : UserControl
    {
        private List<string> m_AllCameras;

        private DahuaCamera m_DahuaCamera;

        bool m_bShowByGDI; // 是否使用GDI绘图 | flag of using GDI to show image 

        private Graphics _g = null;

        public SMCameraDahua()
        {
            InitializeComponent();
        }

        private void SMCameraDahua_Load(object sender, EventArgs e)
        {
            m_AllCameras = new List<string>();
            m_DahuaCamera = new DahuaCamera();
            m_DahuaCamera.listAllDevices(ref m_AllCameras);
            comboBoxCamItems.Items.AddRange(m_AllCameras.ToArray());
            m_DahuaCamera.eventProcessImage += processShowImage;//注册相机回调事件
        }

        private void smButtonOpen_BtnClick(object sender, EventArgs e)
        {
            if (m_DahuaCamera.openDeviceForName(m_AllCameras[comboBoxCamItems.SelectedIndex]))
                MessageBox.Show("打开相机成功");
            else
                MessageBox.Show("打开相机失败");
        }

        private void smButtonSoftTrigger_BtnClick(object sender, EventArgs e)
        {
            if (m_DahuaCamera.setSoftwareTriggerMode())
                MessageBox.Show("设置软触发成功");
            else
                MessageBox.Show("设置软触发失败");
        }

        private void smButtonTrrigerOnce_BtnClick(object sender, EventArgs e)
        {
            if (m_DahuaCamera.softWareTriggerOnce())
                MessageBox.Show("触发相机成功");
            else
                MessageBox.Show("触发相机失败");
        }

        private void smButtonClose_BtnClick(object sender, EventArgs e)
        {
            if (m_DahuaCamera.closeDevice())
                MessageBox.Show("关闭相机成功");
            else
                MessageBox.Show("关闭相机失败");
        }

        /// <summary>
        /// 相机回调
        /// </summary>
        /// <param name="grabbedRawData"></param>
        private void processShowImage(IGrabbedRawData grabbedRawData, bool bColor)
        {
            try
            {
                // 主动调用回收垃圾 
                // call garbage collection 
                GC.Collect();
                // 图像转码成bitmap图像 
                // raw frame data converted to bitmap 
                var bitmap = grabbedRawData.ToBitmap(false);
                Mat mat = new Mat(grabbedRawData.Height, grabbedRawData.Width, MatType.CV_8UC1, arrtoptr(grabbedRawData.Image), grabbedRawData.Width);
                Cv2.ImWrite(@"C:\Users\Administrator\Desktop\Img\test.jpg", mat);
                m_bShowByGDI = true;
                if (m_bShowByGDI)
                {
                    // 使用GDI绘图 
                    // create graphic object 
                    if (_g == null)
                    {
                        _g = pictureBoxShow.CreateGraphics();
                    }
                    _g.DrawImage(bitmap, new Rectangle(0, 0, pictureBoxShow.Width, pictureBoxShow.Height),
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                    bitmap.Dispose();
                }
                else
                {
                    // 使用控件绘图,会造成主界面卡顿 
                    // assign bitmap to image control will cause main window reflect slowly  
                    if (InvokeRequired)
                    {
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                pictureBoxShow.Image = bitmap;
                            }
                            catch (Exception exception)
                            {
                                Catcher.Show(exception);
                            }
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 图像 byte[] 转 Intptr
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        IntPtr arrtoptr(byte[] array)
        {
            return System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
        }

        private void smButtonStartGather_BtnClick(object sender, EventArgs e)
        {

        }

        private void smButtonFreeGather_BtnClick(object sender, EventArgs e)
        {

        }
    }
}
