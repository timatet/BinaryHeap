using System;
using System.Collections.Generic;

namespace BinaryHeapLib
{
    public class ComparerMaxHeap<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x);
        }
    }
    public class ComparerMinHeap<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}
