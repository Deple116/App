using OpenCvSharp;
using SMLogControlLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraControlLibrary.CameraHIK
{
    public delegate void ShowImage(Mat mat);
    public class HIKCameraControl : CameraInterface
    {
        public const int ERROR_OK = 0;
        public const int ERROR_FAILED = -1;

        private List<string> m_AllCameras;

        public string LastError { get; private set; } = "";
       
        public string SN { get; set; } = ""; //sn
        public string StationName { get; set; } = ""; //工站
        public string SerialNumber { get; set; } = "";  //序号

        
        public string ExtraInfo { get; set; } = "";


        public ConcurrentQueue<CameraImageCallPack> cameraImageCallPack_Buffer;
        public ConcurrentQueue<ShowImage> ImageShowPack_Buffer;

        private GigeUsbCamera HikCamera;



        public HIKCameraControl(string _cameraName, string _cameraType)
        {
            CCDName = _cameraName;
            cameraType = _cameraType;


            cameraImageCallPack_Buffer = new ConcurrentQueue<CameraImageCallPack>();
            ImageShowPack_Buffer = new ConcurrentQueue<ShowImage>();
            HikCamera = new GigeUsbCamera();
            HikCamera.SendImageEvent += HikCamera_GetImageEvent;
        }

        ~HIKCameraControl()
        {
            DeInitial();
        }


        #region Interface
        public string CCDName { get; set; } = "";
        public string cameraType { get; set; } = "";
        public int Initial()
        {
            try
            {
                GigeUsbCamera.listAllDevices(ref m_AllCameras);
                if (m_AllCameras.Count <= 0)
                {
                    LastError = $"没有找到任何相机!";
                    return ERROR_FAILED;
                }

                if (!m_AllCameras.Contains(CCDName))
                {
                    LastError = $"没有找到{CCDName}!";
                    return ERROR_FAILED;
                }

                //打开相机
                if (!HikCamera.openDeviceForName(CCDName))
                {
                    LastError = $"打开相机{CCDName}失败!";
                    return ERROR_FAILED;
                }

                //if (!HikCamera.setSoftwareTriggerMode())
                //{
                //    LastError = $"设置相机{CCDName}为软触发失败!";
                //    return ERROR_FAILED;
                //}

                //if (!HikCamera.setExposure(13000))
                //{
                //    LastError = $"设置相机曝光失败!";
                //    return ERROR_FAILED;
                //}

                //打开采集
                if (!HikCamera.startGrab())
                {
                    LastError = $"相机{CCDName}开始采集失败!";
                }

                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int DeInitial()
        {
            try
            {
                if (HikCamera != null)
                {
                    if (HikCamera.closeDevice())
                    {
                        LastError = $"相机{CCDName}关闭失败!";
                        return ERROR_FAILED;
                    }
                    HikCamera = null;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int SetSoftwareTriggerMode()
        {
            if (!HikCamera.setSoftwareTriggerMode())
            {
                LastError = $"设置相机{CCDName}为软触发模式失败!";
                return ERROR_FAILED;
            }
            return ERROR_OK;
        }

        public int SetExternalTriggerMode()
        {
            if (!HikCamera.setExternalTriggerMode())
            {
                LastError = $"设置相机{CCDName}为外部触发模式失败!";
                return ERROR_FAILED;
            }
            return ERROR_OK;
        }

        public int SoftWareTriggerOnce()
        {
            try
            {
                if (!HikCamera.softWareTriggerOnce())
                {
                    LastError = $"相机{CCDName}单次软触发失败!";
                    return ERROR_FAILED;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int SetFreeRunMode()
        {
            try
            {
                if (HikCamera.setFreeRunMode())
                {
                    LastError = $"相机{CCDName}设置为自由采集失败!";
                    return ERROR_FAILED;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int StopGrab()
        {
            try
            {
                if (!HikCamera.stopGrab())
                {
                    LastError = $"相机{CCDName}停止采集失败!";
                    return ERROR_FAILED;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int StartGrab()
        {
            try
            {
                if (!HikCamera.startGrab())
                {
                    LastError = $"相机{CCDName}开始采集失败!";
                    return ERROR_FAILED;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int SetExposure(float _value)
        {
            try
            {
                if (HikCamera.setExposure(_value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}设置曝光时间失败!";
                    SMLogWindow.OutLog(LastError, Color.Red, loglevel:LogLevel.Error);
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int SetBlanceWhite(uint _rvalue, uint _gvalue, uint _bvalue)
        {
            try
            {
                if (HikCamera.setBlanceWhite(_rvalue, _gvalue, _bvalue))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}设置白平衡失败!";
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int SetTriggerDlay(float _value)
        {
            try
            {
                if (HikCamera.setTriggerDelay(_value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}设置触发延时失败.";
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int GetExposure(ref float _value)
        {
            try
            {
                if (HikCamera.getExposure(ref _value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}获取曝光时间失败!";
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int setGain(float _value)
        {
            try
            {
                if (HikCamera.setGain(_value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}设置增益失败!";
                    SMLogWindow.OutLog(LastError, Color.Red, loglevel:LogLevel.Error);
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int getGain(ref float _value)
        {
            try
            {
                if (HikCamera.getGain(ref _value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}获取增益失败!";
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public bool IsEmpty()
        {

            return cameraImageCallPack_Buffer.IsEmpty;
        }

        public CameraImageCallPack TryDequeue()
        {
            CameraImageCallPack cameraImageCallPack = new CameraImageCallPack();
            cameraImageCallPack_Buffer.TryDequeue(out cameraImageCallPack);
            return cameraImageCallPack;
        }

        public void HikCamera_GetImageEvent(ImagePack imagePack)
        {
            Task imageCllPack = CamShowImage(imagePack);
        }

        public async Task CamShowImage(ImagePack imagePack)
        {
            try
            {
                await Task.Run(() =>
                {
                    CameraImageCallPack imageCallPack = new CameraImageCallPack();
                    Mat mat = null;
                    if (imagePack.mono)
                        mat = new Mat(imagePack.height, imagePack.width, MatType.CV_8UC1, imagePack.data, imagePack.width);
                    else
                        mat = new Mat(imagePack.height, imagePack.width, MatType.CV_8UC3, imagePack.data, imagePack.width * 3);
                    imageCallPack.picture = mat.Clone();
                    imageCallPack.stationName = CCDName;
                    cameraImageCallPack_Buffer.Enqueue(imageCallPack);
                });
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
