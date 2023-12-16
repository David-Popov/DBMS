using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators.Constants
{
    public static class LogicOperators
    {
        public const string lt = "<";
        public const string mt = ">";
        public const string eq = "<>";

        public const string and = "AND";
        public const string not = "NOT";
        public const string or = "OR";

        public static bool IsLogicOperator(string src)
        {
            if(src != lt && src != mt && src != eq &&
               src != and && src != not && src != or)
                return false;

            return true;
        }
    }
}
