using System;

namespace Exercise03
{
    // Classes for Bonus Pattern Matching
    class Circle
    {
        public double Radius { get; set; }
    }

    class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shape Calculator");
            Console.WriteLine("1. Circle");
            Console.WriteLine("2. Rectangle");
            Console.WriteLine("3. Triangle");
            Console.Write("Choose shape: ");

            string? choice = Console.ReadLine();

            try
            {
                double area = choice switch
                {
                    "1" => CalculateCircleArea(),
                    "2" => CalculateRectangleArea(),
                    "3" => CalculateTriangleArea(),
                    _ => throw new InvalidOperationException("Invalid choice")
                };

                Console.WriteLine($"Area: {area:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Bonus Pattern Matching Example
            Console.WriteLine("\n=== Pattern Matching Demo ===");
            Circle c = new Circle { Radius = 5 };
            Rectangle r = new Rectangle { Width = 10, Height = 20 };

            Console.WriteLine(DescribeShape(c));
            Console.WriteLine(DescribeShape(r));
        }

        // Circle Area
        static double CalculateCircleArea()
        {
            Console.Write("Enter radius: ");
            double radius = double.Parse(Console.ReadLine() ?? "0");
            return Math.PI * radius * radius;
        }

        // Rectangle Area
        static double CalculateRectangleArea()
        {
            Console.Write("Enter width: ");
            double width = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter height: ");
            double height = double.Parse(Console.ReadLine() ?? "0");

            return width * height;
        }

        // Triangle Area
        static double CalculateTriangleArea()
        {
            Console.Write("Enter base: ");
            double baseValue = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter height: ");
            double height = double.Parse(Console.ReadLine() ?? "0");

            return 0.5 * baseValue * height;
        }

        // Bonus: Pattern Matching
        static string DescribeShape(object shape) => shape switch
        {
            Circle c when c.Radius < 0 => "Invalid circle",
            Circle c => $"Circle with radius {c.Radius}",
            Rectangle r => $"Rectangle {r.Width}x{r.Height}",
            _ => "Unknown shape"
        };
    }
}