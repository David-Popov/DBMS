using Database_Management_System.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators.Constants
{
    public static class Utility
    {
        public static string currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string metaFolderPath = currentDirectory + "\\Files\\MetaFiles\\";
        public static string filesFolderPath = currentDirectory + "\\Files\\TableFiles\\";
        public static string loggerFolderPath = currentDirectory + "\\Log";

        public const string metaExtention = "Meta_";
        public const string tablesFile = "Meta_TableNames";
        public const string tableName = "TableName";

        public const string typeString = "string";
        public const string typeInt = "int";
        public const string typeDateTime = "date";

        public const string columnName = "Column Name";
        public const string type = "Type";
        public const string defaultValue = "Default Value";

        public const int sizeString = 255;
        public const int sizeInt = sizeof(int);
        public const int sizeDateTime = 255;

        public static bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool isSpecialCharacter(char c)
        {
            return LogicOperators.isOperator(c) || c == '(' || c == ')' || c == '\"';
        }
    }
}
