using System;
using System.Collections.Generic;

namespace Exercise2
{
    public class Shape
    {
        public string Name{get;set;}
        public string Color {get;set;}

        public Shape(string name,string color)
        {
            Name=name;
            Color=color;
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
            System.Console.WriteLine($"{Name} {Color}");
            System.Console.WriteLine($"Area:{GetArea():F2}");
            System.Console.WriteLine($"Perimeter:{GetPerimeter():F2}");
        }
    }

    public class Rectangle : Shape
    {
        public double Width {get;set;}
        public double Height {get;set;}

        public Rectangle(double width,double height,string color):base("Rectangle",color)
        {
            Width=width;
            Height=height;
            
        }

        public override double GetArea()
        {
            return Width*Height;
        }

        public override double GetPerimeter()
        {
            return 2*(Width+Height);
        }
    }

    public class Circle : Shape
    {
        public double Raduis {get; set;}

        public Circle(double radius,string color) : base("Circle",color)
        {
            Raduis=radius;
            
        }

        public override double GetArea()
        {
            return Math.PI*Raduis*Raduis;
        }

        public override double GetPerimeter()
        {
            return 2*Math.PI*Raduis;
        }
    }

    public class Traingle : Shape
    {
        public double Base {get;set;}

        public double Height {get;set;}

        public double Side1{get;set;}
        public double Side2{get;set;}
        public double Side3{get;set;}

        public Traingle(double baseVal,double height,double s1,double s2,double s3,string color) : base("Triangle", color)
        {
            Base=baseVal;
            Height=height;
            Side1=s1;
            Side2=s2;
            Side3=s3;
        }

        public override double GetArea()
        {
            return 0.5*Base*Height;
        }

        public override double GetPerimeter()
        {
            return Side1+Side2+Side3;
        }
    }

    public class Square:Rectangle
    {
        public Square(double side,string color) : base(side, side, color)
        {
            Name="Square";
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new()
            {
                new Rectangle(20,10,"red"),
                new Circle(7,"blue"),
                new Traingle(6,4,5,8,5,"Green"),
                new Square(4,"Yellow")
            };

            foreach (var shape in shapes)
            {
                shape.Display();
                System.Console.WriteLine();
                
            }
        }
    }
    
}