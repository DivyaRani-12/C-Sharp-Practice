using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exercise05
{
    public class NamedCollection<T> : IEnumerable<T>
    {
        private List<T> items = new();
        private Dictionary<string, T> namedItems = new();

        public int Count => items.Count;

        // Indexer by integer
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Count)
                    throw new IndexOutOfRangeException();
                return items[index];
            }
            set
            {
                if (index < 0 || index >= items.Count)
                    throw new IndexOutOfRangeException();
                items[index] = value;
            }
        }

        // Indexer by string
        public T this[string key]
        {
            get
            {
                if (!namedItems.ContainsKey(key))
                    throw new KeyNotFoundException($"Key '{key}' not found");

                return namedItems[key];
            }
            set
            {
                if (namedItems.ContainsKey(key))
                {
                    T oldItem = namedItems[key];
                    int index = items.IndexOf(oldItem);

                    if (index != -1)
                        items[index] = value;

                    namedItems[key] = value;
                }
                else
                {
                    items.Add(value);
                    namedItems[key] = value;
                }
            }
        }

        public void Add(T item, string? name = null)
        {
            items.Add(item);

            if (name != null)
                namedItems[name] = item;
        }

        // Remove by item
        public bool Remove(T item)
        {
            bool removed = items.Remove(item);

            if (removed)
            {
                var key = namedItems.FirstOrDefault(x => EqualityComparer<T>.Default.Equals(x.Value, item)).Key;

                if (key != null)
                    namedItems.Remove(key);
            }

            return removed;
        }

        // Remove by name
        public bool Remove(string name)
        {
            if (!namedItems.ContainsKey(name))
                return false;

            T item = namedItems[name];
            namedItems.Remove(name);
            items.Remove(item);

            return true;
        }

        public bool Contains(string name)
        {
            return namedItems.ContainsKey(name);
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        // IEnumerable implementation
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NamedCollection<string> collection = new();

            // Add items
            collection.Add("First item", "first");
            collection.Add("Second item", "second");
            collection.Add("Third item");

            // Access by index
            Console.WriteLine($"Item at index 0: {collection[0]}");

            // Access by name
            Console.WriteLine($"Item named 'first': {collection["first"]}");

            // Iterate
            Console.WriteLine("\nAll items:");
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}