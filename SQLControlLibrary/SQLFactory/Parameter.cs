using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLControlLibrary.SQL
{


    public delegate void delegateFunc<T>(T para);

    public enum EnumReturnVal
    {
        //字符串:1.EnumReturnVal.ERROR_NOTEXIT.ToString();2.Enum.GetName(typeof(EnumReturnVal), i);
        //数字:(int)EnumReturnVal.ERROR_OK
        ERROR_NOTEXIT = -3,
        ERROR_EXIT = -2,
        ERROR_FAIL =-1,
        ERROR_OK =0,
    }


    public enum EnumType
    {
        and,
        or
    }

    public enum EnumFieldType
    {
        INT,
        TEXT,
        DATETIME,
        DATE,
        DOUBLE,
        FALOAT,
        VARCHAR_50,
        VARCHAR_100


    }

    public struct SqlCmdInfo
    {
        public string value1;
        public string value2;
        public EnumType cmd;
    }

    public struct SqlJudgeCmdInfo
    {
        public string key;
        public string value1;
        public string value2;

    }

    public struct SqlCmdUpdateInfo
    {
        public string value;
        public SqlCmdInfo sqlcmdinfo;

    }

    public class ConnectStrc
    {
        public string host=null;
        public uint port = 0;
        public string username = null;
        public string password = null;
        public string database = null;
        public string tablename = null;
    }


}
