using JsonHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using SmartMore;
using SmoreControlLibrary;
using SmoreControlLibrary.SMLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindGoes6.Data;

namespace SmoreVision.FunctionClass
{
    public struct AlgoInput
    {
        public Mat mat;
        public string imgName;
        public string productContent;
        public string productModel;
    }

    public struct AlgoOutput
    {
        public Mat mask;
        public string resJson;
        public bool bRes;
    }


    public class AISDKManage
    {
        private const int ERROR_OK = 0;
        private const int ERROR_NG = 1;
        private const int ERROR_FAILED = -1;

        public string ErrorInfo = "";

        private string configPath = "";
        private string typeName = "";
        private uint solutionId = 0;
        private bool useGpu = false;
        private uint deviceId = 0;
        private string clsResult = "";
        private string ocrResult = "";

        //private ISeriesModule CLSModule = null;
        // private ISeriesModule OCRModule = null;
        private TSTMModule Tstmmodule = null; 

        private XMLConfigParse m_XMLConfigParse;


        //JSONInput_class

        private InputRootobject JsonInputRoot;

        public AISDKManage(ref XMLConfigParse _xMLConfigParse)
        {
            m_XMLConfigParse = _xMLConfigParse;
            

            //string jsonText = Json.GetJsonFile(Application.StartupPath + "\\input.json");
            //JsonInputRoot = JsonConvert.DeserializeObject<InputRootobject>(jsonText);
        }

        ~AISDKManage()
        {
            if (Tstmmodule != null) Tstmmodule.Destroy();

        }

        private int CreateModule()
        {
            try
            {
                for (int i = 0; i < m_XMLConfigParse.SDK.Items.Length; i++)
                {
                    if (!File.Exists(m_XMLConfigParse.SDK.Items[i].ConfigPath))
                    {
                        MessageBox.Show($"{m_XMLConfigParse.SDK.Items[i].Name}找不到SDK的配置文件,文件位置:{m_XMLConfigParse.SDK.Items[i].ConfigPath}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ERROR_FAILED;
                    }
                }

                //configPath = m_XMLConfigParse.SDK.Items[0].ConfigPath;
                //CLSModule = new SeriesModule(configPath);

                //configPath = m_XMLConfigParse.SDK.Items[1].ConfigPath;
                //OCRModule = new SeriesModule(configPath);



                return ERROR_OK;
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                MessageBox.Show($"创建Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }

        public int Init()
        {
            try
            {
                Tstmmodule = new TSTMModule();
                CreateModule();

                var ret = Tstmmodule.Init(m_XMLConfigParse.SDK.Items[0].ConfigPath);
                return ret == 0 ? ERROR_OK : ERROR_FAILED;

                typeName = m_XMLConfigParse.SDK.Items[0].TypeName;
                solutionId = m_XMLConfigParse.SDK.Items[0].SolutionId;
                useGpu = m_XMLConfigParse.SDK.Items[0].GPU;
                deviceId = m_XMLConfigParse.SDK.Items[0].DeviceID;
                //CLSModule.Init(typeName, solutionId, useGpu, deviceId);

                Log.Add("CLS算法模型初始化成功.", Color.Green);

                typeName = m_XMLConfigParse.SDK.Items[1].TypeName;
                solutionId = m_XMLConfigParse.SDK.Items[1].SolutionId;
                useGpu = m_XMLConfigParse.SDK.Items[1].GPU;
                deviceId = m_XMLConfigParse.SDK.Items[1].DeviceID;
                //OCRModule.Init(typeName, solutionId, useGpu, deviceId);

                Log.Add("OCR算法模型初始化成功.", Color.Green);

                return ERROR_OK;
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                MessageBox.Show($"初始化Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }

        public int Run(Mat _img, string imgname = "123")
        {
            try
            {
                TSTMRequest req = new TSTMRequest();
                TSTMResponse rsp = new TSTMResponse();

                req.image_path = imgname;// "./py_resources/image/img1.png";
                //Mat image = new Mat(req.image_path);
                req.input_image = _img.Clone();
                req.config_path = "./py_resources/input.json";
                req.save_json = false;


                int ret = -1;
                //是否运行算法
                if (m_XMLConfigParse.SDK.Items[0].RunEnable)
                {
                    ret = Tstmmodule.Run(ref req, ref rsp);
                }
                else
                {
                    ret = 0;
                }


                return ret == 0 ? ERROR_OK : ERROR_FAILED;

                #region old
                //clsResult = "";
                //ocrResult = "";
                //var request = new Request(_img);
                //var responses = _seriesModule.Run(request);
                //if (responses.Count() > 0)
                //{
                //    var i = 0;
                //    foreach (var response in responses)
                //    {
                //        Console.WriteLine($"Index: {i++}");
                //        switch (response)
                //        {
                //            case IDetectionResponse rsp:
                //                PrintDetectionResponse(rsp);
                //                break;
                //            case IClassificationResponse rsp:
                //                PrintClassificationResponse(rsp);
                //                break;
                //            case IOcrResponse rsp:
                //                PrintOcrResponse(rsp);
                //                break;
                //            case ISegmentationResponse rsp:
                //                PrintSegmentationResponse(rsp);
                //                break;
                //        }
                //        Console.WriteLine();
                //    }
                //    Console.WriteLine("Done");
                //    return ERROR_OK;
                //}
                //else
                //{
                //    Console.WriteLine("responses<0");
                //    SMLogWindow.OutLog("responses < 0,该工件上不包含检测内容.", Color.Red);
                //    return ERROR_FAILED;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                MessageBox.Show($"运行Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }

        public Bitmap MatToBitmap(Mat dst)
        {
            return new Bitmap(dst.Cols, dst.Rows, (int)dst.Step(), PixelFormat.Format24bppRgb, dst.Data);
        }

        #region Print
        //private void PrintDetectionResponse(IDetectionResponse response)
        //{
        //    foreach (var box in response.BoxList)
        //    {
        //        Console.WriteLine($"Id: {box.Id}, Name: {box.Name}, Score: {box.Score}, Rect: {box.Rect}");
        //    }
        //}

        //private void PrintClassificationResponse(IClassificationResponse response)
        //{
        //    clsResult = "";
        //    foreach (var label in response.Labels)
        //    {
        //        Console.WriteLine($"Id: {label.Id}, Name: {label.Name}, Score: {label.Score}");
        //        clsResult += $"Id: {label.Id}, Name: {label.Name}, Score: {label.Score}" + "\r\n";
        //    }
        //}

        //private void PrintOcrResponse(IOcrResponse response)
        //{
        //    SDKExtendClass.IOcrResponse.m_TextPoint.Clear();
        //    SDKExtendClass.IOcrResponse.m_TextDic.Clear();
        //    SDKExtendClass.IOcrResponse.ListPoints.Clear();
        //    SDKExtendClass.IOcrResponse.TextList.Clear();
        //    IList<OpenCvSharp.Point> numbericList = new List<OpenCvSharp.Point>();
        //    foreach (var block in response.Blocks)
        //    {
        //        Console.Write($"Text: {block.Text}, Score: {block.Score}, Polygon:");
        //        foreach (var poly in block.Polygon)
        //        {
        //            Console.Write($"{poly}, ");
        //            numbericList.Add(SDKExtendClass.IOcrResponse.GetPoint(poly.ToString()));
        //        }
        //        if(!SDKExtendClass.IOcrResponse.m_TextDic.ContainsKey(block.Polygon.First()))
        //        {
        //            SDKExtendClass.IOcrResponse.ListPoints.Add(numbericList);
        //            SDKExtendClass.IOcrResponse.TextList.Add(block.Text);
        //            SDKExtendClass.IOcrResponse.m_TextPoint.Add(block.Polygon.First());
        //            SDKExtendClass.IOcrResponse.m_TextDic.Add(block.Polygon.First(), block.Text);
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //private void PrintSegmentationResponse(ISegmentationResponse response)
        //{
        //    Console.WriteLine($"Rows: {response.Mask.Rows}, Cols: {response.Mask.Cols}, Type: {response.Mask.Type()}");
        //    foreach (var kv in response.Names)
        //    {
        //        Console.WriteLine($"Key: {kv.Key}, Value: {kv.Value}");
        //    }
        //}

        #endregion

        /// <summary>
        /// 分类算法推理
        /// </summary>
        /// <param name="_mat"></param>
        /// <returns></returns>
        //public int CLSRun(Mat _mat,ref string _clsResult)
        //{
        //    try
        //    {
        //        _clsResult = "";
        //        int retrunValue = Run(CLSModule, _mat);
        //        if (retrunValue != 0)
        //        {
        //            return ERROR_FAILED;
        //        }
        //        else
        //        {
        //            _clsResult = clsResult;
        //            return ERROR_OK;
        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorInfo = ex.ToString();
        //        MessageBox.Show($"运行Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return ERROR_FAILED;
        //    }
        //}

        /// <summary>
        /// OCR 算法推理
        /// </summary>
        /// <param name="_mat"></param>
        /// <returns></returns>
        //public int OCRRun(Mat _mat,ref string _ocrResult, ref string _Line3Result)
        //{
        //    try
        //    {
        //        _ocrResult = "";
        //        int retrunValue = Run(OCRModule, _mat);
        //        if (retrunValue != 0)
        //        {
        //            return ERROR_FAILED;
        //        }
        //        else
        //        {
        //            SDKExtendClass.IOcrResponse.PointXYSort(SDKExtendClass.IOcrResponse.m_TextPoint, ref _ocrResult, ref _Line3Result); //输出排序后的字符串

        //            _Line3Result = _Line3Result.Replace("-",string.Empty);

        //            if (_Line3Result.Length > 9)
        //            {
        //                _Line3Result = _Line3Result.Substring(0, 9);
        //            }

        //            return ERROR_OK;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorInfo = ex.ToString();
        //        //MessageBox.Show($"运行Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return ERROR_FAILED;
        //    }
        //}


        // <summary>
        // 算法
        // </summary>
        // <param name = "_mat" ></ param >
        //< returns ></ returns >
        public int TSTMRun(AlgoInput algoInput, out AlgoOutput algoOutput)
        {
            try
            {
                SMLogWindow.OutLog($"algo::{GlobalVariables.Variables.bAlgo}", Color.Green);
                //是否运行算法
                if (GlobalVariables.Variables.bAlgo)
                {
                    TSTMRequest req = new TSTMRequest();
                    TSTMResponse rsp = new TSTMResponse();

                    req.image_path = algoInput.imgName;// "./py_resources/image/img1.png";
                                                       //Mat image = new Mat(req.image_path);

                    Mat mat = algoInput.mat.Clone();
                   //if(algoInput.mat.Channels()==1) Cv2.CvtColor(algoInput.mat, mat, ColorConversionCodes.GRAY2RGB);
                    req.input_image = mat.Clone();
                    //req.config_path = "./py_resources/input.json";

                    //产品型号
                    algoInput.productModel = m_XMLConfigParse.Device.Items[0].ProductModel;
                    //产品类别
                    SMLogWindow.OutLog($"algo:Number:{m_XMLConfigParse.Device.Items[0].EquipmentNumber}",Color.Green);
                    List<string> m_list = CSV.Instance[1].Data[int.Parse(m_XMLConfigParse.Device.Items[0].EquipmentNumber)];

                    //是否手动设置日期

                    string strDate = "";

                    if (GlobalVariables.Variables.bManual)
                    {
                        if(GlobalVariables.Variables.dateMsg.Length<2) strDate = DateTime.Now.ToString("MMdd");
                        strDate = GlobalVariables.Variables.dateMsg[0] + GlobalVariables.Variables.dateMsg[1];
                    }
                    else
                    {
                        strDate = DateTime.Now.ToString("MMdd");
                    }

                    string resText = "";
                    if (m_list.Count>=3)
                    {
                        resText = m_list[2] + m_list[1] + strDate + m_list[3];
                    }

                    // string resText = m_list[2] + m_list[1] + "0407" + m_list[3];

                    SMLogWindow.OutLog($"algo:resText:{resText}", Color.Green);
                    algoInput.productContent = resText;


                    JsonInputRoot.product_type = algoInput.productModel; //产品型号
                    JsonInputRoot.product_string = algoInput.productContent;//刻字信息
                    JsonInputRoot.only_check_normal_char = GlobalVariables.Variables.bSpecial;

                    if (GlobalVariables.Variables.dicProduct.ContainsKey(algoInput.productModel))//内圈，外圈半径
                    {
                        JsonInputRoot.convex_radius_scaling_ratio = new float[] {float.Parse(GlobalVariables.Variables.dicProduct[algoInput.productModel][0]), float.Parse(GlobalVariables.Variables.dicProduct[algoInput.productModel][1]) };
                    }
                    else
                    {
                        SMLogWindow.OutLog($"dicProduct:{algoInput.productModel}", Color.Green);
                        JsonInputRoot.convex_radius_scaling_ratio = new float[] {1.0F,1.0F};
                    }

                    string jsonContent = JsonConvert.SerializeObject(JsonInputRoot);
                    req.config_path = jsonContent;
                    SMLogWindow.OutLog($"algoInput::{req.config_path}", Color.Green);
                   // Console.WriteLine($"Input.config_path::{req.config_path}");
                    req.save_json = false;

                    int ret = -1;

                    //保存当前运行图片
                    Cv2.ImWrite("ori.bmp", algoInput.mat.Clone());
                    ret = Tstmmodule.Run(ref req, ref rsp);
                    if (ret != 0)
                    {
                        algoOutput.resJson = @"{}";
                        algoOutput.mask = algoInput.mat;

                        algoOutput.bRes = false;
                        return ERROR_FAILED;
                    }

                    algoOutput.resJson = rsp.result;
                    algoOutput.mask = rsp.output_image;

                    Rootobject root = JsonConvert.DeserializeObject<Rootobject>(algoOutput.resJson);
                    // Console.WriteLine($"Output::{ rsp.result}");
                    SMLogWindow.OutLog($"algoOutput::{rsp.result}", Color.Green);

                   

                    //保存json
                    JsonHelper.Json.WriteJsonFile("Res.json", algoOutput.resJson);
                    // Cv2.ImWrite("mask.bmp", rsp.output_image.Clone());
                    //判断缺陷结果
                    if (root == null)
                    {
                         algoOutput.bRes = false;
                    }
                    else
                    {
                        if(root.defect==null)
                        {
                            algoOutput.bRes = false;
                        }
                        else
                        {
                            if (root.defect.Length != 0)
                            {
                                algoOutput.bRes = false;//NG
                            }
                            else
                            {
                                algoOutput.bRes = true;//OK
                            }
                        }
                        
                    }
                    
                }
                else
                {
                    Thread.Sleep(30);

                    algoOutput.resJson = @"{}";
                    algoOutput.mask = algoInput.mat.Clone();
                    algoOutput.bRes = true;
                }



                return ERROR_OK;

                //int retrunValue = Run(CLSModule, _mat);
                //if (retrunValue != 0)
                //{
                //    return ERROR_FAILED;
                //}
                //else
                //{
                //  //  _clsResult = clsResult;
                //    return ERROR_OK;
                //}
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                MessageBox.Show($"运行Module失败,ErrCode:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                algoOutput.resJson = @"{}";
                algoOutput.mask = algoInput.mat;
                algoOutput.bRes = false;
                return ERROR_FAILED;
            }
        }


    }
}
