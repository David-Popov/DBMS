using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class MyStack<T>
    {
        private T[] _data;
        private int _length;

        public T Top 
        { get { return _data[_length - 1]; } }

        private void resize()
        {
            T[] newData = new T[_data.Length * 2];
            for (int i = 0; i < _length; ++i)
                newData[i] = _data[i];
            _data = newData;
        }

        public MyStack() 
        {
            _data = new T[1];
            _length = 0;
        }

        public void Push(T value)
        {
            if (_length >= _data.Length)
                resize();
            _data[_length++] = value;
        }

        public T Pop()
        {
            T top = Top;
            --_length;
            return top;
        }

        public bool IsEmpty() => _length == 0;
    }
}
