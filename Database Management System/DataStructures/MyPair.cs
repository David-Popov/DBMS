using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class MyPair<T, N>
    {
        public T? First { get; set; }
        public N? Second { get; set; }

        public MyPair(T? first, N? second)
        {
            First = first;
            Second = second;
        }

        public static bool operator<(MyPair<T, N> left, MyPair<T, N> right)
        {
            if (Comparer<T>.Default.Compare(left.First, right.First) == 0)
                return Comparer<N>.Default.Compare(left.Second, right.Second) < 0;
            return Comparer<T>.Default.Compare(left.First, right.First) < 0;
        }

        public static bool operator >(MyPair<T, N> left, MyPair<T, N> right) => !(left < right);
    }
}
