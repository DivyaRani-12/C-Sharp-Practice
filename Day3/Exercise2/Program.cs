using System;

namespace Exercise2
{
    class Matrix
    {
        private int[,] data;

        public int Rows => data.GetLength(0);
        public int Cols => data.GetLength(1);

        public Matrix(int rows, int cols)
        {
            data = new int[rows,cols];
        }

        public Matrix(int[,] array)
        {
            data = array;
        }

        public int this[int row,int col]
        {
            get=>data[row,col];
            set=>data[row,col]=value;
        }

        public Matrix Add(Matrix other)
        {
            if(Rows!= other.Rows || Cols!=other.Cols)
                throw new ArgumentException("Matrices must have same dimension");

            Matrix result = new Matrix(Rows,Cols);
            for(int i=0; i<Rows; i++)
            {
                for(int j=0; j<Cols; j++)
                {
                    result[i, j]=this[i,j] + other[i, j];
                }
            }
            return result;
        }

        public Matrix Multiply(Matrix other)
        {
            if(Cols != other.Rows)
                throw new Exception("Columns of first matrix must equal rows of second matrix");

            Matrix result = new Matrix(Rows, other.Cols);

            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < other.Cols; j++)
                {
                    int sum=0;
                    for(int k = 0; k < Cols; k++)
                    {
                        sum+=this[i, k]*other[k,j];
                    }
                    result[i,j] = sum;
                }
            }
            return result;
            
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(Cols,Rows);
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    result[j,i]=this[i,j];
                }
            }
            return result;
        }

        public void Display()
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    Console.Write($"{data[i,j],4}");
                }
                System.Console.WriteLine();
            }
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(new int[,]
            {
                {1,3,4},
                {4,6,7}
            });

            Matrix m2 = new Matrix(new int[,]
            {
                {8,6,9},
                {2,4,6}
            });

            System.Console.WriteLine("Matrix 1:");
            m1.Display();

            System.Console.WriteLine("Matrix 2:");
            m2.Display();

            System.Console.WriteLine("\nSum:");
            Matrix sum = m1.Add(m2);
            sum.Display();

            System.Console.WriteLine("\nTranspose of Matrix 1:");
            Matrix transpose = m1.Transpose();
            transpose.Display();

            Matrix m3 = new Matrix(new int[,]
            {
                {1,2},
                {3,4},
                {8,9}
            });

            System.Console.WriteLine("Matrix3 :");
            m3.Display();

            System.Console.WriteLine("\nMultiplication (Matrix1 x Matrix3)");

            Matrix product = m1.Multiply(m3);
            product.Display();
            
        }
    }
}