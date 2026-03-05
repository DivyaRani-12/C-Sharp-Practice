using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise5
{
    class Program
    {
        static void Main()
        {
            StringBuilder inputText = new StringBuilder();
            string line;

            System.Console.WriteLine("Enter text (type END on new line to finish):");

            while (true)
            {
                line = Console.ReadLine();
                if (line.ToUpper() == "END")
                    break;

                inputText.Append(line + " ");
                
            }

            string text = inputText.ToString().ToLower();
            char[] punctuation = { '.', ',', '!', '?', ';', ':', '"', '\'', '(', ')', '-' };

            foreach(char c in punctuation)
            {
                text=text.Replace(c.ToString(),"");
            }

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            int totalLength = 0;
            foreach(string word in words)
            {
                totalLength+=word.Length;

                if(wordCount.ContainsKey(word))
                    wordCount[word]++;
                else
                    wordCount[word] = 1;
            }

            int totalWords = words.Length;
            int uniqueWords = wordCount.Count;
            double avgLength = totalWords > 0 ? (double)totalLength/totalWords : 0;

            System.Console.WriteLine("\n=== Analysis ===");
            System.Console.WriteLine($"Total words:{totalWords}");
            System.Console.WriteLine($"Unique words:{uniqueWords}");
            System.Console.WriteLine($"Average word length: {avgLength:F1}");
            
            Console.Write("\nEnter how many top words to display");
            int n = int.Parse(Console.ReadLine());

            var topWords = wordCount
                            .OrderByDescending(w=>w.Value)
                            .ThenBy(w => w.Key)
                            .Take(n);
                        
            System.Console.WriteLine($"\nTop {n} most frequent words:");

            int rank = 1;
            foreach(var item in topWords)
            {
                string timesText = item.Value == 1 ? "time":"times";
                System.Console.WriteLine($"{rank}. {item.Key} - {item.Value} {timesText}");
                rank++;
            }

            
        }
    }
}

