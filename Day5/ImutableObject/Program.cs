using System;

namespace ImmutableObjects
{
    // Class using Init-Only Properties
    public class Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }

        // Computed property
        public int Age => DateTime.Now.Year - BirthDate.Year;
    }

    // Record Type (Immutable with value equality)
    public record PersonRecord(string FirstName, string LastName, int Age);

    // Required members (C# 11)
    public class User
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"UserName: {UserName}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Init-only properties example
            Person person = new Person
            {
                FirstName = "Divya",
                LastName = "Patil",
                BirthDate = new DateTime(2004, 1, 2)
            };

            Console.WriteLine("Init-only Class Example:");
            Console.WriteLine(person.FirstName);
            Console.WriteLine(person.LastName);
            Console.WriteLine(person.Age);

            Console.WriteLine();

            // Record example
            PersonRecord person1 = new("Alice", "Smith", 30);
            PersonRecord person2 = new("Alice", "Smith", 30);

            Console.WriteLine("Record Example:");
            Console.WriteLine(person1 == person2); // value equality
            Console.WriteLine(person1);            // automatic ToString()

            // With expression
            PersonRecord person3 = person1 with { Age = 31 };

            Console.WriteLine("Modified Record:");
            Console.WriteLine(person3);

            Console.WriteLine();

            // Required properties example
            User user = new User
            {
                UserName = "divya",
                Email = "divya@gmail.com"
            };

            Console.WriteLine("User Example:");
            Console.WriteLine(user);
        }
    }
}