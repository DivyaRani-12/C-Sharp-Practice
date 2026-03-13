using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Exercise2
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal originalPrice);
        string GetDescription();
    }

    public class NoDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice;
        }

        public string GetDescription()
        {
            return "No discount";
        }
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscount(decimal percentage)
        {
            if(percentage<0 || percentage>100)
                throw new ArgumentException("Percentage must be between 0 and 100");

            this.percentage=percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            decimal discount=originalPrice*(percentage/100);
            return originalPrice-discount;
        }

        public string GetDescription()
        {
            return $"{percentage}% discount";
        }
    }

    public class FixedAmountDiscount : IDiscountStrategy
    {
        private readonly decimal amount;

        public FixedAmountDiscount(decimal amount)
        {
            if(amount<0)
                throw new ArgumentException("Discount amount cannot be negative");

            this.amount=amount;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            decimal finalPrice = originalPrice-amount;
            return finalPrice < 0 ? 0 : finalPrice;
        }

        public string GetDescription()
        {
            return $"Fixed discount of {amount:C}";
        }
    }

    public class BulkDiscount : IDiscountStrategy
    {
        private readonly int quantity;
        private readonly int requiredQuantity;
        private readonly decimal percentage;

        public BulkDiscount(int quantity,int requiredQuantity=3,decimal percentage = 15)
        {
            this.quantity=quantity;
            this.requiredQuantity=requiredQuantity;
            this.percentage=percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            if (quantity >= requiredQuantity)
            {
                decimal discount=originalPrice*(percentage/100);
                return originalPrice-discount;
            }
            return originalPrice;
        }

        public string GetDescription()
        {
            return $"Bulk discount {percentage}% for {requiredQuantity}+ items";
        }
    }

    public class SeasonalDiscount : IDiscountStrategy
    {
        private readonly DateTime startDate;
        private readonly DateTime endDate;
        private readonly  decimal percentage;
         
        public SeasonalDiscount(DateTime startDate, DateTime endDate, decimal percentage)
        {
            this.startDate=startDate;
            this.endDate=endDate;
            this.percentage=percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            DateTime today=DateTime.Now;

            if(today>=startDate && today <= endDate)
            {
                decimal discount=originalPrice*(percentage/100);
                return originalPrice - discount;
            }
            return originalPrice;
        }

        public string GetDescription()
        {
            return $"Seasonal discount {percentage}% {startDate:d} - {endDate:d}";
        }
    }

    public class LoyaltyDiscount : IDiscountStrategy
    {
        private readonly int customerPoints;

        public LoyaltyDiscount(int customerPoints)
        {
            this.customerPoints=customerPoints;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            decimal percentage=0;

            if(customerPoints>=1000)
                percentage=20;
            else if(customerPoints>=500)
                percentage=20;
            else if(customerPoints>=100)
                percentage=5;

            decimal discount = originalPrice*(percentage/100);
            return originalPrice - discount;
        }

        public string GetDescription()
        {
            return $"Loyalty discount based on points";
        }
    }

    public class ShoppingCart
    {
        private List<decimal> items=new();
        private IDiscountStrategy discountStrategy;

        public ShoppingCart(IDiscountStrategy discountStrategy)
        {
            this.discountStrategy=discountStrategy;
        }

        public void AddItem(decimal price)
        {
            items.Add(price);
        }

        public void SetDiscountStrategy(IDiscountStrategy strategy)
        {
            discountStrategy=strategy;
        }

        public int GetItemCount()
        {
            return items.Count;
        }

        public decimal GetTotal()
        {
            decimal subtotal = items.Sum();
            return discountStrategy.ApplyDiscount(subtotal);
        }

        public void Checkout()
        {
            decimal subtotal = items.Sum();
            decimal total = GetTotal();
            decimal discount=subtotal-total;

            System.Console.WriteLine("-----------------");
            System.Console.WriteLine($"Subtotal: {subtotal:C}");
            System.Console.WriteLine($"Discount: {discountStrategy.GetDescription()}");
            System.Console.WriteLine($"Discount Amount: {discount:C}");
            System.Console.WriteLine($"Total: {total:C}");
        }
    }

    class Program
    {
        static void Main()
        {
             ShoppingCart cart = new(new NoDiscount());

            cart.AddItem(100);
            cart.AddItem(200);
            cart.AddItem(150);

            cart.SetDiscountStrategy(new PercentageDiscount(10));
            cart.Checkout();

            cart.SetDiscountStrategy(new FixedAmountDiscount(50));
            cart.Checkout();

            cart.SetDiscountStrategy(new BulkDiscount(cart.GetItemCount()));
            cart.Checkout();

            cart.SetDiscountStrategy(new SeasonalDiscount(
                new DateTime(2026, 1, 1),
                new DateTime(2026, 12, 31),
                20));
            cart.Checkout();

            cart.SetDiscountStrategy(new LoyaltyDiscount(600));
            cart.Checkout();
        }
    }
}
