﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SmoreControlLibrary;
using System.Drawing;
using SMLogControlLibrary;

namespace SmoreVision.Ini
{
	public class INIClass
	{
		private readonly object locker1 = new object();

		public string FileName; //INI文件名
								//声明读写INI文件的API函数
		[DllImport("kernel32")]
		private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In][Out] char[] lpReturnedString, uint nSize, string lpFileName);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);
		//类的构造函数，传递INI文件名
		public INIClass(string AFileName)
		{
			// 判断文件是否存在
			FileInfo fileInfo = new FileInfo(AFileName);
			//Todo:搞清枚举的用法
			if ((!fileInfo.Exists))
			{ //|| (FileAttributes.Directory in fileInfo.Attributes))
			  //文件不存在，建立文件
				System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
				try
				{
					sw.Write("#表格配置档案");
					sw.Close();
				}

				catch
				{
					throw (new ApplicationException("Ini文件不存在"));
				}
			}
			//必须是完全路径，不能是相对路径
			FileName = fileInfo.FullName;
		}
		//写INI文件
		public void WriteString(string Section, string Ident, string Value)
		{
			if (!WritePrivateProfileString(Section, Ident, Value, FileName))
			{

				throw (new ApplicationException("写Ini文件出错"));
			}
		}
		//读取INI文件指定
		public string ReadString(string Section, string Ident, string Default)
		{
			Byte[] Buffer = new Byte[65535];
			int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
			//必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
			string s = Encoding.GetEncoding(0).GetString(Buffer);
			s = s.Substring(0, bufLen);
			return s.Trim();
		}

		//读整数
		public int ReadInteger(string Section, string Ident, int Default)
		{
			string intStr = ReadString(Section, Ident, Convert.ToString(Default));
			try
			{
				return Convert.ToInt32(intStr);

			}
			catch (Exception ex)
			{
				SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
				//Console.WriteLine(ex.Message);
				return Default;
			}
		}

		public double ReadDouble(string Section, string Ident, double Default)
		{
			string intStr = ReadString(Section, Ident, Convert.ToString(Default));
			try
			{
				return Convert.ToDouble(intStr);

			}
			catch (Exception ex)
			{
				SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel:LogLevel.Error);
				//Console.WriteLine(ex.Message);
				return Default;
			}
		}

		//写整数
		public void WriteInteger(string Section, string Ident, int Value)
		{
			WriteString(Section, Ident, Value.ToString());
		}

		//读布尔
		public bool ReadBool(string Section, string Ident, bool Default)
		{
			try
			{
				return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default)));
			}
			catch (Exception ex)
			{
				SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel:LogLevel.Error);
				//Console.WriteLine(ex.Message);
				return Default;
			}
		}

		//写Bool
		public void WriteBool(string Section, string Ident, bool Value)
		{
			WriteString(Section, Ident, Convert.ToString(Value));
		}

		//从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
		public void ReadSection(string Section, StringCollection Idents)
		{
			Byte[] Buffer = new Byte[16384];
			//Idents.Clear();

			int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0),
	  FileName);
			//对Section进行解析
			GetStringsFromBuffer(Buffer, bufLen, Idents);
		}

		private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
		{
			Strings.Clear();
			if (bufLen != 0)
			{
				int start = 0;
				for (int i = 0; i < bufLen; i++)
				{
					if ((Buffer[i] == 0) && ((i - start) > 0))
					{
						String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
						Strings.Add(s);
						start = i + 1;
					}
				}
			}
		}
		//从Ini文件中，读取所有的Sections的名称
		public void ReadSections(StringCollection SectionList)
		{
			//Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
			byte[] Buffer = new byte[65535];
			int bufLen = 0;
			bufLen = GetPrivateProfileString(null, null, null, Buffer,
			Buffer.GetUpperBound(0), FileName);
			GetStringsFromBuffer(Buffer, bufLen, SectionList);
		}
		//读取指定的Section的所有Value到列表中
		public void ReadSectionValues(string Section, NameValueCollection Values)
		{
			StringCollection KeyList = new StringCollection();
			ReadSection(Section, KeyList);
			Values.Clear();
			foreach (string key in KeyList)
			{
				Values.Add(key, ReadString(Section, key, ""));

			}
		}
		////读取指定的Section的所有Value到列表中，
		//public void ReadSectionValues(string Section, NameValueCollection Values,char splitString)
		//{　 string sectionValue;
		//　　string[] sectionValueSplit;
		//　　StringCollection KeyList = new StringCollection();
		//　　ReadSection(Section, KeyList);
		//　　Values.Clear();
		//　　foreach (string key in KeyList)
		//　　{
		//　　　　sectionValue=ReadString(Section, key, "");
		//　　　　sectionValueSplit=sectionValue.Split(splitString);
		//　　　　Values.Add(key, sectionValueSplit[0].ToString(),sectionValueSplit[1].ToString());

		//　　}
		//}
		//清除某个Section
		public void EraseSection(string Section)
		{
			//
			if (!WritePrivateProfileString(Section, null, null, FileName))
			{

				throw (new ApplicationException("无法清除Ini文件中的Section"));
			}
		}
		//删除某个Section下的键
		public void DeleteKey(string Section, string Ident)
		{
			WritePrivateProfileString(Section, Ident, null, FileName);
		}
		//Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
		//在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
		//执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
		public void UpdateFile()
		{
			WritePrivateProfileString(null, null, null, FileName);
		}

		//检查某个Section下的某个键值是否存在
		public bool ValueExists(string Section, string Ident)
		{
			//
			StringCollection Idents = new StringCollection();
			ReadSection(Section, Idents);
			return Idents.IndexOf(Ident) > -1;
		}

		public static string[] GetAllSectionNames(string IniFilePath)
		{
			JudgeIniFileExist(IniFilePath);
			uint num = 32767u;
			string[] result = new string[0];
			IntPtr intPtr = Marshal.AllocCoTaskMem((int)(num * 2u));
			uint privateProfileSectionNames = GetPrivateProfileSectionNames(intPtr, num, IniFilePath);
			bool flag = privateProfileSectionNames > 0u;
			if (flag)
			{
				string text = Marshal.PtrToStringAuto(intPtr, (int)privateProfileSectionNames).ToString();
				result = text.Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
			}
			Marshal.FreeCoTaskMem(intPtr);
			return result;
		}

		private static void JudgeIniFileExist(string IniFilePath)
		{
			if (!File.Exists(IniFilePath))
			{
				throw new ArgumentException("Ini文件路径不存在", "IniFilePath");
			}
		}
		public static string[] GetAllItemKeys(string IniFilePath, string section)
		{

			JudgeIniFileExist(IniFilePath);
			string[] result = new string[0];
			bool flag = string.IsNullOrEmpty(section);
			if (flag)
			{
				throw new ArgumentException("必须指定节点名称", "section");
			}
			char[] array = new char[10240];
			uint privateProfileString = INIClass.GetPrivateProfileString(section, null, null, array, 10240u, IniFilePath);
			bool flag2 = privateProfileString > 0u;
			if (flag2)
			{
				result = new string(array).Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
			}
			return result;

		}

		//确保资源的释放
		~INIClass()
		{
			UpdateFile();
		}
	}
}
