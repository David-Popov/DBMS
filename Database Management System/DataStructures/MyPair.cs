using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class MyPair<T, N> : IComparable<MyPair<T, N>>
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

        public static MyPair<T, N> MakePair(T first, N second)
        {
            return new MyPair<T, N>(first, second);
        }

        public int CompareTo(MyPair<T, N>? other)
        {
            if (other == null)
                return 1; 

            int firstComparison = Comparer<T>.Default.Compare(First, other.First);
            if (firstComparison != 0)
                return firstComparison; 

            return Comparer<N>.Default.Compare(Second, other.Second);
        }
    }
}
