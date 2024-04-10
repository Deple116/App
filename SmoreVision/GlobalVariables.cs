using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SmoreVision
{
    public class GlobalVariables
    {
        public static class GConst
        {
            //工位数量
            public const int STATION_COUNT = 2;
            public const int IMAGE_WINDOW_COUNT = 2;

            public const string IMAGE_SAVE_BASE_TIME_FORMAT = "HHmmss";
           
        }

        public static class Variables
        {
            public static bool bSpecial=false;
            public static bool bAlgo = false;
            public static bool bManual = false;
            public static string[] dateMsg = null;

            public static Dictionary<string, string[]> dicProduct = new Dictionary<string, string[]>();

            public static string strCurrModel;
            public static string PlcResult;
        }


        public static class Product
        {
            //产品型号
            public static string ProductModel = "";
            //产品组别
            public static string EquipmentNumber = "1";
            //模型版本
            public static string ModelVersion = "V1.0";
            //模型日期
            public static string ModelDate = "20220714";
            //当前批次号
            public static string BatchNumber = "1";
        }

        public class TimeCal
        {
          public static  Stopwatch swatch = new Stopwatch();

        }


    }

    public class DeepCopyHelper<T> where T : class, new()
    {
        /// <summary>
        /// 利用二进制序列化与反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>		
        public static T DeepCopyWithBinarySerialize(T obj)
        {
            object retval;
            //using 语句是在执行末尾释放ms
            //相当于执行ms.Dispose()
            //当然对象必须继承IDisposable 接口
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
    }
}
