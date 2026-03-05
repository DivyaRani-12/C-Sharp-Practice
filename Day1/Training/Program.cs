using System;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            //value type
            int age5= 20;
            Console.WriteLine(age5);

            decimal d = 99.999m;
            Console.WriteLine(d);
            decimal a = 0.1m + 0.2m;
            Console.WriteLine(a);

            bool b = true;
            Console.WriteLine(b);

            DateTime today = DateTime.Now;
            Console.WriteLine(today);

            //Referance type
            string name = "divya";
            Console.WriteLine(name);

            int[] numbers = { 1, 2, 3 };
            Console.WriteLine(numbers);
            Console.WriteLine(string.Join(",", numbers));


            //Person person = new Person(); // Class

            //Numeric type
            byte c= 255;
            Console.WriteLine(c);  //8 bit

            int e = 20;
            Console.WriteLine(e);

            long bign = 10000000000L;
            Console.WriteLine(bign);

            float f = 2.4f;
            Console.WriteLine(f);

            double h = 3.686463;
            Console.WriteLine(h);

            decimal g = 2.78575m;
            Console.WriteLine(g);

            decimal i= 0.1m + 0.2m;
            Console.WriteLine(i);    //0.3

            double j = 0.1 + 0.2;
            Console.WriteLine(j);  //0.30000000000000004

            //text
            char k = 'a';       //not possible to take ""
            Console.WriteLine(k);

            string l = "Hiii";    //not possible to take ''
            Console.WriteLine(l);

            //Boolean
            bool m = true;
            Console.WriteLine(m);


            // Type inference with var
            var n = 10;
            Console.WriteLine(n);

            var o = "heloo";
            Console.WriteLine(o);

            //String Interpolation
            string name1= "divya";
            int age1 = 20;

            string M1 = "Name:" + name1 + ",Age:" + age1;
            Console.WriteLine(M1);

            string m2 = $"Name:{name},Age:{age1}";
            Console.WriteLine(m2);

            string m3 = $"Next Year:{age1 + 1}";
            Console.WriteLine(m3);

            // Verbatim strings (for paths, multi-line)
            string path = @"C:\Users\Documents\file.txt";
            Console.WriteLine(path);

            string text = @"Hello
            Welcome to c#
            Learning";
            Console.WriteLine(text);

            string msg = @"She said ""Hello""";
            Console.WriteLine(msg);

            //Raw String
            string json = """
                "name":"divya";
                "age":25;
                """;
            Console.WriteLine(json);

            //null

            //value type
            int? p = null;
            Console.WriteLine(p);

            int?q = 20;
            Console.WriteLine(q);

            string? nullname = null;
            string r = nullname ?? "Divya";
            Console.WriteLine(r);

            int length = nullname?.Length ?? 0;
            Console.WriteLine(length);


            //console i/o

            //output
            Console.Write("hii");
            Console.WriteLine("hello");

            //input
            string? input = Console.ReadLine();
            Console.WriteLine(input);

          //Formatted output
          int score = 95;
            Console.WriteLine($"Score: {score:F2}"); // 95.00
            Console.WriteLine($"Hex: {score:X}");    // 5F

            Console.Write("Enter your name:");
            string? name3= Console.ReadLine();

            Console.Write("Enter your age:");
            string? ageinp = Console.ReadLine();

            if (int.TryParse(ageinp, out int age))
            {
                int birthy = DateTime.Now.Year - age;
                Console.WriteLine($"Hello {name3 ?? "Guest"}! your borned around {birthy}");
            }
            else
            {
                Console.WriteLine("Invalid age entered");
            }



        }
    }
}
