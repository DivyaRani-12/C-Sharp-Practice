using System;
using System.Collections.Generic;

namespace GenericSortingExercise
{
    public class Product
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
    public class ProductPriceComparer : IComparer<Product>
    {
        private readonly bool ascending;

        public ProductPriceComparer(bool ascending = true)
        {
            this.ascending = ascending;
        }

        public int Compare(Product? x, Product? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int result = x.Price.CompareTo(y.Price);

            return ascending ? result : -result;
        }
    }
    public class PersonComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Compare by Age
            int ageComparison = x.Age.CompareTo(y.Age);
            if (ageComparison != 0)
                return ageComparison;

            // Then compare by Name
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Length.CompareTo(y.Length);
        }
    }
    public class Sorter<T>
    {
        public static void BubbleSort(List<T> list, IComparer<T> comparer)
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (comparer.Compare(list[j], list[j + 1]) > 0)
                    {
                        // Swap
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        public static void QuickSort(List<T> list, IComparer<T> comparer)
        {
            QuickSort(list, 0, list.Count - 1, comparer);
        }

        private static void QuickSort(List<T> list, int low, int high, IComparer<T> comparer)
        {
            if (low < high)
            {
                int pivotIndex = Partition(list, low, high, comparer);

                QuickSort(list, low, pivotIndex - 1, comparer);
                QuickSort(list, pivotIndex + 1, high, comparer);
            }
        }

        private static int Partition(List<T> list, int low, int high, IComparer<T> comparer)
        {
            T pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (comparer.Compare(list[j], pivot) <= 0)
                {
                    i++;

                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            T temp2 = list[i + 1];
            list[i + 1] = list[high];
            list[high] = temp2;

            return i + 1;
        }
    }

    class Program
    {
        static void Main()
        {
            List<Product> products = new()
            {
                new Product { Name = "Laptop", Price = 999.99m },
                new Product { Name = "Mouse", Price = 25.50m },
                new Product { Name = "Keyboard", Price = 75.00m }
            };

            Console.WriteLine("Sort by Price Ascending:");
            products.Sort(new ProductPriceComparer(true));

            foreach (var p in products)
                Console.WriteLine($"{p.Name} - {p.Price}");

            Console.WriteLine("\nSort by Price Descending:");
            products.Sort(new ProductPriceComparer(false));

            foreach (var p in products)
                Console.WriteLine($"{p.Name} - {p.Price}");

            List<Person> people = new()
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 25 },
                new Person { Name = "Charlie", Age = 25 }
            };

            Console.WriteLine("\nSort by Age then Name:");
            people.Sort(new PersonComparer());

            foreach (var person in people)
                Console.WriteLine($"{person.Name} - {person.Age}");

            List<string> words = new()
            {
                "Apple",
                "Banana",
                "Kiwi",
                "Pineapple"
            };

            Console.WriteLine("\nSort strings by length:");
            words.Sort(new StringLengthComparer());

            foreach (var word in words)
                Console.WriteLine(word);
        }
    }
}