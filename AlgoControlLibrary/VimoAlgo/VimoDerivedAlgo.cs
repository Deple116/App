using AlgoControlLibrary.AlgoBaseFactory;
using OpenCvSharp;
using SmartMore.ViMo;
using SMLogControlLibrary;
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

namespace AlgoControlLibrary.VimoAlgo
{
    public class VimoDerivedAlgo : IAlgo
    {

        #region Field
        Solution solution = new Solution();// create an empty solution
        
        IPipelines pipelines=null;
       
        #endregion

        public VimoDerivedAlgo() { 
        
        }

        #region property

        public string Name { get; set; }

        public string MoudleID { get; set; }
        #endregion


        #region Interface
        public EnumReturnVal Init(AlgoInitInput algoinput)
        {
            try
            {
               
                solution.LoadFromFile(algoinput.modelPath);  // load solution from model.vimosln

                pipelines = solution.CreatePipelines(algoinput.moduleId, algoinput.useGpu, algoinput.deviceId); // create pipelines with module id

                MoudleID = algoinput.moduleId;
                return EnumReturnVal.Return_OK;
            }
            catch(Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Red,bshow:true);
                return EnumReturnVal.Return_Fail;
            }
        }

        public EnumReturnVal Run<T>(AlgoRunInput RunInput,out ResponseList<T> rsps) where T : SmartMore.ViMo.Response
        {
            //SMLogWindow.OutLog($"init:start", Color.Green);          
            try
            {
                rsps = RunProcess<T>(RunInput);
                return EnumReturnVal.Return_OK;
            }
            catch(Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green,loglevel: SMLogControlLibrary.LogLevel.Error);
                rsps = null;
                return EnumReturnVal.Return_Fail;
            }
        }

        public EnumReturnVal Free()
        {
            try
            {
                pipelines.Dispose();
                solution.Dispose();
                return EnumReturnVal.Return_OK;
            }
            catch
            {

                return EnumReturnVal.Return_Fail;
            }
        }

        #endregion


        #region Method
        
        private ResponseList<T> RunProcess<T>(AlgoRunInput RunInput) where T : SmartMore.ViMo.Response
        {
            Request req = new Request(RunInput.SourceImg.Clone());    // create request from image

            ResponseList<T> _rsps = null;
            switch (solution.GetModuleInfo(MoudleID).Type)  // get last module type
            {
                case ModuleType.Classification:
                    {
                        SMLogWindow.OutLog("start classification inference", Color.Green);
                        pipelines.Run(req, out ResponseList<ClassificationResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.Detection:
                    {
                        SMLogWindow.OutLog("start detection inference", Color.Green);
                        pipelines.Run(req, out ResponseList<DetectionResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.Segmentation:
                    {
                        SMLogWindow.OutLog("start segmentation inference",Color.Green);
                        pipelines.Run(req, out ResponseList<SegmentationResponse> rsps); // do inference
                        //Mat handleImg = req.Image;
                        //ConvertImageData(rsps, ref handleImg);
                        //Cv2.ImWrite("20230802.png", handleImg.Clone());
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.Ocr:
                    {
                        SMLogWindow.OutLog("start ocr inference", Color.Green);
                        pipelines.Run(req, out ResponseList<OcrResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.SequenceOcr:
                    {
                        SMLogWindow.OutLog("start sequence ocr inference", Color.Green);
                        pipelines.Run(req, out ResponseList<OcrResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.MultiRoi:
                    {
                        SMLogWindow.OutLog("start multi roi inference", Color.Green);
                        pipelines.Run(req, out ResponseList<MultiRoiResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.UAD:
                    {
                        SMLogWindow.OutLog("start uad inference", Color.Green);
                        pipelines.Run(req, out ResponseList<UADResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                case ModuleType.DetRotation:
                    {
                        SMLogWindow.OutLog("start det rotation inference", Color.Green);
                        pipelines.Run(req, out ResponseList<DetRotationResponse> rsps); // do inference
                        _rsps = rsps as ResponseList<T>;
                        break;
                    }
                default:
                    break;
            }

            return _rsps;
        }

        #endregion
    }
}
