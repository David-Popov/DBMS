using Database_Management_System.DataStructures;
using Database_Management_System.Exceptions;
using Database_Management_System.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database_Management_System.String
{
    public static class StringFormatter
    {
        public static bool IsNullOrEmpty(string src)
        {
            return src is null || src == string.Empty || src.Length == 0;
        }

        private static int Count(string src, char toFind)
        {
            int count = 0;
            for (int i = 0; i < src.Length; ++i)
            {
                if (src[i] == '\"')
                {
                    while (src[i] != '\"')
                        ++i;
                }
                if (src[i] == toFind)
                    ++count;
            }

            return count;
        }

        public static string ClearInput(string src)
        {
            if (IsNullOrEmpty(src))
            {
                throw new MyException($"Passed string in method {nameof(ClearInput)} was null!!");
            }

            StringBuilder sb = new StringBuilder();
            int index = 0;

            while (src[index] == ' ')
                ++index;

            for (int i = index; i < src.Length - 1; ++i)
            {
                while (src[i] == ' ' && src[i + 1] == ' ')
                    ++i;
                if (src[i] == ' ' && (src[i + 1] == ',' || src[i + 1] == ':' || src[i + 1] == '(' || src[i + 1] == ')'))
                {
                    ++i;
                    char c = src[i++];
                    sb.ConCat(c);
                    while (src[i] == ' ' || src[i] == c)
                        ++i;
                }
                if (src[i] == ',' || src[i] == ':' || src[i] == '(' || src[i] == ')')
                {
                    char c = src[i++];
                    sb.ConCat(c);
                    while (src[i] == ' ' || src[i] == c)
                        ++i;

                }

                if (src[i] == '\"')
                {
                    sb.ConCat(src[i++]);
                    while (src[i] != '\"')
                        sb.ConCat(src[i++]);
                }

                sb.ConCat(src[i]);
            }

            if (src[src.Length - 1] != ' ')
                sb.ConCat(src[src.Length - 1]);

            return sb.C_str;
        }

        private static string Substring(string src, ref int ind, char c)
        {
            if (IsNullOrEmpty(src))
            {
                throw new MyException($"Passed string in method {nameof(Substring)} was null!!");
            }
           
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

        public static string Substring(string src, int ind, char c)
        {
            if (IsNullOrEmpty(src))
            {
                throw new MyException($"Passed string in method {nameof(Substring)} was null!!");
            }

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
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }
            else if (end is not null && start > end)
            {
                throw new InvalidOperationException(MessageLogger.StartIdxGreaterThanEndIdx());
            }

            StringBuilder sb = new StringBuilder();

            if (end == null)
                end = src.Length - 1;

            for (int i = start; i <= end; ++i)
                sb.ConCat(src[i]);

            return sb.C_str;
        }

        public static int IndexOf(string src, string toFind, int startPos = 0)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }
            else if (startPos < 0)
            {
                throw new MyException(MessageLogger.WrongStartPosition());
            }

            for (int i = startPos; i < src.Length - toFind.Length + 1; ++i)
            {
                if (Substring(src, i, i + toFind.Length - 1) == toFind)
                    return i;
            }

            return -1;
        }

        public static string[] Split(string src, char c = ' ')
        {
            MyList<string> arr = new MyList<string>();
            int index = 0;
            for (int i = 0; index < src.Length; ++i)
            {
                arr.Add(Substring(src, ref index, c));
                ++index;
            }

            return arr.ToArray();
        }

        public static string Replace(string src, string replace, string what)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            int replacePos = IndexOf(src, replace);
            string first = Substring(src, 0, replacePos - 1);
            if (replacePos + replace.Length + 1 < src.Length)
            {
                string second = Substring(src, replacePos + replace.Length);
                return first + what + second;
            }

            return first + what;
        }

        public static string ReplaceAll(string src, string replace, string what)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            while (IndexOf(src, replace) != -1)
                src = Replace(src, replace, what);

            return src;
        }

        public static string Replace(string src, string replace, char what)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            int replacePos = IndexOf(src, replace);
            string first = Substring(src, 0, replacePos - 1);
            if (replacePos + replace.Length + 1 < src.Length)
            {
                string second = Substring(src, replacePos + replace.Length);
                return first + what + second;
            }

            return first + what;
        }

        public static string Replace(string src, int pos, char what)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            string first = Substring(src, 0, pos - 1);
            if (pos + 1 < src.Length)
            {
                string second = Substring(src, pos + 1);
                return first + what + second;
            }

            return first + what;
        }

        public static string ReplaceAll(string src, string replace, char what)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            while (IndexOf(src, replace) != -1)
                src = Replace(src, replace, what);

            return src;
        }

        public static string Trim(string src, char toTrim)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            int beg = 0, end = src.Length - 1;
            while (src[beg] == toTrim)
                ++beg;
            while (src[end] == toTrim)
                --end;

            return Substring(src, beg, end);
        }

        public static string PadLeft(string src, int padding, char symbol = ' ')
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            StringBuilder sb = new StringBuilder(src);
            sb += new StringBuilder(symbol, padding);

            return sb.C_str;
        }

        public static string PadRight(string src, int padding, char symbol = ' ')
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            StringBuilder sb = new StringBuilder(symbol, padding);
            sb += new StringBuilder(src);

            return sb.C_str;
        }

        public static string FixedPrint(string src, int padding)
        {
            if (IsNullOrEmpty(src))
            {
                throw new ArgumentNullException(MessageLogger.NullOrEmptyString());
            }

            if (src.Length % 2 == 1)
                return PadRight(PadLeft(src, padding), padding + 1);
            return PadRight(PadLeft(src, padding), padding);
        }
    }
}
