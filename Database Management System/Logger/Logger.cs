using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Logger
{
    public class Logger
    {
        public static void LogError(string message, string methodName = "", string className = "")
        {
            using (FileStream stream = new FileStream($"{Utility.loggerFolderPath}-{DateTime.Now.Date}", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(stream);

                if (methodName == "" && className == "")
                {
                    writer.WriteLine($"LOGGED ERROR message: {message}");
                }
                else
                {
                    writer.WriteLine($"LOGGED ERROR in class {className} - method {methodName} -> message: {message}");
                }
            }
        }

        public static void LogDebug(string message, string methodName = "", string className = "")
        {
            using (FileStream stream = new FileStream($"{Utility.loggerFolderPath}-{DateTime.Now.Date}", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(stream);

                if (methodName == "" && className == "")
                {
                    writer.WriteLine($"LOGGED DEBUG message: {message}");
                }
                else
                {
                    writer.WriteLine($"LOGGED DEBUG in class {className} - method {methodName} -> message: {message}");
                }
            }
        }
    }
}
