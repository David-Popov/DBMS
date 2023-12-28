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
        public MyStack() 
        {
            _data = new T[1];
            _length = 0;
        }

        private void Resize()
        {
            T[] newData = new T[_data.Length * 2];
            for (int i = 0; i < _length; ++i)
                newData[i] = _data[i];
            _data = newData;
        }

        public T Peek()
        { 
            return _data[_length - 1];
        }

        public void Push(T value)
        {
            if (_length >= _data.Length)
                Resize();
            _data[_length++] = value;
        }

        public T Pop()
        {
            T top = Peek();
            --_length;
            return top;
        }

        public bool IsEmpty() => _length == 0;
    }
}
