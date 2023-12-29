using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public class Drop : Query
    {
        public Drop(string tableName) 
        { 
            _tableName = tableName;
        }

        public override void Execute()
        {
            File.Delete($"{Utility.metaFolderPath}{Utility.metaExtention}{_tableName}.bin");
            File.Delete($"{Utility.filesFolderPath}{_tableName}.bin");

            Console.WriteLine($"Successfully deleted table {_tableName}");
        }
    }
}
