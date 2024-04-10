using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreControlLibrary.SMForm
{
    public partial class SMSystemSet : UserControl
    {
        private static int ErrorOK = 0;
        private static int ErrorFailed = -1;

        private string ErrorInfo = "";

        private XMLConfigParse m_XMLConfigParse = null;

        private const int INFO_MAX_COUNT = 2000;

        private string ConfigFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + "SmoreVisionConfig.xml";

        public SMSystemSet()
        {
            InitializeComponent();
        }

        private void SMSystemSet_Load(object sender, EventArgs e)
        {
            try
            {
                m_XMLConfigParse = new XMLConfigParse();
                int returnValue = InitialConfigFile();
                if (returnValue != ErrorOK)
                {
                    MessageBox.Show($"加载配置文件失败,错误代码:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                textBoxSystemName.Text = m_XMLConfigParse.System.ProjectName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void smButtonSaveSystemSet_BtnClick(object sender, EventArgs e)
        {
            try
            {
                m_XMLConfigParse.System.ProjectName = textBoxSystemName.Text;
                XMLSerialize.SerializeToXml<XMLConfigParse>(ConfigFilePath, m_XMLConfigParse, ref ErrorInfo);
                FormMainBase.formMainBase.ProjectName= textBoxSystemName.Text;
                MessageBox.Show("保存成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败,错误代码{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int InitialConfigFile()
        {
            int returnValue = XMLConfigParse.DeserializeFromXml<XMLConfigParse>(ConfigFilePath, ref m_XMLConfigParse, ref ErrorInfo);
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }
            returnValue = m_XMLConfigParse.GenerateNodeInfo();
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }
            return ErrorOK;
        }

        private void smButtonGetModelInfo_BtnClick(object sender, EventArgs e)
        {
            textBoxSystemInfo.Text = "";

            int returnValue = XMLConfigParse.DeserializeFromXml<XMLConfigParse>(ConfigFilePath, ref m_XMLConfigParse, ref ErrorInfo);
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                textBoxSystemInfo.Text = ErrorInfo;
                return;
            }

            returnValue = m_XMLConfigParse.GenerateNodeInfo();
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                textBoxSystemInfo.Text = "Generate config info error.";
                return;
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> nodeDictionary in m_XMLConfigParse.NodeDictionary)
            {
                AppendConfigInfo(nodeDictionary.Key);
                foreach (KeyValuePair<string, string> node in nodeDictionary.Value)
                {
                    AppendConfigInfo($"{node.Key}\t:\t{node.Value}");
                }
                AppendConfigInfo("");
            }
        }

        private void AppendConfigInfo(string information)
        {
            AppendInfo(textBoxSystemInfo, information);
        }

        private void AppendInfo(TextBox textBox, string information)
        {
            if (textBox.Lines.Length > INFO_MAX_COUNT)
            {
                textBox.Clear();
            }

            textBox.AppendText(information + Environment.NewLine);
            textBox.ScrollToCaret();
        }
    }
}
