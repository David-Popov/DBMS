using Database_Management_System.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if (src[i] == '\"')
                {
                    while (src[++i] != '\"')
                        ++i;
                }
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

            for (int i = index; i < src.Length - 1; ++i)
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
                    char c = src[i++];
                    sb.ConCat(c);
                    while (src[i] == ' ' || src[i] == c)
                        ++i;
                }
                if(src[i] == ',' || src[i] == ':' || src[i] == '(' || src[i] == ')')
                {
                    char c = src[i++];
                    sb.ConCat(c);
                    while (src[i] == ' ' || src[i] == c)
                        ++i;
                        
                }

                sb.ConCat(src[i]);
            }

            if (src[src.Length - 1] != ' ')
                sb.ConCat(src[src.Length - 1]);

            return sb.C_str;
        }

        private static string Substring(string src, ref int ind, char c)
        {
            StringBuilder sb = new StringBuilder();
            while (ind < src.Length && src[ind] != c)
            {
                if(src[ind] == '\"')
                {
                    sb.ConCat(src[ind++]);
                    while (src[ind] != '\"')
                        sb.ConCat(src[ind++]);
                }
                sb.ConCat(src[ind++]);
            }

            return sb.C_str;
        }

        public static string Substring(string src, int ind, char c)
        {
            StringBuilder sb = new StringBuilder();
            while (ind < src.Length && src[ind] != c)
            {
                if (src[ind] == '\"')
                {
                    sb.ConCat(src[ind++]);
                    while (src[ind] != '\"')
                        sb.ConCat(src[ind++]);
                }
                sb.ConCat(src[ind++]);
            }

            return sb.C_str;
        }

        public static string Substring(string src, int start, int? end = null)
        {
            StringBuilder sb = new StringBuilder();

            if (end == null)
                end = src.Length - 1;

            for (int i = start; i <= end; ++i)
                sb.ConCat(src[i]);

            return sb.C_str;
        }

        public static int IndexOf(string src, string toFind, int startPos = 0)
        {
            for (int i = startPos; i < src.Length - toFind.Length + 1; ++i)
            {
                if(Substring(src, i, i + toFind.Length - 1) == toFind)
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
            string first = Substring(src, 0, replacePos - 1);
            if (replacePos + replace.Length + 1 < src.Length)
            {
                string second = Substring(src, replacePos + replace.Length);
                return first + what + second;
            }

            return first + what;
        }

        public static string Trim(string src, char toTrim)
        {
            int beg = 0, end = src.Length - 1;
            while (src[beg] == toTrim)
                ++beg;
            while (src[end] == toTrim)
                --end;

            return Substring(src, beg, end);
        }

        public static string PadLeft(string src, int padding)
        {
            StringBuilder sb = new StringBuilder(src);
            sb += new StringBuilder(' ', padding);

            return sb.C_str;
        }

        public static string PadRight(string src, int padding)
        {
            StringBuilder sb = new StringBuilder(' ', padding);
            sb += new StringBuilder(src);

            return sb.C_str;
        }

        public static string FixedPrint(string src, int padding)
        {
            if (src.Length % 2 == 1)
                return PadRight(PadLeft(src, padding), padding + 1);
            return PadRight(PadLeft(src, padding), padding);
        }
    }
}
