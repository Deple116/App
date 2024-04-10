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
    public partial class SMSaveSet : UserControl
    {
        private static int ErrorOK = 0;
        private static int ErrorFailed = -1;

        private string ErrorInfo = "";

        private XMLConfigParse m_XMLConfigParse = null;

        private string ConfigFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + "SmoreVisionConfig.xml";

        public SMSaveSet()
        {
            InitializeComponent();
        }

        private void SMSaveSet_Load(object sender, EventArgs e)
        {
            m_XMLConfigParse = new XMLConfigParse();
            int returnValue = InitialConfigFile();
            if (returnValue != ErrorOK)
            {
                MessageBox.Show($"加载配置文件失败,错误代码:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            smButtonLookOKLabel.Enabled = textBoxLableImgPath.Enabled = ucCheckBoxSaveLableOKImg.Checked;
            smButtonLookNGLabel.Enabled = textBoxLabelNGImgPath.Enabled = ucCheckBoxSaveLableNGImg.Checked;
            smButtonLookSrcOKImg.Enabled = textBoxSrcOKImgPath.Enabled = ucCheckBoxSaveSrcOKImg.Checked;
            smButtonLookSrcNGImg.Enabled = textBoxSrcNGImgPath.Enabled = ucCheckBoxSaveSrcNGImg.Checked;

            ucCheckBoxSaveLableOKImg.Checked = m_XMLConfigParse.SaveImage.Items[0].SaveEnable;
            ucCheckBoxSaveLableNGImg.Checked = m_XMLConfigParse.SaveImage.Items[1].SaveEnable;
            ucCheckBoxSaveSrcOKImg.Checked = m_XMLConfigParse.SaveImage.Items[2].SaveEnable;
            ucCheckBoxSaveSrcNGImg.Checked = m_XMLConfigParse.SaveImage.Items[3].SaveEnable;

            textBoxLableImgPath.Text = m_XMLConfigParse.SaveImage.Items[0].Path;
            textBoxLabelNGImgPath.Text = m_XMLConfigParse.SaveImage.Items[1].Path;
            textBoxSrcOKImgPath.Text = m_XMLConfigParse.SaveImage.Items[2].Path;
            textBoxSrcNGImgPath.Text = m_XMLConfigParse.SaveImage.Items[3].Path;

            comboBoxLabelImgType.Text = m_XMLConfigParse.SaveImage.Items[1].ImageType;
            comboBoxSrcImgType.Text = m_XMLConfigParse.SaveImage.Items[2].ImageType;

            nUDLabelSaveDays.Value = m_XMLConfigParse.SaveTime.Items[0].SaveDays;
            ucCheckBoxLabelSaveDaysEnable.Checked = m_XMLConfigParse.SaveTime.Items[0].SaveDaysEnable;
            nUDLabelSaveSize.Value = m_XMLConfigParse.SaveTime.Items[0].SaveSize;
            ucCheckBoxLabelSaveSizeEnable.Checked = m_XMLConfigParse.SaveTime.Items[0].SaveSizeEnable;

            nUDSRCSaveDays.Value = m_XMLConfigParse.SaveTime.Items[1].SaveDays;
            ucCheckBoxSrcSaveDaysEnable.Checked = m_XMLConfigParse.SaveTime.Items[1].SaveDaysEnable;
            nUDSRCSaveSize.Value = m_XMLConfigParse.SaveTime.Items[1].SaveSize;
            ucCheckBoxSrcSaveSizeEnable.Checked = m_XMLConfigParse.SaveTime.Items[1].SaveSizeEnable;

            nUDLabelSaveDays.Enabled = ucCheckBoxLabelSaveDaysEnable.Checked;
            nUDLabelSaveSize.Enabled = ucCheckBoxLabelSaveSizeEnable.Checked;
            nUDSRCSaveDays.Enabled = ucCheckBoxSrcSaveDaysEnable.Checked;
            nUDSRCSaveSize.Enabled = ucCheckBoxSrcSaveSizeEnable.Checked;
        }

        private void smButtonSaveSaveSet_BtnClick(object sender, EventArgs e)
        {
            try
            {
                m_XMLConfigParse.SaveImage.Items[0].ImageType = comboBoxLabelImgType.Text;
                m_XMLConfigParse.SaveImage.Items[1].ImageType = comboBoxLabelImgType.Text;
                m_XMLConfigParse.SaveImage.Items[2].ImageType = comboBoxSrcImgType.Text;
                m_XMLConfigParse.SaveImage.Items[3].ImageType = comboBoxSrcImgType.Text;

                m_XMLConfigParse.SaveImage.Items[0].Path = textBoxLableImgPath.Text;
                m_XMLConfigParse.SaveImage.Items[1].Path = textBoxLabelNGImgPath.Text;
                m_XMLConfigParse.SaveImage.Items[2].Path = textBoxSrcOKImgPath.Text;
                m_XMLConfigParse.SaveImage.Items[3].Path = textBoxSrcNGImgPath.Text;

                m_XMLConfigParse.SaveImage.Items[0].SaveEnable = ucCheckBoxSaveLableOKImg.Checked;
                m_XMLConfigParse.SaveImage.Items[1].SaveEnable = ucCheckBoxSaveLableNGImg.Checked;
                m_XMLConfigParse.SaveImage.Items[2].SaveEnable = ucCheckBoxSaveSrcOKImg.Checked;
                m_XMLConfigParse.SaveImage.Items[3].SaveEnable = ucCheckBoxSaveSrcNGImg.Checked;

                m_XMLConfigParse.SaveTime.Items[0].SaveDays =Convert.ToUInt32(nUDLabelSaveDays.Value);
                m_XMLConfigParse.SaveTime.Items[0].SaveDaysEnable = ucCheckBoxLabelSaveDaysEnable.Checked;
                m_XMLConfigParse.SaveTime.Items[0].SaveSize = Convert.ToUInt32(nUDLabelSaveSize.Value);
                m_XMLConfigParse.SaveTime.Items[0].SaveSizeEnable = ucCheckBoxLabelSaveSizeEnable.Checked;

                m_XMLConfigParse.SaveTime.Items[1].SaveDays = Convert.ToUInt32(nUDSRCSaveDays.Value);
                m_XMLConfigParse.SaveTime.Items[1].SaveDaysEnable = ucCheckBoxSrcSaveDaysEnable.Checked;
                m_XMLConfigParse.SaveTime.Items[1].SaveSize = Convert.ToUInt32(nUDSRCSaveSize.Value);
                m_XMLConfigParse.SaveTime.Items[1].SaveSizeEnable = ucCheckBoxSrcSaveSizeEnable.Checked;

                XMLSerialize.SerializeToXml<XMLConfigParse>(ConfigFilePath, m_XMLConfigParse, ref ErrorInfo);
                MessageBox.Show("保存成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败,错误代码{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCheckBoxSaveLableOKImg_CheckedChangeEvent(object sender, EventArgs e)
        {
            smButtonLookOKLabel.Enabled = textBoxLableImgPath.Enabled = ucCheckBoxSaveLableOKImg.Checked;
        }

        private void ucCheckBoxSaveLableNGImg_CheckedChangeEvent(object sender, EventArgs e)
        {
            smButtonLookNGLabel.Enabled = textBoxLabelNGImgPath.Enabled = ucCheckBoxSaveLableNGImg.Checked;
        }

        private void ucCheckBoxSaveSrcOKImg_CheckedChangeEvent(object sender, EventArgs e)
        {
            smButtonLookSrcOKImg.Enabled = textBoxSrcOKImgPath.Enabled = ucCheckBoxSaveSrcOKImg.Checked;
        }

        private void ucCheckBoxSaveSrcNGImg_CheckedChangeEvent(object sender, EventArgs e)
        {
            smButtonLookSrcNGImg.Enabled = textBoxSrcNGImgPath.Enabled = ucCheckBoxSaveSrcNGImg.Checked;
        }

        private void smButtonLookOKLabel_BtnClick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
                DialogResult dialogResult;
                dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    textBoxLableImgPath.Text = openFileDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void smButtonLookNGLabel_BtnClick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
                DialogResult dialogResult;
                dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    textBoxLabelNGImgPath.Text = openFileDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void smButtonLookSrcImg_BtnClick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
                DialogResult dialogResult;
                dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    textBoxSrcOKImgPath.Text = openFileDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void smButtonLookNGSrcImg_BtnClick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
                DialogResult dialogResult;
                dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    textBoxSrcNGImgPath.Text = openFileDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ucCheckBoxLabelSaveDaysEnable_CheckedChangeEvent(object sender, EventArgs e)
        {
            nUDLabelSaveDays.Enabled = ucCheckBoxLabelSaveDaysEnable.Checked;
        }

        private void ucCheckBoxLabelSaveSizeEnable_CheckedChangeEvent(object sender, EventArgs e)
        {
            nUDLabelSaveSize.Enabled = ucCheckBoxLabelSaveSizeEnable.Checked;
        }

        private void ucCheckBoxSrcSaveDaysEnable_CheckedChangeEvent(object sender, EventArgs e)
        {
            nUDSRCSaveDays.Enabled = ucCheckBoxSrcSaveDaysEnable.Checked;
        }

        private void ucCheckBoxSrcSaveSizeEnable_CheckedChangeEvent(object sender, EventArgs e)
        {
            nUDSRCSaveSize.Enabled = ucCheckBoxSrcSaveSizeEnable.Checked;
        }
    }
}
