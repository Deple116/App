using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreVision.HardwareControl
{
    public class ImageType
    {
        //显示图片
        public struct ShowImage
        {
            public Mat picture;
        }

        //存储图片
        public struct SaveImage
        {
            public Mat picture;
            public Mat mask;
            public bool result;
            public string stationName;
            public string ProductModel;
            public string time;
        }

        //相机回调
        public struct CameraImageCallPack
        {
            public Mat picture;
            public string stationName;
        }
    }
}
