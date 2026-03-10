using System;
using System.Collections.Generic;

namespace Exercise3{
    public abstract class Shape
    {
        public string Color { get; set; } = "Black";
        public double X { get; set; }
        public double Y { get; set; }

        public abstract double GetArea();
        public abstract double GetPerimeter();

        public virtual void Draw()
        {
            Console.WriteLine($"Drawing {GetType().Name} at ({X}, {Y}) in {Color}");
        }
    }

    public interface IDrawable
    {
        void Draw();
    }

    public interface IResizable
    {
        void Resize(double factor);
    }

    public interface IRotatable
    {
        void Rotate(double degrees);
    }

    public interface ISelectable
    {
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }

    public class Circle : Shape, IDrawable, IResizable, ISelectable
    {
        public double Radius { get; set; }

        public bool IsSelected { get; private set; }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public void Resize(double factor)
        {
            Radius *= factor;
            Console.WriteLine($"Circle resized to radius: {Radius}");
        }

        public void Select()
        {
            IsSelected = true;
            Console.WriteLine("Circle selected");
        }

        public void Deselect()
        {
            IsSelected = false;
            Console.WriteLine("Circle deselected");
        }
    }

    public class Rectangle : Shape, IDrawable, IResizable, IRotatable
    {
        public double Width { get; set; }
        public double Height { get; set; }

        private double rotationAngle = 0;

        public override double GetArea()
        {
            return Width * Height;
        }

        public override double GetPerimeter()
        {
            return 2 * (Width + Height);
        }

        public void Resize(double factor)
        {
            Width *= factor;
            Height *= factor;
            Console.WriteLine($"Rectangle resized to {Width} x {Height}");
        }

        public void Rotate(double degrees)
        {
            rotationAngle += degrees;
            Console.WriteLine($"Rectangle rotated to {rotationAngle}°");
        }
    }

    public class Triangle : Shape, IDrawable, IRotatable
    {
        public double Base { get; set; }
        public double Height { get; set; }

        private double rotationAngle = 0;

        public override double GetArea()
        {
            return 0.5 * Base * Height;
        }

        public override double GetPerimeter()
        {
            double side = Math.Sqrt(Math.Pow(Base / 2, 2) + Math.Pow(Height, 2));
            return Base + 2 * side;
        }

        public void Rotate(double degrees)
        {
            rotationAngle += degrees;
            Console.WriteLine($"Triangle rotated to {rotationAngle}°");
        }
    }

    public class Line : Shape, IDrawable, IRotatable
    {
        public double Length { get; set; }

        private double rotationAngle = 0;

        public override double GetArea()
        {
            return 0;
        }

        public override double GetPerimeter()
        {
            return 0;
        }

        public void Rotate(double degrees)
        {
            rotationAngle += degrees;
            Console.WriteLine($"Line rotated to {rotationAngle}°");
        }
    }

    public class Canvas
    {
        private List<Shape> shapes = new();

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            Console.WriteLine($"Added {shape.GetType().Name}");
        }

        public void DrawAll()
        {
            Console.WriteLine("\n--- Drawing Canvas ---");

            foreach (var shape in shapes)
            {
                shape.Draw();
                Console.WriteLine($"Area: {shape.GetArea():F2}  Perimeter: {shape.GetPerimeter():F2}");
            }
        }

        public void ResizeAll(double factor)
        {
            Console.WriteLine($"\n--- Resizing all shapes by {factor}x ---");

            foreach (var shape in shapes)
            {
                if (shape is IResizable resizable)
                {
                    resizable.Resize(factor);
                }
            }
        }

        public void RotateAll(double degrees)
        {
            Console.WriteLine($"\n--- Rotating all shapes by {degrees}° ---");

            foreach (var shape in shapes)
            {
                if (shape is IRotatable rotatable)
                {
                    rotatable.Rotate(degrees);
                }
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            Canvas canvas = new();

            canvas.AddShape(new Circle { X = 10, Y = 10, Radius = 5, Color = "Red" });

            canvas.AddShape(new Rectangle { X = 20, Y = 20, Width = 10, Height = 5, Color = "Blue" });

            canvas.AddShape(new Triangle { X = 30, Y = 30, Base = 10, Height = 8, Color = "Green" });

            canvas.AddShape(new Line { X = 5, Y = 5, Length = 15, Color = "Black" });

            canvas.DrawAll();

            canvas.ResizeAll(1.5);

            canvas.RotateAll(45);

            canvas.DrawAll();
        }
    }
}