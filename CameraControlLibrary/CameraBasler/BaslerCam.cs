/*
 * Target：此Demo是基于Pylon5版本的开发库basler.pylon.dll所写，支持黑白/彩色的Gige/USB3.0相机。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Basler.Pylon;

namespace CameraControlLibrary
{
    public class BaslerCam
    {
        public  String strModelName = null;
        public  String strSerialNumber = null;
        public  String strUserID = null;

        public  int imageWidth = 0;          // 图像宽
        public  int imageHeight = 0;         // 图像高
        public  long payloadSize = 0;        // 图像大小
        public  long minExposureTime = 0;    // 最小曝光时间
        public  long maxExposureTime = 0;    // 最大曝光时间
        public  long minGain = 0;            // 最小增益
        public  long maxGain = 0;            // 最大增益
        public  int numWindowIndex = 0;      // pylon自带窗体编号

        private  long grabTime = 0;                          // 采集图像时间
        private  Boolean isColor = false;                    // 判断是否是彩色图像
        private  IntPtr latestFrameAddress = IntPtr.Zero;    // 图像格式转换后的首地址，用于pylon转halcon,visionpro等图像变量
        private  Stopwatch stopWatch = new Stopwatch();
        private  Camera camera = null;
        private  PixelDataConverter converter = new PixelDataConverter();

        // 枚举相机列表
        private  List<ICameraInfo> m_AllCameraInfos;

        /// <summary>
        /// 计算采集图像时间自定义委托
        /// </summary>
        /// <param name="time">采集图像时间</param>
        public delegate void delegateComputeGrabTime(long time);
        /// <summary>
        /// 计算采集图像时间委托事件
        /// </summary>
        public event delegateComputeGrabTime eventComputeGrabTime;

        /// <summary>               
        /// 图像处理自定义委托
        /// </summary>
        public delegate void delegateProcessHImage(Boolean isColor, int width, int height, IntPtr frameAddress);
        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        public event delegateProcessHImage eventProcessImage;

        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        static Version Sfnc2_0_0 = new Version(2, 0, 0);

        public BaslerCam()
        {
       
        }

        /// <summary>
        /// 根据相机UserIP实例化相机
        /// </summary>
        /// <param name="UserIP"></param>
        public BaslerCam(string UserIP)
        {
            try
            {
                // 枚举相机列表
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in allCameraInfos)
                {
                    if (UserIP == cameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        camera = new Camera(cameraInfo);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("未识别到UserIP为“" + UserIP+ "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 枚举出所有的相机自定义名称
        /// </summary>
        /// <param name="_list"></param>
        /// <returns></returns>
        public bool listAllDevices(ref List<string> _list)
        {
            try
            {
                if (null == _list)
                    _list = new List<string>();
                m_AllCameraInfos = CameraFinder.Enumerate();
                foreach (ICameraInfo cameraInfo in m_AllCameraInfos)
                {
                    _list.Add(cameraInfo[CameraInfoKey.UserDefinedName]);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 按照自定义名称打开设备
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool openDeviceForName(string name)
        {
            try
            {
                foreach (ICameraInfo cameraInfo in m_AllCameraInfos)
                {
                    if (name == cameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        camera = new Camera(cameraInfo);
                    }
                }

                if (camera == null)
                {
                    return false;
                }
                else
                {
                    if (OpenCam())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        public bool OpenCam()
        {
            try
            {
                camera.Open();
                if (camera.IsOpen)
                {
                    //camera.Parameters[PLCamera.AcquisitionFrameRateEnable].SetValue(true);  // 限制相机帧率使能
                    //camera.Parameters[PLCamera.AcquisitionFrameRateAbs].SetValue(90);       // 设置最大帧率值
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(10);          // 设置内存中接收图像缓冲区大小

                    strModelName = camera.CameraInfo[CameraInfoKey.ModelName];              // 获取相机型号
                    strSerialNumber = camera.CameraInfo[CameraInfoKey.SerialNumber];        // 获取相机序列号
                    strUserID = camera.CameraInfo[CameraInfoKey.UserDefinedName];           // 获取相机用户自定义名称

                    imageWidth = (int)camera.Parameters[PLCamera.Width].GetValue();         // 获取图像宽 
                    imageHeight = (int)camera.Parameters[PLCamera.Height].GetValue();       // 获取图像高
                    payloadSize = imageWidth * imageHeight;                                 // 计算图像分辨率
                    GetMinMaxExposureTime();                                                // 获取最大最小曝光值 
                    GetMinMaxGain();                                                        // 获取最大最小增益值
                    camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                    // 注册采集回调函数
                    camera.ConnectionLost += OnConnectionLost;                              // 注册掉线回调函数
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 关闭相机,释放相关资源
        /// </summary>
        public bool CloseCam()
        {
            try
            {
                camera.Close();
                if (latestFrameAddress != null)
                {
                    Marshal.FreeHGlobal(latestFrameAddress);
                    latestFrameAddress = IntPtr.Zero;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 单张采集
        /// </summary>
        public bool GrabOne()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    MessageBox.Show("相机当前正处于采集状态！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue("SingleFrame");
                    camera.StreamGrabber.Start(1, GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                    stopWatch.Restart();    // ****  重启采集时间计时器   ****
                    return true ;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 开始连续采集
        /// </summary>
        public bool StartGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    MessageBox.Show("相机当前正处于采集状态！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                    stopWatch.Restart();    // ****  重启采集时间计时器   ****
                    return true; 
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 停止连续采集
        /// </summary>
        public bool StopGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 设置Gige相机心跳时间
        /// </summary>
        /// <param name="value"></param>
        public void SetHeartBeatTime(long value)
        {
            try
            {
                // 判断是否是网口相机，网口相机才有心跳时间
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    camera.Parameters[PLGigECamera.GevHeartbeatTimeout].SetValue(value);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="value"></param>
        public void SetExposureTime(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the ExposureTime value to a specific value,
                // the ExposureAuto function must be disabled first (if ExposureAuto is available).
                camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off); // Set ExposureAuto to Off if it is writable.
                camera.Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed); // Set ExposureMode to Timed if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // In previous SFNC versions, ExposureTimeRaw is an integer parameter,单位us
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.ExposureTimeRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value);

                     // Or,here, we let pylon correct the value if needed.
                     //camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                     // In SFNC 2.0, ExposureTimeRaw is renamed as ExposureTime,is a float parameter, 单位us.
                    camera.Parameters[PLUsbCamera.ExposureTime].SetValue((double)value);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 获取最小最大曝光时间
        /// </summary>
        public  void GetMinMaxExposureTime()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    minExposureTime = camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    maxExposureTime = camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                }
                else
                {
                    minExposureTime = (long)camera.Parameters[PLUsbCamera.ExposureTime].GetMinimum();
                    maxExposureTime = (long)camera.Parameters[PLUsbCamera.ExposureTime].GetMaximum();
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        public void SetGain(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the gain value to a specific value,
                // the Gain Auto function must be disabled first (if gain auto is available).
                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off); // Set GainAuto to Off if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.                              
                    // In previous SFNC versions, GainRaw is an integer parameter.
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.GainRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.GainRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.GainRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.GainRaw].SetValue(value);

                    //Or,here, we let pylon correct the value if needed.
                    //camera.Parameters[PLCamera.GainRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                     // In SFNC 2.0, Gain is a float parameter.
                    camera.Parameters[PLUsbCamera.Gain].SetValue(value);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 获取最小最大增益
        /// </summary>
        public void GetMinMaxGain()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    minGain = camera.Parameters[PLCamera.GainRaw].GetMinimum();
                    maxGain = camera.Parameters[PLCamera.GainRaw].GetMaximum();
                }
                else
                {
                    minGain = (long)camera.Parameters[PLUsbCamera.Gain].GetMinimum();
                    maxGain = (long)camera.Parameters[PLUsbCamera.Gain].GetMaximum();
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 设置相机Freerun模式
        /// </summary>
        public void SetFreerun()
        {
            try
            {
                // Set an enum parameter.
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                stopWatch.Restart();    // ****重启采集时间计时器****
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>
        /// 设置相机软触发模式
        /// </summary>
        public bool SetSoftwareTrigger()
        {
            try
            {
                // Set an enum parameter.
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                    }
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                        }
                    }
                }
                stopWatch.Reset();    // ****重置采集时间计时器****
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 发送软触发命令
        /// </summary>
        public bool SendSoftwareExecute()
        {
            try
            {
                if (camera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                {
                    camera.ExecuteSoftwareTrigger();
                    stopWatch.Restart();    // ****重启采集时间计时器****
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 设置相机外触发模式
        /// </summary>
        public bool SetExternTrigger()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.
                    camera.Parameters[PLCamera.TriggerDelayAbs].SetValue(0);      // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(0); // 设置去抖延时，过滤触发信号干扰

                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.//float
                    camera.Parameters[PLCamera.TriggerDelay].SetValue(0);       // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTime].SetValue(0);  // 设置去抖延时，过滤触发信号干扰

                }
                stopWatch.Reset();    // ****重置采集时间计时器****
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="path">保存图像的路径</param>
        /// <param name="address">图像地址</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        public void SaveImage(string path, IntPtr address, int width, int height, bool isColor)
        {
            try
            {
                if (isColor == false)
                {
                    ImagePersistence.Save(ImageFileFormat.Bmp, path, address, width * height, PixelType.Mono8, width, height, 0, ImageOrientation.TopDown);
                }
                else
                {
                    ImagePersistence.Save(ImageFileFormat.Bmp, path, address, width * height * 3, PixelType.BGR8packed, width, height, 0, ImageOrientation.TopDown);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // 相机取像回调函数.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;
                // Check if the image can be displayed.
                if (grabResult.GrabSucceeded)
                {
                    grabTime = stopWatch.ElapsedMilliseconds;
                    // 抛出计算采集时间处理事件
                    eventComputeGrabTime(grabTime);

                    // 判断是否是黑白图片格式
                    if (grabResult.PixelTypeValue == PixelType.Mono8)
                    {
                        isColor = false;
                    }
                    else
                    {
                        isColor = true;
                    }
                    latestFrameAddress = (IntPtr)grabResult.PixelDataPointer;
                    eventProcessImage(isColor, imageWidth, imageHeight, latestFrameAddress);
                }
                else
                {
                    MessageBox.Show("Grab faild!\n" + grabResult.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        /// <summary>
        /// 掉线重连回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            try
            {
                const int cTimeOutMs = 20;

                System.Threading.Thread.Sleep(100);                
                camera.Close();

                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        camera.Open(cTimeOutMs, TimeoutHandling.ThrowException);
                        if (camera.IsOpen)
                        {
                            MessageBox.Show("已重新连接上UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("请重新连接UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("重连超时20s:未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SetHeartBeatTime(5000);

                imageWidth = (int)camera.Parameters[PLCamera.Width].GetValue();   //获取图像宽 
                imageHeight = (int)camera.Parameters[PLCamera.Height].GetValue(); //获取图像高
                GetMinMaxExposureTime();
                GetMinMaxGain();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // Shows exceptions in a message box.
        private static void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
