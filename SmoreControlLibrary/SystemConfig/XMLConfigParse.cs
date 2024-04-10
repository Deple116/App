using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SmoreControlLibrary
{
	[XmlType(TypeName = "Root")]
	public class XMLConfigParse
	{
		public static int ErrorOK = 0;
		public static int ErrorFailed = -1;

		[XmlIgnore]
		public Dictionary<string, Dictionary<string, string>> NodeDictionary;

		public int GenerateNodeInfo()
		{
			NodeDictionary = new Dictionary<string, Dictionary<string, string>>();

			if (System != null)
			{
				Dictionary<string, string> systemNode = new Dictionary<string, string>();
				systemNode.Add("FullScreen", System.FullScreen.ToString());
                systemNode.Add("ProjectName", System.ProjectName.ToString());
                NodeDictionary.Add("System", systemNode);
			}

            if (PLC != null)
            {
                Dictionary<string, string> plcNode = new Dictionary<string, string>();
                plcNode.Add("IP", PLC.IP);
                plcNode.Add("Port", PLC.Port.ToString());
                plcNode.Add("Slot",PLC.Slot.ToString());
                plcNode.Add("Rack",PLC.Rack.ToString());
                NodeDictionary.Add("PLC", plcNode);
            }

            if (SaveImage != null)
            {
                int saveImgItemCount = SaveImage.Items.Length;
                if (saveImgItemCount > 0)
                {
                    Dictionary<string, string>[] saveImgNode = new Dictionary<string, string>[saveImgItemCount];

                    for (int i = 0; i < saveImgItemCount; i++)
                    {
                        string itemName = $"SaveImage.Items[{i}]";
                        saveImgNode[i] = new Dictionary<string, string>();
                        saveImgNode[i].Add($"{itemName}.Name", SaveImage.Items[i].Name);
                        saveImgNode[i].Add($"{itemName}.Path", SaveImage.Items[i].Path);
                        saveImgNode[i].Add($"{itemName}.ImageType", SaveImage.Items[i].ImageType);
                        saveImgNode[i].Add($"{itemName}.SaveEnable", SaveImage.Items[i].SaveEnable.ToString());
                        NodeDictionary.Add(itemName, saveImgNode[i]);
                    }
                }
            }

            if (SDK != null)
            {

                int sdkItemCount = SDK.Items.Length;
                if (sdkItemCount>0)
                {
                    Dictionary<string, string>[] sdkNode = new Dictionary<string, string>[sdkItemCount];
                    for (int i = 0; i < sdkItemCount; i++)
                    {
                        string itemName = $"SDK.Items[{i}]";
                        sdkNode[i] = new Dictionary<string, string>();
                        sdkNode[i].Add($"{itemName}.Name", SDK.Items[i].Name);
                        sdkNode[i].Add($"{itemName}.ConfigPath", SDK.Items[i].ConfigPath);
                        sdkNode[i].Add($"{itemName}.GPU", SDK.Items[i].GPU.ToString());
                        sdkNode[i].Add($"{itemName}.DeviceID", SDK.Items[i].DeviceID.ToString());
                        sdkNode[i].Add($"{itemName}.TypeName", SDK.Items[i].TypeName);
                        sdkNode[i].Add($"{itemName}.SolutionId", SDK.Items[i].SolutionId.ToString());
                        sdkNode[i].Add($"{itemName}.RunEnable", SDK.Items[i].RunEnable.ToString());
                        NodeDictionary.Add(itemName, sdkNode[i]);
                    }
                }
            }

            if (SaveTime != null)
            {
                int saveTimeItemCount = SaveTime.Items.Length;
                if (saveTimeItemCount > 0)
                {
                    Dictionary<string, string>[] saveTimeNode = new Dictionary<string, string>[saveTimeItemCount];
                    for (int i = 0; i < saveTimeItemCount;i++)
                    {
                        string itemName = $"SaveTime.Items[{i}]";
                        saveTimeNode[i] = new Dictionary<string, string>();
                        saveTimeNode[i].Add($"{itemName}.Name", SaveTime.Items[i].Name);
                        saveTimeNode[i].Add($"{itemName}.SaveDays", SaveTime.Items[i].SaveDays.ToString());
                        saveTimeNode[i].Add($"{itemName}.SaveDaysEnable", SaveTime.Items[i].SaveDaysEnable.ToString());
                        saveTimeNode[i].Add($"{itemName}.SaveSize", SaveTime.Items[i].SaveSize.ToString());
                        saveTimeNode[i].Add($"{itemName}.SaveSizeEnable", SaveTime.Items[i].SaveSizeEnable.ToString());
                        NodeDictionary.Add(itemName, saveTimeNode[i]);
                    }
                }
            }
            return ErrorOK;
		}

		[XmlElement("System")]
		public SystemElement System { get; set;}
        [XmlElement("PLC")]
        public PLCElement PLC { get; set;}
        [XmlElement("SaveImage")]
        public SaveImageElement SaveImage { get; set;}
        [XmlElement("SDK")]
        public SDKElement SDK { get; set;}
        [XmlElement("Device")]
        public DeviceElement Device { get; set; }
        [XmlElement("SaveTime")]
        public SaveTimeElement SaveTime { get; set;}

        public class SystemElement
		{
            [XmlElement("ProjectName")]
            public string ProjectName { get; set;}
            [XmlElement("FullScreen")]
			public bool FullScreen { get; set;}
        }

        [XmlType("PLC")]  
        public class PLCElement
        {
            [XmlElement("IP")]
            public string IP { get; set; }
            [XmlElement("Port")]
            public int Port { get; set; }
            [XmlElement("Slot")]
            public short Slot { get; set;}
            [XmlElement("Rack")]
            public short Rack { get; set;}
        }

        [XmlType("SaveImage")]
        public class SaveImageElement
        {
            [XmlElement("Item")]
            public SaveImageItem[] Items { get; set; }
        }

        [XmlType("SaveImageItem")]
        public class SaveImageItem
        {
            [XmlAttribute("Name")]
            public string Name { get; set; }
            [XmlAttribute("Path")]
            public string Path { get; set; }
            [XmlAttribute("ImageType")]
            public string ImageType { get; set;}
            [XmlAttribute("SaveEnable")]
            public bool SaveEnable { get; set;}
        }

        [XmlType("SDK")]
        public class SDKElement
        {
            [XmlElement("Item")]
            public SDKItem[] Items { get; set; }
        }

        [XmlType("SDKItem")]
        public class SDKItem
        {
            [XmlAttribute("Name")]
            public string Name { get; set; }
            [XmlAttribute("ConfigPath")]
            public string ConfigPath { get; set; }
            [XmlAttribute("GPU")]
            public bool GPU { get; set; }
            [XmlAttribute("DeviceID")]
            public uint DeviceID { get; set; }
            [XmlAttribute("TypeName")] 
            public string TypeName { get; set; }
            [XmlAttribute("SolutionId")] 
            public uint SolutionId { get; set; }
            [XmlAttribute("RunEnable")]
            public bool RunEnable { get; set; }
        }


        [XmlType("Device")]
        public class DeviceElement
        {
            [XmlElement("Item")]
            public DeviceItem[] Items { get; set; }
        }

        [XmlType("DeviceItem")]
        public class DeviceItem
        {
            [XmlAttribute("ProductModel")]
            public string ProductModel { get; set; }
            [XmlAttribute("EquipmentNumber")]
            public string EquipmentNumber { get; set; }
            [XmlAttribute("ModelVer")]
            public string ModelVer { get; set; }
            [XmlAttribute("ModelDate")]
            public string ModelDate { get; set; }
            [XmlAttribute("CurBatch")]
            public string CurBatch { get; set; }
        }

        [XmlType("SaveTime")]
        public class SaveTimeElement
        {
            [XmlElement("Item")]
            public SaveTimeItem[] Items { get; set; }
        }

        [XmlType("SaveTimeItem")]
        public class SaveTimeItem
        {
            [XmlAttribute("Name")]
            public string Name { get; set; }
            [XmlAttribute("SaveDays")]
            public uint SaveDays { get; set; }
            [XmlAttribute("SaveDaysEnable")]
            public bool SaveDaysEnable { get; set; }
            [XmlAttribute("SaveSize")]
            public uint SaveSize { get; set; }
            [XmlAttribute("SaveSizeEnable")]
            public bool SaveSizeEnable { get; set; }
        }

        public static int WriteToXML(string filePath, object xmlObject, ref string errorInfo, bool omitDeclaration = false, string prefix = "", string ns = "")
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(xmlObject.GetType());
				FileStream fileStream = new FileStream(filePath, FileMode.Create);

				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = "    ";
				settings.NewLineChars = Environment.NewLine;
				settings.Encoding = Encoding.UTF8;
				settings.OmitXmlDeclaration = omitDeclaration;

				using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings))
				{
					XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
					namespaces.Add(prefix, ns);
					xmlSerializer.Serialize(xmlWriter, xmlObject, namespaces);
					xmlWriter.Close();
				};
				fileStream.Close();
				return ErrorOK;
			}
			catch (Exception ex)
			{
				errorInfo = ex.Message;
				return ErrorFailed;
			}
		}

		public int SerializeToXml<T>(string filePath, T obj, ref string errorInfo)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(filePath))
				{
					XmlSerializer xs = new XmlSerializer(typeof(T));
					xs.Serialize(writer, obj);
				}
				return ErrorOK;
			}
			catch (Exception ex)
			{
				errorInfo = ex.Message;
				return ErrorFailed;
			}
		}

		public static int DeserializeFromXml<T>(string filePath, ref T t, ref string errorInfo)
		{
			try
			{
				if (!File.Exists(filePath))
				{
					errorInfo = $"{filePath} not exists.";
					return ErrorFailed;
				}

				using (StreamReader reader = new StreamReader(filePath))
				{
					XmlSerializer xs = new XmlSerializer(typeof(T));
					t = (T)xs.Deserialize(reader);
					return ErrorOK;
				}
			}
			catch (Exception ex)
			{
				errorInfo = ex.Message;
				t = default(T);
				return ErrorFailed;
			}
		}
	}
}
