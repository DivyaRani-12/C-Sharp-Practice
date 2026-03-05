using System;
using System.Diagnostics;
using System.Text;

namespace Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 10000;

            Stopwatch sw1 = Stopwatch.StartNew();
            string result1="";
            for (int i=0; i<iterations; i++)
            {
                result1 += i.ToString();
            }
            sw1.Stop();

            System.Console.WriteLine($"String Concationation{sw1.ElapsedMilliseconds}ms");
            System.Console.WriteLine($"Result Length: {result1.Length}");

            Stopwatch sw2=Stopwatch.StartNew();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < iterations; i++)
            {
                sb.Append(i);
            }
            string result2 =  sb.ToString();
            sw2.Stop();

            System.Console.WriteLine($"StringBuilder:{sw2.ElapsedMilliseconds}ms");
            System.Console.WriteLine($"Result Length: {result2.Length}");

            //double speedup = (double)sw1.ElapsedMilliseconds / sw2.ElapsedMilliseconds;
            //System.Console.WriteLine($"\nStringBuilder is: {speedup:F2}xfaster");

            if (sw2.ElapsedMilliseconds > 0)
            {
                double speedup = (double)sw1.ElapsedMilliseconds / sw2.ElapsedMilliseconds;
                Console.WriteLine($"StringBuilder is {speedup:F2}x faster");
            }
            else
            {
                Console.WriteLine("StringBuilder completed too fast to measure");
            }
        }
    }
}