using System;
using System.Text.RegularExpressions;

namespace Exercise4
{ 
    class InputValidator
    {
        public static bool TryParseEmail(string input, out string email)
        {
            email = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            if(input.Contains("@") && input.Contains("."))
            {
                email = input.Trim().ToLower();
                return true;
            }
            return false;
            
        }
        public static bool TryParseAge(string input, out int age)
        {
            age=0;

            if(!int.TryParse(input,out age))
                return false;

            return age >= 1 && age<=120;
        }

        public static bool TryParsePhoneNumber(string input,out string phoneNumber)
        {
            phoneNumber = string.Empty;

            if(string.IsNullOrWhiteSpace(input))
                return false;

            input=input.Trim();

            if (Regex.IsMatch(input, @"^\d{10}$"))
            {
                phoneNumber = input;
                return true;
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Registration Form ===\n");
            
            // Email validation
            Console.Write("Email: ");
            string? emailInput = Console.ReadLine();

            if(InputValidator.TryParseEmail(emailInput ?? "", out string email))
            {
                System.Console.WriteLine($"Valid email: {email}");
            }
            else
            {
                System.Console.WriteLine($"Invalid email format");
            }

            System.Console.Write("Age:");
            string? ageInput=Console.ReadLine();

            if(InputValidator.TryParseAge(ageInput ?? "", out int age))
            {
                System.Console.WriteLine($"Valid Age:{age}");
            }
            else
            {
                System.Console.WriteLine("Invalid age(must be in 1-120)");
            }
            
            Console.Write("\nPhone Number:");
            string? phoneInput = Console.ReadLine();

            if(InputValidator.TryParsePhoneNumber(phoneInput ?? "",out string phone))
            {
                System.Console.WriteLine($"Valid Number:{phone}");
            }
            else
            {
                System.Console.WriteLine("Invalid Number(must be 10 digit)");
            }
        }
    }
}