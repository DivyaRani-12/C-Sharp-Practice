using System;
using System.Reflection.Metadata;
using System.Text;

struct LargeStruct
{
    public int Id;
    public double Value;
    public string Name;
}

 struct Point
        {
            public int X;
            public int Y;
        }

namespace Training
{
    class Program
    {
        static string  GetDayType(int day)
        {
            switch (day)
            {
                case 1:
                case 7:
                    return "Weekend";
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return "Weekday";
                default:
                    return "Invalid";
            }
        }

        static string GetDayType1(int day)=>day switch
        {
            1 or 7=>"Weekend",
            >=2 and <=6 => "Weekday",
            _=>"Invalid"
        };

        static string DescribeNumber(object obj)=>obj switch
        {
            int n when n < 0=> "Negtaive Integer",
            int n when n== 0 => "Zero",
            int n => $"Postive Integer:{n}",
            double d=>$"Double:{d}",
            string s=>$"String: {s}",
            _=>"Unkown type"

        };

        public static int Add(int a,int b)
        {
            return a+b;
        }

        public static void Greet(string name, string greeting = "Hello")
        {
            System.Console.WriteLine($"{greeting},{name}!");
        }

        public static int Calculate(int a,int b)=>a+b;

        public static double Calculate(double a, double b)=>a+b;

        public static int Calculate(int a,int b, int c)=>a+b+c;

        static void IncrementValue(int x)
        {
            x++;
        }

        static void IncrementRef(ref int y)
        {
            y++;
        }

        static void ProcessLargeStruct(in LargeStruct data)
        {
            System.Console.WriteLine("Id:"+data.Id);
            System.Console.WriteLine("Value:"+data.Value);
            System.Console.WriteLine("Name:"+data.Name);

            // Not allowed (will give error)
            // data.Id = 10;  //  Cannot modify because it is 'in'
        }

        public static int Sum(params int[] numbers){

            return numbers.Sum();
        } 

       

       


        static void Main(string[] args)
        {
            System.Console.WriteLine(GetDayType(1));     
            Console.WriteLine(GetDayType1(2));
            System.Console.WriteLine(DescribeNumber(10));

            string[] names = {"Divya","Aishu","Patil"};
            foreach(string name in names)
            {
                System.Console.WriteLine(name);
            }

            for(int i = 0; i < names.Length; i++)
            {
                System.Console.WriteLine($"{i}:{names[i]}");
            }
            
            int count = 0;
            while (count < 5)
            {
                System.Console.WriteLine(count++);
            }

            do
            {
                System.Console.WriteLine("Executed once");
            } while(false);

            foreach(int i in Enumerable.Range(1,10))
            {
                System.Console.WriteLine(i);
            }

            System.Console.WriteLine(Add(10,20));

            Greet(greeting:"Hii", name:"Divya");

            System.Console.WriteLine(Calculate(10,20));

            System.Console.WriteLine(Calculate(10,50));
            
            System.Console.WriteLine(Calculate(41,25,36));

            int num = 5;
            IncrementValue(num);
            System.Console.WriteLine(num);

            int num1=5;
            IncrementRef(ref num1);
            System.Console.WriteLine(num1);

            bool TryParse(string input, out int result)
            {
                result=0;
                return int.TryParse(input,out result);
            }

           

            if(int.TryParse("abc",out int value))
            {
                System.Console.WriteLine(value);
            }


            LargeStruct ls =new LargeStruct();
            ls.Id = 1;
            ls.Value = 99.5;
            ls.Name="divya";

            ProcessLargeStruct(in ls);



            
            System.Console.WriteLine(Sum(1,2,3));
            System.Console.WriteLine(Sum(1,8,3,7,6));
            int[] arr = {8,7,5};
            System.Console.WriteLine(Sum(arr));

            Point p1 = new Point{X=10,Y=30};
            Point p2 = p1;
            p2.X=100;
            System.Console.WriteLine(p1.X);
            System.Console.WriteLine(p2.X);

            Person person1 = new Person{Name="dii",Age=23};
            Person person2 = person1;
            person2.Age=25;
            System.Console.WriteLine(person1.Age);
            System.Console.WriteLine(person2.Age);

            // BOXING - Value type → Reference type (slow!)
            int num3 = 123;
            object obj = num3;  // Boxing: num is copied to heap

            // UNBOXING - Reference type → Value type (slow!)
            int num2 = (int)obj;  // Unboxing: cast required

            // Avoid boxing in performance-critical code
            // Use generics instead of object type
            List<int> numbers = new List<int>();  // No boxing
            //ArrayList oldList = new ArrayList();  // Boxing when adding int!

            // String is a reference type but behaves like value type
            string s1 = "Hello";
            string s2 = s1;  // Reference copied
            s2 = "World";    // Creates NEW string, s1 unchanged

            Console.WriteLine(s1);  // "Hello"
            Console.WriteLine(s2);  // "World"

            // String concatenation creates new strings
            string result = "";
            for (int i = 0; i < 1000; i++)
            {
                result += i;  // BAD! Creates 1000 new strings
            }
            //System.Console.WriteLine(result);

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 1000; i++)
            {
                sb.Append(i);
            }
            string result1 = sb.ToString();
            //System.Console.WriteLine(result1);


            int[] arr4={1,2,3};
            int[] arr5=arr4;
            arr5[0]=100;
            System.Console.WriteLine(arr4[0]);

            int[] arr6 = (int[])arr4.Clone();
            arr6[0]=200;
            System.Console.WriteLine(arr4[0]);
            System.Console.WriteLine(arr6[0]);

            int[] arr7 = arr4.ToArray();
            arr7[0]=300;
            System.Console.WriteLine(arr7[0]);
            System.Console.WriteLine(arr4[0]);

           

        }

        
           
            
    }
     class Person
        {
            public string Name{get;set;}
            public int Age{get;set;}
        }
}