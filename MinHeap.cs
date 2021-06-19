using System;
using System.Collections.Generic;

namespace LeetCodeSolutions
{
    public class MinHeap<T>
    {
        private int size = 0;
        private List<T> items = new List<T>();
        private readonly IComparer<T> comparer;

        public MinHeap()
        {
        }

        public MinHeap(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// retrieves root element
        /// </summary>
        public T Peek()
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            return items[0];
        }

        /// <summary>
        /// removes root element and returns one
        /// </summary>
        public T Poll()
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            size--;
            var item = items[0];

            items[0] = items[size - 1];
            HeapifyDown();

            return item;
        }

        /// <summary>
        /// adds new element
        /// </summary>
        public void Add(T item)
        {
            items.Add(item);
            size++;

            HeapifyUp();
        }

        #region Helpers

        private void HeapifyUp()
        {
            var index = size - 1;

            // TODO: check if comparer is set, if not use default comparer
            while (HasParent(index) && comparer.Compare(GetParent(index), items[index]) == 1)
            {
                var parentIndex = GetParentIndex(index);

                Swap(parentIndex, index);
                index = GetParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            var index = 0;

            while (HasLeftChild(index))
            {
                var smallerChildIndex = GetLeftChildIndex(index);

                if (HasRightChild(index) && comparer.Compare(GetRightChild(index), GetLeftChild(index)) == -1)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (comparer.Compare(items[index], items[smallerChildIndex]) == -1)
                {
                    break;
                }

                Swap(index, smallerChildIndex);
                index = smallerChildIndex;
            }
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool HasLeftChild(int index)
        {
            var leftChildIndex = GetLeftChildIndex(index);
            return leftChildIndex < size;
        }

        private bool HasRightChild(int index)
        {
            var rightChildIndex = GetRightChildIndex(index);
            return rightChildIndex < size;
        }

        private bool HasParent(int index)
        {
            var parentIndex = GetParentIndex(index);
            return parentIndex >= 0;
        }

        private T GetLeftChild(int index)
        {
            var leftChildIndex = GetLeftChildIndex(index);
            return items[leftChildIndex];
        }

        private T GetRightChild(int index)
        {
            var rightChildIndex = GetRightChildIndex(index);
            return items[rightChildIndex];
        }

        private T GetParent(int index)
        {
            var parentIndex = GetParentIndex(index);
            return items[parentIndex];
        }

        private void Swap(int indexOne, int indexTwo)
        {
            var tmp = items[indexOne];
            items[indexOne] = items[indexTwo];
            items[indexTwo] = tmp;
        }

        #endregion
    }
}
