using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeapLib
{
    public class Node<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public int CompareTo(Node<TKey, TValue> other)
        {
            return this.Key.CompareTo(other.Key);
        }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    public class BinaryHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue>[] Data { get; set; }
        private IComparer<TKey> Comparer { get; set; }
        public int Count { get; private set; }
        private int Capacity { get; set; }

        private void IncreaseCapacity()
        {
            Capacity *= 2;
            var temp = new Node<TKey, TValue>[Capacity];
            Array.Copy(Data, temp, Count);
            Data = temp;
        }

        /// <summary>
        /// Удаляет и извлекает верхний элемент кучи.
        /// </summary>
        public Node<TKey, TValue> RemoveUp() // O(log n)
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }

            var First = Data[0];
            Data[0] = Data[--Count];
            HeapifyDown(0);

            return new Node<TKey, TValue>(First.Key, First.Value);
        }

        public Node<TKey, TValue> PeekUp()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }

            return Data[0];
        }

        /// <summary>
        /// Удаляет элемент из кучи с определенным значением.
        /// </summary>
        public void Remove(TKey item)
        {
            var index = Array.IndexOf(Data, item);

            if (index == -1)
            {
                throw new ArgumentException($"{item} not in heap!");
            }

            (Data[index], Data[Count - 1]) = (Data[Count - 1], Data[index]);

            if (index < Count)
            {
                if (Comparer.Compare(Data[--Count].Key, Data[index].Key) < 0)
                {
                    HeapifyDown(index);
                }
                else
                {
                    HeapifyUp(index);
                }
            }
        }

        /// <summary>
        /// Добавляет в кучу новый элемент key.
        /// </summary>
        public void Add(TKey key, TValue value) // O(log n)
        {
            if (Count == Capacity)
            {
                IncreaseCapacity();
            }

            Data[Count] = new Node<TKey, TValue>(key, value);
            HeapifyUp(Count++);
        }

        /// <summary>
        /// Если значение измененного элемента уменьшается, то свойства кучи 
        /// восстанавливаются функцией HeapifyUp.
        /// </summary>
        public void HeapifyUp(int index) // O(log n)
        {
            var Parent = (index - 1) / 2;
            while (Parent >= 0 && Comparer.Compare(Data[index].Key, Data[Parent].Key) < 0)
            {
                (Data[index], Data[Parent]) = (Data[Parent], Data[index]);
                Parent = ((index = (index - 1) / 2) - 1) / 2;
            }
        }

        /// <summary>
        /// Если значение измененного элемента увеличивается, то свойства кучи 
        /// восстанавливаются функцией HeapifyDown.
        /// </summary>
        public void HeapifyDown(int index) // O(log n)
        {
            int Left = 2 * index + 1;
            while (Left < Count)
            {
                int Right = Left + 1;
                int Compared = Left;

                if (Right < Count && Comparer.Compare(Data[Right].Key, Data[Left].Key) < 0)
                {
                    Compared = Right;
                }

                if (Comparer.Compare(Data[index].Key, Data[Compared].Key) <= 0)
                {
                    break;
                }

                (Data[index], Data[Compared]) = (Data[Compared], Data[index]);
                index = Compared;
                Left = 2 * index + 1;
            }
        }

        /// <summary>
        /// Метод восстановления свойств кучи.
        /// </summary>
        public void Heapify() //O(n) www.geeksforgeeks.org/time-complexity-of-building-a-heap/ 
        {
            for (int i = Count / 2; i >= 0; i--)
                HeapifyDown(i);
        }

        /// <summary>
        /// Объединение двух куч. 
        /// </summary>
        /// <param name="other"></param>
        public void Union(BinaryHeap<TKey, TValue> other) //O(n+m)
        {
            for (int i = 0; i < other.Count; i++) // O(m)
            {
                if (Count == Capacity)
                {
                    IncreaseCapacity();
                }

                Data[Count] = other[i];
            }

            Heapify(); //O(n)
        }

        public void DrawHeap()
        {
            int x = Console.WindowWidth / 2;
            int y = Console.CursorTop;
            DrawHeap(0, x, y, 10);
        }

        private void DrawHeap(int index, int x, int y, int dx)
        {
            if (index >= Count)
            {
                return;
            }
            Console.SetCursorPosition(x, y);
            Console.WriteLine(Data[index]);
            DrawHeap(index * 2 + 1, x - dx, y + 1, dx - 2);
            DrawHeap(index * 2 + 2, x + dx, y + 1, dx - 2);
        }

        public Node<TKey, TValue> this[int index]
        {
            get => Data[index];
        }

        public BinaryHeap() : this(7)
        { }
        public BinaryHeap(IComparer<TKey> customComparer) : this(7, customComparer, new Node<TKey, TValue>[7], 0)
        { }
        public BinaryHeap(int Capacity) : this(Capacity, Comparer<TKey>.Default, new Node<TKey, TValue>[Capacity], 0)
        { }
        public BinaryHeap(Node<TKey, TValue>[] source) : this((int)(source.Count() * 1.25), Comparer<TKey>.Default, source, source.Count())
        { }
        private BinaryHeap(int Capacity, IComparer<TKey> customComparer, Node<TKey, TValue>[] source, int count)
        {
            this.Capacity = Capacity;
            Comparer = customComparer;
            Data = source.ToArray();
            Count = count;

            if (Count != 0)
            {
                Heapify();
            }
        }
    }
}
