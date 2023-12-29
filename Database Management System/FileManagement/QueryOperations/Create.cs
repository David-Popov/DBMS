using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Create : Query
    {
        ColumnInfo[] columnsInfos;

        public Create(string src) 
        {
            _tableName = StringFormatter.Substring(src, 0, '(');
            var columns = StringFormatter.Split(
                          StringFormatter.Substring(
                          src, StringFormatter.IndexOf(src, "(") + 1, src.Length - 2), ',');

            columnsInfos = new ColumnInfo[columns.Length];
            for(int i = 0; i < columns.Length; ++i)
            {
                string name, type;
                bool hasDefault = false;
                string defaultValue = string.Empty;

                if (StringFormatter.IndexOf(columns[i], "default") != -1)
                {
                    hasDefault = true;
                    var defautSplitted = StringFormatter.Split(columns[i]);
                    defaultValue = defautSplitted[2];

                    var getNameAndType = StringFormatter.Split(defautSplitted[0], ':');
                    name = getNameAndType[0]; 
                    type = getNameAndType[1];

                    if (type == Utility.typeString || type == Utility.typeDateTime)
                        defaultValue = StringFormatter.Trim(defaultValue, '\"');
                }
                else {
                    var getNameAndType = StringFormatter.Split(columns[i], ':');
                    name = getNameAndType[0];
                    type = getNameAndType[1];
                }

                columnsInfos[i] = new ColumnInfo(name, type, hasDefault, defaultValue);
            }
        }

        public override void Execute()
        {
            MetaHandler.CreateFile(_tableName, columnsInfos);
            Console.WriteLine($"Table {_tableName} successfully created.");
        }
    }
}
