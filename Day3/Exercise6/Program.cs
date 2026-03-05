using System;
using System.Collections.Generic;

class TicTacToe
{
    static Dictionary<string, int> scores = new Dictionary<string, int>()
    {
        { "X", 0 },
        { "O", 0 }
    };

    static Random rand = new Random();

    static void Main()
    {
        Console.WriteLine("=== Tic-Tac-Toe ===");

        bool playAgain = true;

        while (playAgain)
        {
            char[,] board = new char[3, 3];
            InitializeBoard(board);

            Console.Write("Play against computer? (y/n): ");
            bool vsAI = Console.ReadLine().ToLower() == "y";

            char currentPlayer = 'X';
            bool gameOver = false;

            while (!gameOver)
            {
                PrintBoard(board);

                if (vsAI && currentPlayer == 'O')
                {
                    ComputerMove(board);
                }
                else
                {
                    PlayerMove(board, currentPlayer);
                }

                // Check win
                if (CheckWin(board, currentPlayer))
                {
                    PrintBoard(board);
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    scores[currentPlayer.ToString()]++;
                    gameOver = true;
                }
                // Check draw
                else if (IsDraw(board))
                {
                    PrintBoard(board);
                    Console.WriteLine("It's a draw!");
                    gameOver = true;
                }
                else
                {
                    // Switch player
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                }
            }

            // Show scores
            Console.WriteLine("\nScores:");
            Console.WriteLine($"X: {scores["X"]}");
            Console.WriteLine($"O: {scores["O"]}");

            Console.Write("\nPlay again? (y/n): ");
            playAgain = Console.ReadLine().ToLower() == "y";
        }
    }

    // Initialize board
    static void InitializeBoard(char[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';
    }

    // Print board
    static void PrintBoard(char[,] board)
    {
        Console.Clear();
        Console.WriteLine("  1 2 3");

        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + 1 + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();

            if (i < 2)
                Console.WriteLine("  -----");
        }
    }

    // Player move with validation
    static void PlayerMove(char[,] board, char player)
    {
        int row, col;

        while (true)
        {
            Console.Write($"Player {player}, enter row (1-3): ");
            row = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Enter column (1-3): ");
            col = int.Parse(Console.ReadLine()) - 1;

            if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
            {
                board[row, col] = player;
                break;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }

    // Computer random move
    static void ComputerMove(char[,] board)
    {
        int row, col;

        do
        {
            row = rand.Next(0, 3);
            col = rand.Next(0, 3);
        }
        while (board[row, col] != ' ');

        board[row, col] = 'O';
    }

    // Check win
    static bool CheckWin(char[,] board, char player)
    {
        // Rows & Columns
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                return true;

            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                return true;
        }

        // Diagonals
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;

        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;

        return false;
    }

    // Check draw
    static bool IsDraw(char[,] board)
    {
        foreach (char c in board)
        {
            if (c == ' ')
                return false;
        }
        return true;
    }
}