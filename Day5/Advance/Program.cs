using System;

namespace Advance
{
    public class PerformanceExample
    {
        // Public field
        public int fieldValue;

        // Auto-implemented property
        public int PropertyValue { get; set; }

        // Full property with backing field
        private int _value;

        public int FullPropertyValue
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class User
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = "";
    }

    class Program
    {
        static void Main(string[] args)
        {
            PerformanceExample example = new PerformanceExample();

            example.fieldValue = 10;
            example.PropertyValue = 20;
            example.FullPropertyValue = 30;

            Console.WriteLine("Field Value: " + example.fieldValue);
            Console.WriteLine("Auto Property Value: " + example.PropertyValue);
            Console.WriteLine("Full Property Value: " + example.FullPropertyValue);

            User user = new User
            {
                UserName = "Divya",
                Email = "divya@gmail.com",
                Age = 21,
                PhoneNumber = "9876543210"
            };

            Console.WriteLine("\nUser Details:");
            Console.WriteLine(user.UserName);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Age);
            Console.WriteLine(user.PhoneNumber);
        }
    }
}