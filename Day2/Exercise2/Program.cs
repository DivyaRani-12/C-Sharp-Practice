using System;

namespace Exercise2
{ 
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x,int y)
        {
            X=x;
            Y=y;
            
        }
        public override string ToString()=>$"({X},{Y})";
       

    }

    class Rectangle
    {
        public int Width {get; set;}
        public int Height {get; set;}

        public int Area()=> Width * Height;

        public override string ToString() => $"Rectangle {Width}x{Height}";
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Value Type Behavior ===");
            Point p1 = new Point(10,20);
            Point p2 = p1;
            p2.X=100;

            System.Console.WriteLine($"p1:{p1}");
            System.Console.WriteLine($"p2:{p2}");

            Console.WriteLine("\n=== Reference Type Behavior ===");
            Rectangle r1 = new Rectangle{Width=10, Height=20};
            Rectangle r2 = r1;
            r2.Width = 100;

            System.Console.WriteLine($"r1:{r1}");
            System.Console.WriteLine($"r2:{r2}");

            Console.WriteLine("\n=== Using ref Parameter ===");
            Point p3 = new Point(5,5);
            System.Console.WriteLine($"Before: {p3}");
            MovePoint(ref p3,10,10);
            System.Console.WriteLine($"After:{p3}");

            Console.WriteLine("\n=== Array Behavior ===");
            int[] arr1={1,2,3};
            int[] arr2=arr1;

            arr2[0]=100;
            System.Console.WriteLine($"arr1[0]:{arr1[0]}");
            System.Console.WriteLine($"arr2[0]:{arr2[0]}");


        }
        static void MovePoint(ref Point point, int dx, int dy)
        {
            point.X += dx;
            point.Y += dy;
        }
        
    }
}