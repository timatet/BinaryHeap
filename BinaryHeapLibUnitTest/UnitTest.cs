using BinaryHeapLib;
using System;
using Xunit;

namespace BinaryHeapLibUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void AfterCreationHeapItIsNotNull()
        {
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            Assert.NotNull(heap);
        }

        [Fact]
        public void AfterCreationHeapCountEqualsZero()
        {
            int expected = 0;
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            Assert.Equal(heap.Count, expected);
        }

        [Fact]
        public void AfterAddingElementInHeapCountAreIncreased()
        {
            int expected = 1;
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            heap.Add(2, 0);
            Assert.Equal(heap.Count, expected);
        }

        [Fact]
        public void AfterAddingElementInHeapCountAreExist()
        {
            int expected = 2;
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            heap.Add(2, 0);
            Assert.Equal(heap.RemoveUp().Key, expected);
        }

        [Fact]
        public void AfterExtractElementInHeapCountDecreased()
        {
            int expected = 0;
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            heap.Add(2, 0);
            heap.RemoveUp();
            Assert.Equal(heap.Count, expected);
        }

        [Fact]
        public void ThrowsExceptionIfExtractInEmptyHeap()
        {
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>(new ComparerMaxHeap<int>());
            Assert.Throws<InvalidOperationException>(() => heap.RemoveUp());
        }
    }
}