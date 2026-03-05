using System;
using System.Runtime.CompilerServices;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Advanced Calculator ===\n");

            bool running=true;

            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PerformCalculation("Add");
                        break;
                    case "2":
                        PerformCalculation("Substract");
                        break;
                    case "3":
                        PerformCalculation("Multiply");
                        break;
                    case "4":
                        PerformCalculation("Divide");
                        break;
                    case "5":
                        PerformCalculation("Power");
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine();
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Substract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            Console.WriteLine("5. Power");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice:");
        }
        static void PerformCalculation(string operation)
        {
            Console.Write("Enter First number: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter Second number: ");
            string? input2 = Console.ReadLine();

            Console.Write("Precision(1=int, 2=double, 3=decimal): ");
            string? typeChoice = Console.ReadLine();

            try
            {
                switch (typeChoice)
                {
                    case "1":
                        if (int.TryParse(input1, out int aInt) && int.TryParse(input2, out int bInt))
                        {
                            if(operation == "Divide" && bInt ==0){
                                Console.WriteLine("Cannot divide by zero.");
                                return;
                            }
                            int result = CalculateInt(operation,aInt,bInt);
                            Console.WriteLine($"Result:{result}");
                        }
                        else{
                            Console.WriteLine("Invalid Integer input");
                        }
                        break;
                    case "2":
                        if (double.TryParse(input1, out double aDouble) && double.TryParse(input2, out double bDouble))
                        {
                            if (operation == "Divide" && bDouble == 0)
                            {
                                Console.WriteLine("Cannot divide by zero.");
                                return;

                            }
                            double result = CalculateDouble(operation,aDouble,bDouble);
                            Console.WriteLine($"Result:{result}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Double input");
                        }
                        break;
                    case "3":
                        if (decimal.TryParse(input1, out decimal aDecimal) && decimal.TryParse(input2, out decimal bDecimal))
                        {
                            if(operation == "Divide" && bDecimal == 0)
                            {
                                Console.WriteLine("Cannot divide by zero.");
                                return;
                            }
                            decimal result = CalculateDecimal(operation,aDecimal,bDecimal);
                            Console.WriteLine($"Result:{result}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Decimal input");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid precision choice");
                        break;
                    }  
                
            }
            catch
            {
                Console.WriteLine("Error occured during calculation.");

            }
            
        }
        static int CalculateInt(string op,int a, int b)
        {
            return op switch
            {
                "Add"=>a+b,
                "Substract"=>a-b,
                "Multiply"=>a*b,
                "Divide"=>a/b,
                "Power"=>(int)Math.Pow(a, b),_=>0

            };
        }

        static double CalculateDouble(string op,double a,double b)
        {
            return op switch
            {
                "Add"=>a+b,
                "Substract"=>a-b,
                "Multiply"=>a*b,
                "Divide"=>a/b,
                "Power"=>Math.Pow(a, b),_=>0,
            };
        }
        static decimal CalculateDecimal(string op,decimal a,decimal b)
        {
            return op switch
            {
                "Add"=>a+b,
                "Substract"=>a-b,
                "Multiply"=>a*b,
                "Divide"=>a/b,
                "Power"=>(decimal)Math.Pow((double)a, (double)b),_=>0
            };
        }
    }
}