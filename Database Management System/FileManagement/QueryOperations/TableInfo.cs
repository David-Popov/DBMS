using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Database_Management_System.String;
using Database_Management_System.Validators.Constants;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class TableInfo : Query
    {
        public TableInfo(string src) 
        { 
            _tableName = src;
        }

        public override void Execute()
        {
            int rowSize = 0, rowCount = 0;
            ColumnInfo[] infos = MetaHandler.ReadFile(_tableName, out rowSize, out rowCount);

            int padding = 0;
            foreach (ColumnInfo info in infos)
            {
                int maxLen = info.GetMaxPrintLen();
                padding = padding < maxLen ? maxLen : padding;
            }

            double columnPadding = Math.Ceiling(((double)(padding - Utility.columnName.Length)) / 2 + 1);
            double typePadding = Math.Ceiling(((double)(padding - Utility.type.Length)) / 2 + 1);
            double deafultPadding = Math.Ceiling(((double)(padding - Utility.defaultValue.Length)) / 2 + 1);

            Console.WriteLine($"{Utility.tableName}: {_tableName}\n");
            Console.Write($"|{StringFormatter.FixedPrint(Utility.columnName, (int)columnPadding)}|");
            Console.Write($"{StringFormatter.FixedPrint(Utility.type, (int)typePadding)}|");
            Console.Write($"{StringFormatter.FixedPrint(Utility.defaultValue, (int)deafultPadding)}|");
            Console.WriteLine();

            Console.WriteLine(new string('-', (padding + 4) * infos.Length + 1));

            foreach (ColumnInfo info in infos)
                Console.WriteLine(info.ToString(padding));

            Console.WriteLine($"\nRecord count: {rowCount}");
            Console.WriteLine($"File size: {rowSize * rowCount} bytes");
        }
    }
}
