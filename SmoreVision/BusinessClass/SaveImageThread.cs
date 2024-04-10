using OpenCvSharp;
using SMLogControlLibrary;
using SmoreControlLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmoreVision.HardwareControl.ImageType;

namespace SmoreVision.BusinessClass
{
    public class SaveImageThread
    {
        private const int ERROR_OK = 0;

        private const int ERROR_FAILED = -1;

        public bool Cycled = false;

        private string LastError = "";

        private Task m_SaveImageThread = null;

        public ConcurrentQueue<SaveImage> SaveImagePack_Buffer;

        private XMLConfigParse m_XMLConfig;


        public SaveImageThread( XMLConfigParse _xMLConfig)
        {
            m_XMLConfig = _xMLConfig;
            SaveImagePack_Buffer = new ConcurrentQueue<SaveImage>();
        }

        public int StartThread()
        {
            if (m_SaveImageThread == null || m_SaveImageThread.IsCompleted == true)
            {
                m_SaveImageThread = Task.Factory.StartNew(() => ThreadProcedureProcess());
                SMLogWindow.OutLog("存图线程开启.",Color.Green);
            }
            return ERROR_OK;
        }

      

        public int ThreadProcedureProcess()
        {
            try
            {
                Cycled = true;
                SaveImage saveImage;
                while (Cycled)
                {
                    if (SaveImagePack_Buffer.IsEmpty)
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    saveImage = new SaveImage();
                    SaveImagePack_Buffer.TryDequeue(out saveImage);
                    string data = DateTime.Now.ToString("yyyy_MM_dd");

                    string strProduct = m_XMLConfig.Device.Items[0].ProductModel;

                    string labelOKRootDir = m_XMLConfig.SaveImage.Items[0].Path + $"\\{data}\\{strProduct}\\OK渲染图";
                    string labelNGRootDir = m_XMLConfig.SaveImage.Items[1].Path + $"\\{data}\\{strProduct}\\NG渲染图";
                    string origOKRootDir = m_XMLConfig.SaveImage.Items[2].Path + $"\\{data}\\{strProduct}\\OK原图";
                    string origNGRootDir = m_XMLConfig.SaveImage.Items[3].Path + $"\\{data}\\{strProduct}\\NG原图";

                    
                    if (!File.Exists(labelOKRootDir)) Directory.CreateDirectory(labelOKRootDir);
                    if (!File.Exists(labelNGRootDir)) Directory.CreateDirectory(labelNGRootDir);
                    if (!File.Exists(origOKRootDir))  Directory.CreateDirectory(origOKRootDir);
                    if (!File.Exists(origNGRootDir))  Directory.CreateDirectory(origNGRootDir);

                    string baseName = strProduct + "_" + DateTime.Now.ToString("yyyyMMdd") + "_"+saveImage.time;
                    SMLogWindow.OutLog($"origOKRootDir:{origOKRootDir}:baseName:{baseName}", Color.Green);
                    ImageEncodingParam Params = null;

                    SMLogWindow.OutLog($"ImageType:Items[0]:OK渲染图:{m_XMLConfig.SaveImage.Items[0].ImageType}", Color.Green);
                    SMLogWindow.OutLog($"ImageType:Items[1]:NG渲染图:{m_XMLConfig.SaveImage.Items[1].ImageType}", Color.Green);
                    SMLogWindow.OutLog($"ImageType:Items[2]:OK原图:{m_XMLConfig.SaveImage.Items[2].ImageType}", Color.Green);
                    SMLogWindow.OutLog($"ImageType:Items[3]:NG原图:{m_XMLConfig.SaveImage.Items[3].ImageType}", Color.Green);
                    if (saveImage.result)
                    {
                        if (m_XMLConfig.SaveImage.Items[0].SaveEnable)
                        {
                           if(saveImage.mask!=null)
                            {
                                if(m_XMLConfig.SaveImage.Items[0].ImageType==".png") Params = new ImageEncodingParam(ImwriteFlags.PngCompression, 9);
                                if(m_XMLConfig.SaveImage.Items[0].ImageType == ".jpg") Params = new ImageEncodingParam(ImwriteFlags.JpegQuality,50);
                                saveImage.mask.ImWrite(labelOKRootDir + "\\" + baseName + m_XMLConfig.SaveImage.Items[0].ImageType, Params);
                            }
                                
                        }
                        if (m_XMLConfig.SaveImage.Items[2].SaveEnable)
                        {
                            if (saveImage.picture != null)
                            {
                                if (m_XMLConfig.SaveImage.Items[2].ImageType == ".png") Params = new ImageEncodingParam(ImwriteFlags.PngCompression, 9);
                                if (m_XMLConfig.SaveImage.Items[2].ImageType == ".jpg") Params = new ImageEncodingParam(ImwriteFlags.JpegQuality, 50);
                                saveImage.picture.ImWrite(origOKRootDir + "\\" + baseName + m_XMLConfig.SaveImage.Items[2].ImageType, Params);
                            } 
                        }
                    }
                    else
                    {
                        if (m_XMLConfig.SaveImage.Items[1].SaveEnable)
                        {
                            if (saveImage.mask != null)
                            {
                                if (saveImage.mask != null)
                                {
                                    if (m_XMLConfig.SaveImage.Items[1].ImageType == ".png") Params = new ImageEncodingParam(ImwriteFlags.PngCompression, 9);
                                    if (m_XMLConfig.SaveImage.Items[1].ImageType == ".jpg") Params = new ImageEncodingParam(ImwriteFlags.JpegQuality, 50);
                                    saveImage.mask.ImWrite(labelNGRootDir + "\\" + baseName + m_XMLConfig.SaveImage.Items[1].ImageType, Params);
                                }
                                    
                            }
                        }
                        if (m_XMLConfig.SaveImage.Items[3].SaveEnable)
                        {
                            if (saveImage.picture!=null)
                            {
                                if (saveImage.picture != null) 
                                {
                                    if (m_XMLConfig.SaveImage.Items[3].ImageType == ".png") Params = new ImageEncodingParam(ImwriteFlags.PngCompression, 9);
                                    if (m_XMLConfig.SaveImage.Items[3].ImageType == ".jpg") Params = new ImageEncodingParam(ImwriteFlags.JpegQuality, 50);
                                    saveImage.picture.ImWrite(origNGRootDir + "\\" + baseName + m_XMLConfig.SaveImage.Items[3].ImageType, Params);
                                } 
                            }
                        }
                    }

                    SMLogWindow.OutLog("存图成功",Color.Green);
                    Thread.Sleep(10);
                }
                SMLogWindow.OutLog("存图线程结束.",Color.Green);
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return ERROR_FAILED;
            }
        }
    }
}
