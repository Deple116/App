
using SmoreVision.HardwareControlClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreVision.BusinessClass
{
    public class ActionRunThread
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        public bool Cycled = false;

        private string LastError = "";

        private int returnValue = 0;

        private SiemensPLCControl m_SiemensPLCControl;

        private Task m_ActionThreadProcess;

        string CCDName = "CCD1";

        public ActionRunThread(SiemensPLCControl _siemensPLCControl)
        {
           
            m_SiemensPLCControl = _siemensPLCControl;
        }

        public int StartThread()
        {
            if (m_ActionThreadProcess == null || m_ActionThreadProcess.IsCompleted == true)
            {
                m_ActionThreadProcess = Task.Factory.StartNew(()=> ThreadProcedureProcess());
                // SMLogWindow.OutLog("动作交互线程开启.", Color.Green);
                Console.WriteLine($"动作交互线程开启.");
            }
            return ERROR_OK;
        }

        public int ThreadProcedureProcess()
        {
            try
            {
                Cycled = true;
                while (Cycled)
                {
                   
                    switch (CCDName)
                    {
                        case "CCD1":
                            {
                                if (m_SiemensPLCControl.ReadBool("DB2000.4.5"))
                                {
                                    returnValue = m_SiemensPLCControl.WriteBool("DB2000.4.5", false);
                                    if (returnValue != ERROR_OK)
                                    {
                                        //SMLogWindow.OutLog($"{m_HIKCameraControl.CCDName}复位切换程序号使能信号失败!",Color.Red);
                                    }
                                    ushort returnProNum = m_SiemensPLCControl.ReadUshort("DB2000.6.0");
                                    returnValue = m_SiemensPLCControl.WriteUshort("DB2000.2.0", returnProNum);
                                    if (returnValue != ERROR_OK)
                                    {
                                        //SMLogWindow.OutLog($"{m_HIKCameraControl.CCDName}写程序号失败!", Color.Red);
                                    }
                                    returnValue = m_SiemensPLCControl.WriteBool("DB2000.0.6",true);
                                    if (returnValue != ERROR_OK)
                                    {
                                        //SMLogWindow.OutLog($"{m_HIKCameraControl.CCDName}写切换程序号完成信号失败!", Color.Red);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(10);
                }
                //SMLogWindow.OutLog("动作交互线程结束.",Color.Green);
                Console.WriteLine($"动作交互线程结束.");
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }
    }
}
