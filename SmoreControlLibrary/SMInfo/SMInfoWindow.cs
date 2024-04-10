using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SmoreControlLibrary.SMInfo
{

    public delegate void TriDevice();

    public struct DeviceInfo
    {
        //产品型号
        public string  ProductModel;
        //设备编号
        public string EquipmentNumber;
        //模型版本
        public string ModelVersion;
        //模型日期
        public string ModelDate;
        //当前批次号
        public string BatchNumber;
    }

    public partial class SMInfoWindow : UserControl
    {
        #region property
        private string _productModel;
        [Category("SmoreControl"),Description("产品型号")]
        public string ProductModel
        {
            get { return _productModel; }
            set { _productModel = value; this.lbProductModel.Text =$"  产品型号：{value}";}
        }

        private string _equipmentNumber;
        [Category("SmoreControl"), Description("产品组别")]
        //public string EquipmentNumber
        //{
        //    get { return _equipmentNumber; }
        //    set { _equipmentNumber = value; this.lbEquipmentNumber.Text = $"  产品组别：{value}"; }
        //}

        private string _modelVersion;
        [Category("SmoreControl"), Description("模型版本")]
        public string ModelVersion
        {
            get { return _modelVersion; }
            set { _modelVersion = value; this.lbModelVersion.Text = $"  模型版本：{value}"; }
        }

        private string _modelDate;
        [Category("SmoreControl"), Description("当前用户")]
        public string ModelDate
        {
            get { return _modelDate; }
            set { _modelDate = value; this.lbCurUser.Text = $"  当前用户：{value}"; }
        }

        private string _batchNumber;
        [Category("SmoreControl"), Description("当前批号")]
        //public string BatchNumber
        //{
        //    get { return _batchNumber; }
        //    set { _batchNumber = value; this.lbBatchNumber.Text = $"  当前批号：{value}"; }
        //}
        #endregion

        private XMLConfigParse m_XMLConfigParse = null;
        private static int ErrorOK = 0;
        private string ErrorInfo = "";
        private string ConfigFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\" + "SmoreVisionConfig.xml";
       // private string ConfigFilePath = "";
        //设备信息
        private DeviceInfo _DeviceInfo;
        public DeviceInfo GetDeviceInfo
        {
            get { return _DeviceInfo; }
            set { _DeviceInfo = value; }
        }

        public TriDevice m_TriDevice;

        public SMInfoWindow()
        {
            InitializeComponent();
            
        }

        public void AddConfig(XMLConfigParse xmlConfig)
        {
            m_XMLConfigParse = xmlConfig;
        }

        private void smButton1_BtnClick(object sender, EventArgs e)
        {
            FormChangeInfo formChangeInfo = new FormChangeInfo();

            List<FileInfo> list = GetFamilyFiles(new DirectoryInfo($@"{AppDomain.CurrentDomain.BaseDirectory}Formula"));
            
            formChangeInfo.ProductModel = m_XMLConfigParse.Device.Items[0].ProductModel;
            formChangeInfo.Batch = m_XMLConfigParse.Device.Items[0].CurBatch;
            formChangeInfo.ProductGroup = m_XMLConfigParse.Device.Items[0].EquipmentNumber;
            formChangeInfo.AddItems(list);

            formChangeInfo.ShowDialog();

            if (!formChangeInfo.CHANGE) return;
            if (formChangeInfo.Batch == "" || formChangeInfo.Batch == null || formChangeInfo.ProductModel == "" || formChangeInfo.ProductModel == null || formChangeInfo.ProductGroup == "" || formChangeInfo.ProductGroup == null) return;
            m_XMLConfigParse.Device.Items[0].EquipmentNumber = formChangeInfo.ProductGroup;
            m_XMLConfigParse.Device.Items[0].CurBatch = formChangeInfo.Batch;
            m_XMLConfigParse.Device.Items[0].ProductModel = formChangeInfo.ProductModel;
            XMLSerialize.SerializeToXml<XMLConfigParse>(ConfigFilePath, m_XMLConfigParse, ref ErrorInfo);

            Update();
            m_TriDevice();
        }

        private void SMInfoWindow_Load(object sender, EventArgs e)
        {
            if(m_XMLConfigParse==null) m_XMLConfigParse = new XMLConfigParse();


        }

        public void Init()
        {
            int returnValue = InitialConfigFile();
            if (returnValue != ErrorOK)
            {
                MessageBox.Show($"加载配置文件失败,错误代码:{ErrorInfo}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Update();

        }

        private void Update()
        {
            //产品型号
            ProductModel = m_XMLConfigParse.Device.Items[0].ProductModel;
            ////设备编号
            //EquipmentNumber = m_XMLConfigParse.Device.Items[0].EquipmentNumber;
            //模型版本
            ModelVersion = m_XMLConfigParse.Device.Items[0].ModelVer;
            //模型日期
            ModelDate = m_XMLConfigParse.Device.Items[0].ModelDate;
            ////当前批次号
            //BatchNumber = m_XMLConfigParse.Device.Items[0].CurBatch;
        }

        private int InitialConfigFile()
        {
            int returnValue = XMLConfigParse.DeserializeFromXml<XMLConfigParse>(ConfigFilePath, ref m_XMLConfigParse, ref ErrorInfo);
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }
            returnValue = m_XMLConfigParse.GenerateNodeInfo();
            if (returnValue != XMLConfigParse.ErrorOK)
            {
                return returnValue;
            }
            return ErrorOK;
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
    }
}
