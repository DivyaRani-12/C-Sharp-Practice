using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Exercise2
{
    public class Product
    {
        private decimal price;
        private int stock;

        public string Id{get;set;}
        public string Name {get; set;}

        public decimal Price
        {
            get=>price;
            set
            {
                if(value<0)
                    throw new Exception("Price canot be negative");
                price=value;
            }
        }

        public int Stock=>stock;

        public Product(string id,string name,decimal price,int initialStock = 0)
        {
            Id=id;
            Name=name;
            Price=price;
            stock=initialStock;
        }

        public void AddStock(int quantity)
        {
            if(quantity <= 0)
                throw new ArgumentException("Quantity must be postive");
            stock+=quantity;
        }

        public bool RemoveStock(int quantity)
        {
            if(quantity<=0)
                throw new ArgumentException("Quantity must be postive");

            if(quantity>stock)
                return false;

            stock-=quantity;
            return true;
        }

        public decimal GetTotalValue()
        {
            return price*stock;
        }

        public override string ToString()
        {
            return $"{Id}|{Name,-20} | Price: {Price:C} | Stock:{Stock} | Value:{GetTotalValue():C}";
        }

    }

    public class Inventory
    {
        private Dictionary<string, Product> products = new Dictionary<string, Product>();

        public bool AddProduct(Product product)
        {
            if(products.ContainsKey(product.Id))
                return false;
            
            products[product.Id] = product;
            return true;
        }

        public bool RemoveProduct(string id)
        {
            return products.Remove(id);
        }

        public Product FindProduct(string id)
        {
            products.TryGetValue(id, out Product product);
            return product;
        }

        public List<Product> FindProductsByName(string namePart)
        {
            return products.Values
                           .Where(p=>p.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(p=>p.Name)
                           .ToList();
        }

        public List<Product> ListAllProducts()
        {
            return products.Values.OrderBy(p=>p.Name).ToList();
        }

        public decimal GetTotalInventoryValue()
        {
            return products.Values.Sum(p=>p.GetTotalValue());
        }

        public void PrintInventoryReport()
        {
            System.Console.WriteLine("\n===Inventory Report===");
            System.Console.WriteLine($"Total Products:{products.Count}");
            System.Console.WriteLine($"Total Value:{GetTotalInventoryValue():C}\n");

            foreach(var product in ListAllProducts())
            {
                System.Console.WriteLine(product);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory =  new Inventory();

            inventory.AddProduct(new Product("P001","Laptop",999.9m,10));
            inventory.AddProduct(new Product("P002","Mouse",89.9m,50));
            inventory.AddProduct(new Product("P003","Keyboard",58.5m,26));

            inventory.PrintInventoryReport();

            while (true)
            {
                System.Console.WriteLine("\nInventory Menu");
                System.Console.WriteLine("1. Add Product");
                System.Console.WriteLine("2. Remove Product");
                System.Console.WriteLine("3. Find product by ID");
                System.Console.WriteLine("4. Search product by Name");
                System.Console.WriteLine("5. Add Stock");
                System.Console.WriteLine("6. Remove Stock");
                System.Console.WriteLine("7. Show Inventory Report");
                System.Console.WriteLine("8. Show Total Inventory Value");
                System.Console.WriteLine("0. Exit");
                Console.Write("choose option:");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter ID:");
                            string id = Console.ReadLine();

                            Console.Write("Enter name:");
                            string name = Console.ReadLine();

                            Console.Write("Enter Price:");
                            decimal price = decimal.Parse(Console.ReadLine());

                            Console.Write("Enter Initial Stock:");
                            int stock = int.Parse(Console.ReadLine());

                            bool added = inventory.AddProduct(new Product(id,name,price,stock));
                            System.Console.WriteLine(added ? "Product added":"Product already exists.");
                            break;

                        case "2":
                            Console.Write("Enter ID to remove:");
                            string removeId = Console.ReadLine();
                            System.Console.WriteLine(inventory.RemoveProduct(removeId)
                                ? "Product removed.":"Product not found");
                            break;

                        case "3":
                            Console.Write("Enter ID:");
                            string findId = Console.ReadLine();
                            var product = inventory.FindProduct(findId);
                            System.Console.WriteLine(product != null ? product.ToString() : "Product not found");
                            break;

                        case "4":
                            Console.Write("Enter name to search:");
                            string namePart = Console.ReadLine();
                            var results = inventory.FindProductsByName(namePart);
                            if (results.Count==0)
                                System.Console.WriteLine("No product found.");
                            else
                                results.ForEach(p=>System.Console.WriteLine(p));
                            break;

                        case "5":
                            Console.Write("Enter Product ID:");
                            string addStockId = Console.ReadLine();
                            var p1=inventory.FindProduct(addStockId);
                            if (p1 != null)
                            {
                                Console.Write("Enter quantity:");
                                int qty = int.Parse(Console.ReadLine());
                                p1.AddStock(qty);
                                System.Console.WriteLine("stock added.");
                            }
                            else
                                System.Console.WriteLine("Product not found");
                            break;
                        
                        case "6":
                            Console.Write("Enter Product ID:");
                            string removeStockId = Console.ReadLine();
                            var p2=inventory.FindProduct(removeStockId);
                            if(p2 != null)
                            {
                                Console.Write("Enter quantity:");
                                int qty = int.Parse(Console.ReadLine());
                                bool success = p2.RemoveStock(qty);
                                System.Console.WriteLine(success ? "Stock removed":"Not enough stock.");
                            }
                            else
                                System.Console.WriteLine("Product not found");
                            break;

                        case "7":
                            inventory.PrintInventoryReport();
                            break;

                        case "8":
                            System.Console.WriteLine($"Total inventory value:{inventory.GetTotalInventoryValue():C}");
                            break;

                        case "0":
                            return;

                        default:
                            System.Console.WriteLine("Invalid Choice.");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine($"Error:{ex.Message}");
                }
            }


            
        }
    }
}