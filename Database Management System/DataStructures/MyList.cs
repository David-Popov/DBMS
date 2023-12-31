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
        public int Length { get { return _length; } }

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

        private void Resize()
        {
            T[] newData = new T[_data.Length * 2];
            for (int i = 0; i < _length; ++i)
                newData[i] = _data[i];
            _data = newData;
        }

        public MyList<T> Add(T value)
        {
            if (_length >= _data.Length)
                Resize();

            _data[_length++] = value;
            return this;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _length)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _length - 1; ++i)
                _data[i] = _data[i + 1];

            --_length;
        }

        public void Remove(T value)
        {
            if (value is null)
                return;

            int index = IndexOf(value);

            if (index >= 0)
                RemoveAt(index);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _length; ++i)
            {
                if (Equals(_data[i], item))
                    return i;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _length; ++i)
            {
                if (Equals(_data[i], item))
                    return true;
            }
            return false;
        }

        public T[] ToArray()
        {
            var data = new T[_length];
            Array.Copy(_data, data, _length);
            return data;
        }

        public void Clear()
        {
            _length = 0;
        }

        public void PopBack()
        {
            --_length;
        }

        public T this[int index]
        { get { return _data[index]; } set { _data[index] = value; } }

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
