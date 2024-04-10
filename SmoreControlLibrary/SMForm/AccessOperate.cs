using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace SmoreControlLibrary.SMForm
{
    public struct DatabasePara
    {
        public static string m_strPath = @".\Database\History.mdb";
        public static Queue<string> m_quRevise = new Queue<string>();
        public static AccessOperate database = new AccessOperate();
    }

    public class TimePoint
    {
        private DateTime XTime;
        private double YValue;

        public DateTime X
        {
            set { XTime = value; }
            get { return XTime; }
        }

        public double Y
        {
            set { YValue = value; }
            get { return YValue; }
        }

        public TimePoint(DateTime dt, double y)
        {
            XTime = dt;
            YValue = y;
        }
    }

    public class AccessOperate : Database
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAIL = -1;

        private string m_strLastError = "";
        private bool m_bConnectSuccess = false;
        private OleDbConnection odcConnection = null;

        private int m_nKeyToAccess = 0;
        private int m_nRightKeyToAccess = 0;
        private ManualResetEvent m_eventAccessAvilable = new ManualResetEvent(true);

        public int ConnectToDatabase(string mdbPath)
        {
            try
            {
                if (!File.Exists(mdbPath))
                {
                    m_bConnectSuccess = false;
                    return ERROR_FAIL;
                }
                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbPath;
                //string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath;//32位
                odcConnection = new OleDbConnection(strConn);
                if (odcConnection.State == ConnectionState.Open)
                {
                    m_bConnectSuccess = true;
                    return ERROR_FAIL;
                }
                odcConnection.Open();
                m_bConnectSuccess = true;
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_bConnectSuccess = false;
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int ReadData(string strSql, ref DataTable dt)
        {
            if (!m_bConnectSuccess)
                return ERROR_FAIL;
            if (strSql == null)
                return ERROR_FAIL;
            try
            {
                DataRow dr;
 
                OleDbCommand odCommand = odcConnection.CreateCommand();

                odCommand.CommandText = strSql;

                OleDbDataReader odrReader = odCommand.ExecuteReader();

                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] =
                        odrReader[odrReader.GetName(i)].ToString();
                    }
                    dt.Rows.Add(dr);
                }

                odrReader.Close();

                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int UpdateData(string strUpdate)
        {
            if (!m_bConnectSuccess)
                return ERROR_FAIL;

            try
            {
                string sql = strUpdate;
                OleDbCommand cmd = new OleDbCommand(strUpdate, odcConnection);
                cmd.ExecuteNonQuery();

                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int InsertData(string strInsert)
        {
            if (!m_bConnectSuccess)
                return ERROR_FAIL;

            try
            {
                string sql = strInsert;
                OleDbCommand cmd = new OleDbCommand(strInsert, odcConnection);
                cmd.ExecuteNonQuery();
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int DeleteData(string strDelete)
        {

            if (!m_bConnectSuccess)
                return ERROR_FAIL;

            try
            {
                string sql = strDelete;
                OleDbCommand cmd = new OleDbCommand(strDelete, odcConnection);
                cmd.ExecuteNonQuery();
                return ERROR_OK;
            }
            catch (System.Exception e)
            {
                m_strLastError = e.Message;
                return ERROR_FAIL;
            }
        }

        public int ReviseData(string strSql)
        {
            if (!m_bConnectSuccess)
                return ERROR_FAIL;
            try
            {
                string sql = strSql;
                OleDbCommand cmd = new OleDbCommand(strSql, odcConnection);
                cmd.ExecuteNonQuery();
                return ERROR_OK;
            }
            catch (System.Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int CloseDatabase()
        {
            try
            {
                odcConnection.Close();
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public int ShowDataTableToList(DataTable dt, ListView list, int nPageSize = 100)
        {
            DataTable dtTemp = dt.Clone();
            return ERROR_OK;
        }

        public DataTable ReadData(string strSQL)
        {
            int returnValue = 0;
            int nKey = m_nKeyToAccess++;
            while (nKey != m_nRightKeyToAccess)
            {
                Thread.Sleep(100);
            }
            try
            {
                lock (DatabasePara.database)
                {
                    DataTable dt = new DataTable();

                    returnValue = DatabasePara.database.ConnectToDatabase(DatabasePara.m_strPath);
                    if (returnValue != ERROR_OK)
                    {
                        MessageBox.Show("连接数据库失败\r\n地址:" + DatabasePara.m_strPath + "\r\n异常：" + DatabasePara.database.m_strLastError, "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return null;
                    }

                    returnValue = DatabasePara.database.ReadData(strSQL, ref dt);
                    if (returnValue != ERROR_OK)
                    {
                        MessageBox.Show("查询数据库失败\r\n語句:" + strSQL + "\r\n异常：" + DatabasePara.database.m_strLastError, "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        m_nRightKeyToAccess++;
                        return null;
                    }
                    else
                    {
                        DatabasePara.database.CloseDatabase();
                        m_nRightKeyToAccess++;
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                m_nRightKeyToAccess++;
                m_strLastError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 执行数据库增、删、改功能
        /// </summary>
        public int ReviseData()
        {
            int returnValue = 0;
            try
            {
                lock (DatabasePara.database)
                {
                    returnValue = DatabasePara.database.ConnectToDatabase(DatabasePara.m_strPath);
                    if (returnValue != ERROR_OK)
                    {
                        MessageBox.Show("连接数据库失败\r\n語句:" + DatabasePara.m_strPath + "\r\n异常：" + DatabasePara.database.m_strLastError, "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return ERROR_FAIL;
                    }

                    while (DatabasePara.m_quRevise.Count > 0)
                    {
                        string strRevise = DatabasePara.m_quRevise.Dequeue();

                        returnValue = DatabasePara.database.ReviseData(strRevise);
                        if (returnValue != ERROR_OK)
                        {
                            MessageBox.Show("数据库操作失败\r\n語句:" + strRevise + "\r\n异常：" + DatabasePara.database.m_strLastError, "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            return ERROR_FAIL;
                        }

                        Thread.Sleep(100);
                    }

                    DatabasePara.database.CloseDatabase();
                    return ERROR_OK;
                }
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }


        public int WriteDatabase(ConcurrentDictionary<string, string> _keyValuePairs)
        {
            try
            {
                string insertstr = "INSERT INTO tHistory(" +
                    "測試時間," +
                    "建模照片," +
                    "料號," +
                    "批號," +
                    "模型版本," +
                    "程序版本," +
                    "配方版本," +
                    "正面PAD," +
                    "正面SM," +
                    "背面PAD," +
                    "背面SM," +
                    "生產條數," +
                    "照片總數," +
                    "AI判斷為不允收張數," +
                    "篩檢張數," +
                    "漏檢張數," +
                    "篩檢率," +
                    "逃漏率PPM," +
                    "狀態) VALUES ('";
                insertstr += DateTime.Now + "', '";
                insertstr += "思謀" + "','";
                insertstr += _keyValuePairs["料號"] + "','";
                insertstr += _keyValuePairs["批號"] + "','";
                insertstr += _keyValuePairs["模型版本"] + "','";
                insertstr += _keyValuePairs["程序版本"] + "','";
                insertstr += _keyValuePairs["配方版本"] + "','";
                insertstr += _keyValuePairs["正面PAD"] + "','";
                insertstr += _keyValuePairs["正面SM"] + "','";
                insertstr += _keyValuePairs["背面PAD"] + "','";
                insertstr += _keyValuePairs["背面SM"] + "','";
                insertstr += _keyValuePairs["生產條數"] + "','";
                insertstr += _keyValuePairs["照片總數"] + "','";
                insertstr += _keyValuePairs["AI判斷為不允收張數"] + "','";
                insertstr += _keyValuePairs["篩檢張數"] + "','";
                insertstr += _keyValuePairs["漏檢張數"] + "','";
                insertstr += _keyValuePairs["篩檢率"] + "','";
                insertstr += _keyValuePairs["逃漏率PPM"] + "','";
                insertstr += _keyValuePairs["狀態"] + "')";
                DatabasePara.m_quRevise.Enqueue(insertstr);
                int returnValue = ReviseData();
                if (returnValue != ERROR_OK)
                {
                    return ERROR_FAIL;
                }
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                return ERROR_FAIL;
            }
        }

        public void AccessToExcel()
        {
            OleDbConnection con = new OleDbConnection();
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = ("Excel 文件(*.xls)|*.xls");//指定文件后缀名为Excel 文件。  
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFile.FileName;
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);//如果文件存在删除文件。  
                    }
                    int index = filename.LastIndexOf("//");//获取最后一个/的索引  
                    filename = filename.Substring(index + 1);//获取excel名称(新建表的路径相对于SaveFileDialog的路径)  
                    string sql = "select top 65535 *  into   [Excel 8.0;database=" + filename + "].[数据信息] from tHistory";
                    con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "//History.mdb";
                    OleDbCommand com = new OleDbCommand(sql, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    MessageBox.Show("导出数据成功!", "导出数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void AccessToExcel(string result)
        {
            OleDbConnection con = new OleDbConnection();
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = ("Excel 文件(*.xls)|*.xls");  
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFile.FileName;
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);  
                    }
                    int index = filename.LastIndexOf("//");
                    filename = filename.Substring(index + 1);
                    string sql = $"select top 65535 *  into   [Excel 8.0;database=" + filename + $"].[数据信息] from tHistory where Pass = {result} ";
                    con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "//History.mdb";
                    OleDbCommand com = new OleDbCommand(sql, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    MessageBox.Show("导出数据成功!", "导出数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void AccessToExcelByTime()
        {
            throw new NotImplementedException();
        }
    }
}
