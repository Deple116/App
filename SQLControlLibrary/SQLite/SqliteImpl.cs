using SQLControlLibrary.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using SMLogControlLibrary;

namespace SQLControlLibrary.SQL
{
    //public delegate void delegateFunc<T>(T para);

    public class SqliteImpl : ISql
    {
        public string m_strLastError = "";
        private bool m_bConnectSuccess = false;
        private SQLiteConnection m_connection = null;
        private SQLiteCommand m_command = null;
        private SQLiteDataReader m_dataReader = null;

        static object obj = new object();

        delegateFunc<DataTable> delegatefunc;

        ConnectStrc constrc;

        public SqliteImpl()
        {

        }

        ~SqliteImpl()
        {
            CloseDatabase();
        }

        #region Interface

        public ConnectStrc CONNECTSQL { get; set; }

        public string CONNECTSQLSTR { get; set; }

        public EnumReturnVal Init()
        {
            try
            {
                constrc = CONNECTSQL;
                CONNECTSQLSTR = constrc.database;

                //SQLiteConnection.CreateFile(CONNECTSQLSTR);
                return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal CreateDataBase(string databaseName)
        {
            try
            {
                constrc.database = databaseName;
                if (!File.Exists($"{constrc.database}.db")) SQLiteConnection.CreateFile($"{constrc.database}.db");
                m_connection = new SQLiteConnection($"data source={constrc.database}.db;Pooling = true;FailIfMissing = true");
                if (m_connection == null) return EnumReturnVal.ERROR_FAIL;
                //string sqlstr = "";

                //sqlstr = $".open {databaseName}.db";
                //return Excute(sqlstr);
                return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal DeleteDataBase(string databaseName)
        {
            try
            {
                ReleaseCommand();
                //if (m_connection == null) return EnumReturnVal.ERROR_FAIL;
                //string sqlstr = $"drop database if exists {databaseName}";
                //return Excute(sqlstr);
                if (File.Exists($"{databaseName}.db"))
                {
                    //m_connection.Close();
                    if (m_connection != null) m_connection.Dispose();
                    m_connection = null;
                    Thread.Sleep(1000);
                    File.Delete($"{databaseName}.db");
                }

                return EnumReturnVal.ERROR_OK;

            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Red);
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal SelectDatabase(string databaseName)
        {
            try
            {
                //$"server=localhost;port=3306;user=root;password=Smore2021@; database=mysql;Allow User Variables=True;SslMode=None"

                constrc.database = databaseName;
                CONNECTSQL = constrc;


                if (m_connection.State == ConnectionState.Closed)
                {
                    m_connection.Open();
                }
                //m_connection.ChangeDatabase(databaseName);



                ////使用Builder写语句，实现分段
                ////与数据库连接的信息
                //m_connectionStringBuilder builder = new m_connectionStringBuilder();
                ////数据库连接时的用户名，可以用pid
                //builder.UserID = "root";
                ////数据库连接时的密码，可以用pwd
                //builder.Password = CONNECTSQL.password;
                ////数据库连接时的服务器地址
                //builder.Server = "localhost";
                ////要连接的数据库
                //builder.Database = "data";


                return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal DeleteTable(string TableName)
        {
            try
            {
                if (m_connection == null) return EnumReturnVal.ERROR_FAIL;
                string sqlstr = "";

                sqlstr = $"drop table if exists {TableName}";
                return Excute(sqlstr);
                //return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal InsertData<T>(string sqlSheet, Dictionary<string, T> dicData)
        {
            try
            {
                // INSERT INTO runoob_tbl (runoob_title, runoob_author, submission_date) VALUES ("学习 PHP", "菜鸟教程", NOW()); 

                if (CreateTable(sqlSheet, dicData.Keys.ToArray()) == EnumReturnVal.ERROR_FAIL) return EnumReturnVal.ERROR_FAIL;

                string sqlstr = GetInsertContent(sqlSheet, dicData);
                SMLogWindow.OutLog($"sqlcontent:{sqlstr}", Color.Green);
                return Excute(sqlstr);
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }

        }

        public EnumReturnVal DeleteData(string sqlSheet, Dictionary<string, SqlCmdInfo> dicdata = null)
        {
            try
            {
                // delete from leftrunoob_tbl where runoob_id between 1205 and 1210;
                //DELETE FROM 表名 
                //WHERE 字段名1 BETWEEN 值1 AND 值2
                //AND 字段名2 BETWEEN 值3 AND 值4;

                //sqlstr = "delete from leftrunoob_tbl where runoob_id between 1211 and 1215";

                constrc.tablename = sqlSheet;
                string sqlstr = GetDeleteContent(sqlSheet, dicdata);
                SMLogWindow.OutLog($"database:{constrc.database}:tablename:{constrc.tablename}:sqlstr:{sqlstr}",Color.Green);
                return Excute(sqlstr);


            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"database:{constrc.database}:tablename:{constrc.tablename}:Error:{ex.ToString()}", Color.Red,loglevel:LogLevel.Error);
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal DeleteData(string sqlSheet, string RequireSql)
        {
            try
            {
                string sqlstr = $"delete from {sqlSheet} where {RequireSql}";
                return Excute(sqlstr);

            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal UpdateData(string sqlSheet, SqlJudgeCmdInfo sqljudgecmdinfo, Dictionary<string, string> dicdata = null)
        {
            try
            {
                //update leftrunoob_tbl set runoob_title='sm20230729'where runoob_id between 1278 and 1290;

                //sqlstr = "delete from leftrunoob_tbl where runoob_id between 1211 and 1215";

                string sqlstr = GetUpdateContent(sqlSheet, sqljudgecmdinfo, dicdata);
                SMLogWindow.OutLog($"database:{constrc.database}:tablename:{constrc.tablename}:sqlstr:{sqlstr}", Color.Green);
                return Excute(sqlstr);

            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"database:{constrc.database}:tablename:{constrc.tablename}:Error:{ex.ToString()}", Color.Red, loglevel: LogLevel.Error);
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal UpdateData(string RequireSql)
        {
            try
            {
                //update leftrunoob_tbl set runoob_title='sm20230729'where runoob_id between 1278 and 1290;

                return Excute(RequireSql);


            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal ReadData(string sqlSheet, string[] arr)
        {
            //SMLogWindow.OutLog($"ReadData:{strSql}",Color.Green);


            if (sqlSheet != null)
            {
                try
                {
                    //select * from runoob_tbl;




                    string strsql = GetReadsqlContent(sqlSheet, arr);
                    SMLogWindow.OutLog($"sqlcontent:{strsql}", Color.Green);
                    return Excute(strsql, true);




                    //int i = 0;
                    //object[] meta = new object[sdr.FieldCount];
                    //while (sdr.Read())
                    //{

                    //    sdr.GetValues(meta);
                    //    string content="";
                    //    foreach(var temp in meta)
                    //    {
                    //        if(temp.Equals(meta.Last()))
                    //        {
                    //            content += $"{temp}";
                    //        }
                    //        else
                    //        {
                    //            content += $"{temp};";
                    //        }

                    //    }                      
                    //    i++;
                    //    Debug.WriteLine($"index:{i}:{content}");
                    //}

                }
                catch (Exception ex)
                {
                    //SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                    // MessageBox.Show(ex.ToString());
                    return EnumReturnVal.ERROR_FAIL;
                }
            }
            return EnumReturnVal.ERROR_FAIL;
            //SMLogWindow.OutLog($"ReadData:{dt.Rows.Count}", Color.Green);

        }

        public EnumReturnVal ReadData(string RequireSql)
        {

            try
            {

                return Excute(RequireSql, true);


            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }

        }

        public EnumReturnVal CloseDatabase()
        {
            try
            {
                if (m_bConnectSuccess)
                {
                    m_connection.Close();
                    m_bConnectSuccess = false;
                    return EnumReturnVal.ERROR_OK;
                }
                else
                {
                    return EnumReturnVal.ERROR_OK;
                }
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        public EnumReturnVal RegisterCallBack(delegateFunc<DataTable> func)
        {
            try
            {
                delegatefunc = func;
                return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        #endregion

        #region Method

        private EnumReturnVal CreateTable(string tableName, string[] arr)
        {
            try
            {
                //CREATE TABLE IF NOT EXISTS `runoob_tbl`(
                //`runoob_id` INT UNSIGNED AUTO_INCREMENT,
                //`runoob_title` VARCHAR(100) NOT NULL,
                //`runoob_author` VARCHAR(40) NOT NULL,
                //`submission_date` DATE,
                // PRIMARY KEY( `runoob_id` )
                //)ENGINE = InnoDB DEFAULT CHARSET = utf8;

                string sqlstr = $"create table if not exists {tableName}(`ID` integer primary key AUTOINCREMENT not null,";

                foreach (string val in arr)
                {
                    //SplitFieldType(EnumFieldType.VARCHAR_50.ToString(), ref sqlstr, val, val.Equals(arr.Last()));
                    SplitFieldType(EnumFieldType.VARCHAR_50.ToString(), ref sqlstr, val);
                }



                sqlstr = sqlstr.Remove(sqlstr.Length - 1, 1) + $")";

                return Excute(sqlstr);


            }
            catch (Exception ex)
            {
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        /// <summary>
        /// 设置字段类型
        /// </summary>
        /// <param name="fieldType">字段类型</param>
        /// <param name="sqlstr">语法</param>
        /// <param name="val">字段名称</param>
        /// <param name="blast">是否是最后一个字段名称</param>
        private void SplitFieldType(string fieldType, ref string sqlstr, string val/*,bool blast=false*/)
        {
            string[] temp = fieldType.Split('_');
            if (temp.Length > 1)
            {

                sqlstr += $"`{val}` {temp[0]}({temp[1]}) not null,";

            }
            else
            {
                sqlstr += $"`{val}` {temp[0]} not null,";
            }
            //if (!blast)
            //{
            //    sqlstr += ",";
            //}

        }

        private string GetInsertContent<T>(string sqlSheet, Dictionary<string, T> dicData)
        {



            string sqlstr = $"insert into {sqlSheet}(";

            foreach (var key in dicData.Keys)
            {
                if (dicData[key].Equals(dicData.Last().Value))
                {
                    sqlstr += key;
                }
                else
                {
                    sqlstr += key + ",";
                }

            }
            sqlstr += ")values('";

            foreach (var val in dicData.Values)
            {
                if (val.Equals(dicData.Last().Value))
                {
                    sqlstr += val;
                }
                else
                {
                    sqlstr += val + "','";
                }
            }
            sqlstr += "')";
            ///SMLogWindow.OutLog($"insertstr:{insertstr}", Color.Green);
            return sqlstr;
        }

        private string GetDeleteContent(string sqlSheet, Dictionary<string, SqlCmdInfo> dicData)
        {

            string sqlstr = $"delete from {sqlSheet}";
            if (dicData != null)
            {
                sqlstr += $" where ";
                foreach (var key in dicData.Keys)
                {
                    //是否为最后一个元素
                    if (dicData[key].Equals(dicData.Last().Value))
                    {
                        sqlstr += key + $" between '{dicData[key].value1}' and '{dicData[key].value2}'";
                    }
                    else
                    {
                        sqlstr += key + $" between '{dicData[key].value1}' and '{dicData[key].value2}' {dicData[key].cmd} ";
                    }
                }
            }
            ///SMLogWindow.OutLog($"insertstr:{insertstr}", Color.Green);
            return sqlstr;
        }

        private string GetUpdateContent(string sqlSheet, SqlJudgeCmdInfo sqljudgecmdinfo, Dictionary<string, string> dicData)
        {

            string sqlstr = $"update {sqlSheet}";
            if (dicData != null)
            {
                sqlstr += $" set ";
                foreach (var key in dicData.Keys)
                {
                    //是否为最后一个元素
                    if (dicData[key].Equals(dicData.Last().Value))
                    {
                        sqlstr += key + $"='{dicData[key]}'";
                    }
                    else
                    {
                        sqlstr += key + $"='{dicData[key]}',";
                    }
                }
                sqlstr += $" where {sqljudgecmdinfo.key} between '{sqljudgecmdinfo.value1}' and '{sqljudgecmdinfo.value2}'";


            }
            ///SMLogWindow.OutLog($"insertstr:{insertstr}", Color.Green);
            return sqlstr;
        }

        private string GetReadsqlContent(string sqlSheet, string[] arr)
        {

            string sqlstr = "select ";
            if (arr == null)
            {
                sqlstr += "*";
            }
            else
            {
                foreach (var temp in arr)
                {

                    if (temp.Equals(arr.Last()))
                    {
                        sqlstr += $"{temp}";
                    }
                    else
                    {
                        sqlstr += $"{temp},";
                    }

                }
            }

            sqlstr += " from " + sqlSheet;
            ///SMLogWindow.OutLog($"insertstr:{insertstr}", Color.Green);
            return sqlstr;
        }

        private string GetTableContent(string[] ChartName, string Type = "")
        {
            string insertstr = $"CREATE TABLE  tHistory (ID Counter primary key, ";

            insertstr += "TimeEx Datetime null,";

            for (int i = 1; i < ChartName.Length; i++)
            {
                if (i != ChartName.Length - 1)
                {
                    if (ChartName[i].Contains("FAI") || ChartName[i].Contains("Number"))
                    {
                        //insertstr += $"{NewChartName[i]} string(100) null,";
                        insertstr += $"{ChartName[i]} double,";
                    }
                    else
                    {
                        insertstr += $"{ChartName[i]} text null,";
                    }
                }
                else
                {
                    if (ChartName[i].Contains("FAI") || ChartName[i].Contains("Number"))
                    {
                        insertstr += $"{ChartName[i]} double,TAG string(10) null)";
                        //insertstr += $"{ChartName[i]} string(100) null,TAG string(100) null)";
                    }
                    else
                    {
                        insertstr += $"{ChartName[i]} string(100) null,TAG string(10) null)";
                    }

                }
            }
            //SMLogWindow.OutLog($"insertstr:{insertstr}",Color.Green);
            return insertstr;
        }

        private EnumReturnVal GetTable(string name)
        {
            try
            {

                return EnumReturnVal.ERROR_FAIL;
            }
            catch (Exception ex)
            {
                //SMLogWindow.OutLog($"{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                m_bConnectSuccess = false;
                m_strLastError = ex.Message;
                return EnumReturnVal.ERROR_FAIL;
            }
        }

        private EnumReturnVal Excute(string sqlText, bool bReadSql = false)
        {
            try
            {
                lock (obj)
                {
                    ReleaseCommand();
                    if (m_connection == null) m_connection = new SQLiteConnection(CONNECTSQLSTR);
                    if (m_connection.State != ConnectionState.Open)
                    {
                        //SMLogWindow.OutLog($"odcConnection.State:{odcConnection.State}", Color.Green);
                        m_connection.Open();
                    }
                    if (m_command == null || m_command.Connection.State == ConnectionState.Closed) m_command = m_connection.CreateCommand();

                    m_command.CommandText = sqlText;

                    if (bReadSql)
                    {
                        SQLiteDataAdapter adap = new SQLiteDataAdapter(m_command);
                        DataTable ds = new DataTable();
                        adap.Fill(ds);
                        //dataGridView1.DataSource = ds.Tables[0].DefaultView;
                        m_dataReader = m_command.ExecuteReader();

                        delegatefunc(ds);
                        m_dataReader.Close();
                    }
                    else
                    {
                        int ireback = m_command.ExecuteNonQuery();

                        Debug.WriteLine($"query:{ireback}");
                        return ireback == -1 ? EnumReturnVal.ERROR_FAIL : EnumReturnVal.ERROR_OK;
                    }
                }
                return EnumReturnVal.ERROR_OK;
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green);
                return EnumReturnVal.ERROR_FAIL;
            }

        }

        /// <summary>
        /// Close Connection
        /// </summary>
        private void CloseConnection()
        {
            ReleaseCommand();

            if (m_connection != null)
            {
                try
                {
                    m_connection.Close();
                }
                catch (Exception  ex)
                {
                   
                }
                finally
                {
                    m_connection.Dispose();
                }
            }
            m_connection = null;

       
        }

        /// <summary>
        /// Release Comand and reader
        /// </summary>
        private void ReleaseCommand()
        {
            if (m_dataReader != null)
            {
                m_dataReader.Dispose();
            }
            m_dataReader = null;

            if (m_command != null)
            {
                m_command.Dispose();
            }
            m_command = null;
        }
        #endregion
    }
}
