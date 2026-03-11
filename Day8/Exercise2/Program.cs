using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise2
{
    public interface IEntity
    {
        int Id {get;set;}
    }

    public interface IRepository<T> where T : IEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T? GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T,bool>predicate);
        IEnumerable<T> GetPage(int pageNumber, int pageSize);
    }

    public class InMemoryRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> entities=new();
        private int nextId=1;

        public void Add(T entity)
        {
            entity.Id=nextId++;
            entities.Add(entity);
        }

        public void Update(T entity)
        {
            var existing = entities.FirstOrDefault(e=>e.Id==entity.Id);

            if (existing != null)
            {
                int index=entities.IndexOf(existing);
                entities[index]=entity;
            }
        }

        public void Delete(int id)
        {
            var entity=entities.FirstOrDefault(e=>e.Id==id);

            if (entity != null)
            {
                entities.Remove(entity);
            }
        }

        public T? GetById(int id)
        {
            return entities.FirstOrDefault(e=>e.Id==id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return entities.Where(predicate);
        }

        public IEnumerable<T> GetPage(int pageNumber,int pageSize)
        {
            return entities
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize);
        }
    }

    public class Product : IEntity
    {
        public int Id {get;set;}
        public string Name {get;set;}=string.Empty;
        public decimal Price{get;set;}
        public string Category{get;set;}=string.Empty;
    }

    public class Customer : IEntity
    {
        public int Id {get;set;}
        public string Name{get;set;}=string.Empty;
        public string Email{get;set;}=string.Empty;
    }

    class Program
    {
        static void Main()
        {
            IRepository<Product> productRepo = new InMemoryRepository<Product>();

            productRepo.Add(new Product {Name="Laptop",Price=999.9m,Category="Electronics"});
            productRepo.Add(new Product {Name="Mouse",Price=25.50m,Category="Electronics"});
            productRepo.Add(new Product {Name="Desk",Price=299.9m,Category="Furniture"});

            System.Console.WriteLine("All Products:");
            foreach(var p in productRepo.GetAll())
            {
                System.Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} - {p.Category}");
            }

            System.Console.WriteLine("\n Expesive Products(>100):");
            var expesive=productRepo.Find(p=>p.Price>100);

            foreach(var p in expesive)
            {
                System.Console.WriteLine(p.Name);
            }
            System.Console.WriteLine("\n Page 1 (2 items):");
            var page=productRepo.GetPage(1,2);

            foreach(var p in page)
            {
                System.Console.WriteLine(p.Name);
            }

            System.Console.WriteLine("\n Testing Customer Repository");

            IRepository<Customer> customerRepo = new InMemoryRepository<Customer>();
            customerRepo.Add(new Customer {Name="Divya",Email="divya@gmail.com"});
            customerRepo.Add(new Customer {Name="Aishu",Email="Aishu@gmail.com"});

            foreach(var c in customerRepo.GetAll())
            {
                System.Console.WriteLine($"{c.Id} - {c.Name} - {c.Email}");
            }
        }
    }
}