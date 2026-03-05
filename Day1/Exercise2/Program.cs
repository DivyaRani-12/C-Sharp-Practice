using System;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Text Analyzer ===\n");

            Console.WriteLine("Enter Sentence:");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input is provided");
                return;
            }

            // TODO: Count characters (including spaces)
            int totalChars = input.Length;
            Console.WriteLine($"Total charcters:{totalChars}");

            // TODO: Count characters (excluding spaces)
            int charsWithoutSpace = input.Replace(" ","").Length;
            Console.WriteLine($"Execluding charcter without space:{charsWithoutSpace}");
            
            // TODO: Count words (split by spaces)
            string[] words = input.Split(' ');
            Console.WriteLine($"Word Count:{words.Length}");
            
            // TODO: Display transformations
            Console.WriteLine($"UpperCase:{input.ToUpper()}");
            Console.WriteLine($"LowerCase:{input.ToLower()}");

            // TODO: Check for keywords (e.g., "C#", "programming")
            if (input.Contains("c#"))
            {
                Console.WriteLine("contains keyword:c#");
            }

            int count = Math.Min(3,words.Length);
            Console.WriteLine("First 3 words:");
            for (int i = 0; i < count; i++)
            {
                Console.Write(words[i] + " ");

            }
            Console.WriteLine();


        }
    }
}