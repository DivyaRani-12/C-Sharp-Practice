using System;

namespace Training
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.name=name;
            this.age=age;
        }

        public void Introduce()
        {
            System.Console.WriteLine($"Hi,I'm {name},{age} years old.");
        }
    }

    public class BankAccount
    {
        private string accountNumber;
        private decimal balance;
        private string ownerName;

        public BankAccount()
        {
            accountNumber=GenerateAccountNumber();
            balance=0;
            ownerName="Unkown";

        }

        public BankAccount(string owner)
        {
            accountNumber=GenerateAccountNumber();
            balance=0;
            ownerName=owner;
        }

        public BankAccount(string owner,decimal initialBalance)
        {
            accountNumber=GenerateAccountNumber();
            balance=initialBalance;
            ownerName=owner;
        }

        public BankAccount(string owner,decimal initialBalance,string accountNum):this(owner,initialBalance)
        {
            accountNumber=accountNum;
            
        }

        public string GenerateAccountNumber()
        {
            return $"ACC{Random.Shared.Next(100000,999999)}";
        }

        public void DisplayAccount()
        {
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Owner Name: {ownerName}");
            Console.WriteLine($"Balance: {balance}");
            Console.WriteLine("-------------------------");
        }
    }

    public class Person1
    {
        private string name;

        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            name=value;
        }
    }

    public class Person2
    {
        public string Name{ get; set; }
        public int Age{ get; set; }
    }

    public class Person3
    {
        public string Name{get;set;}
        public string Id{get;set;}
        public int Age{get;set;}
        public int BirthYear=>DateTime.Now.Year - Age;

        private decimal _balance;
        public decimal Balance
        {
            get {return _balance;}
            set
            {
                if(value<0)
                    throw new ArgumentException("Balance can't be negative");
                    _balance=value;
                
            }
        }

        private string _name;
        public string Name1
        {
            get=>_name;
            set=>_name= value?.Trim() ?? throw new ArgumentException();
        }

        public int LoginAccount{get;private set;}

        public void Login()
        {
            LoginAccount++;
        }
    }

    public class Product
    {
        private decimal _cost;

        public decimal Price
        {
            get=> _cost*1.2m;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("price must be postive");
                _cost=value/1.2m;
                
            }
        }
        public string Name{get;set;}
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p1=new Person("Divya",21);
            Person p2=new Person("Aishu",22);
            
            p1.Introduce();
            p2.Introduce();

            BankAccount acc1=new BankAccount();
            BankAccount acc2=new BankAccount("Divya");
            BankAccount acc3=new BankAccount("aishu",1000);

            acc1.DisplayAccount();
            acc2.DisplayAccount();
            acc3.DisplayAccount();
            
            

            Person2 p3 = new Person2();

            // Setter (assign value)
            p3.Name = "Divya";
            p3.Age = 21;

            // Getter (read value and print)
            Console.WriteLine("Person2 Details:");
            Console.WriteLine($"Name: {p3.Name}");
            Console.WriteLine($"Age: {p3.Age}");
            Console.WriteLine("-------------------------");
            
            //Printing Old Getter/Setter Method (Person1)
            Person1 p4 = new Person1();

            // Setter method
            p4.SetName("Aishu");

            // Getter method and print
            Console.WriteLine("Person1 Details:");
            Console.WriteLine($"Name: {p4.GetName()}");
            Console.WriteLine("-------------------------");

            Person3 p5=new Person3 {Id="P001", Name="Divya",Age=21};

            p5.Name1 = "  Divya Rani  ";   // Setter will Trim()
            p5.Balance = 5000;             // Validation property
            p5.Login();
            p5.Login();

            Console.WriteLine("Person3 Details:");
            Console.WriteLine($"Id: {p5.Id}");
            Console.WriteLine($"Name: {p5.Name}");
            Console.WriteLine($"Age: {p5.Age}");
            Console.WriteLine($"Birth Year: {p5.BirthYear}");
            Console.WriteLine($"Trimmed Name (Name1): {p5.Name1}");
            Console.WriteLine($"Balance: {p5.Balance}");
            Console.WriteLine($"Login Count: {p5.LoginAccount}");
            Console.WriteLine("-------------------------");

            //p.Id = "P002";  // ❌ Compile error (init-only)

            Product prod = new Product();
            // Setter (this calls set)
            prod.Name = "Laptop";
            prod.Price = 1200;   // Selling price

            // Getter (this calls get)
            Console.WriteLine("Product Details:");
            Console.WriteLine($"Product Name: {prod.Name}");
            Console.WriteLine($"Selling Price: {prod.Price}");
            Console.WriteLine("-------------------------");
                    }
    }
}