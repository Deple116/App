using AlgoControlLibrary.AlgoBaseFactory;
using AlgoControlLibrary.VimoAlgo;
using CameraControlLibrary;
using CameraControlLibrary.CameraDahua;
using CameraControlLibrary.CameraHIK;
using CameraControlLibrary.DalsaLine;
using FileControlLibrary;
using HalconAlgoCtrlLib;
using HalconDotNet;
using IOControlLibrary.TAS;
using Newtonsoft.Json;
using OpenCvSharp;
using SMLogControlLibrary;
using SmoreControlLibrary;
using SmoreControlLibrary.EquipmentDriver;
using SmoreControlLibrary.ProductStatistics;
using SmoreControlLibrary.SMData;
using SmoreControlLibrary.SMForm;
using SmoreControlLibrary.SMImage;
using SmoreControlLibrary.SMInfo;
using SmoreControlLibrary.SystemConfig;
using SmoreVision.BusinessClass;
using SmoreVision.CommClass;
using SmoreVision.HardwareControl;
using SmoreVision.HardwareControlClass;
using SmoreVision.Ini;
using SmoreVisionOffline.Folder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindGoes6.Data;
using static SmoreControlLibrary.SystemConfig.JsonFileParse;
using static SmoreVision.HardwareControl.ImageType;

namespace SmoreVision
{
    public partial class FormMain : FormMainBase
    {
        public const int ERROR_OK = 0;
        public const int ERROR_FAILED = -1;

        private const string CONFIG_FILE_TYPE = "xml";
        //private bool BtnRunState = false;
        private bool BtnOK = false;
        private bool BtnOCRState = false;
        private bool BtnSpecialOCRState = false;

        private XMLConfigParse XMLConfig = new XMLConfigParse();
        private string ConfigFilePath = "";
        private string ErrorInfo = "";
        private int returnValue = 0;

        private SMImageWindow[] m_SMImageWindows = new SMImageWindow[GlobalVariables.GConst.IMAGE_WINDOW_COUNT];
        private CameraInterface[] m_CameraControl;
        private ActionRunThread[] m_ActionRunThread;
        private ConnectHeartbeatThread[] m_ConnectHeartbeatThreads;
        private AIRunThread[] m_AIRunThread=null;
        private VimoManager m_AISDKManage;
        private SaveImageThread m_SaveImageThread;
        private SiemensPLCControl m_SiemensPLCControl;

        private Task SimplexTask = null;
        private Task SimplexTrigerTask = null;
        private Task MultipleTask = null;
        private bool MultipleTaskRun = false;

        public static bool bJson = true;
        bool bSetUpdate = true;

        private SaveImage m_SaveImage;

        private FormProductNumber form_productnumber = new FormProductNumber();//数据统计界面(图表)
        private FormProductInfo form_productinfo = new FormProductInfo();//数据统计界面(数据库)
        private ShowData smShowData=new ShowData();

        TasControl iOControlClass = new TasControl();

        HalcoImgProc m_halconImgProc = new HalcoImgProc();

        private ConcurrentQueue<string[]> dataqueue = new ConcurrentQueue<string[]>();

        private ManualResetEvent m_dataevent = new ManualResetEvent(false);

        public FormMain()
        {
            InitializeComponent();
            InitialControlArray();

            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyDescriptionAttribute asmDesc = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute));
            AssemblyCopyrightAttribute asmCpr = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            AssemblyCompanyAttribute asmCpn = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            AssemblyConfigurationAttribute asmConfig = (AssemblyConfigurationAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyConfigurationAttribute));

            InitTest();

            //读取SmoreVisionConfig.xml文件信息
            ConfigFilePath = Path.GetFullPath($"{asmConfig.Configuration}.{CONFIG_FILE_TYPE}");
            if (InitialConfigFile() != ERR_OK)
            {
                Log.Add("加载配置文件失败", Color.Green, bshow: true);
                SMFormWelcom.LoadingMsg("加载配置文件失败", 5);
                return;
            }

            Log.Add("加载配置文件成功", Color.Green, bshow: true);
            SMFormWelcom.LoadingMsg("加载配置文件成功", 5);

            GlobalVariables.Variables.bAlgo = XMLConfig.SDK.Items[0].RunEnable;

            if (XMLConfig.System.FullScreen)
            {
                this.WinState = FormWindowState.Maximized;
            }
            else
            {
                this.WinState = FormWindowState.Normal;
            }
            //项目名称
            this.ProjectName = XMLConfig.System.ProjectName;
        }

        ~FormMain()
        {

            bSetUpdate = false;
        }

        private List<FileInfo> GetFamilyFiles(FileSystemInfo familyPath)
        {
            List<FileInfo> familyFileList = new List<FileInfo>();
            //判断目录是否存在
            if (!familyPath.Exists) return null;
            DirectoryInfo dir = familyPath as DirectoryInfo;
            //不是目录则返回
            if (dir == null) return null;
            //将文件后缀为rfa的文件添加进列表
            familyFileList.AddRange(dir.GetFiles("*.json"));
            //递归遍历文件夹
            foreach (DirectoryInfo dinfo in dir.GetDirectories())
            {
                familyFileList.AddRange(GetFamilyFiles(dinfo));
            }

            return familyFileList;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_CameraControl = new CameraInterface[GlobalVariables.GConst.STATION_COUNT];
            m_ActionRunThread = new ActionRunThread[GlobalVariables.GConst.STATION_COUNT];
            m_ConnectHeartbeatThreads = new ConnectHeartbeatThread[GlobalVariables.GConst.STATION_COUNT];
            m_AIRunThread = new AIRunThread[GlobalVariables.GConst.STATION_COUNT];
            m_SaveImageThread = new SaveImageThread(XMLConfig);
            m_AISDKManage = new VimoSegmentManager();
            m_SiemensPLCControl = new SiemensPLCControl(ref XMLConfig);
            smImageWindow1.ShowManualButton = true;
            //smImageWindow2.ShowManualButton = true;
            smImageWindow1.InitData();
            //smImageWindow2.InitData();

            //m_SaveImageThread.StartThread(); //临时启动存图线程

            //string pathTemp = Application.StartupPath + "\\Formula";
            //List<FileInfo> m_fileinfo = GetFamilyFiles(new DirectoryInfo(pathTemp));

            //设备信息更新
            smInfoWindow1.Init();

            //初始化UI
            InitUI();

            //初始化算法
            InitAlgo();

            //更新数据显示
            CheckData();

            m_halconImgProc.StartDebug();

            ////算法初始化
            //returnValue = (int)m_AISDKManage.Init(new AlgoInitInput() { modelPath = XMLConfig.SDK.Items[0].ConfigPath, useGpu = XMLConfig.SDK.Items[0].GPU, moduleId = XMLConfig.SDK.Items[0].SolutionId.ToString() });
            //if (returnValue != ERROR_OK)
            //{
            //    Log.Add($"AI SDK 初始化失败!", Color.Red, bshow: true);
            //    SMFormWelcom.LoadingMsg("AI SDK 初始化失败", 50);
            //    // return;
            //}
            //else
            //{
            //    Log.Add($"AI SDK 初始化成功!", Color.Green, bshow: true);
            //    SMFormWelcom.LoadingMsg("AI SDK 初始化成功", 50);
            //}




            //相机初始化
            //InitCam();


            ////IO初始化
            //if(!iOControlClass.Init("192.168.0.80","10123"))
            //{
            //    Log.Add($"IO初始化初始化失败!", Color.Red, bshow: true);
            //    SMFormWelcom.LoadingMsg("IO初始化初始化失败", 90);
            //}
            //else
            //{
            //    Log.Add($"IO初始化初始化成功!", Color.Green, bshow: true);
            //    SMFormWelcom.LoadingMsg("IO初始化初始化成功", 90);
            //}

            ////产品型号
            ////Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Model\\productModel.csv");
            //CSV.Instance[0].Read(AppDomain.CurrentDomain.BaseDirectory + "\\Model\\productModel.csv");
            //int index=CSV.Instance[0].Data.FindIndex(r => r.Contains($"{XMLConfig.Device.Items[0].ProductModel}"));
            ////加载产品组别
            //if(index!=-1) CSV.Instance[1].Read(AppDomain.CurrentDomain.BaseDirectory + $"\\Content\\{CSV.Instance[0].Data[index][1]}.csv");

            ////产品组别
            //if (CSV.Instance[1].Data.Count < int.Parse(XMLConfig.Device.Items[0].EquipmentNumber))
            //{
            //    MessageBox.Show("超过产品组别最大数");
            //    SMLogWindow.OutLog($"超过产品组别最大数:Count:{CSV.Instance[1].Data.Count}:EquipmentNumber:{XMLConfig.Device.Items[0].EquipmentNumber}",Color.Red);
            //    return;
            //}
            //List<string> m_list = CSV.Instance[1].Data[int.Parse(XMLConfig.Device.Items[0].EquipmentNumber)];
            //string resText = m_list[2] + m_list[1] + DateTime.Now.ToString("MMdd") + m_list[3];
            //smInfoWindow1.EquipmentNumber = XMLConfig.Device.Items[0].EquipmentNumber + ";" + resText;




            //plc初始化
            //returnValue = m_SiemensPLCControl.Initial();
            //if (returnValue != ERROR_OK)
            //{
            //    Log.Add($"PLC初始化失败!", Color.Red);
            //    //return;
            //}
            //else
            //{
            //    Log.Add($"PLC初始化成功!", Color.Green);

            //    //获取当前型号
            //    byte data = m_SiemensPLCControl.ReadByte("DB89.67.0");
            //    string strSN = m_SiemensPLCControl.ReadString("DB89.68.0", (ushort)data);

            //    GlobalVariables.Variables.strCurrModel = strSN;
            //    Log.Add($"当前型号:{strSN}!", Color.Green);

            //    ProductRecive2(new ProductInfo() { Product_Model = strSN, Product_content = "change" });

            //    //获取当前组别
            //    ushort returnGroup = m_SiemensPLCControl.ReadUshort("DB89.64.0");
            //    SMLogWindow.OutLog($"当前组别:{returnGroup}!", Color.Green);
            //    ProductRecive1(new ProductInfo() { Product_Model = "change", Product_content = returnGroup.ToString() });

            //    //发送相机ready信号
            //    m_SiemensPLCControl.WriteBool("DB89.0.0", true);
            //}


            smInfoWindow1.AddConfig(XMLConfig);

            smButtonRun.Enabled = true;



            #region plc_ready
            ////CCD15 ready信号
            //returnValue = m_SiemensPLCControl.WriteBool("DB2000.0.1", true);
            //if (returnValue != ERROR_OK)
            //{
            //    Log.Add($"CCD15 PC->PLC Ready 信号失败!", Color.Red);
            //    return;
            //}
            //else
            //{
            //    Log.Add($"CCD15 PC->PLC Ready 信号成功!", Color.Green);
            //}

            ////CCD17 ready信号
            //returnValue = m_SiemensPLCControl.WriteBool("DB2000.8.1", true);
            //if (returnValue != ERROR_OK)
            //{
            //    Log.Add($"CCD17 PC->PLC Ready 信号失败!", Color.Red);
            //    return;
            //}
            //else
            //{
            //    Log.Add($"CCD17 PC->PLC Ready 信号成功!", Color.Green);
            //}

            #endregion


            Task.Run(() =>
            {
                while (bSetUpdate)
                {
                    if (bJson)//加载型号内圈，外圈信息
                    {
                        bJson = false;

                        if (InitialConfigFile() != ERR_OK)
                        {
                            Log.Add("加载配置文件失败", Color.Green, bshow: true);
                        }
                        LoadProductInfo();
                    }
                    Thread.Sleep(100);
                }
            });


            FileControl.DellogsDir(XMLConfig.SaveImage.Items[0].Path, XMLConfig.SaveTime.Items[0].SaveDays);
            SMFormWelcom.LoadingMsg("初始化成功", 100);

        }
        private void InitUI()
        {
            tabControl1.SelectedIndex = 2;
            //数据统计界面
            //图表
            form_productnumber.Dock = DockStyle.Fill;
            panelDataStatistics.Controls.Add(form_productnumber);
            form_productnumber.Show();
            //数据库
            form_productinfo.Dock = DockStyle.Fill;
            panelSql.Controls.Add(form_productinfo);
            form_productinfo.Show();

            //数据显示界面
            smShowData.Dock = DockStyle.Fill;
            panelDataShow.Controls.Add(smShowData);
            smShowData.Show();
            if (smShowData != null)
            {
                //smShowData.changeControl(GlobalVariables.Variable.uShowCount);
                smShowData.changeControl(111);
                tabControl1.SelectedIndex = 1;
            }

        }
        private void InitCam()
        {
            Dictionary<string, CamInfo> dicCam = new Dictionary<string, CamInfo>();
            dicCam.Add("CCD1", new CamInfo { camType = CamType.DahuaArea });
            dicCam.Add("CCD2", new CamInfo { camType = CamType.HikArea });
            //dicCam.Add("CCD3", new CamInfo{ camType = CamType.HikArea});

            //Init

            if (dicCam.Count == m_CameraControl.Length)
            {
                for (int i = 0; i < dicCam.Count; i++)
                {
                    switch (dicCam.Values.ElementAt(i).camType)
                    {
                        case CamType.HikArea:
                            m_CameraControl[i] = new HIKCameraControl(dicCam.Keys.ElementAt(i), dicCam.Values.ElementAt(i).camType.ToString());
                            break;
                        case CamType.DahuaArea:
                            m_CameraControl[i] = new DahuaCameraControl(dicCam.Keys.ElementAt(i), dicCam.Values.ElementAt(i).camType.ToString());
                            break;
                        case CamType.DalsaLine:
                            //string iniPath1 = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\split.ccf";
                            //string strCard = "Xtium-CL_MX4_1";
                            //string strCam = "CameraLink_1";
                            string iniPath1 = dicCam.Values.ElementAt(i).ccfPath;
                            string strCard = dicCam.Values.ElementAt(i).strCard;
                            string strCam = dicCam.Values.ElementAt(i).strCam;
                            m_CameraControl[i] = new class_CameraDalsaLine(iniPath1, strCard, strCam, dicCam.Keys.ElementAt(i), dicCam.Values.ElementAt(i).camType.ToString());
                            break;
                    }

                    if (m_CameraControl[i].CCDName == "CCD2") continue;
                    returnValue = m_CameraControl[i].Initial();
                    if (returnValue != ERROR_OK)
                    {
                        Log.Add($"{dicCam.Keys.ElementAt(i)}:初始化失败", Color.Red, bshow: true);
                        SMFormWelcom.LoadingMsg($"{dicCam.Keys.ElementAt(i)}:初始化失败", 80);
                        // Console.WriteLine($"{dicCam.Keys.ElementAt(i)}:初始化失败");
                        // return;
                    }
                    else
                    {
                        //m_CameraControl[i].SetSoftwareTriggerMode();
                        m_AIRunThread[i] = new AIRunThread(m_CameraControl[i], m_SaveImageThread, m_AISDKManage, m_SiemensPLCControl,iOControlClass);
                        Log.Add($"{dicCam.Keys.ElementAt(i)}:初始化成功", Color.Green, bshow: true);
                        SMFormWelcom.LoadingMsg($"{dicCam.Keys.ElementAt(i)}:初始化成功", 80);

                        //Console.WriteLine($"{dicCam.Keys.ElementAt(i)}:初始化成功");
                    }
                }
            }
            else
            {
                Log.Add($"dicCount:{dicCam.Count}:camLength:{m_CameraControl.Length}", Color.Red);
                //Console.WriteLine($"dicCount:{dicCam.Count}:camLength:{m_CameraControl.Length}");
            }
        }

        private void InitAlgo()
        {
            m_halconImgProc.yfInit();
        }

        #region  进度条初始化信息      
        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitTest()
        {
            while (!SMFormWelcom.frmLoadingOpen)
            {
                Thread.Sleep(10);
            }

            SMFormWelcom.LoadingMsg("正在加载界面....", 1);

            SMFormWelcom.LoadingMsg("开始初始化....", 10);
            //int i = 0;
            //while (i < 9)
            //{
            //    i++;
            //    SMFormWelcom.LoadingMsg("正在初始化串口" + i.ToString() + "....", 10 * i);
            //    Thread.Sleep(1000);

            //}


        }
        #endregion

        private void InitialControlArray()
        {
            try
            {
                int controlIndex = 0;
                foreach (Control control in tableLayoutPanelMainHome.Controls)
                {
                    if (control.Name.StartsWith("smImageWindow"))
                    {
                        controlIndex = Convert.ToInt32(control.Name.Substring(control.Name.Length - 1));
                        m_SMImageWindows[controlIndex] = (SMImageWindow)control;
                        m_SMImageWindows[controlIndex].Tag = controlIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private int InitialConfigFile()
        {
            int returnValue = XMLConfigParse.DeserializeFromXml<XMLConfigParse>(ConfigFilePath, ref XMLConfig, ref ErrorInfo);
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }

            returnValue = XMLConfig.GenerateNodeInfo();
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }
            return ERR_OK;
        }

        private void smButtonRun_BtnClick(object sender, EventArgs e)
        {
            if (!BtnRunState)
            {
                smButtonRun.BtnImage = global::SmoreVision.Properties.Resources.auto_on;
                smImageWindow1.ShowManualButton = false;
                //smImageWindow2.ShowManualButton = false;
                for (int i = 0; i < GlobalVariables.GConst.STATION_COUNT; i++)
                {
                   // m_ActionRunThread[i].StartThread();
                    
                    //m_ConnectHeartbeatThreads[i].StartThread();
                    if (i == 1) continue;
                    if (m_CameraControl[i]!=null) m_CameraControl[i].SetExternalTriggerMode();
                    if (m_AIRunThread[i] != null) m_AIRunThread[i].StartThread();
                    if (m_AIRunThread[i] != null) m_AIRunThread[i].m_ShowImage = m_SMImageWindows[i + 1].ImageShow;
                    if (m_AIRunThread[i] != null) m_AIRunThread[i].m_ShowResult = m_SMImageWindows[i + 1].ResultShow;
                    //m_ConnectHeartbeatThreads[i].m_ShowHeartColor += m_SMImageWindows[i+1].HeartResultShow;
                }
                

            }
            else
            {
                smButtonRun.BtnImage = global::SmoreVision.Properties.Resources.auto_off;
                smImageWindow1.ShowManualButton = true;
                //smImageWindow2.ShowManualButton = true;
                for (int i = 0; i < GlobalVariables.GConst.STATION_COUNT; i++)
                {
                    //m_ActionRunThread[i].Cycled = false;
                    if (i == 1) continue;
                    if (m_CameraControl[i] != null) m_CameraControl[i].SetSoftwareTriggerMode();
                    if (m_AIRunThread[i] != null) m_AIRunThread[i].Cycled = false;
                    //m_ConnectHeartbeatThreads[i].Cycled = false;
                }

            }
            BtnRunState = !BtnRunState;
        }

        /// <summary>
        /// CCD1手动推理单张图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smImageWindow1_BtnRunSinglePicClick(object sender, EventArgs e)
        {
            try
            {
                SimplexTestTask("TSTM");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// CCD1手动推理整个文件夹中的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smImageWindow1_BtnRunFolderPicClick(object sender, EventArgs e)
        {
            try
            {
                MultipleTestTask("TSTM");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 手动推理单张图片任务
        /// </summary>
        /// <param name="_taskName">CLS[进行分类算法推理]OCR[进行OCR算法推理]</param>
        private void SimplexTestTask(string _taskName)
        {
            int returnValue = 0;
            m_SaveImage = new SaveImage();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择图片文件夹";
            dialog.Filter = "图片文件(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp;*.tif";
            dialog.InitialDirectory = Application.StartupPath+"\\Algo";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // m_SaveImageThread.StartThread();

                HObject halconImage = new HObject();
                HOperatorSet.ReadImage(out halconImage, dialog.FileName);

                List<string> listTemp=m_halconImgProc.yfReadImgFolder();

                int indexof=listTemp.IndexOf(dialog.FileName);

             
                Mat mat = HalconToMat(m_halconImgProc.yfGetImg(indexof % 2 == 0 ? indexof+1: indexof));
                //Mat mat = Cv2.ImRead(dialog.FileName, ImreadModes.Unchanged);

                SMLogWindow.OutLog($"SimplexTestTask:path:{dialog.FileName}", Color.Green);
                //Mat srcMat = mat.Clone();
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                if (mat != null)
                {
                    SimplexTask = Task.Factory.StartNew(() =>
                    {

                        switch (_taskName)
                        {
                            case "TSTM":
                                {

                                    if(yfImgProc(indexof % 2 == 0 ? indexof + 1 : indexof))
                                    {

                                        mat = HalconToMat(m_halconImgProc.GetMaskImg(),false);
                                        SMDataWindow.AddData(true);
                                        smImageWindow1.ImageShow(mat);
                                    }
                                    
                                    

                                    // AlgoProcess(mat);
                                }
                                break;
                            default:
                                break;
                        }
                        Thread.Sleep(10);
                        //m_SaveImageThread.Cycled = false;
                    });
                }
            }
        }


        Dictionary<string, string> dicShowData = new Dictionary<string, string>();
        string stralgoRes = "";
        string NG_Algo = "";
        private bool yfImgProc(int index)
        {
            try
            {
                dicShowData.Clear();
                Dictionary<string, string> dicTemp = m_halconImgProc.yfExcute(index);

                dicShowData.Add("product_datetime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                if (dicTemp.Count == 3)
                {
                    string[] tempx = dicTemp["xval"].Split(';');
                    string[] tempy = dicTemp["yval"].Split(';');
                    string[] tempz = dicTemp["zval"].Split(';');

                    for (int i = 0; i < tempx.Length; i++)
                    {
                        if (tempx[i] == "") continue;

                        stralgoRes += $"Reg_{(i + 1).ToString("000")}:{tempx[i]},{tempy[i]},{tempz[i]};";
                        dicShowData.Add($"Reg_{(i + 1).ToString("000")}", $"{tempx[i]},{tempy[i]},{tempz[i]}");
                    }
                }


                NG_Algo = "Reg_001,Reg_010,Reg_015,Reg_030,";

                ShowData(new string[] { stralgoRes, NG_Algo });
                form_productinfo.WriteSqlData("product", dicShowData);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }

        private void ShowData(string[] dataRes)
        {

            dataqueue.Enqueue(dataRes);
            m_dataevent.Set();
        }

        private void CheckData()
        {
            bool bcheck = true;


            Dictionary<string, string[]> dicData = new Dictionary<string, string[]>();

            Task.Run(() =>
            {

                while (bcheck)
                {
                    m_dataevent.WaitOne();

                    if (dataqueue.IsEmpty)
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    //SMLogWindow.OutLog($"AlgoShowData_start", Color.Green);
                    try
                    {
                        string[] Res;
                        dataqueue.TryDequeue(out Res);
                        m_dataevent.Reset();
                        //if (dic.Count > 0) dic.Clear();

                        dicData.Clear();
                        if (Res.Length < 2)
                        {
                            SMLogWindow.OutLog($"Res_Length:{Res.Length}", Color.Green);
                            continue;
                        }
                        // SMLogWindow.OutLog($"AlgoShowData_data:{Res[0]}:{Res[1]}", Color.Green);
                        string[] arr = Res[0].Split(';');//全部数据
                        string[] arrNG = Res[1].Split(',');//NGName


                        // SMLogWindow.OutLog($"arr_len:{arr.Length}", Color.Green);
                        foreach (string s in arr)
                        {
                            // Console.WriteLine(s);
                            string[] arr1 = s.Split(':');//name:value

                            //SMLogWindow.OutLog($"arr1_len:{arr1.Length}", Color.Green);
                            if (arr1.Length < 2) continue;

                            //SMLogWindow.OutLog($"arr1[0]:{arr1[0]}", Color.Green);
                            if (dicData.ContainsKey(arr1[0]))//name
                            {
                                SMLogWindow.OutLog($"已包含key:{arr1[0]}", Color.Green);
                                continue;
                            }

                            //显示数据是否包含key
                            if (dicShowData.ContainsKey(arr1[0]))
                            {
                                string valtemp = dicShowData[arr1[0]];

                                if (valtemp != "")
                                {
                                    if (-1 == Array.IndexOf(arrNG, arr1[0]))
                                    {
                                        //OK
                                        dicData.Add(arr1[0], new string[] { arr1[1], "OK" });
                                    }
                                    else
                                    {
                                        //NG
                                        dicData.Add(arr1[0], new string[] { arr1[1], "NG" });
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }



                        }
                        //SMLogWindow.OutLog($"dicData_3:{dicData.Count}", Color.Green);
                        //string pp = dic.FirstOrDefault(x => x.Key == "FAI57_3").Value?.ToString();

                        if (smShowData != null) smShowData.Show(DeepCopyHelper<Dictionary<string, string[]>>.DeepCopyWithBinarySerialize(dicData));
                        //SMLogWindow.OutLog($"AlgoShowData_end", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel:LogLevel.Error);
                        SMLogWindow.OutLog($"AlgoShowData_error:{ex.ToString()}", Color.Green);
                    }


                    // });
                }

            });

        }

        private void AlgoProcess(Mat mat)
        {

            //算法Run之前参数输入
            Dictionary<string, DefectLimit> dicdefect = new Dictionary<string, DefectLimit>();

            foreach (var temp in GlobalVariables.Variables.dicProduct)
            {
                dicdefect.Add(temp.Key, new DefectLimit() { Minval = int.Parse(temp.Value[0]), Maxval = int.Parse(temp.Value[1]) });
            }


            AlgoRunInput algoRunInput = new AlgoRunInput() { SourceImg = mat.Clone(), DicDefect = dicdefect, ShowMask = true };

            if (GlobalVariables.Variables.bAlgo)//是否使用算法
            {
                returnValue = (int)m_AISDKManage.Run(algoRunInput, out AlgoRunOutput algorunOutput);

                if (returnValue == ERR_FAILED)
                {
                    SMLogWindow.OutLog("算法推理失败.", Color.Red, bshow: true);
                }
                else
                {
                    SMLogWindow.OutLog($"算法推理成功:count:{algorunOutput.Dicdefect.Count}", Color.Green);
                    // if (smImageWindow1.InvokeRequired)
                    {
                        if (algorunOutput.Dicdefect.Count < 1)
                        {
                            BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), true);
                            SMDataWindow.AddData(true);
                            smImageWindow1.ImageShow(algorunOutput.mask);

                            //存图
                            m_SaveImage.stationName = "CCD1";
                            m_SaveImage.picture = mat;
                            m_SaveImage.result = true;
                            m_SaveImage.mask = algorunOutput.mask;
                            m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                            m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
                        }
                        else
                        {
                            BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), false);
                            SMDataWindow.AddData(false);
                            smImageWindow1.ImageShow(algorunOutput.mask);

                            //存图
                            m_SaveImage.stationName = "CCD1";
                            m_SaveImage.picture = mat;
                            m_SaveImage.result = false;
                            m_SaveImage.mask = algorunOutput.mask;
                            m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                            m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
                        }
                    }

                }

            }
            else
            {
                BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), true);
                SMDataWindow.AddData(true);
                smImageWindow1.ImageShow(mat);

                //存图
                m_SaveImage.stationName = "CCD1";
                m_SaveImage.picture = mat;
                m_SaveImage.result = false;
                m_SaveImage.mask = mat;
                m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
            }
        }

        /// <summary>
        /// 手动推理整个文件夹中的图片
        /// </summary>
        /// <param name="_taskName"></param>
        private void MultipleTestTask(string _taskName)
        {
            FileInfo[] m_files;
            m_SaveImage = new SaveImage();
            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "请选择图像文件夹";
            if (dlg.ShowDialog())
            {
                //m_SaveImageThread.StartThread();

                DirectoryInfo dir = new DirectoryInfo(dlg.FileName);
                m_files = dir.GetFiles($"*.*", SearchOption.AllDirectories).Where(s => s.Name.EndsWith("jpg") || s.Name.EndsWith("png") || s.Name.EndsWith("bmp")).ToArray();
                if (m_files.Length > 0)
                {
                    MultipleTask = Task.Factory.StartNew(() =>
                    {
                        MultipleTaskRun = true;
                        int iIndex = 0;
                        while (MultipleTaskRun)
                        {
                            Mat mat = new Mat(m_files[iIndex].FullName, ImreadModes.Unchanged);
                            Mat srcMat = mat.Clone();
                            switch (_taskName)
                            {
                                case "TSTM":
                                    {
                                        AlgoProcess(mat);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            iIndex++;
                            if (iIndex >= m_files.Length)
                                break;
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        // m_SaveImageThread.Cycled = false;
                    });
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// CCD1手动触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smImageWindow1_TriggerCamera(object sender, EventArgs e)
        {
            if (m_CameraControl[0] != null)
            {

                m_CameraControl[0].SetSoftwareTriggerMode();
                //if (GlobalVariables.Variables.dicProduct.ContainsKey(GlobalVariables.Variables.strCurrModel))
                //{
                //    this.m_CameraControl[0].SetExposure(float.Parse(GlobalVariables.Variables.dicProduct[GlobalVariables.Variables.strCurrModel][2]));
                //    SMLogWindow.OutLog("model:" + GlobalVariables.Variables.strCurrModel + ":exp:" + GlobalVariables.Variables.dicProduct[GlobalVariables.Variables.strCurrModel][2], Color.Green, LogLevel.Info, false);
                //}
                //else
                //{
                //    this.m_CameraControl[0].SetExposure(11000f);
                //    SMLogWindow.OutLog("model:error", Color.Green, LogLevel.Info, false);
                //}

                int returnValue = m_CameraControl[0].SoftWareTriggerOnce();
                if (returnValue != ERROR_OK)
                {
                    SMLogWindow.OutLog($"CCD{m_CameraControl[0].CCDName}触发失败.", Color.Red);
                }
                else
                {
                    SMLogWindow.OutLog($"CCD{m_CameraControl[0].CCDName}触发成功.", Color.Green);
                    Thread.Sleep(500);
                    if (!m_CameraControl[0].IsEmpty())
                    {
                        CameraControlLibrary.CameraImageCallPack cameraImageCallPack = new CameraControlLibrary.CameraImageCallPack();
                        cameraImageCallPack = m_CameraControl[0].TryDequeue();
                        //SimplexTrrigerTask("TSTM", cameraImageCallPack.picture);


                        if (true)//是否使用算法
                        {
                            AlgoProcess(cameraImageCallPack.picture);
                        }
                        else
                        {
                            //smImageWindow1.ImageShow(SDKExtendClass.IOcrResponse.VisualizeMat(cameraImageCallPack.picture, Scalar.LightGreen, "OK"));
                        }

                        //smImageWindow1.ImageShow(SDKExtendClass.IOcrResponse.VisualizeMat(mask, Scalar.Red, "NG"));
                    }
                }
            }
            else
            {
                SMLogWindow.OutLog("CCD1没有初始化.", Color.Red);
            }
        }

        /// <summary>
        /// CCD2手动触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smImageWindow2_TriggerCamera(object sender, EventArgs e)
        {
            if (m_CameraControl[1] != null)
            {
                int returnValue = m_CameraControl[1].SoftWareTriggerOnce();
                if (returnValue != ERROR_OK)
                {
                    SMLogWindow.OutLog("CCD17触发失败.", Color.Red);
                }
                else
                {
                    SMLogWindow.OutLog("CCD17触发成功.", Color.Green);
                    Thread.Sleep(1000);
                    if (!m_CameraControl[1].IsEmpty())
                    {
                        CameraControlLibrary.CameraImageCallPack cameraImageCallPack = new CameraControlLibrary.CameraImageCallPack();
                        cameraImageCallPack = m_CameraControl[1].TryDequeue();
                        SimplexTrrigerTask("OCR", cameraImageCallPack.picture);
                    }
                }
            }
            else
            {
                SMLogWindow.OutLog("CCD17没有初始化.", Color.Red);
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭相机
            for (int i = 0; i < GlobalVariables.GConst.STATION_COUNT; i++)
            {
                if (m_CameraControl[i] != null)
                {
                    m_CameraControl[i].DeInitial();
                }
            }
            //关闭PLC
            if (m_SiemensPLCControl != null)
            {
                m_SiemensPLCControl.DeInitial();
            }
        }

        private void smDataWindow1_ClearData(object sender, EventArgs e)
        {
            smImageWindow1.ClearProduceData();
            //smImageWindow2.ClearProduceData();
        }

        /// <summary>
        /// 手动触发相机推理单张图片任务
        /// </summary>
        /// <param name="_taskName">CLS[进行分类算法推理]OCR[进行OCR算法推理]</param>
        private void SimplexTrrigerTask(string _taskName, Mat mat)
        {
            int returnValue = 0;
            m_SaveImage = new SaveImage();
            Mat srcMat = mat.Clone();
            if (mat != null)
            {
                SimplexTrigerTask = Task.Factory.StartNew(() =>
                {

                    switch (_taskName)
                    {
                        case "CLS":
                            {
                                string clsResult = "";
                                returnValue = Error_OK; //m_AISDKManage.CLSRun(mat, ref clsResult);
                                if (returnValue != Error_OK)
                                {
                                    SMLogWindow.OutLog("CLS算法推理失败.", Color.Red);
                                }
                                else
                                {
                                    if (clsResult != "")
                                    {
                                        SMLogWindow.OutLog($"CLS算法推理成功.推理结果:{clsResult}", Color.Green);
                                        if (smImageWindow1.InvokeRequired)
                                        {
                                            if (clsResult.Contains("OK"))
                                            {
                                                BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), true);
                                                SMDataWindow.AddData(true);
                                                // smImageWindow1.ImageShow(SDKExtendClass.IOcrResponse.VisualizeMat(mat, Scalar.LightGreen, "OK"));

                                                //存图
                                                m_SaveImage.stationName = "CCD1";
                                                m_SaveImage.picture = srcMat;
                                                m_SaveImage.result = true;
                                                // m_SaveImage.mask = SDKExtendClass.IOcrResponse.VisualizeMat(mat, Scalar.LightGreen, "OK");
                                                m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                                                m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
                                            }
                                            else
                                            {
                                                BeginInvoke(new Action<bool>(smImageWindow1.ResultShow), false);
                                                SMDataWindow.AddData(false);
                                                // smImageWindow1.ImageShow(SDKExtendClass.IOcrResponse.VisualizeMat(mat, Scalar.Red, "NG"));

                                                //存图
                                                m_SaveImage.stationName = "CCD1";
                                                m_SaveImage.picture = srcMat;
                                                m_SaveImage.result = false;
                                                //m_SaveImage.mask = SDKExtendClass.IOcrResponse.VisualizeMat(mat, Scalar.Red, "NG");
                                                m_SaveImage.time = DateTime.Now.ToString(GlobalVariables.GConst.IMAGE_SAVE_BASE_TIME_FORMAT);
                                                m_SaveImageThread.SaveImagePack_Buffer.Enqueue(m_SaveImage);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        SMLogWindow.OutLog($"CLS算法推理失败.推理结果为:NULL", Color.Red);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                });
            }
        }

        private void ProductRecive1(ProductInfo proinfo)
        {
            SMLogWindow.OutLog($"Model:{proinfo.Product_Model};Content:{proinfo.Product_content}.", Color.Green);

            //组别
            XMLConfig.Device.Items[0].EquipmentNumber = proinfo.Product_content;

            if (CSV.Instance[1].Data.Count > int.Parse(XMLConfig.Device.Items[0].EquipmentNumber))
            {
                List<string> m_list = CSV.Instance[1].Data[int.Parse(XMLConfig.Device.Items[0].EquipmentNumber)];
                string resText = m_list[2] + m_list[1] + DateTime.Now.ToString("MMdd") + m_list[3];
                SMLogWindow.OutLog($"resText:{resText}", Color.Green);

                //smInfoWindow1.EquipmentNumber = XMLConfig.Device.Items[0].EquipmentNumber + ";" + resText;
            }
            else
            {
                SMLogWindow.OutLog($"当前组别索引:{int.Parse(XMLConfig.Device.Items[0].EquipmentNumber)};最大索引:{CSV.Instance[1].Data.Count}", Color.Red);
                XMLConfig.Device.Items[0].EquipmentNumber = "0";
                //smInfoWindow1.EquipmentNumber = "null";
            }



        }

        private void ProductRecive2(ProductInfo proinfo)
        {
            SMLogWindow.OutLog($"Model:{proinfo.Product_Model};Content:{proinfo.Product_content}.", Color.Green);

            //型号信息
            string Temp = proinfo.Product_Model;
            SMLogWindow.OutLog($"Model:{Temp}", Color.Green);
            XMLConfig.Device.Items[0].ProductModel = Temp;
            smInfoWindow1.ProductModel = XMLConfig.Device.Items[0].ProductModel;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $"\\Content\\{Temp}.csv"))
            {
                //加载产品组别
                CSV.Instance[1].Read(AppDomain.CurrentDomain.BaseDirectory + $"\\Content\\{Temp}.csv");
                SMLogWindow.OutLog($"加载{AppDomain.CurrentDomain.BaseDirectory + $"\\Content\\{Temp}.csv"}刻字信息成功", Color.Green);
                XMLConfig.Device.Items[0].EquipmentNumber = "0";
                //smInfoWindow1.EquipmentNumber = "null";
            }
            else
            {
                MessageBox.Show($"不存在{Temp}刻字信息");
                SMLogWindow.OutLog($"不存在{Temp}刻字信息", Color.Red);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.smImageWindow1.Visible)
            {
                this.tableLayoutPanelMainHome.ColumnStyles[0].Width = 2F;
                this.tableLayoutPanelMainHome.ColumnStyles[1].Width = 98F;
            }
            else
            {
                this.tableLayoutPanelMainHome.ColumnStyles[0].Width = 50F;
                this.tableLayoutPanelMainHome.ColumnStyles[1].Width = 50F;
            }
            this.smImageWindow1.Visible = !this.smImageWindow1.Visible;
        }

        private void smButton1_BtnClick(object sender, EventArgs e)
        {
            if (BtnRunState)
            {
                MessageBox.Show("自动运行中，不可更改");
                return;
            }

            if (!BtnOCRState)
            {
                //smButtonOCR.BtnImage = global::SmoreVision.Properties.Resources.auto_on;
                //XMLConfig.SDK.Items[0].RunEnable = true;
                GlobalVariables.Variables.bAlgo = true;
            }
            else
            {
                // smButtonOCR.BtnImage = global::SmoreVision.Properties.Resources.auto_off;
                //XMLConfig.SDK.Items[0].RunEnable = false;
                GlobalVariables.Variables.bAlgo = false;
            }
            BtnOCRState = !BtnOCRState;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            iOControlClass.SendCmd(1, true);
            Thread.Sleep(50);
            //通道,状态
            iOControlClass.SendCmd(0, true);
        }

        private void SetDateMsg(string[] msg)
        {
            if (msg != null)
            {
                GlobalVariables.Variables.bManual = true;
                GlobalVariables.Variables.dateMsg = msg;
                SMLogWindow.OutLog($"bManual:{GlobalVariables.Variables.bManual}:dateMsg:{GlobalVariables.Variables.dateMsg[0]}:{GlobalVariables.Variables.dateMsg[1]}", Color.Green);
            }
            else
            {
                GlobalVariables.Variables.bManual = false;
                GlobalVariables.Variables.dateMsg = null;

                SMLogWindow.OutLog($"bManual:{GlobalVariables.Variables.bManual}:dateMsg:{GlobalVariables.Variables.dateMsg}", Color.Green);

            }
        }

        private void LoadProductInfo()
        {
            try
            {
                if (!Directory.Exists(SMProductInfoSet.JsonRootDir))
                {
                    SMLogWindow.OutLog($"{SMProductInfoSet.JsonRootDir}不存在", Color.Green);
                    return;
                }

                List<FileInfo> jsonFileName = SMProductInfoSet.getFile(SMProductInfoSet.JsonRootDir, ".json");
                JsonFileParse jsonFileParse = new JsonFileParse();
                ParametricRecord parametricRecord = null;
                foreach (var item in jsonFileName)
                {
                    string jsonFilePath = SMProductInfoSet.JsonRootDir + item;
                    jsonFileParse.ReadJsonFile(jsonFilePath, ref parametricRecord);

                    if (GlobalVariables.Variables.dicProduct.ContainsKey(parametricRecord.ID))
                    {
                        GlobalVariables.Variables.dicProduct[parametricRecord.ID] = new string[] { parametricRecord.Items.Value1, parametricRecord.Items.Value2 };
                    }
                    else
                    {
                        GlobalVariables.Variables.dicProduct.Add(parametricRecord.ID, new string[] { parametricRecord.Items.Value1, parametricRecord.Items.Value2 });
                    }
                }

            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green);
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public override void UpDateConfig()
        {

            switch (idname)
            {
                case IDName.Operator:
                    smInfoWindow1.ModelDate = "操作员";                  
                    break;
                case IDName.Engineer:
                    smInfoWindow1.ModelDate = "工程师";                   
                    break;
                case IDName.Admin:                 
                    smInfoWindow1.ModelDate = "管理员";
                   
                    break;
            }

            bJson = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_SaveImageThread.Cycled = false;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            m_SaveImageThread.StartThread();
        }

        private void smImageWindow1_Load(object sender, EventArgs e)
        {

        }
      
        private void label2_Click(object sender, EventArgs e)
        {
            iOControlClass.SendCmd(1, false);
            Thread.Sleep(50);
            //通道,状态
            iOControlClass.SendCmd(0, false);
        }

        // 将Halcon的HObject转换为OpenCV的Mat
        static Mat HalconToMat(HObject halconImage,bool bGray=true)
        {
            // 获取Halcon图像的宽度和高度
            HTuple width, height;
            HOperatorSet.GetImageSize(halconImage, out width, out height);

            // 获取Halcon图像的指针和图像数据
            HTuple ptr;
            HTuple ptrR,ptrG,ptrB;
            HTuple type,w,h;
            HTuple channels;
            HOperatorSet.CountChannels(halconImage, out channels);
            HOperatorSet.GetImagePointer1(halconImage, out ptr, out type, out w, out h);

            // 创建OpenCV的Mat对象
            Mat opencvImage = null;
            if (bGray)
            {
                
                opencvImage = new Mat(height, width, MatType.CV_16SC1, (IntPtr)ptr);
            }
            else
            {
                HOperatorSet.GetImagePointer3(halconImage, out ptrR,out ptrG,out ptrB, out type, out w, out h);
                //opencvImage = new Mat(height, width, MatType.CV_8SC3, (IntPtr)ptr);

                // 将IntPtr对象转换为Mat
                opencvImage = new Mat(height, width, MatType.CV_8UC3);

                // 使用OpenCVSharp的方法填充Mat对象

                Cv2.Merge(new[] { new Mat(height, width, MatType.CV_8UC1, (IntPtr)ptrB),
                          new Mat(height, width, MatType.CV_8UC1, (IntPtr)ptrG),
                          new Mat(height, width, MatType.CV_8UC1, (IntPtr)ptrR)}, opencvImage);
            }
           
            

            return opencvImage.Clone(); // 避免共享相同的数据
        }

    }

    public class CamInfo
    {
        public CamType camType;
        public string ccfPath = "";
        public string strCard = "";
        public string strCam = "";
    }

    public enum CamType
    {
        HikArea,
        DahuaArea,
        DalsaLine,
        DalsaGige
    }

    
}
