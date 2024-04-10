using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DALSA.SaperaLT.SapClassBasic;
using OpenCvSharp;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Drawing;
using SMLogControlLibrary;

namespace CameraControlLibrary.DalsaLine
{
    //public enum DalsaCamName
    //{
    //    left7,
    //    right7,
    //}

    public struct dalsaImagePack
    {
        public IntPtr data;
        public int width;
        public int height;
        public bool mono;
        public int size;
    }

    public class class_CameraDalsaLine : CameraInterface
    {

        //public string m_strCamName;    //相机名称
        public const int ERROR_OK = 0;
        public const int ERROR_FAILED = -1;

        public delegate void CamCallBack(Mat m_image);
        public CamCallBack m_CamCallBack = null;


        public ConcurrentQueue<CameraImageCallPack> cameraImageCallPack_Buffer;


        public string LastError { get; private set; } = "";
        public string PathCCFfile { get; private set; } = "";
        public string StrCard { get; private set; } = "";
        public string StrCam { get; private set; } = "";
       
        public string ExtraInfo { get; set; } = "";


        /// <summary>
        /// 相机是否为黑白相机
        /// </summary>
        private bool m_Mono = true;


        #region  定义
        private SapAcquisition _acquisition = null;
        private SapBuffer _buffers = null;
        //private SapBuffer _buffers_r = null;
        //private SapBuffer _buffers_g = null;
        //private SapBuffer _buffers_b = null;

        private SapAcqToBuf _xfer = null;
        private SapLocation _location_forcard = null;
        private SapLocation _location_forcamera = null;
        private SapAcqDevice _device = null;
        private SapFeature _feature = null;

        private bool _getImg;

        public class ImgInfo
        {
            public IntPtr ImgPtr;
            public IntPtr ImgPtr_R;
            public IntPtr ImgPtr_G;
            public IntPtr ImgPtr_B;
            public long ImgTicks = 0;
        }

        private Queue<ImgInfo> _imgQueue = new Queue<ImgInfo>();


        CDalsaConfig acqParams = new CDalsaConfig();


        //图像的长宽尺寸
        public int in_ImageWidth_Cam1, in_ImageHeight_Cam1;

        ////////////////////////////////////////////////////////////

        public static bool CameraOpenStatus = false;
        ////////////////////////////////////////////

        #endregion


        #region Constructors
        public class_CameraDalsaLine(string _pathCCFfile, string _strCard, string _strCam, string _cameraName,string _cameraType)
        {
            PathCCFfile = _pathCCFfile;
            StrCard = _strCard;
            StrCam = _strCam;
            CCDName = _cameraName;
            cameraType = _cameraType;


            cameraImageCallPack_Buffer = new ConcurrentQueue<CameraImageCallPack>();
            _getImg = false;
            _imgQueue.Clear();
        }
        ~class_CameraDalsaLine()
        {
            DeInitial();
        }
        #endregion

        #region Properties
        public SapLocation LocationForCard
        {
            get { return _location_forcard; }
            set { _location_forcard = value; }
        }
        public SapLocation LocationForCamera
        {
            get { return _location_forcamera; }
            set { _location_forcamera = value; }
        }
        public bool GetImg
        {
            get { return _getImg; }
            set { _getImg = value; }
        }
        #endregion


        #region interface


        public string CCDName { get; set; } = "";
        public string cameraType { get; set; } = "";

        public int Initial()
        {
            try
            {
                string str_SetingFileName1 = PathCCFfile;
                //图像采集卡服务名称
                string str_ServerName_ForCard1 = StrCard;
                //图像采集卡资源索引
                string str_ResourceIndex_ForCard1 = "0";
                //相机服务名称
                string str_ServerName_ForCamera1 = StrCam;
                //相机资源索引
                string str_ResourceIndex_ForCamera1 = "0";

                int in_ResourceIndex_ForCard1 = Int32.Parse(str_ResourceIndex_ForCard1);
                int in_ResourceIndex_ForCamera1 = Int32.Parse(str_ResourceIndex_ForCamera1);

                acqParams.LoadConfig(str_SetingFileName1, str_ServerName_ForCard1, in_ResourceIndex_ForCard1, str_ServerName_ForCamera1, in_ResourceIndex_ForCamera1);

                LocationForCard = new SapLocation(acqParams.ServerName_ForCard, acqParams.ResourceId_ForCard);
                LocationForCamera = new SapLocation(acqParams.ServerName_ForCamera, acqParams.ResourceId_ForCamera);
                bool bl_LineCameraStatus1 = CreateNewObjects(LocationForCard, LocationForCamera, acqParams.SetingFileName, true);
                if (bl_LineCameraStatus1)
                {
                    //  MessageBox.Show("线阵相机打开成功");

                    // m_CamCallBack = callback;
                    if (GetImageSize(out in_ImageWidth_Cam1, out in_ImageHeight_Cam1) == false)
                    {
                        //MessageBox.Show("获取线阵相机采图尺寸失败");
                        return -1;
                    }

                    //if (ERROR_FAILED == m_algo.InitSDK())
                    //{
                    //    LastError = $"相机{CCDName}初始化算法失败!";
                    //}

                    Grab();
                    return 0;
                }
                else
                {
                    // MessageBox.Show("线阵相机打开失败");
                    return -1;
                }
            }
            catch (System.Exception ex)
            {
                // MessageBox.Show("线阵相机打开失败");
                return -1;
            }

        }

        public int DeInitial()
        {
            try
            {
                DestroyObjects();
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetSoftwareTriggerMode()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetExternalTriggerMode()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetFreeRunMode()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SoftWareTriggerOnce()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int StopGrab()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int StartGrab()
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetExposure(float _value)
        {
            try
            {
                if(_device.SetFeatureValue("ExposureTime", _value))
                {
                    return ERROR_OK;
                }
                else
                {
                    LastError = $"相机{CCDName}设置曝光时间失败!";
                    SMLogWindow.OutLog(LastError, Color.Red, loglevel: LogLevel.Error);
                    return ERROR_FAILED;
                }
                
            }
            catch (Exception ex)
            {
                LastError = $"相机{CCDName}设置曝光时间失败!";
                SMLogWindow.OutLog(LastError+$";{ex.ToString()}", Color.Red, loglevel:LogLevel.Error);
                return ERROR_FAILED;
            }
        }

        public int GetExposure(ref float _value)
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetBlanceWhite(uint _rvalue, uint _gvalue, uint _bvalue)
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int SetTriggerDlay(float _value)
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                return ERROR_FAILED;
            }
        }

        public int setGain(float _value)
        {
            try
            {
                if(_device.SetFeatureValue("Gain", _value))
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
                LastError = $"相机{CCDName}设置增益失败!";
                SMLogWindow.OutLog(LastError+$":{ex.ToString()}", Color.Red, loglevel:LogLevel.Error);
                return ERROR_FAILED;
            }
        }

        public int getGain(ref float _value)
        {
            try
            {
                return ERROR_OK;
            }
            catch (Exception ex)
            {
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


        #endregion

        #region Methods
        //将图像缓冲区组织成队列


        #region 相机回调
        // public static IntPtr bufferAddress;
        //相机
        void xfer_XferNotify(object sender, SapXferNotifyEventArgs argsNotify)
        {
            if (argsNotify.Trash)
            { }
            else
            {
                //if (_imgQueue.Count > 1)
                //{
                //    _imgQueue.Clear();
                //    return;
                //}
                unsafe
                {
                    // _buffers.Clear();
                    IntPtr bufferAddress;
                    _buffers.GetAddress(out bufferAddress);

                    dalsaImagePack imagePack = new dalsaImagePack()
                    {
                        data = bufferAddress,
                        width = _buffers.Width,
                        height = _buffers.Height,
                        mono = m_Mono,
                        size = IntPtr.Size
                    };

                    Task imageCllPack = CamShowImage(imagePack);

                    //if (null != ho_image1)
                    //{
                    //    ho_image1.Dispose();
                    //}
                    //HOperatorSet.GenEmptyObj(out ho_image1);
                    //ho_image1 = PToObject(bufferAddress);
                }


                //ImgInfo imgInfo = new ImgInfo();
                //imgInfo.ImgPtr = bufferAddress;

                //_imgQueue.Enqueue(imgInfo);

                //if (null != ho_image1)
                //{
                //    if (m_CamCallBack != null)
                //    {
                //        m_CamCallBack(ho_image1);
                //    }

                //    _buffers.Clear();
                //}


                //代表采集结束
                // _getImg = false;


            }
        }

        public async Task CamShowImage(dalsaImagePack imagePack)
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

        public static List<Mat> SplitImage(Mat src, int splitNum)
        {
            if (src.Rows % splitNum != 0)
                return new List<Mat>();
            List<Mat> outs = new List<Mat>(splitNum);
            for (int i = 0; i < splitNum; i++)
            {
                outs.Add(new Mat(src.Rows / splitNum, src.Cols, src.Type()));
            }

            for (int i = 0; i < src.Rows; i++)
            {
                byte[] dataByteRow = new byte[src.Cols];
                Marshal.Copy(src.Ptr(i), dataByteRow, 0, src.Cols);
                Marshal.Copy(dataByteRow, 0, outs[i % splitNum].Ptr(i / splitNum), src.Cols);
            }

            return outs;
        }


        //Point转Hobject
        //public HObject PToObject(IntPtr Pointer)
        //{
        //    #region Point转Hobject

        //    HTuple width = null, hegiht = null;
        //    HObject hImage = null;
        //    try
        //    {
        //        HOperatorSet.GenEmptyObj(out hImage);
        //        width = _buffers.Width;
        //        hegiht = _buffers.Height;
        //        HOperatorSet.GenImage1(out hImage, "byte", width, hegiht, Pointer);
        //    }
        //    catch (Exception exc)
        //    {
        //        //futureDll.frmMsg.Log("Point转化为Hobject error:" + exc.Message, 1, 2, 0);

        //    }
        //    return hImage;

        //    #endregion
        //}
        #endregion

        //外触发事件的响应函数
        void AcqCallback(object sender, SapAcqNotifyEventArgs argsSignal)
        {
            //代表收到触发信号，采集开始    
            _getImg = true;
        }


        //获取图像数据加入时间戳判断
        public bool GetImgByte(out IntPtr imgPtr_R, out IntPtr imgPtr_G, out IntPtr imgPtr_B, bool needArea, long trigT)
        {
            while (_imgQueue.Count < 1)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }

            ImgInfo imgInfo = _imgQueue.Peek();

            if (needArea)
            {
                //在设定时间区间
                if (imgInfo.ImgTicks > trigT)
                {
                    imgPtr_R = imgInfo.ImgPtr_R;
                    imgPtr_G = imgInfo.ImgPtr_G;
                    imgPtr_B = imgInfo.ImgPtr_B;
                    _imgQueue.Dequeue();
                    return true;
                }
                else
                {
                    imgPtr_R = IntPtr.Zero;
                    imgPtr_G = IntPtr.Zero;
                    imgPtr_B = IntPtr.Zero;
                    _imgQueue.Dequeue();
                    return false;
                }
            }
            else
            {
                imgPtr_R = imgInfo.ImgPtr_R;
                imgPtr_G = imgInfo.ImgPtr_G;
                imgPtr_B = imgInfo.ImgPtr_B;
                _imgQueue.Dequeue();
                return true;
            }
        }

        //获取黑白图像数据
        public bool GetImgMonoByte(out IntPtr imgPtr, bool needArea, long trigT)
        {
            while (_imgQueue.Count < 1)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }

            ImgInfo imgInfo = _imgQueue.Peek();

            if (needArea)
            {
                //在设定时间区间
                if (imgInfo.ImgTicks > trigT)
                {
                    imgPtr = imgInfo.ImgPtr;
                    _imgQueue.Dequeue();
                    return true;
                }
                else
                {
                    imgPtr = IntPtr.Zero;
                    _imgQueue.Dequeue();
                    return false;
                }
            }
            else
            {
                imgPtr = imgInfo.ImgPtr;
                _imgQueue.Dequeue();
                return true;
            }
        }

        //创建采集所需的对象
        public bool CreateNewObjects(SapLocation location_forcard, SapLocation location_forcamera, string fileName, bool updatelocation)
        {
            try
            {
                if (updatelocation)
                {
                    _location_forcard = location_forcard;
                    _location_forcamera = location_forcamera;
                }

                if (!SapManager.IsResourceAvailable(_location_forcard, SapManager.ResourceType.Acq))
                {
                    return false;
                }

                //这边的fileName可能会存在问题，请注意
                _acquisition = new SapAcquisition(_location_forcard, fileName);


                //_acquisition.EventType = SapAcquisition.AcqEventType.ExternalTrigger;
                _acquisition.EventType = SapAcquisition.AcqEventType.EndOfFrame;
                _acquisition.AcqNotify += new SapAcqNotifyHandler(AcqCallback);
                _acquisition.AcqNotifyContext = this;

                _device = new SapAcqDevice(location_forcamera, false);
                _feature = new SapFeature(location_forcamera);

                if (SapBuffer.IsBufferTypeSupported(_location_forcard, SapBuffer.MemoryType.ScatterGather))
                    _buffers = new SapBuffer(1, _acquisition, SapBuffer.MemoryType.ScatterGather);
                else
                    _buffers = new SapBuffer(1, _acquisition, SapBuffer.MemoryType.ScatterGatherPhysical);

                //_buffers_r = new SapBuffer();
                //_buffers_g = new SapBuffer();
                //_buffers_b = new SapBuffer();

                _xfer = new SapAcqToBuf(_acquisition, _buffers);
                _xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                _xfer.XferNotify += new SapXferNotifyHandler(xfer_XferNotify);
                _xfer.XferNotifyContext = this;

                if (!CreateObjects())
                {
                    DestroyObjects();
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        //设置曝光值
        public bool SetExposureVal(double featureValue, out double setValue, out double minVal, out double maxVal)
        {
            if (_device == null)
            { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

            if (_device.IsFeatureAvailable("ExposureTime"))
            {
                if (_device.GetFeatureInfo("ExposureTime", _feature) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

                if (_feature.GetValueMax(out maxVal) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMin(out minVal) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

                if (featureValue < minVal)
                {
                    featureValue = minVal;
                }

                if (featureValue > maxVal)
                {
                    featureValue = maxVal;
                }

                if (_device.SetFeatureValue("ExposureTime", featureValue) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
            }
            else
            {
                setValue = 0.0; minVal = 0.0; maxVal = 0.0;
                return false;
            }

            setValue = featureValue;
            return true;
        }

        //获得曝光值
        public bool GetExposureVal(out double featureValue, out double minVal, out double maxVal)
        {
            if (_device == null)
            {
                featureValue = 0.0; minVal = 0.0; maxVal = 0.0;
                return false;
            }

            if (_device.IsFeatureAvailable("ExposureTime"))
            {
                if (_device.GetFeatureInfo("ExposureTime", _feature) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMax(out maxVal) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMin(out minVal) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_device.GetFeatureValue("ExposureTime", out featureValue) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
            }
            else
            {
                featureValue = 0.0; minVal = 0.0; maxVal = 0.0;
                return false;
            }

            return true;
        }

        //设置增益值
        public bool SetGainVal(double featureValue, out double setValue, out double minVal, out double maxVal)
        {
            if (_device == null)
            { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

            if (_device.IsFeatureAvailable("Gain"))
            {
                if (_device.GetFeatureInfo("Gain", _feature) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

                if (_feature.GetValueMax(out maxVal) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMin(out minVal) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }

                if (featureValue < minVal)
                {
                    featureValue = minVal;
                }

                if (featureValue > maxVal)
                {
                    featureValue = maxVal;
                }

                if (_device.SetFeatureValue("Gain", featureValue) == false)
                { setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
            }
            else
            {
                setValue = 0.0; minVal = 0.0; maxVal = 0.0; return false;
            }

            setValue = featureValue;
            return true;
        }

        //获得增益值
        public bool GetGainVal(out double featureValue, out double minVal, out double maxVal)
        {
            if (_device == null)
            {
                featureValue = 0.0; minVal = 0.0; maxVal = 0.0;
                return false;
            }

            if (_device.IsFeatureAvailable("Gain"))
            {
                if (_device.GetFeatureInfo("Gain", _feature) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMax(out maxVal) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_feature.GetValueMin(out minVal) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
                if (_device.GetFeatureValue("Gain", out featureValue) == false)
                { featureValue = 0.0; minVal = 0.0; maxVal = 0.0; return false; }
            }
            else
            {
                featureValue = 0.0; minVal = 0.0; maxVal = 0.0;
                return false;
            }

            return true;
        }

        //获取图像缓冲区尺寸
        public bool GetImageSize(out int imageWidth, out int imageHeight)
        {
            if (_buffers != null)
            {
                imageWidth = _buffers.Width;
                imageHeight = _buffers.Height;
                return true;
            }
            else
            {
                imageWidth = 0;
                imageHeight = 0;
                return false;
            }
        }

        // Call Create method  
        public bool CreateObjects()
        {
            // Create acquisition object
            if (_acquisition != null && !_acquisition.Initialized)
            {
                if (_acquisition.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }

            // Create buffer object
            if (_buffers != null && !_buffers.Initialized)
            {
                if (_buffers.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
                _buffers.Clear();
            }

            //_buffers_r.Count = _buffers.Count;
            //_buffers_r.Width = _buffers.Width;
            //_buffers_r.Height = _buffers.Height;
            //_buffers_r.Format = SapFormat.Mono8;
            //_buffers_r.Type = _buffers.Type;

            //if (_buffers_r != null && !_buffers_r.Initialized)
            //{
            //    if (_buffers_r.Create() == false)
            //    {
            //        DestroyObjects();
            //        return false;
            //    }
            //    _buffers_r.Clear();
            //}

            //_buffers_g.Count = _buffers.Count;
            //_buffers_g.Width = _buffers.Width;
            //_buffers_g.Height = _buffers.Height;
            //_buffers_g.Format = SapFormat.Mono8;
            //_buffers_g.Type = _buffers.Type;

            //if (_buffers_g != null && !_buffers_g.Initialized)
            //{
            //    if (_buffers_g.Create() == false)
            //    {
            //        DestroyObjects();
            //        return false;
            //    }
            //    _buffers_g.Clear();
            //}

            //_buffers_b.Count = _buffers.Count;
            //_buffers_b.Width = _buffers.Width;
            //_buffers_b.Height = _buffers.Height;
            //_buffers_b.Format = SapFormat.Mono8;
            //_buffers_b.Type = _buffers.Type;

            //if (_buffers_b != null && !_buffers_b.Initialized)
            //{
            //    if (_buffers_b.Create() == false)
            //    {
            //        DestroyObjects();
            //        return false;
            //    }
            //    _buffers_b.Clear();
            //}

            // Create xfer object
            if (_xfer != null && !_xfer.Initialized)
            {
                if (_xfer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }

            // Create device object
            if (_device != null && !_device.Initialized)
            {
                if (_device.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }

            // Create feature object
            if (_feature != null && !_feature.Initialized)
            {
                if (!_feature.Create())
                {
                    DestroyObjects();
                    return false;
                }
            }

            return true;
        }

        //Call Destroy method
        public void DestroyObjects()
        {
            if (_xfer != null)
            {
                _xfer.Destroy();
                _xfer.Dispose();
                _xfer = null;
            }

            if (_acquisition != null)
            {
                _acquisition.Destroy();
                _acquisition.Dispose();
                _acquisition = null;
            }

            if (_buffers != null)
            {
                _buffers.Destroy();
                _buffers.Dispose();
                _buffers = null;
            }

            //if (_buffers_r != null)
            //{
            //    _buffers_r.Destroy();
            //    _buffers_r.Dispose();
            //    _buffers_r = null;
            //}

            //if (_buffers_g != null)
            //{
            //    _buffers_g.Destroy();
            //    _buffers_g.Dispose();
            //    _buffers_g = null;
            //}

            //if (_buffers_b != null)
            //{
            //    _buffers_b.Destroy();
            //    _buffers_b.Dispose();
            //    _buffers_b = null;
            //}

            if (_device != null)
            {
                _device.Destroy();
                _device.Dispose();
                _device = null;
            }

            if (_feature != null)
            {
                _feature.Destroy();
                _feature.Dispose();
                _feature = null;
            }
        }


        //Call Freeze method
        public void Freeze()
        {
            if (_xfer != null)
            {
                _xfer.Freeze();

                if (!_xfer.Wait(100))
                {
                    _xfer.Abort();
                }
            }
        }

        //Call Grab method
        public bool Grab()
        {
            if (_xfer.Grab())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Snap()
        {
            if (_xfer.Snap())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //设置触发模式
        public void setCameraTriggerOn()
        {
            #region 设置触发模式


            try
            {
                //有采集卡和相机（CamLink）

                //停止传输和等待（timeout = 5 senconds)
                _xfer.Freeze();
                _xfer.Abort();
                //futureDll.clsTimeDelay.Delay(1000);
                //class_HalconSource.timeDelay(1000);

                _acquisition.SetParameter(SapAcquisition.Prm.EXT_TRIGGER_ENABLE, 1, true);
                //Tungray.clsTimeDelay.Delay(200);
                _acquisition.SetParameter(SapAcquisition.Prm.CAM_TRIGGER_ENABLE, 1, true);

                _acquisition.SetParameter(SapAcquisition.Prm.INT_LINE_TRIGGER_ENABLE, 0, true);
                _acquisition.SetParameter(SapAcquisition.Prm.EXT_LINE_TRIGGER_ENABLE, 1, true);

                _acquisition.SetParameter(SapAcquisition.Prm.EXT_TRIGGER_SOURCE, 1, true);
                //Tungray.clsTimeDelay.Delay(200); 
                _acquisition.SetParameter(SapAcquisition.Prm.EXT_LINE_TRIGGER_SOURCE, 1, true);
                //Tungray.clsTimeDelay.Delay(200);

                _xfer.Grab();

            }
            catch (Exception exc)
            {
                //   futureDll.frmMsg.Log("设置Dalsa相机触发模式出错！" + ">:" + exc.Message, 1, 2, 1);
                MessageBox.Show("设置Dalsa相机触发模式出错！");
            }

            #endregion
        }

        //设置连续模式
        public void setCameraContinue()
        {
            #region 设置连续模式
            try
            {
                //有采集卡和相机（CamLink）

                _xfer.Freeze();
                _xfer.Abort();
                // class_HalconSource.timeDelay(1000);
                //futureDll.clsTimeDelay.Delay(1000);
                //acq.SetParameter(SapAcquisition.Prm.EXT_TRIGGER_SOURCE, 0, true);
                //Tungray.clsTimeDelay.Delay(200);
                _acquisition.SetParameter(SapAcquisition.Prm.EXT_LINE_TRIGGER_ENABLE, 0, true);
                _acquisition.SetParameter(SapAcquisition.Prm.INT_LINE_TRIGGER_ENABLE, 1, true);

                // transfer.Grab();//先关闭采集才可以调用该函数
                // _xfer.Snap();


            }
            catch (Exception exc)
            {
                //futureDll.frmMsg.Log("设置Dalsa相机连续模式出错！" + exc.Message, 1, 2, 1);
                MessageBox.Show("设置Dalsa相机连续模式出错！");
            }

            #endregion
        }
        #endregion

    }


    //Dalsa相机配置类
    public class CDalsaConfig
    {
        private string _setingfilename;
        private string _servername_forcard;
        private int _resourceid_forcard;
        private string _servername_forcamera;
        private int _resourceid_forcamera;

        //载入相机配置
        public void LoadConfig(string setingfilename, string servername_forcard, int resourceid_forcard, string servername_forcamera, int resourceid_forcamera)
        {
            SetingFileName = setingfilename;
            ServerName_ForCard = servername_forcard;
            ResourceId_ForCard = resourceid_forcard;
            ServerName_ForCamera = servername_forcamera;
            ResourceId_ForCamera = resourceid_forcamera;
        }

        #region Properties
        public string SetingFileName
        {
            set { _setingfilename = value; }
            get { return _setingfilename; }
        }

        public string ServerName_ForCard
        {
            set { _servername_forcard = value; }
            get { return _servername_forcard; }
        }

        public int ResourceId_ForCard
        {
            set { _resourceid_forcard = value; }
            get { return _resourceid_forcard; }
        }

        public string ServerName_ForCamera
        {
            set { _servername_forcamera = value; }
            get { return _servername_forcamera; }
        }

        public int ResourceId_ForCamera
        {
            set { _resourceid_forcamera = value; }
            get { return _resourceid_forcamera; }
        }
        #endregion
    }


}
