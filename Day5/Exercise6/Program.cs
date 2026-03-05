using System;
using System.Collections.Generic;

namespace SparseMatrixDemo
{
    public class SparseMatrix
    {
        private readonly Dictionary<(int row, int col), double> _values;
        public int Rows { get; }
        public int Cols { get; }

        public SparseMatrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            _values = new Dictionary<(int, int), double>();
        }

        // 2D Indexer
        public double this[int row, int col]
        {
            get
            {
                if (_values.TryGetValue((row, col), out double value))
                    return value;

                return 0;
            }

            set
            {
                if (value == 0)
                    _values.Remove((row, col));
                else
                    _values[(row, col)] = value;
            }
        }

        // Get Row
        public double[] GetRow(int row)
        {
            double[] result = new double[Cols];

            for (int col = 0; col < Cols; col++)
                result[col] = this[row, col];

            return result;
        }

        // Get Column
        public double[] GetColumn(int col)
        {
            double[] result = new double[Rows];

            for (int row = 0; row < Rows; row++)
                result[row] = this[row, col];

            return result;
        }

        // Matrix Addition
        public SparseMatrix Add(SparseMatrix other)
        {
            if (Rows != other.Rows || Cols != other.Cols)
                throw new Exception("Matrix sizes must match");

            SparseMatrix result = new SparseMatrix(Rows, Cols);

            foreach (var item in _values)
                result[item.Key.row, item.Key.col] = item.Value;

            foreach (var item in other._values)
                result[item.Key.row, item.Key.col] += item.Value;

            return result;
        }

        // Matrix Multiplication
        public SparseMatrix Multiply(SparseMatrix other)
        {
            if (Cols != other.Rows)
                throw new Exception("Invalid matrix sizes for multiplication");

            SparseMatrix result = new SparseMatrix(Rows, other.Cols);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < Cols; k++)
                        sum += this[i, k] * other[k, j];

                    if (sum != 0)
                        result[i, j] = sum;
                }
            }

            return result;
        }

        // Display Matrix
        public void Display()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write($"{this[i, j],5}");
                }
                Console.WriteLine();
            }
        }

        // Memory Usage Comparison
        public void ShowMemoryUsage()
        {
            int denseSize = Rows * Cols;
            int sparseSize = _values.Count;

            Console.WriteLine($"Dense Matrix elements stored: {denseSize}");
            Console.WriteLine($"Sparse Matrix elements stored: {sparseSize}");
            Console.WriteLine($"Memory saved: {denseSize - sparseSize} elements");
        }
    }

    class Program
    {
        static void Main()
        {
            SparseMatrix matrix = new SparseMatrix(4,4);

            matrix[0,1] = 5;
            matrix[1,2] = 8;
            matrix[3,0] = 3;

            Console.WriteLine("Matrix:");
            matrix.Display();

            Console.WriteLine();

            Console.WriteLine("Row 1:");
            foreach(var v in matrix.GetRow(1))
                Console.Write(v + " ");

            Console.WriteLine("\n");

            Console.WriteLine("Column 2:");
            foreach(var v in matrix.GetColumn(2))
                Console.Write(v + " ");

            Console.WriteLine("\n");

            matrix.ShowMemoryUsage();
        }
    }
}