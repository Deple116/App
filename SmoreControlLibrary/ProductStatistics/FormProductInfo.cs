using SMLogControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreControlLibrary.ProductStatistics
{
    public delegate void Refresh();
    public partial class FormProductInfo : UserControl
    {
        public SMForm.SMCountSet smCountSet1;
        SqlClass m_sqlclass = new SqlClass();
        public bool isopen;

        public FormProductInfo()
        {
            InitializeComponent();
        }

        public void RunStateSet(bool runstate)
        {
           // smCountSet1.BtnRunState = runstate;          
        }

        private void FormProductInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isopen = false;
        }

        private void FormProductInfo_Load(object sender, EventArgs e)
        {
            this.isopen = true;

            m_sqlclass.InitSql();
        }

        private void smCountSet1_Load(object sender, EventArgs e)
        {

        }

        public void WriteSqlData(string productName,Dictionary<string,string> dicSqlData)
        {
            m_sqlclass.InsertData(productName, dicSqlData);

        }

       
    }
}
