using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconAlgoCtrlLib
{
   public class HalcoImgProc
    {

        private HDevEngine MyEngine = new HDevEngine();

        HImage hImage = new HImage();
        HImage hImageX = new HImage();
        HImage hImageY = new HImage();
        HImage hImageZ = new HImage();
        public HRegion hRegion = new HRegion();
        HRegion FinRegion = new HRegion();

        HObject dupImg = new HObject();

        HDevengineClass m_HDevengineClass;
        HTuple ImageFiles;
        HObject obj;

        #region 延锋3D

        public void yfInit()
        {
            m_HDevengineClass = new HDevengineClass();

            HOperatorSet.SetSystem("clip_region", "false");
            hRegion.ReadObject(Application.StartupPath + "\\Algo\\Halcon\\settings\\yf\\CircleTest.hobj");

            m_HDevengineClass.Init(Application.StartupPath + "\\Algo\\Halcon\\Main\\yf_test.hdev",
                                   Application.StartupPath + "\\Algo\\Halcon\\ThirdParty\\yf", 
                                   new HTuple("ReadImg", "yf_get_sort_regoion", "Preprocess", "yf_get_xyz_value", "yf_grap_3d_image"));
           // m_ListBox.AddInfo("初始化成功");
        }
        public List<string> yfReadImgFolder()
        {
            //yfcmb.Items.Clear();
            //HTuple ImageFiles;

            List<string> m_list = new List<string>();

            m_HDevengineClass.SetTup(0, "ImagePath", Application.StartupPath + "\\Algo\\Halcon\\Images\\yf");
            m_HDevengineClass.Excute(0);
            ImageFiles = m_HDevengineClass.GetTup(0, "ImageFiles");

            int inum = ImageFiles.Length;
            for (int i = 0; i < inum; i++)
            {
                m_list.Add(ImageFiles[i]);
                //yfcmb.Items.Add(i);
            }
            //yfcmb.SelectedIndex = 0;
            //m_ListBox.AddInfo("加载成功");
            return m_list;
        }
        public HObject yfGetImg(int index)
        {
            HObject objTemp;
            HOperatorSet.ReadImage(out objTemp, (HTuple)ImageFiles[index]);
            hImage.ReadImage((HTuple)ImageFiles[index]);
            return hImage;
        }
        public Dictionary<string,string> yfExcute(int index)
        {
            Dictionary<string, string> dictemp = new Dictionary<string, string>();
            try
            {            
                //  int index = yfcmb.SelectedIndex;

                hImage.ReadImage((HTuple)ImageFiles[index-1]);
                m_HDevengineClass.SetObj(2, "HeightImg", hImage);
                m_HDevengineClass.Excute(2);
                hImageX = m_HDevengineClass.GetImg(2, "ImageX");
                hImageY = m_HDevengineClass.GetImg(2, "ImageY");
                hImageZ = m_HDevengineClass.GetImg(2, "ImageZ");


                hImage.ReadImage((HTuple)ImageFiles[index]);
                m_HDevengineClass.SetObj(3, "ImageGray", hImage);
                m_HDevengineClass.SetObj(3, "ImageX", hImageX);
                m_HDevengineClass.SetObj(3, "ImageY", hImageY);
                m_HDevengineClass.SetObj(3, "ImageZ", hImageZ);
                m_HDevengineClass.SetObj(3, "RegionPoint", hRegion);
                m_HDevengineClass.Excute(3);


                dupImg = m_HDevengineClass.GetObj(3, "MaskImg");
                HTuple xTuples = m_HDevengineClass.GetTup(3, "x_tuples");
                HTuple yTuples = m_HDevengineClass.GetTup(3, "y_tuples");
                HTuple zTuples = m_HDevengineClass.GetTup(3, "z_tuples");




                //Debug.WriteLine($"xtuples;{GetValArr(xTuples.ToFArr().ToList())}");
                //Debug.WriteLine($"ytuples;{GetValArr(yTuples.ToFArr().ToList())}");
                //Debug.WriteLine($"ztuples;{GetValArr(zTuples.ToFArr().ToList())}");

                dictemp.Add("xval", GetValArr(xTuples.ToFArr().ToList()));
                dictemp.Add("yval", GetValArr(yTuples.ToFArr().ToList()));
                dictemp.Add("zval", GetValArr(zTuples.ToFArr().ToList()));

                // m_ListBox.AddInfo("执行成功");
                return dictemp;
            }
            catch (System.Exception ex)
            {
                return dictemp;
                //m_ListBox.AddInfo(ex.ToString());
            }
        }

        public HObject GetMaskImg()
        {
            return dupImg;
        }
        private string GetValArr<T>(List<T> list)
        {
            string xval = "";
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())//判断是否是最后一个元素
            {
                xval += enumerator.Current + ";";
            }
            return xval;
        }

        #endregion

        #region 调试

        public void StartDebug()
        {
            MyEngine.SetEngineAttribute("execute_procedures_jit_compiled", "false");

            // Set debug parameters
            MyEngine.SetEngineAttribute("debug_port", 57786);
            // MyEngine.SetEngineAttribute("debug_password", "1234");
            //MyEngine.SetEngineAttribute("debug_wait_for_connection","true");

            // Start debug server
            MyEngine.StartDebugServer();
        }
        #endregion
    }
}
