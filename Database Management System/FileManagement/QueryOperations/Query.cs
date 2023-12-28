using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.FileManagement.QueryOperations
{
    public abstract class Query
    {
        protected string _tableName;

        abstract public void Execute();
    }
}
