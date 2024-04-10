using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreControlLibrary.SMForm
{
    public partial class FormSet : Form
    {
        private SMSystemSet systemSet = null;
        private SMCountSet countSet = null;
        private SMLightSet lightSet = null;
        private SMProductInfoSet productInfoSet = null;
        private SMSaveSet saveSet = null;
        private SMThresholdSet thresholdSet = null;

        private List<UserControl> m_listUserControl =null;
        private List<Button> m_listButton = null;

        public FormSet()
        {
            InitializeComponent();
        }

        private void panelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSystemSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonSystemSet, m_listButton);
            ShowUserControl(systemSet);
        }

        private void buttonSaveSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonSaveSet, m_listButton);
            ShowUserControl(saveSet);
        }

        private void buttonThresholdSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonThresholdSet, m_listButton);
            ShowUserControl(thresholdSet);
        }

        private void buttonLightSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonLightSet, m_listButton);
            ShowUserControl(lightSet);
        }

        private void buttonProductInfoSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonProductInfoSet, m_listButton);
            ShowUserControl(productInfoSet);
        }

        private void buttonCountSet_Click(object sender, EventArgs e)
        {
            DisplayButton(buttonCountSet, m_listButton);
            ShowUserControl(countSet);
        }

        private void FormSet_Load(object sender, EventArgs e)
        {
            systemSet = new SMSystemSet();
            countSet = new SMCountSet();
            lightSet = new SMLightSet();
            productInfoSet = new SMProductInfoSet();
            saveSet = new SMSaveSet();
            thresholdSet = new SMThresholdSet();

            m_listUserControl = new List<UserControl>();
            m_listButton = new List<Button>();

            m_listUserControl.Add(systemSet);
            m_listUserControl.Add(countSet);
            m_listUserControl.Add(lightSet);
            m_listUserControl.Add(productInfoSet);
            m_listUserControl.Add(saveSet);
            m_listUserControl.Add(thresholdSet);

            m_listButton.Add(buttonSystemSet);
            m_listButton.Add(buttonSaveSet);
            m_listButton.Add(buttonThresholdSet);
            m_listButton.Add(buttonLightSet);
            m_listButton.Add(buttonProductInfoSet);
            m_listButton.Add(buttonCountSet);

            foreach (UserControl userControl in m_listUserControl)
            {
                userControl.Dock = DockStyle.Fill;
                panelFormSetHome.Controls.Add(userControl);
                userControl.Show();
            }

            ShowUserControl(systemSet);
            DisplayButton(buttonSystemSet, m_listButton);
        }

        public static void DisplayButton(Button buttonClick, List<Button> listButton)
        {
            foreach (Button button in listButton)
            {
                if (button.Handle == buttonClick.Handle)
                {
                    button.BackColor = System.Drawing.Color.FromArgb(125, 136, 149);
                }
                else
                {
                    button.BackColor = System.Drawing.Color.FromArgb(153, 167, 182);
                }
            }
        }

        private void ShowUserControl(UserControl formSel)
        {
            foreach (UserControl form in m_listUserControl)
            {
                if (form == formSel)
                {
                    form.Show();
                }
                else
                {
                    form.Hide();
                }
            }
        }
    }
}
