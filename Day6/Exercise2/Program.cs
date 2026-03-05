using System;
using System.Collections.Generic;

// Base Class
public class Shape
{
    public string Name { get; set; }
    public string Color { get; set; }

    public Shape(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public virtual double GetArea()
    {
        return 0;
    }

    public virtual double GetPerimeter()
    {
        return 0;
    }

    public void Display()
    {
        Console.WriteLine($"{Name} ({Color})");
        Console.WriteLine($"Area: {GetArea():F2}");
        Console.WriteLine($"Perimeter: {GetPerimeter():F2}");
    }
}

//////////////////////////////////////////////////////
// Rectangle
//////////////////////////////////////////////////////

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height, string color)
        : base("Rectangle", color)
    {
        Width = width;
        Height = height;
    }

    public override double GetArea()
    {
        return Width * Height;
    }

    public override double GetPerimeter()
    {
        return 2 * (Width + Height);
    }
}

//////////////////////////////////////////////////////
// Circle
//////////////////////////////////////////////////////

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius, string color)
        : base("Circle", color)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

//////////////////////////////////////////////////////
// Triangle
//////////////////////////////////////////////////////

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public double Side1 { get; set; }
    public double Side2 { get; set; }
    public double Side3 { get; set; }

    public Triangle(double baseVal, double height, double s1, double s2, double s3, string color)
        : base("Triangle", color)
    {
        Base = baseVal;
        Height = height;
        Side1 = s1;
        Side2 = s2;
        Side3 = s3;
    }

    public override double GetArea()
    {
        return 0.5 * Base * Height;
    }

    public override double GetPerimeter()
    {
        return Side1 + Side2 + Side3;
    }
}

//////////////////////////////////////////////////////
// Square (inherits Rectangle)
//////////////////////////////////////////////////////

public class Square : Rectangle
{
    public Square(double side, string color)
        : base(side, side, color)
    {
        Name = "Square";
    }
}

//////////////////////////////////////////////////////
// Program
//////////////////////////////////////////////////////

class Program
{
    static void Main()
    {
        List<Shape> shapes = new()
        {
            new Rectangle(5, 10, "Blue"),
            new Circle(7, "Red"),
            new Triangle(6, 4, 5, 5, 6, "Green"),
            new Square(4, "Yellow")
        };

        foreach (var shape in shapes)
        {
            shape.Display();
            Console.WriteLine();
        }
    }
}