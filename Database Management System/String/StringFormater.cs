using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database_Management_System.String
{
    public static class StringFormatter
    {
        private static int Count(string src, char toFind)
        {
            int count = 0;
            for (int i = 0; i < src.Length; ++i)
            {
                if (src[i] == toFind)
                    ++count;
            }

            return count;
        }

        public static string ClearInput(string src)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;

            while (src[index] == ' ')
                ++index;

            for (int i = 0; i < src.Length - 1; ++i)
            {
                if (src[i] == '\"')
                {
                    sb.ConCat(src[i++]);
                    while (src[i] != '\"')
                        sb.ConCat(src[i++]);
                }
                while (src[i] == ' ' && src[i + 1] == ' ')
                    ++i;
                if(src[i] == ' ' && (src[i + 1] == ',' || src[i + 1] == ':' || src[i + 1] == '(' || src[i + 1] == ')'))
                {
                    ++i;
                    sb.ConCat(src[i++]);
                    while (src[i] == ' ')
                        ++i;
                }

                sb.ConCat(src[i]);
            }

            if (src[src.Length - 1] != ' ')
                sb.ConCat(src[src.Length - 1]);

            return sb.C_str;
        }

        public static string Substring(string src, ref int ind, char c)
        {
            StringBuilder sb = new StringBuilder();
            while (ind < src.Length && src[ind] != c)
                sb.ConCat(src[ind++]);

            return sb.C_str;
        }

        public static string Substring(string src, int ind, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = ind; i < count + ind; ++i)
                sb.ConCat(src[i]);

            return sb.C_str;
        }

        public static int IndexOf(string src, string toFind)
        {
            for (int i = 0; i < src.Length - toFind.Length + 1; ++i)
            {
                if(Substring(src, i, toFind.Length) == toFind)
                    return i;
            }

            return -1;
        }

        public static string[] Split(string src, char c = ' ') 
        {
            string[] arr = new string[Count(src, c) + 1];
            int index = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = Substring(src, ref index, c);
                ++index;
            }

            return arr;
        }

        public static string Replace(string src, string replace, string what)
        {
            int replacePos = IndexOf(src, replace);
            string first = Substring(src, 0, replacePos);
            if (replacePos + replace.Length + 1 < src.Length)
            {
                string second = Substring(src, replacePos + replace.Length, src.Length - (replacePos + replace.Length));
                return first + what + second;
            }

            return first + what;
        }
    }
}
