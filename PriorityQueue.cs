using System;
using System.Collections.Generic;

namespace LeetCodeSolutions
{
    // TODO: think about throwing exception
    public class PriorityQueue<TKey, TPriority>
    {
        private int _capacity = 10;
        private int _size = 0;
        private KeyValuePair<TKey, TPriority>[] _items;
        private readonly IComparer<KeyValuePair<TKey, TPriority>> _comparer;

        public PriorityQueue(IComparer<KeyValuePair<TKey, TPriority>> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();

            _comparer = comparer;
            _items = new KeyValuePair<TKey, TPriority>[_capacity];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        /// <summary>
        /// retrieves root element
        /// </summary>
        public KeyValuePair<TKey, TPriority> Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            return _items[0];
        }

        /// <summary>
        /// removes root element and returns it
        /// </summary>
        public KeyValuePair<TKey, TPriority> Poll()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            var item = _items[0];

            _items[0] = _items[_size - 1];
            _size--;

            HeapifyDown();

            return item;
        }

        /// <summary>
        /// adds new element
        /// </summary>
        public void Add(KeyValuePair<TKey, TPriority> item)
        {
            EnsureExtraCapacity();
            _items[_size] = item;
            _size++;

            HeapifyUp();
        }

        #region Helpers

        private void HeapifyUp()
        {
            var index = _size - 1;

            // move up item till 
            while (HasParent(index) && _comparer.Compare(GetParent(index), _items[index]) == 1)
            {
                var parentIndex = GetParentIndex(index);

                Swap(parentIndex, index);
                index = GetParentIndex(index);
            }
        }

        private void EnsureExtraCapacity()
        {
            if (_size == _capacity)
            {
                var biggerArray = new KeyValuePair<TKey, TPriority>[_capacity * 2];
                _capacity *= 2;

                Array.Copy(_items, biggerArray, _size);
                _items = biggerArray;
            }
        }

        private void HeapifyDown()
        {
            var index = 0;

            while (HasLeftChild(index))
            {
                var smallerChildIndex = GetLeftChildIndex(index);

                if (HasRightChild(index) && _comparer.Compare(GetRightChild(index), GetLeftChild(index)) == -1)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (_comparer.Compare(_items[index], _items[smallerChildIndex]) == -1)
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
            return leftChildIndex < _size;
        }

        private bool HasRightChild(int index)
        {
            var rightChildIndex = GetRightChildIndex(index);
            return rightChildIndex < _size;
        }

        private bool HasParent(int index)
        {
            var parentIndex = GetParentIndex(index);
            return parentIndex >= 0;
        }

        private KeyValuePair<TKey, TPriority> GetLeftChild(int index)
        {
            var leftChildIndex = GetLeftChildIndex(index);
            return _items[leftChildIndex];
        }

        private KeyValuePair<TKey, TPriority> GetRightChild(int index)
        {
            var rightChildIndex = GetRightChildIndex(index);
            return _items[rightChildIndex];
        }

        private KeyValuePair<TKey, TPriority> GetParent(int index)
        {
            var parentIndex = GetParentIndex(index);
            return _items[parentIndex];
        }

        private void Swap(int indexOne, int indexTwo)
        {
            var tmp = _items[indexOne];
            _items[indexOne] = _items[indexTwo];
            _items[indexTwo] = tmp;
        }

        #endregion
    }
}
