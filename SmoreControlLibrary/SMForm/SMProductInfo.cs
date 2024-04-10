using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmoreControlLibrary.SystemConfig;
using static SmoreControlLibrary.SystemConfig.JsonFileParse;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmoreControlLibrary.SMForm
{
    public partial class SMProductInfo : UserControl
    {
        [Description("该条信息是否选中"), Category("SmoreControl")]
        public bool Checked
        {
            get { return ucCheckBox.Checked; }
            set {ucCheckBox.Checked = value;}
        }

        [Description("该条信息的ID编号"), Category("SmoreControl")]
        public string ID
        {
            get { return textBoxID.Text; }
            set { textBoxID.Text = value; }
        }

        [Description("单元格1的内容"), Category("SmoreControl")]
        public string CellContent1
        {
            get { return textBoxValue1.Text; }
            set { textBoxValue1.Text = value; }
        }

        [Description("单元格1的颜色"), Category("SmoreControl")]
        public Color CellColor1
        {
            get { return textBoxValue1.ForeColor; }
            set { textBoxValue1.ForeColor = value; }
        }


        [Description("单元格2的内容"), Category("SmoreControl")]
        public string CellContent2
        {
            get { return textBoxValue2.Text; }
            set { textBoxValue2.Text = value; }
        }

        [Description("单元格2的颜色"), Category("SmoreControl")]
        public Color CellColor2
        {
            get { return textBoxValue2.ForeColor; }
            set { textBoxValue2.ForeColor = value; }
        }

        [Description("单元格3的内容"), Category("SmoreControl")]
        public string CellContent3
        {
            get { return textBoxValue3.Text; }
            set { textBoxValue3.Text = value; }
        }
        [Description("单元格3的颜色"), Category("SmoreControl")]
        public Color CellColor3
        {
            get { return textBoxValue3.ForeColor; }
            set { textBoxValue3.ForeColor = value; }
        }


        public ParametricRecord parametricRecord = null;

        public SMProductInfo()
        {
            InitializeComponent();
        }

        private void SMProductInfo_Load(object sender, EventArgs e)
        {
            textBoxID.Enabled = ucCheckBox.Checked;
            textBoxValue1.Enabled = ucCheckBox.Checked;
            textBoxValue2.Enabled = ucCheckBox.Checked;
            textBoxValue3.Enabled = ucCheckBox.Checked;
            panelUpLoadImg.Enabled = ucCheckBox.Checked;

            parametricRecord = new ParametricRecord();
        }

        private void ucCheckBox_CheckedChangeEvent(object sender, EventArgs e)
        {
            textBoxID.Enabled = ucCheckBox.Checked;
            textBoxValue1.Enabled = ucCheckBox.Checked;
            textBoxValue2.Enabled = ucCheckBox.Checked;
            textBoxValue3.Enabled = ucCheckBox.Checked;
            panelUpLoadImg.Enabled = ucCheckBox.Checked;
        }

        private void panelUpLoadImg_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
                DialogResult dialogResult;
                dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    string filePath = openFileDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
