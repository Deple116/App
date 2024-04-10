using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreControlLibrary.EquipmentDriver
{
    public interface PLCInterface
    {
        /// <summary>
        /// 初试化设备
        /// </summary>
        /// <returns></returns>
        int Initial();
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        int DeInitial();
        /// <summary>
        /// 写数据
        /// </summary>
        /// <returns></returns>
        int WriteValue(string dbAddress, object value);
        /// <summary>
        /// 读数据
        /// </summary>
        /// <returns></returns>
        int ReadValue(string dbAddress, ref object value);
    }
}
