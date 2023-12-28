using Database_Management_System.DataStructures;
using Database_Management_System.String;
using Database_Management_System.Validators.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Validators
{
    public static class Validator
    {
        public static bool HasQuery(string src)
        {
            var arr = StringFormatter.Split(src);
            if (arr[0] == Queries.insert)
                arr[0] += arr[1];

            return Queries.isQuery(arr[0]);
        }

        public static bool HasValidBrackets(string src)
        {
            MyStack<char> brakets = new MyStack<char>();
            for (int i = 0; i < src.Length; ++i)
            {
                if (src[i] == '\"')
                {
                    while (src[++i] != '\"')
                    {
                        if (i == src.Length - 1)
                            return false;
                    }
                }

                if (src[i] == '(')
                    brakets.Push(src[i]);
                if (src[i] == ')')
                    brakets.Pop();
            }

            return brakets.IsEmpty();
        }


    }
}
