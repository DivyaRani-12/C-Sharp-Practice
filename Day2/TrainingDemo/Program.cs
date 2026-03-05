using System;

namespace Day02Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demonstrate value vs reference
            Console.WriteLine("=== Value Type Demo ===");
            int x = 10;
            ModifyValue(x);
            Console.WriteLine($"After ModifyValue: {x}");  // Still 10
            
            ModifyValueRef(ref x);
            Console.WriteLine($"After ModifyValueRef: {x}");  // Now 20
            
            Console.WriteLine("\n=== Reference Type Demo ===");
            Person person = new Person { Name = "Alice", Age = 30 };
            ModifyPerson(person);
            Console.WriteLine($"After ModifyPerson: {person.Name}, {person.Age}");
            
            // Try Parse demo
            Console.WriteLine("\n=== TryParse Pattern ===");
            Console.Write("Enter a number: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine($"You entered: {number}");
            }
            else
            {
                Console.WriteLine("Invalid number");
            }
        }
        
        static void ModifyValue(int num)
        {
            num = 20;  // Only changes local copy
        }
        
        static void ModifyValueRef(ref int num)
        {
            num = 20;  // Changes original
        }
        
        static void ModifyPerson(Person p)
        {
            p.Age = 31;  // Modifies original object
            p = new Person { Name = "Bob", Age = 25 };  // Only changes local reference
        }
    }
    
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}