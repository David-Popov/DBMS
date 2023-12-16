using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class TableInfo : Query
    {
        public TableInfo(string src) 
        { 
            _tableName = src;
        }

        public override void execute()
        {
            ColumnInfo[] infos = MetaHandler.ReadFile(_tableName);

            int padding = 0;
            foreach (ColumnInfo info in infos)
                padding = padding < info.getMaxLen() ? info.getMaxLen() : padding;

            foreach (ColumnInfo info in infos)
                Console.WriteLine(info.ToString(padding));
        }
    }
}
