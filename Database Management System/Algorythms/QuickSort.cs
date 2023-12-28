using Database_Management_System.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Algorythms
{
    public static class QuickSort<T>
    {
        public static MyPair<T, T>[] Sort(MyPair<T, T>[] pair, int start, int end)
        {
            SortData(pair, start, end);

            return pair;
        }

        private static void SortData(MyPair<T, T>[] pair, int start, int end)
        {
            if (start < end)
            {
                int pivot = Partition(pair, start, end);

                if (pivot > 1)
                    SortData(pair, start, pivot - 1);

                if (pivot + 1 > end)
                    SortData(pair, start + 1, pivot);
            }
        }

        private static int Partition(MyPair<T, T>[] pair, int start, int end)
        {
            MyPair<T, T> temp;
            int pivotIdx = start++;

            while (true)
            {

                while (start <= end && pair[start] < pair[pivotIdx])
                    start++;

                while (start <= end && pair[start] > pair[pivotIdx])
                    end--;

                if (start >= end)
                    break;
                else
                {
                    temp = pair[start];
                    pair[start] = pair[end];
                    pair[end] = temp;
                }
            }

            temp = pair[end];
            pair[end] = pair[pivotIdx];
            pair[pivotIdx] = temp;

            return end;
        }
    }
}
