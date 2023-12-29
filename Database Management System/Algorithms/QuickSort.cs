using Database_Management_System.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.Algorithms
{
    public static class QuickSort
    {
        public static T[] Sort<T>(T[] array, int start, int end, bool reverse) where T : IComparable<T>
        {
            if (reverse)
            {
                SortDataDesc(array, start, end);
            }
            else
            {
                SortDataAsc(array, start, end);
            }

            return array;
        }

        private static void SortDataAsc<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            if (start < end)
            {
                int pivot = PartitionAsc(array, start, end);
                SortDataAsc(array, start, pivot - 1);
                SortDataAsc(array, pivot + 1, end);
            }
        }

        private static void SortDataDesc<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            if (start < end)
            {
                int pivot = PartitionDesc(array, start, end);
                SortDataDesc(array, start, pivot - 1);
                SortDataDesc(array, pivot + 1, end);
            }
        }

        private static int PartitionAsc<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            T pivotValue = array[start];
            int left = start + 1;
            int right = end;

            while (true)
            {
                while (left <= right && array[left].CompareTo(pivotValue) <= 0)
                    left++;

                while (left <= right && array[right].CompareTo(pivotValue) > 0)
                    right--;

                if (left >= right)
                    break;

                T temp = array[left];
                array[left] = array[right];
                array[right] = temp;
            }

            array[start] = array[right];
            array[right] = pivotValue;

            return right;
        }

        private static int PartitionDesc<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            T pivotValue = array[start];
            int left = start + 1;
            int right = end;

            while (true)
            {
                while (left <= right && array[left].CompareTo(pivotValue) >= 0)
                    left++;

                while (left <= right && array[right].CompareTo(pivotValue) < 0)
                    right--;

                if (left >= right)
                    break;

                T temp = array[left];
                array[left] = array[right];
                array[right] = temp;
            }

            array[start] = array[right];
            array[right] = pivotValue;

            return right;
        }
    }
}
