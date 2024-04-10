using SMLogControlLibrary;
using SQLControlLibrary.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreControlLibrary
{
    public class SqlClass
    {
        ISql imysql;
        string DataBaseName = "Deple";

        public bool InitSql()
        {
            if (ConfigSql())
            {
                if (CreateSql())
                {
                    if (ConnectSql())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ConfigSql()
        {
            ConnectStrc connectStrc = new ConnectStrc();
            connectStrc.host = "localhost";
            connectStrc.port = 3306;
            connectStrc.username = "root";
            connectStrc.password = "deple123";
            connectStrc.database = "SmoreSqlite0818.db";

            imysql = new MySqlFactory().CreateSql();

            imysql.CONNECTSQL = connectStrc;
            EnumReturnVal iRes = imysql.Init();

            //log
            if (iRes == EnumReturnVal.ERROR_OK)
            {
                SMLogWindow.OutLog($"SqlInit:{iRes}", Color.Green, bshow: true);
                return true;
            }
            else
            {
                SMLogWindow.OutLog($"SqlInit:{iRes}", Color.Red, bshow: true);
                return false;
            }
        }
        private bool ConnectSql()
        {
            if (imysql.SelectDatabase(DataBaseName) == 0)
            {
                SMLogWindow.OutLog($"数据库{DataBaseName}:连接成功", Color.Green, bshow: true);
                //imysql.RegisterCallBack(funcToDelegate);
                return true;
            }
            else
            {
                SMLogWindow.OutLog($"数据库{DataBaseName}:连接失败", Color.Red, bshow: true);
                return false;
            }
        }
        private bool CreateSql()
        {
            bool bres = false;
            switch (imysql.CreateDataBase(DataBaseName))
            {
                case EnumReturnVal.ERROR_FAIL:
                    SMLogWindow.OutLog($"数据库{DataBaseName}:创建失败", Color.Red, bshow: true);

                    break;
                case EnumReturnVal.ERROR_OK:
                    SMLogWindow.OutLog($"数据库{DataBaseName}:创建成功", Color.Green, bshow: true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"数据库{DataBaseName}:创建成功");
                    bres = true;
                    break;
                case EnumReturnVal.ERROR_EXIT:
                    SMLogWindow.OutLog($"数据库{DataBaseName}:已存在", Color.Red, bshow: true);
                    bres = true;
                    break;
            }
            return bres;
        }
        public bool InsertData(string TableName, Dictionary<string, string> dicSqlData)
        {
            //判断
            if (TableName == "")
            {
                SMLogWindow.OutLog($"数据表{TableName}:连接失败", Color.Red, bshow: true);
                return false;
            }

            //主体
            //EnumReturnVal iRes = imysql.InsertData<string>(TableName, new Dictionary<string, string>()
            //{{"submission_date",$"{DateTime.Now.ToString("yyyy-MM-dd")} {DateTime.Now.ToString("HH:mm:ss")}"},
            //   {"runoob_author","456"},{ "runoob_title","123"}});

            EnumReturnVal iRes = imysql.InsertData<string>(TableName, dicSqlData);

            //log
            if (iRes == EnumReturnVal.ERROR_OK)
            {
                SMLogWindow.OutLog($"{DataBaseName}:{TableName}:InsertData:{iRes}", Color.Green, bshow: true);
                return true;
            }
            else
            {
                SMLogWindow.OutLog($"{DataBaseName}:{TableName}:InsertData:{iRes}", Color.Red, bshow: true);
                return false;
            }
        }

        public void ReadAllSqlData(string TableName)
        {
            EnumReturnVal iRes = imysql.ReadData(TableName);

            //log
            if (iRes == EnumReturnVal.ERROR_OK)
            {
                SMLogWindow.OutLog($"{DataBaseName}:{TableName}:ReadSqlAllData:{iRes}", Color.Green, bshow: true);
            }
            else
            {
                SMLogWindow.OutLog($"{DataBaseName}:{TableName}:ReadSqlAllData:{iRes}", Color.Red, bshow: true);
            }
        }

        public void RegisterCallBack(delegateFunc<DataTable> func)
        {
            imysql.RegisterCallBack(func);
        }

        private void funcToDelegate(DataTable dtdata)
        {
            int x = 0;
            //dtdata.Columns.RemoveAt(0);
            //if (m_SMDataGridViewShow != null)
            //{
            //    Invoke((MethodInvoker)delegate
            //    {
            //        m_SMDataGridViewShow.InitData(dtdata);
            //    });
            //    //}
            //    //smDataGridViewShow2.dgvInfo.DataSource = data.Tables[0].DefaultView;
            //}
        }
    }
}
