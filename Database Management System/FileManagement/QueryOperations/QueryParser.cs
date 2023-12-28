using Database_Management_System.Logger;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public static class QueryParser
    {
        public static Query CreateQuery(string src)
        {
            if (StringFormatter.IsNullOrEmpty(src))
            {
                throw new ArgumentException(MessageLogger.NullOrEmptyString());
            }

            src = StringFormatter.ClearInput(src);
            string query = StringFormatter.Substring(src, 0, ' ');
            
            switch (query)
            {
                case Queries.create: return new Create(StringFormatter.Substring(src, query.Length + 1));
                case Queries.drop: return new Drop(StringFormatter.Substring(src, query.Length + 1));
                case Queries.info: return new TableInfo(StringFormatter.Substring(src, query.Length + 1));
                case Queries.SELECT: return new Select(StringFormatter.Substring(src, query.Length + 1));
                case Queries.insert: return new Insert(StringFormatter.Substring(src, query.Length + Queries.into.Length + 2));
                case Queries.delete: return new Delete(StringFormatter.Substring(src, query.Length + Queries.from.Length + 2));
                case Queries.list: return new ListTables();

                default: throw new Exception("Unknown query ;(");
            }
        }
    }
}
