using System;
using System.Reflection.Metadata;

namespace Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            int totalGames=0;
            int totalAttempts=0;
            int? highScore = null;

            bool playAgain = true;
            Console.ForegroundColor=ConsoleColor.Cyan;
            Console.WriteLine("=== NUMBER GUESSING GAME ===\n");
            Console.ResetColor();

            while (playAgain)
            {
                totalGames++;

                int maxNumber = SelectDiffculty();
                int secretNumber = random.Next(1,maxNumber+1);

                int attempts = 0;
                bool guessCorrectly = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nI am thinking of a number from 1 to {maxNumber}.");

                Console.ResetColor();

                while (!guessCorrectly)
                {
                    Console.Write("Enter ur guess:");
                    string? input = Console.ReadLine();

                    if(!int.TryParse(input, out int guess))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid number");
                        Console.ResetColor();
                        continue;
                    }
                    attempts++;

                    if(guess < secretNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Too Low! Try again");
                        Console.ResetColor();
                    }
                    else if(guess > secretNumber)
                    {
                        Console.ForegroundColor=ConsoleColor.DarkYellow;
                        Console.WriteLine("Too High! Try again");
                        Console.ResetColor();
                    }
                    else
                    {
                        guessCorrectly=true;
                        Console.ForegroundColor=ConsoleColor.Green;
                        Console.WriteLine("$\nCorrect! You guessed it in {attempts} attempts!");
                        Console.ResetColor();
                    }
                }

                totalAttempts+=attempts;

                if(highScore == null || attempts < highScore)
                {
                    highScore = attempts;
                    Console.ForegroundColor=ConsoleColor.Magenta;
                    Console.WriteLine("🎉New High Score!");
                    Console.ResetColor();
                }

                double averageAttempts = (double)totalAttempts/totalGames;

                Console.ForegroundColor=ConsoleColor.Cyan;
                Console.WriteLine("\n=== GAME STATS ===");
                Console.WriteLine($"Games played: {totalGames}");
                Console.WriteLine($"High Score(fewest attempts): {highScore}");
                Console.WriteLine($"Average Attempts: {averageAttempts}");
                Console.ResetColor();

                Console.Write("\nDo u want to play again?(y/n)");
                string? response = Console.ReadLine();

                playAgain = response?.ToLower()=="y";

                
            }
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("\nThanks for playing");
            Console.ResetColor();
        }
        static int SelectDiffculty()
        {
            while (true)
            {
                Console.WriteLine("\n Select Diffculty:");
                Console.WriteLine("1 - Easy(1-50)");
                Console.WriteLine("2 - Medium(1-100)");
                Console.WriteLine("3 -Hard");
                Console.Write("choice:");

                string? choice=Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return 50;
                    case "2":
                        return 100;
                    case "3":
                        return 1000;
                    default:
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("Invalid Selection Try Again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}