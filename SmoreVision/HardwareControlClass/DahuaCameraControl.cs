using OpenCvSharp;
using OpenCvSharp.Extensions;
using SmoreControlLibrary;
using SmoreControlLibrary.EquipmentDriver.CameraDahua;
using SmoreControlLibrary.SMLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using ThridLibray;

namespace SmoreVision.HardwareControlClass
{


	public delegate void CamLoss(string CCDName);
	public struct caminfo
    {
		public IGrabbedRawData camdata;

		public bool bcolor;
	}

    public class DahuaCameraControl
	{
		public const int ERROR_OK = 0;
		public const int ERROR_FAILED = -1;
		private List<string> _list = new List<string>();
		public string LastError { get; private set; } = "";
		public string CCDName { get; private set; } = "";
		public string SN { get; set; } = ""; //sn
		public string StationName { get; set; } = ""; //工站  
		public int SerialNumber { get; set; } = 0; //拍照次数 

		//private ConcurrentQueue<bool> BColor;
		private ConcurrentQueue<caminfo> ImageTransformQueue;
		private Task m_Initial = null;
		public bool CycledInitial = false;

		public ConcurrentQueue<Mat>[] cameraImageCallPack_Buffer=new ConcurrentQueue<Mat>[2];
		public ManualResetEvent[] m_cameraImageCallPackEvent =new ManualResetEvent[2];

		public DahuaCamera m_DahuaCamera;
		private Mutex m_mutex = new Mutex();

		//Mutex m_mutex = new Mutex();
		//ManualResetEvent m_event = new ManualResetEvent(false);



		public DahuaCameraControl(string _cameraName)
		{
			CCDName = _cameraName;
			m_DahuaCamera = new DahuaCamera();
			m_DahuaCamera.CCDName = CCDName;
			
			m_DahuaCamera.eventProcessImage += processShowImage;//注册相机回调事件

			for (int i = 0; i < cameraImageCallPack_Buffer.Length; i++)
			{
				cameraImageCallPack_Buffer[i] = new ConcurrentQueue<Mat>();
				m_cameraImageCallPackEvent[i] = new ManualResetEvent(false);
			}
			

			ImageTransformQueue = new ConcurrentQueue<caminfo>();
			//BColor = new ConcurrentQueue<bool>();
		}

		public int Initial()
		{
			try
			{
				//if ("CCD3" == CCDName || "CCD7" == CCDName) return ERROR_OK;

				m_DahuaCamera.listAllDevices(ref _list);
				if (_list.Count <= 0)
				{
					LastError = $"没有找到任何相机!";
					return ERROR_FAILED;
				}

				if (!_list.Contains(CCDName))
				{
					LastError = $"没有找到{CCDName}!";
					return ERROR_FAILED;
				}

				//打开相机
				if (!m_DahuaCamera.openDeviceForName(CCDName))
				{
					LastError = $"打开相机{CCDName}失败!";
					return ERROR_FAILED;
				}

				StartInitialThread();
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
				if ("CCD3" == CCDName || "CCD7" == CCDName) return ERROR_OK;

				if (m_DahuaCamera != null)
				{
					if (m_DahuaCamera.closeDevice())
					{
						LastError = $"相机{CCDName}关闭失败!";
						return ERROR_FAILED;
					}
					m_DahuaCamera = null;
				}
				return ERROR_OK;
			}
			catch (Exception ex)
			{
				LastError = ex.ToString();
				return ERROR_FAILED;
			}
		}

		public int StartInitialThread()
		{
			if (m_Initial == null || m_Initial.IsCompleted == true)
			{
				CycledInitial = true;
				m_Initial = Task.Factory.StartNew(() => StartInitial(), TaskCreationOptions.LongRunning);
			}
			return ERROR_OK;
		}

		public int StartInitial()
		{
			return ERROR_OK;
			//bool TempBool = new bool();
			caminfo TempData;
			try
			{
				while (CycledInitial)
				{
					//m_event.WaitOne(GlobalVariables.Variable.iTimeout);

					while(ImageTransformQueue.Count != 0)
					{
						SMLogWindow.OutLog($"CCDName:{CCDName}:ImageTransformQueue.Count:{ImageTransformQueue.Count}", Color.Green);
						//GC.Collect();
						ImageTransformQueue.TryDequeue(out TempData);
						//BColor.TryDequeue(out TempBool);
						

						var bitmap = TempData.camdata.ToBitmap(TempData.bcolor);
						Mat mat = BitmapConverter.ToMat(bitmap);

						SerialNumber++;

						if ("CCD6" == CCDName)
						{
							cameraImageCallPack_Buffer[1].Enqueue(mat.Clone());
							m_cameraImageCallPackEvent[1].Set();
							SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[1].Count:{cameraImageCallPack_Buffer[1].Count}", Color.Green);
							continue;
						}

					    if (SerialNumber % 2 == 1)
						{

							cameraImageCallPack_Buffer[0].Enqueue(mat.Clone());
							m_cameraImageCallPackEvent[0].Set();
							SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[0].Count:{cameraImageCallPack_Buffer[0].Count}", Color.Green);
						
							
						}
						else if (SerialNumber % 2 == 0)
						{
							SerialNumber = 0;
							cameraImageCallPack_Buffer[1].Enqueue(mat.Clone());
							m_cameraImageCallPackEvent[1].Set();
							SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[1].Count:{cameraImageCallPack_Buffer[1].Count}", Color.Green);
						}

						bitmap.Dispose();
					}
					//GC.Collect();
					//m_event.Reset();
					//Thread.Sleep(10);
				}
				
				return ERROR_OK;
			}
			catch (Exception)
			{
				Log.Add($"相机{CCDName}图像转化失败", Color.Green);
				return ERROR_FAILED;
			}
		}

		public int LoadCfg(string filepath)
        {
			try
            {
				
				if (m_DahuaCamera.loadCfg(filepath))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}loadcfg失败!";
					return ERROR_FAILED;
				}
				//return 0;
            }
			catch(Exception)
			{
				return -1;
			}
        }
		public int SetSoftwareTriggerMode()
		{
			if (!m_DahuaCamera.setSoftwareTriggerMode())
			{
				LastError = $"设置相机{CCDName}为软触发模式失败!";
				return ERROR_FAILED;
			}
			return ERROR_OK;
		}

		public int SetExternalTriggerMode()
		{
			if (!m_DahuaCamera.setExternalTriggerMode())
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
				if (!m_DahuaCamera.softWareTriggerOnce())
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
				if (m_DahuaCamera.setFreeRunMode())
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
				if (!m_DahuaCamera.stopGrab())
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
				if (!m_DahuaCamera.startGrab())
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
				if (m_DahuaCamera.setExposure(_value))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}设置曝光时间失败!";
					return ERROR_FAILED;
				}
			}
			catch (Exception ex)
			{
				LastError = ex.ToString();
				return ERROR_FAILED;
			}
		}

		public int SetGamma(double _value)
		{
			try
			{
				if (m_DahuaCamera.setGamma(_value))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}设置Gamma失败!";
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
				if (m_DahuaCamera.setTriggerDelay(_value))
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
				if (m_DahuaCamera.getExposure(ref _value))
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
				if (m_DahuaCamera.setGain(_value))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}设置增益失败!";
					return ERROR_FAILED;
				}
			}
			catch (Exception ex)
			{
				LastError = ex.ToString();
				return ERROR_FAILED;
			}
		}

		public int setUserConfig(int index)
        {
			try
			{
				if (m_DahuaCamera.setUserConfig(index))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}用户设置失败!";
					return ERROR_FAILED;
				}
			}
			catch (Exception ex)
			{
				LastError = ex.ToString();
				return ERROR_FAILED;
			}
		}


		public int setSharpness(long _value)
		{
			try
			{
				if (m_DahuaCamera.setSharpness(_value))
				{
					return ERROR_OK;
				}
				else
				{
					LastError = $"相机{CCDName}设置锐度失败!";
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
				if (m_DahuaCamera.getGain(ref _value))
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

		private void processShowImage(IGrabbedRawData grabbedRawData, bool bcolor)
		{
			try
			{
				//m_mutex.WaitOne();
				processImageDirectly(grabbedRawData, bcolor);
				//m_mutex.WaitOne();
				return;
				//GC.Collect();
				//var bitmap = grabbedRawData.ToBitmap(bcolor);
				//Mat mat = BitmapConverter.ToMat(bitmap);

				//SerialNumber++;

				//if (SerialNumber % 2 == 1)
				//{
				//	cameraImageCallPack_Buffer[0].Enqueue(mat.Clone());
				//}
				//if (SerialNumber % 2 == 0)
				//{
				//	cameraImageCallPack_Buffer[1].Enqueue(mat.Clone());
				//}

				caminfo m_caminfo;
				m_caminfo.camdata = grabbedRawData.Clone();
				m_caminfo.bcolor = bcolor;


				//SMLogWindow.OutLog($"callback:{CCDName}:", Color.Green);

				ImageTransformQueue.Enqueue(m_caminfo);
                //BColor.Enqueue(bcolor);
				//m_event.Set();

            }
			catch (Exception)
			{
			}
		}

		private void processImageDirectly(IGrabbedRawData grabbedRawData, bool bcolor)
        {
			//var bitmap = grabbedRawData.ToBitmap(bcolor);
			//Mat mat = BitmapConverter.ToMat(bitmap);


			Mat mat;
			if (bcolor == false)
			{
				mat = new Mat(grabbedRawData.Height, grabbedRawData.Width, MatType.CV_8UC1, grabbedRawData.Image);
				
			}
			else
			{
				int nRGB = RGBFactory.EncodeLen(grabbedRawData.Width, grabbedRawData.Height, true);
				mat = new Mat(grabbedRawData.Height, grabbedRawData.Width, MatType.CV_8UC3);
				RGBFactory.ToRGB(grabbedRawData.Image, grabbedRawData.Width, grabbedRawData.Height, true, grabbedRawData.PixelFmt, mat.Data, nRGB);
				
			}

			SerialNumber++;

			if ("CCD6" == CCDName)
			{
				cameraImageCallPack_Buffer[1].Enqueue(mat.Clone());
				m_cameraImageCallPackEvent[1].Set();
				//bitmap.Dispose();
				SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[1].Count:{cameraImageCallPack_Buffer[1].Count}", Color.Green);
				return;
			}

			if (SerialNumber % 2 == 1)
			{

				cameraImageCallPack_Buffer[0].Enqueue(mat.Clone());
				m_cameraImageCallPackEvent[0].Set();
				SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[0].Count:{cameraImageCallPack_Buffer[0].Count}", Color.Green);


			}
			else if (SerialNumber % 2 == 0)
			{
				SerialNumber = 0;
				cameraImageCallPack_Buffer[1].Enqueue(mat.Clone());
				m_cameraImageCallPackEvent[1].Set();
				SMLogWindow.OutLog($"CCDName:{CCDName}:cameraImageCallPack_Buffer[1].Count:{cameraImageCallPack_Buffer[1].Count}", Color.Green);
			}

			//bitmap.Dispose();
		}
		IntPtr arrtoptr(byte[] array)
		{
			return System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
		}

	}
}
