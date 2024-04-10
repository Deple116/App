using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoControlLibrary.AlgoBaseFactory
{
    public enum   EnumReturnVal
    {
        Return_OK = 0,
        Return_Fail = 1,

    }

    public class  AlgoInitInput
    {

        public string modelPath = @"./model.vimosln"; // replace with your `model.vimosln` path
        public string moduleId = "5";                 // get module id from `index.html`
        public bool useGpu = false;                   // whether to use gpu for inference
        public int deviceId = 0;                      // GPU device id, ignore if use_gpu == false
        public AlgoInitInput()
        {

        }
    }

    public struct DefectLimit
    {
        public int Maxval;
        public int Minval;

    }

    public class  AlgoRunInput
    {
        public Mat SourceImg { get; set; } = null;
        public Dictionary<string, DefectLimit> DicDefect { get; set; } = null;

        public bool ShowMask { get; set; } = false;

        public AlgoRunInput() { }
    }

    public class  AlgoRunOutput
    {
        public Mat mask = null;
        public Dictionary<string, ArrayList> Dicdefect = new Dictionary<string, ArrayList>();
    }

}
