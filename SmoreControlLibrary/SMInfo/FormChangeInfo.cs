using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SmoreControlLibrary.SMInfo
{
    public partial class FormChangeInfo : Form
    {

        public string Batch { get; set; }
        public string ProductModel { get; set; }
        public string ProductGroup { get; set; }

        public bool CHANGE { get; set; }

        public FormChangeInfo()
        {
            InitializeComponent();
        }

        private void panelClose_Click(object sender, EventArgs e)
        {
            CHANGE = false;
            this.Close();
        }

        private void smButtonCancle_BtnClick(object sender, EventArgs e)
        {
        
            this.Close();
        }

        private void smButtonConfirm_BtnClick(object sender, EventArgs e)
        {
            string temp1 = (string)comboBox1.Text;
            string temp2 = (string)comboBox2.Text;
            string temp3 = (string)comboBox3.Text;

            if ("" == temp1 || temp1 == null || "" == temp2 || temp2 == null || "" == temp3 || temp3 == null)
            {
                CHANGE = false;
                return;
            }

            CHANGE = true;
            ProductModel = temp1;
            ProductGroup = temp2;
            Batch = temp3;


            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormChangeInfo_Load(object sender, EventArgs e)
        {
            //comboBox1.Text = ProductModel;
            comboBox3.Text = Batch;
            comboBox2.Text = ProductGroup;
        }

        public void AddItems(List<FileInfo> list)
        {
            comboBox1.Items.Clear();
            foreach(var temp in list)
            {
                
                string name = temp.Name;
                string[] sArray = Regex.Split(name, ".json", RegexOptions.IgnoreCase);

                comboBox1.Items.Add(sArray[0]);
            }

            CheckAllItems();
        }

        private void CheckAllItems()
        {
            bool bCheck = false;
            for (int i = 0; i <= comboBox1.Items.Count - 1; i++)
            {
                if (ProductModel == comboBox1.Items[i].ToString())
                {
                    comboBox1.SelectedIndex = i;
                    bCheck = true;
                }  
            }

            if(!bCheck) comboBox1.SelectedIndex = 0;
        }

    }
}
