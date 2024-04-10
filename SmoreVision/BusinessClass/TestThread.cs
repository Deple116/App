using SMLogControlLibrary;
using SmoreControlLibrary;
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
    public delegate void ShowResultTest(bool _result);
    public delegate void ShowHeartColorTest(Color _color);

    public class TestThread
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        public bool Cycled = false;

        private string LastError = "";

        private int returnValue = 0;

        private Task m_ActionThreadProcess;

        public ShowResultTest m_ShowResultTest;
        public ShowHeartColorTest m_ShowHeartColorTest;

        public int StartThread()
        {
            if (m_ActionThreadProcess == null || m_ActionThreadProcess.IsCompleted == true)
            {
                m_ActionThreadProcess = Task.Factory.StartNew(() => ThreadProcedureProcess());
                SMLogWindow.OutLog("测试交互线程开启.", Color.Green);
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
                    m_ShowResultTest(false);
                    m_ShowHeartColorTest(Color.Red);
                    Thread.Sleep(1000);
                    m_ShowResultTest(true);
                    m_ShowHeartColorTest(Color.Green);
                    Thread.Sleep(1000);
                }
                SMLogWindow.OutLog("测试交互线程结束.", Color.Green);
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
