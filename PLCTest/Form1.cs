using SmoreVision.BusinessClass;
using SmoreVision.HardwareControlClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTest
{
    public partial class Form1 : Form
    {

        public const int ERROR_OK = 0;
        public const int ERROR_FAILED = -1;

        private int returnValue = 0;

        private SiemensPLCControl m_SiemensPLCControl;

        private ActionRunThread m_ActionRunThread;

        public Form1()
        {
            InitializeComponent();
            m_SiemensPLCControl = new SiemensPLCControl();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            m_ActionRunThread.StartThread();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            returnValue = m_SiemensPLCControl.Initial();
            if (returnValue != ERROR_OK)
            {
                Console.WriteLine($"PLC初始化失败!");
                //Log.Add($"PLC初始化失败!", Color.Red);
                return;
            }
            else
            {
                Console.WriteLine($"PLC初始化成功!");
                //Log.Add($"PLC初始化成功!", Color.Green);
            }

           // m_ActionRunThread = new ActionRunThread(m_SiemensPLCControl);




            //发送相机ready信号
            //m_SiemensPLCControl.WriteBool("DB89.0.0",true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_ActionRunThread.Cycled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           byte data =m_SiemensPLCControl.ReadByte("DB89.67.0");
            MessageBox.Show($"size:{data}");
            string strdata = m_SiemensPLCControl.ReadString("DB89.68.0");
            MessageBox.Show("Read::"+ strdata);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_SiemensPLCControl.WriteUshort("DB89.2.0", 13);
        }
    }
}
