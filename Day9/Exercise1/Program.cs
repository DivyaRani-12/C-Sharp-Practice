using System;
using System.Collections.Generic;

namespace Exercise1
{
    public interface IReader<out T>
    {
        T Read();
        IEnumerable<T> ReadAll();
    }

    public interface IWriter<in T>
    {
        void Write(T item);
        void WriteAll(IEnumerable<T> items);

    }

    public class Animal
    {
        public string Name{get;set;}=string.Empty;
        public int Age {get;set;}
    }

    public class Dog : Animal
    {
        public string Breed{get;set;}=string.Empty;
    }

    public class Cat : Animal
    {
        public int Lives {get;set;} = 9;
    }

    public class DogReader : IReader<Dog>
    {
        private List<Dog> dogs = new()
        {
            new Dog {Name="Buddy",Age=3,Breed="Golden Retriever"},
            new Dog {Name="Max", Age=4,Breed="German Shepherd"}
        };

        public Dog Read()
        {
            return dogs[0];
        }

        public IEnumerable<Dog> ReadAll()
        {
            return dogs;
        }
    }

    public class AnimalWriter : IWriter<Animal>
    {
        public void Write(Animal item)
        {
            System.Console.WriteLine($"Writing animal: {item.Name}");
        }

        public void WriteAll(IEnumerable<Animal> items)
        {
            foreach(var item in items)
            {
                Write(item);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("===== Coveriance Test ======");

            IReader<Dog> dogReader = new DogReader();

            IReader<Animal> animalReader = dogReader;

            Animal animal = animalReader.Read();
        Console.WriteLine($"Read animal: {animal.Name}");

        Console.WriteLine("\nReading all animals:");
        foreach (var a in animalReader.ReadAll())
        {
            Console.WriteLine(a.Name);
        }

        Console.WriteLine("\n=== CONTRAVARIANCE TEST ===");

        IWriter<Animal> animalWriter = new AnimalWriter();

        // Contravariance: Animal -> Dog
        IWriter<Dog> dogWriter = animalWriter;

        dogWriter.Write(new Dog
        {
            Name = "Rex",
            Age = 4,
            Breed = "Labrador"
        });

        Console.WriteLine("\nWriting multiple dogs:");
        List<Dog> dogs = new()
        {
            new Dog { Name = "Rocky", Age = 2 },
            new Dog { Name = "Charlie", Age = 6 }
        };

        dogWriter.WriteAll(dogs);

        Console.WriteLine("\n=== FLEXIBILITY WITH CAT ===");

        // Same writer can handle Cat because Cat is also Animal
        animalWriter.Write(new Cat
        {
            Name = "Kitty",
            Age = 2,
            Lives = 9
        });

        List<Animal> animals = new()
        {
            new Dog { Name = "Tiger", Age = 3 },
            new Cat { Name = "Misty", Age = 1 }
        };

        animalWriter.WriteAll(animals);
        }
    }
}