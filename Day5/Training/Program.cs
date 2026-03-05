using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Training
{
    class Person
    {
        private string _name;
        public string Name
        {
            get
            {
                Console.WriteLine("Getting name");
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name required");
                Console.WriteLine($"Setting name to {value}");
                _name = value;
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                    throw new ArgumentException("Email required");
                _email = value.ToLower();
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Computed property
        public string DisplayName => $"{FirstName} {LastName}";

        public int Age { get; private set; }

        public string Id { get; init; }

        public required string Email1 { get; init; }

        public void SetAge(int age)
        {
            Age = age;
        }

        // ---------------- PRODUCT CLASS ----------------
        public class Product : INotifyPropertyChanged
        {
            private List<string>? _tags;

            public List<string> Tags
            {
                get
                {
                    if (_tags == null)
                        _tags = LoadTagsFromDatabase();
                    return _tags;
                }
            }

            private decimal? _totalPrice;

            public decimal TotalPrice
            {
                get
                {
                    if (_totalPrice == null)
                        _totalPrice = CalculatePrice();
                    return _totalPrice.Value;
                }
            }

            private string _name;

            public string Name
            {
                get => _name;
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        OnPropertyChanged(nameof(Name));
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Simulated methods
            private List<string> LoadTagsFromDatabase()
            {
                return new List<string> { "Electronics", "Sale", "Popular" };
            }

            private decimal CalculatePrice()
            {
                return 199.99m;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person
            {
                Id = "P001",
                Email1 = "alice@example.com",
                Email = "alice@example.com",
                FirstName = "Alice",
                LastName = "Johnson"
            };

            person.Name = "Alice";
            person.SetAge(25);

            Console.WriteLine(person.DisplayName);

            // Testing Product
            Person.Product product = new Person.Product();
            product.Name = "Laptop";

            Console.WriteLine(product.Name);
            Console.WriteLine(product.TotalPrice);

            foreach (var tag in product.Tags)
                Console.WriteLine(tag);
        }
    }
}