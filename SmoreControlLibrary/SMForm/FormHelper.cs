﻿using System;
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
    public partial class FormHelper : Form
    {
        public FormHelper()
        {
            InitializeComponent();
        }

        private void panelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
