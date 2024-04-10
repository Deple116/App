using HslCommunication;
using HslCommunication.Profinet.Siemens;
using S7.Net;
using SMLogControlLibrary;
using SmoreControlLibrary;
using SmoreControlLibrary.EquipmentDriver;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SmoreVision.HardwareControlClass
{
    public class SiemensPLCControl
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        private const bool ERROR_TRUE = true;
        private const bool ERROR_FALSE = false;

        public string LastError { get; private set; } = "";

        private static object objWrite = new object();

        private XMLConfigParse m_XMLConfigParse;

        public string ErrorInfo = "";

        private SiemensS7Net m_Siemens;

        public SiemensPLCControl(ref XMLConfigParse _xMLConfigParse)
        {
            m_XMLConfigParse = _xMLConfigParse;
            if (HslCommunication.Authorization.SetAuthorizationCode("f562cc4c-4772-4b32-bdcd-f3e122c534e3"))
            {
                SMLogWindow.OutLog("PLC通信注册成功.", Color.Green);
            }
            else
            {
                SMLogWindow.OutLog("PLC通信注册失败.", Color.Red);
            }
        }

        public int Initial()
        {
            try
            {
                m_Siemens = new SiemensS7Net(SiemensPLCS.S1500, m_XMLConfigParse.PLC.IP) { ConnectTimeOut = 5000 };
                OperateResult connect = m_Siemens.ConnectServer();
                if (connect.IsSuccess)
                {
                    return ERROR_OK;
                }
                else
                {
                    return ERROR_FAILED;
                }
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }

        public int DeInitial()
        {
            try
            {
                if(m_Siemens!=null) m_Siemens.ConnectClose();
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }

        /// <summary>
        /// 写Bool操作
        /// </summary>
        /// <returns></returns>
        public int WriteBool(string _dbAddress, bool _result)
        {
            try
            {
               // lock (objWrite)
                {
                    OperateResult operateResult = m_Siemens.Write(_dbAddress, _result);
                    //while (true)
                    //{
                    //    //Thread.Sleep(50);
                    //    OperateResult<bool> result = m_Siemens.ReadBool(_dbAddress);
                    //    if (result.Content == _result)
                    //    {
                    //        return ERROR_OK;
                    //    }
                    //    else
                    //    {
                    //        OperateResult operateResultAgain = m_Siemens.Write(_dbAddress, _result);
                    //        continue;
                    //    }
                    //}
                    return ERROR_OK;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        /// <summary>
        /// 读Bool操作
        /// </summary>
        /// <returns></returns>
        public bool ReadBool(string _dbAddress)
        {
            try
            {
                OperateResult<bool> result = m_Siemens.ReadBool(_dbAddress);
                return result.Content;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                MessageBox.Show(ex.ToString());
                return ERROR_FALSE;
            }
        }

        /// <summary>
        /// 读ushort
        /// </summary>
        /// <param name="_dbAddress"></param>
        /// <returns></returns>
        public ushort ReadUshort(string _dbAddress)
        {
            try
            {
                OperateResult<ushort> result = m_Siemens.ReadUInt16(_dbAddress);
                return result.Content;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int WriteUshort(string _dbAddress, ushort value)
        {
            try
            {
                //lock (objWrite)
                {
                    OperateResult operateResult = m_Siemens.Write(_dbAddress, value);
                    //while (true)
                    //{
                    //    //Thread.Sleep(50);
                    //    OperateResult<ushort> result = m_Siemens.ReadUInt16(_dbAddress);
                    //    if (result.Content == value)
                    //    {
                    //        return ERROR_OK;
                    //    }
                    //    else
                    //    {
                    //        OperateResult operateResultAgain = m_Siemens.Write(_dbAddress, value);
                    //        continue;
                    //    }
                    //}
                    return ERROR_OK;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }

        /// <summary>
        /// 读ushort
        /// </summary>
        /// <param name="_dbAddress"></param>
        /// <returns></returns>
        public Byte ReadByte(string _dbAddress)
        {
            try
            {
                OperateResult<Byte> result = m_Siemens.ReadByte(_dbAddress);
                return result.Content;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// 读字符串
        /// </summary>
        /// <param name="_dbAddress"></param>
        /// <returns></returns>
        public string ReadString(string _dbAddress,ushort length)
        {
            try
            {
                OperateResult<string> result = m_Siemens.ReadString(_dbAddress, length, Encoding.UTF8);
                return result.Content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int WriteString(string _dbAddress, string value)
        {
            try
            {
                //lock (objWrite)
                {
                    OperateResult operateResult = m_Siemens.Write(_dbAddress, value);
                    //while (true)
                    //{
                    //    //Thread.Sleep(50);
                    //    OperateResult<ushort> result = m_Siemens.ReadUInt16(_dbAddress);
                    //    if (result.Content == value)
                    //    {
                    //        return ERROR_OK;
                    //    }
                    //    else
                    //    {
                    //        OperateResult operateResultAgain = m_Siemens.Write(_dbAddress, value);
                    //        continue;
                    //    }
                    //}
                    return ERROR_OK;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                return ERROR_FAILED;
            }
        }
    }
}
