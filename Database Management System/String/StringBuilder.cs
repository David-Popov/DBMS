﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.String
{
    public class StringBuilder
    {
        private char[] _data;
        private int _length;

        public int Length 
        { get { return _length; } }
        public string C_str
        { get { return new string(_data, 0, _length); } }

        private StringBuilder(StringBuilder other)
        {
            _data = other._data;
            _length = other._length;
        }

        private void resize(int size)
        {
            char[] newData = new char[(_data.Length + size) * 2];
            for (int i = 0; i < _length; ++i)
                newData[i] = _data[i];
            _data = newData;
        }

        public StringBuilder()
        {
            _data = new char[1];
            _length = 0;
            _data[_length] = '\0';
        }

        public StringBuilder(string s)
        {
            _data = new char[s.Length * 2];
            for (int i = 0; i < s.Length; ++i)
                _data[i] = s[i];
            _length = s.Length;
            _data[_length] = '\0';
        }

        public void ConCat(string s)
        {
            if (_data.Length <= _length + s.Length)
                resize(s.Length);

            for (int i = 0; i < s.Length; ++i)
                _data[_length + i] = s[i];
            _length += s.Length;
            _data[Length] = '\0';
        }

        public void ConCat(char c)
        {
            if (_data.Length <= _length + 1)
                resize(1);

            _data[_length++] = c;
            _data[_length] = '\0';
        }

        public static StringBuilder operator +(StringBuilder sb, string s)
        {
            StringBuilder sbCopy = new StringBuilder(sb);
            sbCopy.ConCat(s);
            return sbCopy;
        }

        public static StringBuilder operator +(StringBuilder sb, char c)
        {
            StringBuilder sbCopy = new StringBuilder(sb);
            sbCopy.ConCat(c);
            return sbCopy;
        }

        public static StringBuilder operator +(StringBuilder sb, StringBuilder other)
        {
            StringBuilder sbCopy = new StringBuilder(sb);
            sbCopy.ConCat(other.C_str);
            return sbCopy;
        }
    }
}