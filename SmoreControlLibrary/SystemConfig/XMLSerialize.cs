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
	public class XMLSerialize
	{
		public static int ErrorOK = 0;
		public static int ErrorFailed = -1;

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

		public static int SerializeToXml<T>(string filePath, T obj, ref string errorInfo)
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
