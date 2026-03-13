using System;

namespace Exercise6
{
    public class Product
    {
        public string Name {get;set;}
        public decimal Price {get;set;}

        public string Category {get;set;}

        public override string ToString()
        {
            return $"Product: {Name},Price:{Price},Category:{Category}";
        }
    }

    public abstract class Builder<TBuilder, TObject>
        where TBuilder:Builder<TBuilder, TObject>
        where TObject : new()
    {
        protected TObject Instance = new TObject();

        protected TBuilder This => (TBuilder)this;

        public TObject Build()
        {
            Validate();
            return Instance;
        }

        protected virtual void Validate()
        {
            
        }

        public TBuilder Clone()
        {
            var clone = (TBuilder)Activator.CreateInstance(typeof(TBuilder));
            clone.Instance=Instance;
            return clone;
        }
    }

    public class ProductBuilder : Builder<ProductBuilder, Product>
    {
        public static ProductBuilder Create()
        {
            return new ProductBuilder();
            
        }

        public ProductBuilder WithName(string name)
        {
            Instance.Name=name;
            return This;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            Instance.Price=price;
            return This;
        }

        public ProductBuilder InCategory(string category)
        {
            Instance.Category=category;
            return This;
        }

        protected override void Validate()
        {
            if(string.IsNullOrWhiteSpace(Instance.Name))
                throw new Exception("Product must have a name");

            if(Instance.Price<=0)
                throw new Exception("Price must be greater then zero");
        }
    }

    class Program
    {
        static void Main()
        {
            System.Console.WriteLine("=== Fluent Builder Example ===");

            var product=ProductBuilder
                .Create()
                .WithName("Laptop")
                .WithPrice(999.9m)
                .InCategory("Electronics")
                .Build();

            System.Console.WriteLine(product);

            System.Console.WriteLine("\n ==== Clone Example ====");

            var cloneProduct = ProductBuilder
                .Create()
                .WithName("Phone")
                .WithPrice(500)
                .InCategory("Electronics")
                .Clone()
                .WithPrice(450)
                .Build();

            System.Console.WriteLine(cloneProduct);
        }
    }
}
