using System;
using System.Collections.Generic;

namespace Exercise3
{
    public abstract class Bird
    {
        public string Name { get; set; } = string.Empty;

        public abstract void Move();
    }

    public abstract class FlyingBird : Bird
    {
        public abstract void Fly();

        public override void Move()
        {
            Fly();
        }
    }
    public abstract class NonFlyingBird : Bird
    {
        public override void Move()
        {
            Console.WriteLine($"{Name} is walking or swimming");
        }
    }
    public class Sparrow : FlyingBird
    {
        public override void Fly()
        {
            Console.WriteLine($"{Name} is flying fast");
        }
    }

    public class Eagle : FlyingBird
    {
        public override void Fly()
        {
            Console.WriteLine($"{Name} is soaring high in the sky");
        }
    }
    public class Penguin : NonFlyingBird
    {
        public override void Move()
        {
            Console.WriteLine($"{Name} is swimming");
        }
    }
    public class Ostrich : NonFlyingBird
    {
        public override void Move()
        {
            Console.WriteLine($"{Name} is running fast");
        }
    }

    class Program
    {
        static void MakeBirdMove(Bird bird)
        {
            bird.Move();
        }

        static void MakeBirdFly(FlyingBird bird)
        {
            bird.Fly();
        }

        static void Main(string[] args)
        {
            Sparrow sparrow = new Sparrow { Name = "Sparrow" };
            Eagle eagle = new Eagle { Name = "Eagle" };
            Penguin penguin = new Penguin { Name = "Penguin" };
            Ostrich ostrich = new Ostrich { Name = "Ostrich" };

            Console.WriteLine("All birds moving:");
            MakeBirdMove(sparrow);
            MakeBirdMove(eagle);
            MakeBirdMove(penguin);
            MakeBirdMove(ostrich);

            Console.WriteLine("\nOnly flying birds:");
            MakeBirdFly(sparrow);
            MakeBirdFly(eagle);
        }
    }
}