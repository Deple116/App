using SMLogControlLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThridLibray;

namespace CameraControlLibrary.CameraDahua
{
	public class DahuaCamera
	{
		private const bool ERR_OK = true;
		private const bool ERR_FAIL = false;

		public string ErrorInfo = "";
		private Mutex m_mutex = new Mutex(); // 锁，保证多线程安全 | mutex  
											 //图像处理自定义委托
		public delegate void delegateProcessHImage(IGrabbedRawData grabbedRawData,bool bColor);
		//图像处理自定义事件
		public event delegateProcessHImage eventProcessImage;

		// 设备搜索 
		// device search 
		private List<IDeviceInfo> li;
		//设备对象
		private IDevice m_dev;


		List<IGrabbedRawData> m_frameList = new List<IGrabbedRawData>();        // 图像缓存列表 | frame data list 
		Thread renderThread = null;         // 显示线程 | image display thread 
		bool m_bShowLoop = true;            // 线程控制变量 | thread looping flag 

		IFloatParameter pGain=null;
		IFloatParameter pExp = null;
		IFloatParameter pGamma = null;
		IIntegraParameter pSharpness = null;
		IEnumParameter pSetUserConfig = null;
		ICommandParameter pUserLoad = null;

	    public CamLoss m_camloss;

		public string CCDName { get; set; } = "";
		public DahuaCamera()
		{

		}

		/// <summary>
		/// 枚举出所有的设备列表
		/// </summary>
		/// <param name="_list"></param>
		/// <returns></returns>
		public bool listAllDevices(ref List<string> _list)
		{
			try
			{
				_list = new List<string>();
				li = Enumerator.EnumerateDevices();
				if (li.Count > 0)
				{
					foreach (var item in li)
					{
						_list.Add(item.Name);
					}
					return ERR_OK;
				}
				else
				{
					ErrorInfo = "没有发现任何可用设备";
					return ERR_FAIL;
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 通过用户自定义名字打开相机
		/// </summary>
		/// <param name="_name"></param>
		/// <returns></returns>
		public bool openDeviceForName(string _name)
		{
			try
			{
				List<IDeviceInfo> li = Enumerator.EnumerateDevices();
				if (li.Count > 0)
				{
					m_dev = Enumerator.GetDeviceByUserID(_name);
					if (!m_dev.Open())
					{
						return ERR_FAIL;
					}

					//
					if (_name == "CCD4" || _name == "CCD5" || _name == "CCD6")
					{
						m_dev.TriggerSet.Open(TriggerSourceEnum.Software);
					}
                    if (_name == "CCD1" || _name == "CCD2" || _name == "CCD3" || _name == "CCD7")
                    {
                        m_dev.TriggerSet.Open(TriggerSourceEnum.Line1);//茅文豪修改，通达项目专用
                    }

					m_dev.StreamGrabber.SetBufferCount(8);
					//m_dev.StreamGrabber.setInterPacketTimeout(10000);
					m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;
					//m_dev.StreamGrabber.im
					m_dev.ConnectionLost += OnConnectLoss;

					// 开启码流 
					if (!m_dev.GrabUsingGrabLoopThread())
					{
						return ERR_FAIL;
					}
				}
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 开启码流
		/// </summary>
		/// <returns></returns>
		public bool openGrabLoopThread()
		{
			try
			{
				if (m_dev.GrabUsingGrabLoopThread())
				{
					return ERR_OK;
				}
				else
				{
					return ERR_FAIL;
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置缓存个数
		/// </summary>
		/// <param name="_value">默认值为16</param>
		/// <returns></returns>
		public bool setBufferCount(int _value)
		{
			try
			{
				if (m_dev.StreamGrabber.SetBufferCount(_value))
				{
					return ERR_OK;
				}
				else
				{
					return ERR_FAIL;
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}

		}

		/// <summary>
		/// 开启采集，注册回调
		/// </summary>
		/// <returns></returns>
		public bool startGrab()
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 停止采集
		/// </summary>
		/// <returns></returns>
		public bool stopGrab()
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 关闭设备
		/// </summary>
		/// <returns></returns>
		public bool closeDevice()
		{
			try
			{
				if(m_dev==null)
				{
                    return ERR_FAIL;
                }
				if (m_dev != null || !m_dev.IsOpen)
				{
					//throw new InvalidOperationException("Device is invalid");
					m_dev.ShutdownGrab();                                       // 停止码流 | stop grabbing 
					m_dev.Close();
					return ERR_FAIL;
				}
				m_dev.StreamGrabber.ImageGrabbed -= OnImageGrabbed;         // 反注册回调 | unregister grab event callback 
				m_dev.ShutdownGrab();                                       // 停止码流 | stop grabbing 
				m_dev.Close();                                              // 关闭相机 | close camera 
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置曝光
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool setExposure(double _value)
		{
			try
			{
				if (pExp == null) pExp = m_dev.ParameterCollection[ParametrizeNameSet.ExposureTime];
				//using (IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ExposureTime])
				{
					if (pExp.SetValue(_value))
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		public bool loadCfg(string filePath)
		{
			try
			{
				List<string> temp=new List<string>();
				//m_dev.LoadDeviceCfg(filePath, ref temp);
				m_dev.UserSet.Current = UserSetType.userSet1;

				return ERR_OK;

				
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置Gamma
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool setGamma(double _value)
		{
			try
			{
				if (pGamma == null) pGamma = m_dev.ParameterCollection[ParametrizeNameSet.Gamma];
				//using (IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.Gamma])
				{
					if (pGamma.SetValue(_value))
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		public bool setSharpness(long _value)
		{
			try
			{
				if (pSharpness == null) pSharpness = m_dev.ParameterCollection[ParametrizeNameSet.Sharpness];
				// using (IIntegraParameter p = m_dev.ParameterCollection[ParametrizeNameSet.Sharpness])
				{
					if (pSharpness.SetValue(_value))
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置触发延时
		/// </summary>
		/// <param name="fValue"></param>
		/// <returns></returns>
		public bool setTriggerDelay(float fValue)
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 获取曝光值
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool getExposure(ref float _value)
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置增益
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool setGain(float _value)
		{
			try
			{
			
				if (pGain == null) pGain = m_dev.ParameterCollection[ParametrizeNameSet.GainRaw];
				//using (IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.GainRaw])
				{
					if (pGain.SetValue(_value))
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
				//return true;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置用户配置
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool setUserConfig(int index)
		{
			try
			{
				string strUserConfig = "";
				if (1 == index)
				{
					strUserConfig = "UserSet1";
				}
				else if (2==index)
				{
					strUserConfig = "UserSet2";
				}

				if (pSetUserConfig == null)
				{
					pSetUserConfig = m_dev.ParameterCollection[new EnumName("UserSetSelector")];
				}
				{


					//bool bset = pSetUserConfig.CanSetValue(temp);

					string temp = pSetUserConfig.GetValue();
					//Console.WriteLine($"GetValue::{temp}");
					bool bres = pSetUserConfig.SetValue(temp);
					//Console.WriteLine($"SetValue::{bres}");

					if (pSetUserConfig.SetValue(temp))
					{
						
						loadUserConfig();
						
						
						return ERR_OK;
					}
					else
					{
						//string temp = pSetUserConfig.GetValue();
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 加载用户配置
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		private bool loadUserConfig()
		{
			try
			{
				if (pUserLoad == null)
				{
					pUserLoad = m_dev.ParameterCollection[new CommandName("UserSetLoad")];
				}
				{
					if (pUserLoad.Execute())
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}
		


		/// <summary>
		/// 获取增益
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public bool getGain(ref float _value)
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 关闭触发，自由采集
		/// </summary>
		/// <returns></returns>
		public bool setFreeRunMode()
		{
			try
			{
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 开启触发，设置相机为软触发
		/// </summary>
		/// <returns></returns>
		public bool setSoftwareTriggerMode()
		{
			try
			{
				if(m_dev==null) return false;
				if (m_dev.TriggerSet.Open(TriggerSourceEnum.Software))
				{
					return ERR_OK;
				}
				else
				{
					return ERR_FAIL;
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 开启触发，设置相机为外部触发line1
		/// </summary>
		/// <returns></returns>
		public bool setExternalTriggerMode()
		{
			try
			{
				if (m_dev == null) return false;
				if (m_dev.TriggerSet.Open(TriggerSourceEnum.Line1))
				{
					return ERR_OK;
				}
				else
				{
					return ERR_FAIL;
				}
				return ERR_OK;
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 设置图像格式
		/// </summary>
		/// <param name="_value">Mono8||</param>
		/// <returns></returns>
		public bool setImageFormat(string _value)
		{
			try
			{
				using (IEnumParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ImagePixelFormat])
				{
					if (p.SetValue(_value))
					{
						return ERR_OK;
					}
					else
					{
						return ERR_FAIL;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 执行一次软触发
		/// </summary>
		/// <returns></returns>
		public bool softWareTriggerOnce()
		{
			if (m_dev == null)
			{
				throw new InvalidOperationException("Device is invalid");
			}

			try
			{
				if (m_dev.ExecuteSoftwareTrigger())
				{
					return ERR_OK;
				}
				else
				{
					return ERR_FAIL;
				}
			}
			catch (Exception ex)
			{
				ErrorInfo = ex.ToString();
				return ERR_FAIL;
			}
		}

		/// <summary>
		/// 相机丢失回调
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnConnectLoss(object sender, EventArgs e)
		{
			SMLogWindow.OutLog($"OnConnectLoss:{CCDName}:", Color.Green);
			m_camloss(CCDName);
			//m_dev.ShutdownGrab();
			//m_dev.Dispose();
			//m_dev = null;
		}

		// 码流数据回调 
		// grab callback function 
		private void OnImageGrabbed(Object sender, GrabbedEventArgs e)
		{
			
			//m_mutex.WaitOne();
			SMLogWindow.OutLog($"OnImageGrabbed:CCDName:{CCDName}", Color.Green);
			


			
			bool bcolor = e.GrabResult.PixelFmt == GvspPixelFormatType.gvspPixelMono8 ? false : true;

			
			eventProcessImage(e.GrabResult.Clone(), bcolor);
			//m_mutex.ReleaseMutex();
		}
	}
}
