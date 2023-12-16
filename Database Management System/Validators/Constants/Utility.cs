using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators.Constants
{
    public static class Utility
    {
        public const string metaExtention = "Meta_";
        public const string typeString = "string";
        public const string typeInt = "int";
        public const string typeDateTime = "date";

        public const string columnName = "Column Name";
        public const string type = "Type";
        public const string defaultValue = "Default Value";

        public const int sizeString = 255;
        public const int sizeInt = sizeof(int);
        public const int sizeDateTime = sizeof(int) * 3;
    }
}
