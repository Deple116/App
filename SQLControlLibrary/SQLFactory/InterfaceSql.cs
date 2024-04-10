using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections.Concurrent;
using SQLControlLibrary.SQL;

namespace SQLControlLibrary.SQL
{



    public interface ISql
    {
        ConnectStrc CONNECTSQL { get; set; }
        string CONNECTSQLSTR { get; set; }


        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <returns></returns>
        EnumReturnVal Init();

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        EnumReturnVal CreateDataBase(string databaseName);

        /// <summary>
        /// 删除指定数据库
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        EnumReturnVal DeleteDataBase(string databaseName);

        /// <summary>
        /// 选择数据库
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        EnumReturnVal SelectDatabase(string databaseName);

        /// <summary>
        /// 删除指定数据表
        /// </summary>
        /// <returns></returns>
        EnumReturnVal DeleteTable(string TableName);

        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <param name="strInsert"></param>
        /// <returns></returns>
        EnumReturnVal InsertData<T>(string sqlSheet, Dictionary<string, T> dicData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlSheet">数据表名称</param>
        /// <param name="dicdata"></param>
        /// <returns></returns>
        EnumReturnVal DeleteData(string sqlSheet, Dictionary<string, SqlCmdInfo> dicdata = null);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sqlSheet">数据表名称</param>
        /// <param name="RequireSql">判断条件</param>
        /// <returns></returns>
        EnumReturnVal DeleteData(string sqlSheet, string RequireSql);

       /// <summary>
       /// 更新数据库
       /// </summary>
       /// <param name="RequireSql"></param>
       /// <returns></returns>
        EnumReturnVal UpdateData(string RequireSql);

        EnumReturnVal UpdateData(string sqlSheet, SqlJudgeCmdInfo sqljudgecmdinfo, Dictionary<string, string> dicdata = null);

        /// <summary>
        ///  读数据库，返回DataTable格式的数据
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        EnumReturnVal ReadData(string sqlSheet, string[] arr);
        EnumReturnVal ReadData(string RequireSql);

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns></returns>
        EnumReturnVal CloseDatabase();

        /// <summary>
        /// 注册回调
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        EnumReturnVal RegisterCallBack(delegateFunc<DataTable> func);
    }
}
