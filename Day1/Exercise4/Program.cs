using System;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== User Registration ===\n");

            Console.Write("Username(required): ");
            string? Username=Console.ReadLine();

            Console.Write("Email (required):");
            string? Email=Console.ReadLine();

            Console.Write("Age (optional, press Enter to skip): ");
            string ageInput = Console.ReadLine();
            int? age=string.IsNullOrWhiteSpace(ageInput) ? null : int.Parse(ageInput);

           Console.Write("Phone Number (optional)");
           string phone = Console.ReadLine();
           phone = string.IsNullOrWhiteSpace(phone) ? null : phone;

           Console.Write("Middle Name (optional)");
           string middleName = Console.ReadLine();
           middleName = string.IsNullOrWhiteSpace(middleName) ? null : middleName;

            Console.WriteLine("\n=== Registration Summary ===");
            Console.WriteLine($"Username:{Username ?? "N/A"}");
            Console.WriteLine($"Email:{Email ?? "N/a"}");
            Console.WriteLine($"Age: {age.ToString() ?? "Not Provided"}");
            Console.WriteLine($"Middlename: {middleName ?? "Not Provided"}");
            Console.WriteLine($"Phone: {phone ?? "Not Provided"}");

            bool isValid = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Email);
            Console.WriteLine($"Registration {(isValid ? "sucessful":"failed")}");

        }
    }
}