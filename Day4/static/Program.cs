using System;

namespace Static
{
    public class Counter
    {
        private int instanceCount;

        private static int totalInsurances = 0;
        public Counter()
        {
            totalInsurances++;
            instanceCount=totalInsurances;
            
        }

        public void ShowInstanceNumber()
        {
            System.Console.WriteLine($"I am instance #{instanceCount}");
        }

        public static int GetTotalInstance()
        {
            return totalInsurances;
        }
    }

    public static class MathHelper
    {
        public static double Pi=3.14159;

        public static double CircleArea(double radius)
        {
            return Pi*radius*radius;
            
        }

        public static double CircleCircufernce(double radius)
        {
            return 2*Pi*radius;
        }
            
    }

    public class Configuration
    {
        public const double TaxRate=0.80;
        public const string AppName = "MyApp";

        public readonly string InstanceId;
        public static readonly DateTime StartTime;
        
        static Configuration()
        {
            StartTime=DateTime.Now;
        }

        public Configuration()
        {
            InstanceId=Guid.NewGuid().ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(Counter.GetTotalInstance());

            Counter c1=new Counter();
            Counter c2=new Counter();
            Counter c3=new Counter();

            c1.ShowInstanceNumber();
            c2.ShowInstanceNumber();

            System.Console.WriteLine(Counter.GetTotalInstance());

            double area=MathHelper.CircleArea(5);
            double circ = MathHelper.CircleCircufernce(5);

            Console.WriteLine($"Area: {area}");
            Console.WriteLine($"Circumference: {circ}");

            System.Console.WriteLine(Configuration.TaxRate);
            var config = new Configuration();
            System.Console.WriteLine(config.InstanceId);
            System.Console.WriteLine(Configuration.StartTime);

            
        }
    }
}