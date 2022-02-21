using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomLinkedListLib
{
    public class CustomLinkedList<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>
    {
        public event Action<T> AddedElement;

        public event Action<T> RemovedElement;

        public event Action ClearedCollection;

        public ListNode<T> First { get; private set; }

        public ListNode<T> Last { get; private set; }

        public int Count { get; private set; }

        public bool IsEmpty
        {
            get => Count == 0;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public CustomLinkedList()
        {
            Count = default(int);
            First = null;
            Last = null;
        }

        public ListNode<T> AddFirst(T value)
        {
            ListNode<T> NodeToAdd = new ListNode<T>(value);

            if (First != null)
            {
                ListNode<T> tempNode = First;
                NodeToAdd.NextNode = tempNode;
                First = NodeToAdd;
                tempNode.PreviousNode = NodeToAdd;
            }
            else
            {
                First = NodeToAdd;
                Last = First;
            }

            AddedElement?.Invoke(NodeToAdd.Value);
            Count++;

            return NodeToAdd;
        }

        public ListNode<T> AddLast(T value)
        {
            ListNode<T> NodeToAdd = new ListNode<T>(value);

            if (First == null)
            {
                First = NodeToAdd;
            }
            else
            {
                Last.NextNode = NodeToAdd;
                NodeToAdd.PreviousNode = Last;
            }

            AddedElement?.Invoke(NodeToAdd.Value);
            Last = NodeToAdd;
            Count++;

            return NodeToAdd;
        }

        // Adding the specified new node before the specified existing node
        // in current LinkedList
        public ListNode<T> AddBefore(ListNode<T> Node, T value)
        {
            if (Node == null)
                throw new ArgumentNullException();
            
            if (First == null && Last == null)
                throw new InvalidOperationException();
            
            if (!this.ContainsNode(Node))
                throw new ArgumentException();
            
            ListNode<T> NodeToAdd = new ListNode<T>(value);

            // if node to add before is the first one
            if (Node == First)
            {
                NodeToAdd.NextNode = Node;
                Node.PreviousNode = NodeToAdd;
            }
            else
            {
                NodeToAdd.PreviousNode = Node.PreviousNode;
                NodeToAdd.NextNode = Node;

                Node.PreviousNode.NextNode = NodeToAdd;
                Node.PreviousNode = NodeToAdd;
            }

            AddedElement?.Invoke(NodeToAdd.Value);
            Count++;

            return NodeToAdd;
        }

        // Adding the specified new node after the specified existing node
        // in current LinkedList
        public ListNode<T> AddAfter(ListNode<T> Node, T value)
        {
            if (Node == null)
                throw new ArgumentNullException();
            
            if (First == null && Last == null)
                throw new InvalidOperationException();
            
            if (!this.ContainsNode(Node))
                throw new ArgumentException();
            
            ListNode<T> NodeToAdd = new ListNode<T>(value);

            // if node to add after is the last one
            if (Node == Last)
            {
                NodeToAdd.PreviousNode = Node;
                Node.NextNode = NodeToAdd;
            }
            else
            {
                NodeToAdd.PreviousNode = Node;
                NodeToAdd.NextNode = Node.NextNode;

                Node.NextNode.PreviousNode = NodeToAdd;
                Node.NextNode = NodeToAdd;
            }

            AddedElement?.Invoke(NodeToAdd.Value);
            Count++;

            return NodeToAdd;
        }

        // to implement ICollection
        public void Add(T value)
        {
            this.AddLast(value);
        }

        // Remove the first occurrence of the specified value from the linkedlist
        public bool Remove(T data)
        {
            ListNode<T> Current = First;

            // looking for the item to remove
            while (Current != null)
            {
                if (Current.Value.Equals(data))
                {
                    break;
                }
                Current = Current.NextNode;
            }

            // Removing the reference to the found node
            if (Current != null)
            {
                // if found node is not the last one
                if (Current.NextNode != null)
                {
                    Current.NextNode.PreviousNode = Current.PreviousNode;
                }
                else
                {
                    Last = Current.PreviousNode;
                }

                // if found node is not the first one 
                if (Current.PreviousNode != null)
                {
                    Current.PreviousNode.NextNode = Current.NextNode;
                }
                else
                {
                    First = Current.NextNode;
                }

                RemovedElement?.Invoke(Current.Value);
                Count--;
                return true;
            }

            return false;
        }

        // Removing the node at the start of LinkedList
        public bool RemoveFirst()
        {
            if (First == null)
            {
                return false;
            }

            RemovedElement?.Invoke(First.Value);
            First.NextNode.PreviousNode = null;
            First = First.NextNode ?? null;
            
            Count--;
            return true;
        }

        // Removing the node at the end of LinkedList
        public bool RemoveLast()
        {
            if (Last == null)
            {
                return false;
            }

            RemovedElement?.Invoke(Last.Value);
            Last.PreviousNode.NextNode = null;
            Last = Last.PreviousNode ?? null;
            
            Count--;
            return true;
        }

        // Find the first node that contains the specified value
        public ListNode<T> Find(T value)
        {
            ListNode<T> Current = First;

            while (Current != null)
            {
                if (Current.Value.Equals(value))
                    return Current;
                Current = Current.NextNode;
            }

            return null;
        }

        // Find the last node that contains the specified value
        public ListNode<T> FindLast(T value)
        {
            ListNode<T> Current = Last;

            while (Current != null)
            {
                if (Current.Value.Equals(value))
                    return Current;
                Current = Current.PreviousNode;
            }

            return null;
        }

        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;

            ClearedCollection?.Invoke();
        }

        public bool Contains(T value)
        {
            ListNode<T> Current = First;

            while (Current != null)
            {
                if (Current.Value.Equals(value))
                    return true;
                Current = Current.NextNode;
            }

            return false;
        }

        public bool ContainsNode(ListNode<T> nodeToCheck)
        {
            if (nodeToCheck == null)
               throw new ArgumentNullException();
            
            ListNode<T> Current = First;

            while (Current != null)
            {
                if (Current == nodeToCheck)
                    return true;
                Current = Current.NextNode;
            }

            return false;
        }

        // Copies the entire LinkedList<T> to a compatible one-dimensional Array,
        // starting at the specified index of the target array.
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (index < 0)
                throw new ArgumentOutOfRangeException();

            if (this.Count > (array.Length - index))
                throw new ArgumentException();

            foreach (var value in this)
            {
                array[index] = value;
                index++;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            ListNode<T> Current = First;
            while (Current != null)
            {
                yield return Current.Value;
                Current = Current.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
