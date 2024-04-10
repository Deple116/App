using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;

namespace CameraControlLibrary.CameraHIK
{
    public struct ImagePack
    {
        public IntPtr data;
        public int width;
        public int height;
        public bool mono;
        public int size;
    }

    
    public class GigeUsbCamera
    {
        public delegate void SendImage(ImagePack pack);

        public event SendImage SendImageEvent;
        /// <summary>
        /// 存储枚举出的所有设备 键值对
        /// key 用户定义设备名字
        /// value 设备句柄信息
        /// </summary>
        private static Dictionary<string, MyCamera.MV_CC_DEVICE_INFO> m_Devices = new Dictionary<string, MyCamera.MV_CC_DEVICE_INFO>();

        private MyCamera m_Camera = new MyCamera();

        private static MyCamera.cbOutputExdelegate ImageCallback;

        /// <summary>
        /// 当前设备是否打开的标识符
        /// </summary>
        private bool m_Opened = false;

        /// <summary>
        /// 当前设备是否采集中的标识符
        /// </summary>
        private bool m_Grabbing = false;

        /// <summary>
        /// 相机是否为黑白相机
        /// </summary>
        private bool m_Mono = false;

        /// <summary>
        /// 枚举gige或者usb接口的相机设备
        /// </summary>
        /// <param name="_list">枚举到的设备名字</param>
        /// <returns></returns>
        public static bool listAllDevices(ref List<string> _list)
        {
            if (m_Devices.Count > 0)
                m_Devices.Clear();
            if (null == _list)
                _list = new List<string>();
            MyCamera.MV_CC_DEVICE_INFO_LIST stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref stDeviceList);
            if (0 != nRet || stDeviceList.nDeviceNum == 0)
                return false;
            for (int i = 0; i < stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    m_Devices[gigeInfo.chUserDefinedName] = device;
                    _list.Add(gigeInfo.chUserDefinedName);
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    m_Devices[usbInfo.chUserDefinedName] = device;
                    _list.Add(usbInfo.chUserDefinedName);
                }
            }
                return true;
        }

        /// <summary>
        /// 通过用户自定义名字来打开相机
        /// </summary>
        /// <param name="_name">用户手动给相机的命名</param>
        /// <returns></returns>
        public bool openDeviceForName(string _name)
        {
            if (m_Opened || !m_Devices.ContainsKey(_name))
                return false;
            MyCamera.MV_CC_DEVICE_INFO info = m_Devices[_name];
            int nRet = m_Camera.MV_CC_CreateDevice_NET(ref info);
            if (MyCamera.MV_OK != nRet)
                return false;
            nRet = m_Camera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_Camera.MV_CC_DestroyDevice_NET();
                return false;
            }

            //设置连续采集模式
            nRet = m_Camera.MV_CC_SetAcquisitionMode_NET((uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            if (MyCamera.MV_OK != nRet)
            {
                m_Camera.MV_CC_CloseDevice_NET();
                m_Camera.MV_CC_DestroyDevice_NET();
                return false;
            }
            m_Opened = true;
            return true;
        }

        /// <summary>
        /// 开启采集
        /// </summary>
        /// <returns></returns>
        public bool startGrab()
        {
            if (!m_Opened || m_Grabbing)
                return false;
            if (null == ImageCallback)
                ImageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
            MyCamera.MVCC_ENUMVALUE pixType = new MyCamera.MVCC_ENUMVALUE();
            if (MyCamera.MV_OK != m_Camera.MV_CC_GetPixelFormat_NET(ref pixType))
                return false;
            switch ((MyCamera.MvGvspPixelType)pixType.nCurValue)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    m_Mono = true;
                    break;
                default:
                    m_Mono = false;
                    break;
            }
            //注册回调
            int nRet = 0;
            if (m_Mono)
            {
                nRet = m_Camera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, GCHandle.ToIntPtr(GCHandle.Alloc(this)));
                if (MyCamera.MV_OK != nRet)
                    return false;
            }else
            {
                nRet = m_Camera.MV_CC_RegisterImageCallBackForBGR_NET(ImageCallback, GCHandle.ToIntPtr(GCHandle.Alloc(this)));
                if (MyCamera.MV_OK != nRet)
                    return false;
            }
            nRet = m_Camera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
                return false;
            m_Grabbing = true;
            return true;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public bool stopGrab()
        {
            if (!m_Opened || !m_Grabbing)
                return false;
            int nRet = m_Camera.MV_CC_StopGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
                return false;
            m_Grabbing = false;
            return true;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool closeDevice()
        {
            stopGrab();
            // ch:关闭设备 | en:Close Device
            int nRet = m_Camera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
                return false;
            nRet = m_Camera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
                return false;
            m_Opened = false;
            return true;
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="_value">曝光值</param>
        /// <returns></returns>
        public bool setExposure(float _value)
        {
            if (!m_Opened)
                return false;
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetExposureTime_NET(_value))
                return false;
            return true;
        }

        /// <summary>
        /// 设置白平衡
        /// </summary>
        /// <returns></returns>
        public bool setBlanceWhite(uint _rvalue ,uint _gvalue,uint _bvalue)
        {
            try
            {
                if (!m_Opened)
                    return false;
                if (MyCamera.MV_OK != m_Camera.MV_CC_SetBalanceRatioRed_NET(_rvalue))
                    return false;
                if (MyCamera.MV_OK != m_Camera.MV_CC_SetBalanceRatioGreen_NET(_gvalue))
                    return false;
                if (MyCamera.MV_OK != m_Camera.MV_CC_SetBalanceRatioBlue_NET(_bvalue))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置触发延时us
        /// </summary>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public bool setTriggerDelay(float fValue)
        {
            try
            {
                if (!m_Opened)
                    return false;
                if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerDelay_NET(fValue))
                    return false;
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool getExposure(ref float _value)
        {
            if (!m_Opened)
                return false;
            MyCamera.MVCC_FLOATVALUE value = new MyCamera.MVCC_FLOATVALUE();
            if (MyCamera.MV_OK != m_Camera.MV_CC_GetExposureTime_NET(ref value))
                return false;
            _value = value.fCurValue;
            return true;
        }
        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="_value">增益值</param>
        /// <returns></returns>
        public bool setGain(float _value)
        {
            if (!m_Opened)
                return false;
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetGain_NET(_value))
                return false;
            return true;
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public bool getGain(ref float _value)
        {
            if (!m_Opened)
                return false;
            MyCamera.MVCC_FLOATVALUE value = new MyCamera.MVCC_FLOATVALUE();
            if (MyCamera.MV_OK != m_Camera.MV_CC_GetGain_NET(ref value))
                return false;
            _value = value.fCurValue;
            return true;
        }

        /// <summary>
        /// 关闭触发，自由采集
        /// </summary>
        /// <returns></returns>
        public bool setFreeRunMode()
        {
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerMode_NET((uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF))
                return false;
            return true;
        }

        /// <summary>
        /// 开启触发，设置触发模式为软触发
        /// </summary>
        /// <returns></returns>
        public bool setSoftwareTriggerMode()
        {
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerMode_NET((uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON))
                return false;
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerSource_NET((uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE))
                return false;
            return true;
        }

        /// <summary>
        /// 开启触发，设置触发模式为外部触发
        /// </summary>
        /// <returns></returns>
        public bool setExternalTriggerMode()
        {
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerMode_NET((uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON))
                return false;
            if (MyCamera.MV_OK != m_Camera.MV_CC_SetTriggerSource_NET((uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0))
                return false;
            return true;
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public bool softWareTriggerOnce()
        {
            if (MyCamera.MV_OK != m_Camera.MV_CC_TriggerSoftwareExecute_NET())
                return false;
            return true;
        }

        private static void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            GCHandle handle = GCHandle.FromIntPtr(pUser);
            GigeUsbCamera camera = (GigeUsbCamera)handle.Target;

            ImagePack pack = new ImagePack() { data = pData, width = pFrameInfo.nWidth, height = pFrameInfo.nHeight, mono = camera.m_Mono,size= IntPtr.Size };
            camera.SendImageEvent?.Invoke(pack);
        }
    }
}
