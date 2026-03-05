using System;
 
namespace Exercise1
{
    class MathUtils
    {
        public static int Calculate(int a,int b)
        {
            return a+b;
        }

        public static double Calculate(double a,double b)
        {
            return a+b;
        }

        public static int Calculate(int a,int b,int c)
        {
            return a+b+c;
        }

        public static int Calculate(params int[] numbers)
        {
            int sum=0;
            foreach(int num in numbers)
            {
                sum+=num;
            }
            return sum;
        }

        public static string Format(double value, int decimals = 2, string prefix = "$")
        {
            return $"{prefix}{value.ToString($"F{decimals}")}";
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(MathUtils.Calculate(5,3));
            System.Console.WriteLine(MathUtils.Calculate(5.5,5.2));
            System.Console.WriteLine(MathUtils.Calculate(4,2,5,6,9));

            System.Console.WriteLine(MathUtils.Format(123.456));
            System.Console.WriteLine(MathUtils.Format(123.456,decimals:1));
            Console.WriteLine(MathUtils.Format(123.456, prefix: "€", decimals: 3));

            
        }

        
    }
}