using System;

namespace Training
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers=new int[5];
           

            int[] numbers2 = {1,5,7,9,5};
            System.Console.WriteLine(numbers2);
            foreach (int num in numbers2)
            {
                Console.WriteLine(num);
            }

            System.Console.WriteLine(numbers.Length);
            System.Console.WriteLine(numbers.Rank);

            numbers[0] = 10;
            int first = numbers[0];

            System.Console.WriteLine(first);
            

             foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                System.Console.WriteLine($"{i}:{numbers[i]}");
            }
            
            //Multi-Dimensional Arrays
            // RECTANGULAR ARRAY (2D)
            int[,] matrix = new int[3,4];
            int[,] matrix2 =
            {
                {1,2,3,4},
                {5,8,3,6},
                {3,6,7,5},
            };

            matrix[0,0]=1;
            int value = matrix[1,2];
            System.Console.WriteLine(value);

            System.Console.WriteLine(matrix.GetLength(0));
            System.Console.WriteLine(matrix.GetLength(1));

            for(int i = 0; i < matrix2.GetLength(0); i++)
            {
                for(int j = 0; j < matrix2.GetLength(1); j++)
                {
                    System.Console.Write($"{matrix2[i, j]}");
                }
                System.Console.WriteLine();
            }

            int[] [] jagged = new int[3][];
            jagged[0] = new int[] {1,5};
            jagged[1] = new int[] {4,8,9};
            jagged[2] = new int[] {4,2,4,9};

            foreach(int[] row in jagged)
            {
                System.Console.WriteLine(string.Join(", ",row));
            }

            int[] numbers1 = { 5, 2, 8, 1, 9 };

            // Original array
            Console.WriteLine("Original: " + string.Join(", ", numbers1));

            // Sorting
            Array.Sort(numbers1);
            Console.WriteLine("After Sort: " + string.Join(", ", numbers1));

            // Reverse
            Array.Reverse(numbers1);
            Console.WriteLine("After Reverse: " + string.Join(", ", numbers1));




        }
    }
}
