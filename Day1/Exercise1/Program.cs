using System;

namespace Exercise1
{
    class Program
   {
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Type System Exploration ===\n");

        byte minByte = byte.MinValue;
        byte maxByte = byte.MaxValue;
        Console.WriteLine($"byte:{minByte} to {maxByte}");
          
        Console.WriteLine($"int:{int.MinValue} to {int.MaxValue}");

        Console.WriteLine($"{long.MinValue} to {long.MaxValue}");

        Console.WriteLine($"{decimal.MinValue} to {decimal.MaxValue}");

        Console.WriteLine($"{double.MinValue} to {double.MaxValue}");

        Console.WriteLine($"{float.MinValue} to {float.MaxValue}");
        
        //Arithmetic operations
        int a=10;
        int b=3;
        Console.WriteLine($"\n=== Integer Division ===");
        Console.WriteLine($"{a} / {b} = {a/b}");

        Console.WriteLine($"{a} / (double){b} = {a/(double)b}");

        int x=100;
        long y=x;

        Console.WriteLine($"int value: {x}");
        Console.WriteLine($"long value:  {y}");

        float f=5;
        double d = f;
        Console.WriteLine($"float value: {f}");
        Console.WriteLine($"double value: {d}");

        //explicite typecasting
        double i=10.75;
        int j =(int)i;
        Console.WriteLine(j);
        
        double g=9.8;
        int h = Convert.ToInt32(g);

        Console.WriteLine(h);

        string s = "25";
        //int num = int.Parse(s);
        //Console.WriteLine(num);

        int num;
        if (int.TryParse(s,out num))
        {
            Console.WriteLine(num);
        }
    }
}

}

