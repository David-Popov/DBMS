using Database_Management_System.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Algorythms
{
    public static class QuickSort
    {
        public static MyPair<T,N>[] Sort<T,N>(MyPair<T, N>[] pair, int start, int end, bool reverse) where T : IComparable<T> where N : IComparable<N>
        {
            if (reverse)
            {
                SortDataDesc(pair, start, end);
            }
            else
            {
                SortDataAsc(pair, start, end);
            }


            return pair;
        }

        private static void SortDataAsc<T,N>(MyPair<T, N>[] pair, int start, int end) where T : IComparable<T> where N : IComparable<N>
        {
            if (start < end)
            {
                int pivot = PartitionAsc(pair, start, end);

                if (pivot > 1)
                    SortDataAsc(pair, start, pivot - 1);

                if (pivot + 1 > end)
                    SortDataAsc(pair, start + 1, pivot);
            }
        }

        private static void SortDataDesc<T, N>(MyPair<T, N>[] pair, int start, int end) where T : IComparable<T> where N : IComparable<N>
        {
            if (start < end)
            {
                int pivot = PartitionDesc(pair, start, end);

                if (pivot > 1)
                    SortDataDesc(pair, start, pivot - 1);

                if (pivot + 1 > end)
                    SortDataDesc(pair, start + 1, pivot);
            }
        }

        private static int PartitionAsc<T,N>(MyPair<T, N>[] pair, int start, int end) where T : IComparable<T> where N : IComparable<N>
        {
            MyPair<T, N> temp;
            int pivotIdx = start++;
            MyPair<T, N> pivotValue = pair[pivotIdx];

            while (true)
            {
                while (start <= end && pair[start] < pivotValue)
                    start++;

                while (start <= end && pair[end] > pivotValue)
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

        private static int PartitionDesc<T, N>(MyPair<T, N>[] pair, int start, int end) where T : IComparable<T> where N : IComparable<N>
        {
            MyPair<T, N> temp;
            int pivotIdx = start++;
            MyPair<T, N> pivotValue = pair[pivotIdx];

            while (true)
            {
                while (start <= end && pair[start] > pivotValue)
                    start++;

                while (start <= end && pair[end] < pivotValue)
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
