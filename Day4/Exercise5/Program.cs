using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise5
{
    public class Product
    {
        private decimal price;

        public string Id {get;set;}
        public string Name {get;set;}
        public string Category {get;set;}

        public decimal Price
        {
            get=>price;
            set
            {
                if(value<0)
                    throw new ArgumentException("Price canot be negative");
                price=value;
            }
        }

        public Product(string id,string name, decimal price,string Category)
        {
            Id = id;
            Name=name;
            Price=price;
            Category=Category;
        }
    }

    public class CartItem
    {
        private int quantity;
        public const int MaxQuantity = 99;

        public Product Product {get;}

        public int Quantity
        {
            get=>quantity;
            set
            {
                if(value<=0)
                    throw new ArgumentException("Quantity must be greater than 0.");

                if(value>MaxQuantity)
                    throw new ArgumentException($"Cannot exceed {MaxQuantity} items per product.");

                quantity=value;
            }
        }
        public decimal Subtotal=>Product.Price*Quantity;

        public CartItem(Product product,int quantity)
        {
            Product=product ?? throw new ArgumentException(nameof(product));
            Quantity=quantity;
        }
    }

    public class ShoppingCart
    {
        private List<CartItem> items=new List<CartItem>();
        private decimal discountAmount = 0;
        public IReadOnlyList<CartItem> Items => items.AsReadOnly();

        public void AddItem(Product product,int quantity)
        {
            if(quantity<=0)
                throw new ArgumentException("Quantity must be postive");

            var existingItem = items.FirstOrDefault(i=>i.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity+=quantity;
            }
            else
            {
                items.Add(new CartItem(product, quantity));
            }
        }

        public void RemoveItem(string productId)
        {
            var item = items.FirstOrDefault(i=>i.Product.Id==productId);

            if(item != null)
                items.Remove(item);
        }

        public void UpdateQuantity(string productId, int newQuantity)
        {
            var item = items.FirstOrDefault(i=>i.Product.Id == productId);

            if(item==null)
                throw new ArgumentException("Product nto found in cart.");

            item.Quantity = newQuantity;
        }

        public decimal CalculateTotal()
        {
            return items.Sum(i=>i.Subtotal);
        }

        public void ApplyDiscountCode(string code)
        {
            decimal total = CalculateTotal();

            switch (code.ToUpper())
            {
                case "SAVE10":
                    discountAmount = total * 0.10m;
                    break;

                case "FLAT100":
                    discountAmount=100m;
                    break;

                default:
                    throw new ArgumentException("Invalid discount code.");
            }

            if(discountAmount>total)
                discountAmount=total;
        }

        public decimal GetFinalTotal()
        {
            return CalculateTotal()-discountAmount;
        }

        public void ClearCart()
        {
            items.Clear();
            discountAmount=0;
        }

        public void DisplayCart()
        {
            System.Console.WriteLine("=======SHOPPING CART SUMMARY ======");

            foreach(var item in items)
            {
                System.Console.WriteLine($"{item.Product.Name} | ₹{item.Product.Price:F2} x {item.Quantity} = ₹{item.Subtotal:F2}");

            }

            System.Console.WriteLine("------------------------");
            Console.WriteLine($"Total: ₹{CalculateTotal():F2}");
            Console.WriteLine($"Discount: ₹{discountAmount:F2}");
            Console.WriteLine($"Final Total: ₹{GetFinalTotal():F2}");
            Console.WriteLine("==================================");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Product p1=new Product("P001","Laptop",5000m,"Electronics");
            Product p2=new Product("P002","Headphone",2000m,"Electronics");

            ShoppingCart cart=new ShoppingCart();

            cart.AddItem(p1,1);
            cart.AddItem(p2,2);

            cart.ApplyDiscountCode("SAVE10");

            cart.DisplayCart();
        }
    }
}