using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7.Net;

namespace SmoreControlLibrary.EquipmentDriver.PLCSiemens
{
    public enum PLCType
    {
        S7200 = CpuType.S7200,
        Logo0BA8 = CpuType.Logo0BA8,
        S7200Smart = CpuType.S7200Smart,
        S7300 = CpuType.S7300,
        S7400 = CpuType.S7400,
        S71200 = CpuType.S71200,
        S71500 = CpuType.S71500
    }

    public partial class SMPLCSiemens : UserControl
    {
        public SMPLCSiemens()
        {
            InitializeComponent();
        }
    }
}
