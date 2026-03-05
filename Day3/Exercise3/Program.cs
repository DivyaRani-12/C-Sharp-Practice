using System;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Exercise3
{ 
    class StringAnalyzer
    {
        private string text;
        
        public StringAnalyzer(string input)
        {
            text = input ?? string.Empty; 
        }

        public (int vowels, int consonants) CountVowelsAndConsonants()
        {
            int vowels = 0, consonants = 0;
            string vowelsChars = "aeiouAEIOU";

            foreach(char c in text)
            {
                if (char.IsLetter(c))
                {
                    if(vowelsChars.Contains(c))
                        vowels++;
                    else
                        consonants++;
                }
                
            }
            return (vowels,consonants);
        }

        public char GetMostFrequentCharcter()
        {
            Dictionary<char,int> frequency = new Dictionary<char, int>();

            foreach(Char c in text.ToLower())
            {
                if (char.IsLetter(c))
                {
                    if (frequency.ContainsKey(c))
                        frequency[c]++;
                    else
                        frequency[c] = 1;
                }
            }

            if (frequency.Count == 0)
                return '\0';

            return frequency.OrderByDescending(p => p.Value).First().Key;
        }

        public bool IsPalindrome()
        {
            string cleaned = new string(text.Where(char.IsLetterOrDigit).ToArray()).ToLower();

            int left=0,right=cleaned.Length-1;
            while(left < right)
            {
                if (cleaned[left] != cleaned[right])
                    return false;
                left++;
                right--;
            }
            return true;
        }

        public Dictionary<string,int> GetWordFrequancy()
        {
            Dictionary<string,int> wordFreq = new Dictionary<string, int>();

            string cleaned = RemovePunctuation().ToLower();
            string[] words = cleaned.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string word in words)
            {
                if(wordFreq.ContainsKey(word))
                    wordFreq[word]++;
                else
                    wordFreq[word]=1;
            }
            return wordFreq;


        }
        public string RemovePunctuation()
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in text)
            {
                if(!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public string ToProperTitleCase()
        {
            string cleaned = RemovePunctuation().ToLower();
            string[] words = cleaned.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();

            foreach(string word in words)
            {
                if(word.Length > 0)
                {
                    sb.Append(char.ToUpper(word[0]));
                    if (word.Length>1)
                        sb.Append(word.Substring(1));
                    sb.Append(" ");
                }
            }
            return sb.ToString().Trim();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a text:");
            string input = Console.ReadLine() ?? "";

            StringAnalyzer analyzer = new StringAnalyzer(input);

            var (vowels,consonants) = analyzer.CountVowelsAndConsonants();
            System.Console.WriteLine($"Vowels:{vowels}, Consonants:{consonants}");

            System.Console.WriteLine($"Most frequent:{analyzer.GetMostFrequentCharcter()}");
            System.Console.WriteLine($"Is Plaindrome:{analyzer.IsPalindrome()}");
            System.Console.WriteLine($"Without Punctuation:{analyzer.RemovePunctuation()}");

            System.Console.WriteLine("\nWord Frequency:");
            var freq = analyzer.GetWordFrequancy();
            foreach(var item in freq)
            {
                System.Console.WriteLine($"{item.Key} : {item.Value}");
            }
            System.Console.WriteLine($"\nTitle Case: {analyzer.ToProperTitleCase()}");
            
        }
    }
}