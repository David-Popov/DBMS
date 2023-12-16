using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Insert : Query
    {
        private string[] columns;
        private string[] values;

        public Insert(string src)
        {
            _tableName = StringFormatter.Substring(src, 0, '(');

            int columnsEnd = StringFormatter.IndexOf(src, ")");
            columns = StringFormatter.Split(StringFormatter.Substring(
                      src, _tableName.Length + 1, columnsEnd - 1), ',');
            values = StringFormatter.Split(StringFormatter.Substring(
                      src, columnsEnd + Queries.values.Length + 2, 
                      StringFormatter.IndexOf(src, ")", columnsEnd 
                      + Queries.values.Length + 1) - 1), ',');
        }

        public override void execute()
        {
            //TODO
        }
    }
}
