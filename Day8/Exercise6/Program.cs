using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericLinkedListDemo
{
    // Node class
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    // LinkedList class
    public class LinkedList<T> : ICollection<T>
    {
        public Node<T>? First { get; private set; }
        public Node<T>? Last { get; private set; }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        // AddFirst
        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (First == null)
            {
                First = Last = newNode;
            }
            else
            {
                newNode.Next = First;
                First.Previous = newNode;
                First = newNode;
            }

            Count++;
        }

        // AddLast
        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (Last == null)
            {
                First = Last = newNode;
            }
            else
            {
                Last.Next = newNode;
                newNode.Previous = Last;
                Last = newNode;
            }

            Count++;
        }

        // AddBefore
        public void AddBefore(Node<T> node, T value)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node == First)
            {
                AddFirst(value);
                return;
            }

            Node<T> newNode = new Node<T>(value);
            Node<T>? prev = node.Previous;

            newNode.Next = node;
            newNode.Previous = prev;

            if (prev != null)
                prev.Next = newNode;

            node.Previous = newNode;

            Count++;
        }

        // AddAfter
        public void AddAfter(Node<T> node, T value)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node == Last)
            {
                AddLast(value);
                return;
            }

            Node<T> newNode = new Node<T>(value);
            Node<T>? next = node.Next;

            newNode.Next = next;
            newNode.Previous = node;

            node.Next = newNode;

            if (next != null)
                next.Previous = newNode;

            Count++;
        }

        // RemoveFirst
        public void RemoveFirst()
        {
            if (First == null)
                return;

            if (First == Last)
            {
                First = Last = null;
            }
            else
            {
                First = First.Next;
                if (First != null)
                    First.Previous = null;
            }

            Count--;
        }

        // RemoveLast
        public void RemoveLast()
        {
            if (Last == null)
                return;

            if (First == Last)
            {
                First = Last = null;
            }
            else
            {
                Last = Last.Previous;
                if (Last != null)
                    Last.Next = null;
            }

            Count--;
        }

        // Remove(value)
        public bool Remove(T item)
        {
            Node<T>? node = Find(item);

            if (node == null)
                return false;

            if (node == First)
            {
                RemoveFirst();
                return true;
            }

            if (node == Last)
            {
                RemoveLast();
                return true;
            }

            Node<T>? prev = node.Previous;
            Node<T>? next = node.Next;

            if (prev != null)
                prev.Next = next;

            if (next != null)
                next.Previous = prev;

            Count--;
            return true;
        }

        // Find
        public Node<T>? Find(T value)
        {
            Node<T>? current = First;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                    return current;

                current = current.Next;
            }

            return null;
        }

        // Contains
        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        // Clear
        public void Clear()
        {
            First = Last = null;
            Count = 0;
        }

        // Add for ICollection
        public void Add(T item)
        {
            AddLast(item);
        }

        // CopyTo
        public void CopyTo(T[] array, int arrayIndex)
        {
            Node<T>? current = First;

            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        // Enumerator for foreach
        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = First;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    // Main Program
    class Program
    {
        static void Main()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddFirst(0);

            Console.WriteLine("Initial List:");
            foreach (int value in list)
            {
                Console.WriteLine(value);
            }

            list.Remove(2);

            Console.WriteLine("\nContains 2: " + list.Contains(2));

            Node<int>? node = list.Find(1);

            if (node != null)
            {
                list.AddAfter(node, 99);
            }

            Console.WriteLine("\nAfter AddAfter:");
            foreach (int value in list)
            {
                Console.WriteLine(value);
            }
        }
    }
}