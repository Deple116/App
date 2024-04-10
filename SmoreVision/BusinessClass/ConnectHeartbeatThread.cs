using CameraControlLibrary;
using SMLogControlLibrary;
using SmoreControlLibrary;
using SmoreVision.HardwareControl;
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
    public delegate void ShowHeartColor(Color _color);
    public class ConnectHeartbeatThread
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        public bool Cycled = false;
        private string LastError = "";
        private int returnValue = 0;

        public ShowHeartColor m_ShowHeartColor = null;

        private SiemensPLCControl m_SiemensPLCControl;
        private CameraInterface m_CameraControl = null;
        private Task m_ActionThreadProcess;

        public ConnectHeartbeatThread(CameraInterface _CameraControl,SiemensPLCControl _siemensPLCControl)
        {
            m_CameraControl = _CameraControl;
            m_SiemensPLCControl = _siemensPLCControl;
        }

        public int StartThread()
        {
            if (m_ActionThreadProcess == null || m_ActionThreadProcess.IsCompleted == true)
            {
                m_ActionThreadProcess = Task.Factory.StartNew(() => ThreadProcedureProcess());
                SMLogWindow.OutLog("Connect Heartbeat 线程开启.", Color.Green);
            }
            return ERROR_OK;
        }

        public int ThreadProcedureProcess()
        {
            try
            {
                Cycled = false;
                while (Cycled)
                {
                    
                    Thread.Sleep(100);
                }
                SMLogWindow.OutLog("动作交互线程结束.", Color.Green);
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
