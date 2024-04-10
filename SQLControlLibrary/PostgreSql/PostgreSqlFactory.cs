using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLControlLibrary.SQL
{
    public class PostgreSqlFactory : SQLFactory
    {
        public override ISql CreateSql()
        {
            return new PostgreSqlImpl();
        }
    }
}
