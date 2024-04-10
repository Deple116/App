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
    public partial class SMProductInfoSetTitle : UserControl
    {

        [Description("是否选中"), Category("SmoreControl")]
        public bool Checked
        {
            get { return ucCheckBox1.Checked; }
            set { ucCheckBox1.Checked = value;}
        }

        [Description("单元格1的内容"), Category("SmoreControl")]
        public string HeaderCellContent1
        {
            get { return lbl1.Text; }
            set { lbl1.Text = value; }
        }

        [Description("单元格2的内容"), Category("SmoreControl")]
        public string HeaderCellContent2
        {
            get { return lbl2.Text; }
            set { lbl2.Text = value; }
        }

        [Description("单元格3的内容"), Category("SmoreControl")]
        public string HeaderCellContent3
        {
            get { return lbl3.Text; }
            set { lbl3.Text = value; }
        }

        [Description("是否选中"), Category("SmoreControl")]
        public event EventHandler CheckedChangedEvent;
        public SMProductInfoSetTitle()
        {
            InitializeComponent();
        }

        private void SMProductInfoSetTitle_Load(object sender, EventArgs e)
        {

        }

        private void ucCheckBox1_CheckedChangeEvent(object sender, EventArgs e)
        {
            CheckedChangedEvent(sender,e);
        }
    }
}
