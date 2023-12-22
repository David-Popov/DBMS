using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class MyList<T> : IEnumerable
    {
        private T[] _data;
        private int _length;
        
        public int Length 
        { get { return _length; } }

        public T[] Data
        { get { return _data; } }

        private void Resize()
        {
            T[] newData = new T[_data.Length * 2];
            for (int i = 0; i < _length; ++i)
                newData[i] = _data[i];
            _data = newData;
        }

        public MyList(MyList<T> other)
        {
            _data = other._data;
            _length = other._length;
        }

        public MyList()
        {
            _data = new T[1];
            _length = 0;
        }

        public MyList(int capacity)
        {
            _data = new T[capacity];
            _length = 0;
        }

        public MyList<T> Append(T value)
        {
            if (_length >= _data.Length)
                Resize();
            _data[_length++] = value;

            return this;
        }

        public T this[int index]
        { get { return _data[index]; } set { _data[index] = value;} }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _length; ++i)
                yield return _data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        { 
            return GetEnumerator();
        }
    }
}
