using System;

namespace Exercise02
{
    public class Matrix
    {
        private double[,] data;
        
        public int Rows => data.GetLength(0);
        public int Cols => data.GetLength(1);
        
        // Constructor
        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Dimensions must be positive");
            data = new double[rows, cols];
        }
        
        public Matrix(double[,] array)
        {
            data = (double[,])array.Clone();
        }
        
        // Indexer
        public double this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                    throw new IndexOutOfRangeException("Invalid matrix index");

                return data[row, col];
            }
            set
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                    throw new IndexOutOfRangeException("Invalid matrix index");

                data[row, col] = value;
            }
        }
        
        public void Fill(double value)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    data[i, j] = value;
                }
            }
        }
        
        // Add method
        public Matrix Add(Matrix other)
        {
            if (Rows != other.Rows || Cols != other.Cols)
                throw new ArgumentException("Matrix dimensions must match for addition");

            Matrix result = new Matrix(Rows, Cols);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            }

            return result;
        }
        
        // Multiply method
        public Matrix Multiply(Matrix other)
        {
            if (Cols != other.Rows)
                throw new ArgumentException("Matrix dimensions are not valid for multiplication");

            Matrix result = new Matrix(Rows, other.Cols);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < Cols; k++)
                    {
                        sum += this[i, k] * other[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }
        
        public Matrix Transpose()
        {
            Matrix result = new Matrix(Cols, Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    result[j, i] = this[i, j];
                }
            }
            return result;
        }
        
        public void Display()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write($"{data[i, j],8:F2}");
                }
                Console.WriteLine();
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(new double[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            
            Console.WriteLine("Matrix 1:");
            m1.Display();
            
            // Using indexer
            Console.WriteLine($"\nElement at [0,0]: {m1[0, 0]}");
            m1[0, 0] = 10;
            
            Console.WriteLine("\nAfter modification:");
            m1.Display();
        }
    }
}