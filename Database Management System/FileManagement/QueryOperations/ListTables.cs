using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Database_Management_System.Validators.Constants;
using Database_Management_System.String;
using System.Reflection.Emit;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class ListTables : Query
    {
        public override void Execute()
        {
            string[] paths = Directory.GetFiles(Utility.filesFolderPath);
            string[] fileNames = new string[paths.Length];
            for(int i = 0; i < paths.Length; ++i)
                fileNames[i] = StringFormatter.Substring(Path.GetFileName(paths[i]), 0, '.');

            int maxPadding = 0;
            foreach (var table in fileNames) 
                maxPadding = maxPadding < table.Length ? table.Length : maxPadding;
            maxPadding = maxPadding < Utility.tableName.Length ? Utility.tableName.Length : maxPadding;

            double padding = Math.Ceiling(((double)(maxPadding - Utility.tableName.Length)) / 2 + 1);
            Console.WriteLine($"|{StringFormatter.FixedPrint(Utility.tableName, (int)padding)}|");
            Console.WriteLine(new string('-', maxPadding + 5));
            foreach (var table in fileNames)
            {
                double namePadding = Math.Ceiling(((double)(maxPadding - table.Length)) / 2 + 1);
                Console.WriteLine($"|{StringFormatter.FixedPrint(table, (int)namePadding)}|");
            }

            Console.WriteLine($"Returned {fileNames.Length} tables.");
        }
    }
}
