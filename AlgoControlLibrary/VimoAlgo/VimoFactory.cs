using AlgoControlLibrary.AlgoBaseFactory;
using OpenCvSharp;
using SmartMore.ViMo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoControlLibrary.VimoAlgo
{
    public class VimoFactory : AlgoFactory
    {
        
        public override IAlgo CreateVimo()
        {
            return new VimoDerivedAlgo();
        }

        
    }
}
