using BinaryHeapLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyApp
{
    internal class Program
    {
        const int CountSortedLists = 4;
        const int CountItemsInEachList = 3;
        const int MinItemInLists = -50;
        const int MaxItemInLists = 100;
        static void Main(string[] args)
        {
            BinaryHeap<int, int> binaryHeap = new BinaryHeap<int, int>();

            var lists = GetSortedListsWithCursorToCurrentPos(CountSortedLists);

            //var arrays = new List<Node<int, List<int>>>
            //{
            //    new Node<int, List<int>>(0, new List<int>{ 1, 5, 10}),
            //    new Node<int, List<int>>(0, new List<int>{ 0, 4, 8}),
            //    new Node<int, List<int>>(0, new List<int>{ 1, 2, 7}),
            //};

            //foreach (var n in arrays)
            //{
            //    PrintList(n.Value);
            //}

            /* Merging lists */
            List<int> mergedList = new List<int>();

            for (int i = 0; i < CountSortedLists; i++) // O(k)
            {
                binaryHeap.Add(lists[i].Value[lists[i].Key], i); //O(log k)
                lists[i].Key++;
            } // O(k * log(k))

            while (binaryHeap.Count > 0) // O(n)
            {
                Node<int, int> item = binaryHeap.RemoveUp(); //O(log k)
                mergedList.Add(item.Key);

                int numberOfList = item.Value;
                List<int> currentList = lists[numberOfList].Value;
                int currentIndexInList = lists[numberOfList].Key;
                if (currentIndexInList < currentList.Count)
                {                 
                    binaryHeap.Add(currentList[currentIndexInList], numberOfList); //O(log k)
                    lists[numberOfList].Key++;
                }
            } //O(n * 2log(k))

            //Total complexity : O(k log k) + O(k) + O(n * 2log k) = O(n log k)

            PrintList(mergedList);
            /* Merging lists */

            Console.ReadKey();
        }

        static List<Node<int, List<int>>> GetSortedListsWithCursorToCurrentPos(int k)
        {
            List<Node<int, List<int>>> list = new List<Node<int, List<int>>>();
            for (int i = 0; i < k; i++)
            {
                var randomList = GenerateRandomList().ToList();
                randomList.Sort();
                PrintList(randomList);
                list.Add(new Node<int, List<int>>(0, randomList));
            }

            return list;
        }
        static IEnumerable<int> GenerateRandomList()
        {
            Thread.Sleep(1);
            return Enumerable.Range(1, CountItemsInEachList).Select(s => new Random(DateTime.Now.Millisecond << s).Next(MinItemInLists, MaxItemInLists));
        }
        static void PrintList(IEnumerable<int> source)
        {
            foreach (int i in source)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
