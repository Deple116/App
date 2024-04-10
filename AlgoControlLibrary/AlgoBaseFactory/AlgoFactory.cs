using AlgoControlLibrary.VimoAlgo;
using OpenCvSharp;
using SmartMore.ViMo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoControlLibrary.AlgoBaseFactory
{

    public abstract class AlgoFactory
    {
        public abstract IAlgo CreateVimo();
    }
}
