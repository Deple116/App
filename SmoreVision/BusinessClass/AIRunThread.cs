using AlgoControlLibrary.AlgoBaseFactory;
using AlgoControlLibrary.VimoAlgo;
using CameraControlLibrary;
using IOControlLibrary.TAS;
using OpenCvSharp;
using SMLogControlLibrary;
using SmoreControlLibrary;
using SmoreControlLibrary.SMData;
using SmoreControlLibrary.SMImage;
using SmoreVision.HardwareControl;
using SmoreVision.HardwareControlClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmoreVision.HardwareControl.ImageType;

namespace SmoreVision.BusinessClass
{
    public delegate void ShowImage(Mat mat);
    public delegate void ShowResult(bool _result);

    public class AIRunThread
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        public bool Cycled = false;

        private string LastError = "";

        private Task m_AIThreadProcess;

        public ShowImage m_ShowImage = null;
        public ShowResult m_ShowResult = null;

        private CameraInterface m_CameraControl;

        private SaveImageThread m_SaveImageThread;

        private VimoManager m_AISDKManage;

        private SiemensPLCControl m_SiemensPLCControl;
        TasControl iOControlClass;

        int returnValue = 0;

        const int ERR_OK = 0;

        const int ERR_FAILED = -1;

        public AIRunThread(CameraInterface _CameraControl, SaveImageThread _saveImageThread, VimoManager _aISDKManage, SiemensPLCControl _siemensPLCControl, TasControl _iOControlClass)
        {
            iOControlClass = _iOControlClass;
            m_CameraControl = _CameraControl;
            m_SaveImageThread = _saveImageThread;
            m_AISDKManage = _aISDKManage;
            m_SiemensPLCControl = _siemensPLCControl;
        }

        public int StartThread()
        {
            if (m_AIThreadProcess == null || m_AIThreadProcess.IsCompleted == true)
            {
                m_AIThreadProcess = Task.Factory.StartNew(() => ThreadProcedureProcess());
                SMLogWindow.OutLog("AI推理线程开启.", Color.Green);
            }
            return ERROR_OK;
        }

        public int ThreadProcedureProcess()
        {
            try
            {
                int returnValue = -1;

                CameraControlLibrary.CameraImageCallPack cameraImageCallPack;
                Cycled = true;
                while (Cycled)
                {
                    if (m_CameraControl.IsEmpty())
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    //Console.WriteLine("相机触发成功.");
                    bool result = true;
                    //string _result = "";
                    string line3_result = "";
                    cameraImageCallPack = new CameraControlLibrary.CameraImageCallPack();

                    cameraImageCallPack = m_CameraControl.TryDequeue();

                    SMLogWindow.OutLog($"收到{m_CameraControl.CCDName}图片", Color.Green);
                    switch (m_CameraControl.CCDName)
                    {
                        case "CCD1":
                            {

                                AlgoProcess(cameraImageCallPack.picture.Clone());
                            }
                            break;
                    }

                    //SMLogWindow.OutLog("AI 推理完成", Color.Green);

                    Thread.Sleep(10);
                }
                SMLogWindow.OutLog("AI推理线程结束.", Color.Green);
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }

        private void AlgoProcess(Mat mat)
        {
            SaveImage m_SaveImage = new SaveImage();
            //算法Run之前参数输入
            Dictionary<string, DefectLimit> dicdefect = new Dictionary<string, DefectLimit>();

            foreach (var temp in GlobalVariables.Variables.dicProduct)
            {
                dicdefect.Add(temp.Key, new DefectLimit() { Minval = int.Parse(temp.Value[0]), Maxval = int.Parse(temp.Value[1]) });
            }


            AlgoRunInput algoRunInput = new AlgoRunInput() { SourceImg = mat.Clone(), DicDefect = dicdefect, ShowMask = true };

            SMLogWindow.OutLog($"bAlgo:{GlobalVariables.Variables.bAlgo}", Color.Green);

            if (GlobalVariables.Variables.bAlgo)
            {
                returnValue = (int)m_AISDKManage.Run(algoRunInput, out AlgoRunOutput algorunOutput);
                SMLogWindow.OutLog($"algoback:{returnValue}", Color.Green);
                if (returnValue == ERR_FAILED)
                {
                    SMLogWindow.OutLog("算法推理失败.", Color.Red, bshow: true);
                    //存图
                    m_SaveImage.stationName = "CCD1";
                    m_SaveImage.picture = mat;
                    m_SaveImage.result = true;
                    m_SaveImage.mask = mat;
                    m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                    PLCResult(false);
                }
                else
                {
                    SMLogWindow.OutLog($"算法推理成功:count:{algorunOutput.Dicdefect.Count}", Color.Green);
                    // if (smImageWindow1.InvokeRequired)
                    {
                        SMLogWindow.OutLog($"defectcount:{algorunOutput.Dicdefect.Count}", Color.Green);
                        if (algorunOutput.Dicdefect.Count < 1)
                        {
                            //BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), true);
                            //SMDataWindow.AddData(true);
                            //smImageWindow1.ImageShow(algorunOutput.mask);

                            m_ShowImage(mat.Clone());
                            m_ShowResult(true);
                            SMDataWindow.AddData(true);


                            //存图
                            m_SaveImage.stationName = "CCD1";
                            m_SaveImage.picture = mat;
                            m_SaveImage.result = true;
                            m_SaveImage.mask = algorunOutput.mask;
                            m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                            //m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);

                            PLCResult(true);

                        }
                        else
                        {
                            m_ShowImage(algorunOutput.mask.Clone());
                            m_ShowResult(false);
                            SMDataWindow.AddData(false);
                            //存图
                            m_SaveImage.stationName = "CCD1";
                            m_SaveImage.picture = mat;
                            m_SaveImage.result = false;
                            m_SaveImage.mask = algorunOutput.mask;
                            m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                            // m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);

                            PLCResult(false);
                        }
                    }

                }
            }
            else
            {
                m_ShowImage(mat);
                m_ShowResult(true);
                SMDataWindow.AddData(true);

                //存图
                m_SaveImage.stationName = "CCD1";
                m_SaveImage.picture = mat;
                m_SaveImage.result = true;
                m_SaveImage.mask = mat;
                m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                PLCResult(true);
            }



            m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
        }

        private void PLCResult(bool bAlgoRes)
        {
            string plcResult = GlobalVariables.Variables.PlcResult;

            SMLogWindow.OutLog($"plcResult:{plcResult}:bAlgoRes:{bAlgoRes}", Color.Green);

           
            if (plcResult == "OK")
            {
                bAlgoRes = true;
            }
            else if (plcResult == "NG")
            {
                bAlgoRes = false;
            }

            if(bAlgoRes)
            {
                //ok
                iOControlClass.SendCmd(1, false);
                Thread.Sleep(50);
                //通道,状态
                iOControlClass.SendCmd(0, true);

                SMLogWindow.OutLog($"IOcontrol:middle", Color.Green);
                Thread.Sleep(500);
                iOControlClass.SendCmd(0, false);
                SMLogWindow.OutLog($"IOcontrol:end", Color.Green);
            }
            else
            {
                //ng
                iOControlClass.SendCmd(0, false);
                Thread.Sleep(50);
                //通道,状态
                iOControlClass.SendCmd(1, true);

                SMLogWindow.OutLog($"IOcontrol:middle", Color.Green);
                Thread.Sleep(500);
                iOControlClass.SendCmd(1, false);
                SMLogWindow.OutLog($"IOcontrol:end", Color.Green);
            }
            
        }
    }
}
