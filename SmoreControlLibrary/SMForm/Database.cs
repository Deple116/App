using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections.Concurrent;

namespace SmoreControlLibrary.SMForm
{
    interface Database
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        int ConnectToDatabase(string mdbPath);

        /// <summary>
        /// 读数据库，返回DataTable格式的数据
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        int ReadData(string strSql, ref DataTable dt);

        /// <summary>
        ///  读数据库，返回DataTable格式的数据
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        DataTable ReadData(string strSQL);

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="strUpdate"></param>
        /// <returns></returns>
        int UpdateData(string strUpdate);

        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <param name="strInsert"></param>
        /// <returns></returns>
        int InsertData(string strInsert);

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns></returns>
        int CloseDatabase();

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="strDelete"></param>
        /// <returns></returns>
        int DeleteData(string strDelete);

        /// <summary>
        /// 增删改查数据库
        /// </summary>
        /// <returns></returns>
        int ReviseData();

        /// <summary>
        /// 写数据库
        /// </summary>
        /// <param name="_keyValuePairs"></param>
        /// <returns></returns>
        int WriteDatabase(ConcurrentDictionary<string, string> _keyValuePairs);

        /// <summary>
        /// 导出Access到excel
        /// </summary>
        void AccessToExcel();

        /// <summary>
        /// 按照结果查询将Access导出到excel
        /// </summary>
        /// <param name="result">"False" "True" </param>
        void AccessToExcel(string result);

        /// <summary>
        /// 按照时间间隔将Access数据导出到excel
        /// </summary>
        void AccessToExcelByTime();
        
    }
}
