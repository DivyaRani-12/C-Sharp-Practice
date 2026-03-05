using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise01
{
    public class CartItem
    {
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public int Quantity { get; set; }
        public decimal Price { get; init; }
        
        public decimal Subtotal => Quantity * Price;
        
        public override string ToString()
        {
            return $"{ProductName} x{Quantity} @ {Price:C} = {Subtotal:C}";
        }
    }
    
    public class ShoppingCart
    {
        private Dictionary<string, CartItem> items = new();
        
        // Implemented indexer
        public CartItem? this[string productId]
        {
            get
            {
                // Return item or null if not found
                if (items.ContainsKey(productId))
                {
                    return items[productId];
                }
                return null;
            }
            set
            {
                // Add or update item
                if (value == null)
                {
                    items.Remove(productId);
                }
                else
                {
                    items[productId] = value;
                }
            }
        }
        
        public void AddItem(string productId, string productName, decimal price, int quantity = 1)
        {
            if (items.ContainsKey(productId))
            {
                items[productId].Quantity += quantity;
            }
            else
            {
                items[productId] = new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity
                };
            }
        }
        
        // Implemented RemoveItem
        public void RemoveItem(string productId)
        {
            if (items.ContainsKey(productId))
            {
                items.Remove(productId);
            }
        }
        
        public decimal GetTotal()
        {
            return items.Values.Sum(item => item.Subtotal);
        }
        
        public void Clear()
        {
            items.Clear();
        }
        
        public void DisplayCart()
        {
            Console.WriteLine("\n=== Shopping Cart ===");
            if (items.Count == 0)
            {
                Console.WriteLine("Cart is empty");
                return;
            }
            
            foreach (var item in items.Values)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"\nTotal: {GetTotal():C}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new();
            
            cart.AddItem("P001", "Laptop", 999.99m, 1);
            cart.AddItem("P002", "Mouse", 25.50m, 2);
            cart.AddItem("P003", "Keyboard", 75.00m, 1);
            
            cart.DisplayCart();
            
            // Using indexer
            Console.WriteLine($"\nLaptop quantity: {cart["P001"]?.Quantity}");
            
            // Update quantity using indexer
            if (cart["P002"] is CartItem mouseItem)
            {
                mouseItem.Quantity = 3;
            }
            
            cart.DisplayCart();
        }
    }
}