
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLControlLibrary.SQL
{
    public abstract class SQLFactory
    {
        public abstract ISql CreateSql();    
    }

}
