using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SmoreControlLibrary.SystemConfig
{
    public class JsonFileParse
    {
        public static int Error_OK = 0;
        public static int Error_Failed = -1;

        public string ErrorInfo = "";

        public JsonFileParse()
        {

        }

        [DataContract]
        public class ParametricItem
        {
            [DataMember(Order = 0, Name = "value1")]
            public string Value1 { get; set; }
            [DataMember(Order = 1, Name = "value2")]
            public string Value2 { get; set; }
            [DataMember(Order = 2, Name = "value3")]
            public string Value3 { get; set; }
        }

        [DataContract]
        [KnownType(typeof(ParametricItem))]
        public class ParametricRecord
        {
            public static readonly DataContractJsonSerializerSettings Settings;

            static ParametricRecord()
            {
                Settings = new DataContractJsonSerializerSettings();
                Settings.EmitTypeInformation = EmitTypeInformation.Never;
            }

            [DataMember(Order = 0, Name = "id")]
            public string ID { get; set; }
            [DataMember(Order = 1, Name = "items")]
            public ParametricItem Items { get; set; }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        public int WriteJsonFile(ParametricRecord parametricRecord, string fileFullPath)
        {
            try
            {
                FileStream fs;
                StreamWriter wr;
                fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);
                wr = new StreamWriter(fs, Encoding.Default);
                wr.BaseStream.Seek(0, SeekOrigin.Begin);
                wr.Write(JsonSerializer<ParametricRecord>(parametricRecord));
                wr.Close();
                fs.Close();
                return Error_OK;
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                return Error_Failed;
            }
        }

        public int ReadJsonFile(string fileFullPath, ref ParametricRecord parametricRecord)
        {
            try
            {
                FileStream fs;
                StreamReader rr;
                fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
                rr = new StreamReader(fs, Encoding.Default);
                rr.BaseStream.Seek(0, SeekOrigin.Begin);
                string readStr = "";
                while (rr.Peek() != -1)
                {
                    readStr += rr.ReadLine();

                }
                parametricRecord = JsonDeserialize<ParametricRecord>(readStr.Trim());
                rr.Close();
                fs.Close();
                return Error_OK;
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                return Error_Failed;
            }
        }
    }
}
