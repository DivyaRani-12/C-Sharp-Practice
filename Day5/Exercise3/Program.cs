using System;

namespace Exercise03
{
    // Using record for immutability
    public record Person
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateTime BirthDate { get; init; }
        public required string Email { get; init; }
        
        // Computed properties
        public string FullName => $"{FirstName} {LastName}";
        
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }
        
        public bool IsAdult() => Age >= 18;
        
        // Custom ToString
        public override string ToString()
        {
            return $"{FullName} (Age: {Age}, Email: {Email})";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Create person (all required properties must be provided)
            Person person1 = new Person
            {
                FirstName = "Alice",
                LastName = "Smith",
                BirthDate = new DateTime(1990, 5, 15),
                Email = "alice@example.com"
            };
            
            Console.WriteLine(person1);
            Console.WriteLine($"Is adult: {person1.IsAdult()}");
            
            // Try to create copy with different email (with-expression)
            Person person2 = person1 with { Email = "alice.smith@example.com" };
            
            Console.WriteLine("\nUsing with-expression:");
            Console.WriteLine($"Original Email: {person1.Email}");
            Console.WriteLine($"Modified Copy Email: {person2.Email}");
            
            // Value-based equality
            Person person3 = new Person
            {
                FirstName = "Alice",
                LastName = "Smith",
                BirthDate = new DateTime(1990, 5, 15),
                Email = "alice@example.com"
            };
            
            Console.WriteLine("\nEquality Check:");
            Console.WriteLine($"person1 == person3: {person1 == person3}");
            Console.WriteLine($"ReferenceEquals(person1, person3): {ReferenceEquals(person1, person3)}");
            
            // Create more persons
            Person person4 = new Person
            {
                FirstName = "Bob",
                LastName = "Johnson",
                BirthDate = new DateTime(2008, 3, 10),
                Email = "bob@example.com"
            };

            Person person5 = new Person
            {
                FirstName = "Carol",
                LastName = "Williams",
                BirthDate = new DateTime(1985, 11, 25),
                Email = "carol@example.com"
            };

            Console.WriteLine("\nMore Persons:");
            Console.WriteLine(person4);
            Console.WriteLine($"Is Adult: {person4.IsAdult()}");

            Console.WriteLine(person5);
            Console.WriteLine($"Is Adult: {person5.IsAdult()}");

            // Demonstrating immutability
            Console.WriteLine("\nImmutability Demonstration:");

            // Instead of modifying person4, we create a new copy
            Person updatedPerson = person4 with { Email = "bob.johnson@example.com" };

            Console.WriteLine($"Original Person Email: {person4.Email}");
            Console.WriteLine($"Updated Person Email: {updatedPerson.Email}");

            Console.WriteLine("\nOriginal object remains unchanged.");
        }
    }
}