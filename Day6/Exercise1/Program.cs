using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise1
{
    public class Employee
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;

        public DateTime JoinDate {get;set;}

        public decimal BaseSalary {get;set;}

        public Employee(string name,string email, decimal baseSalary)
        {
            Name=name;
            Email=email;
            BaseSalary=baseSalary;
            JoinDate=DateTime.Now;
        }

        public virtual decimal CalculateSalary()
        {
            return BaseSalary;
        }

        public virtual void GetDetails()
        {
            System.Console.WriteLine($"Employee:{Name}");
            System.Console.WriteLine($"Email:{Email}");
            System.Console.WriteLine($"Join Date: {JoinDate:d}");
            System.Console.WriteLine($"Calculate Salary: {CalculateSalary():C}");
        }
    }

    public class PermanentEmployee : Employee
    {
        public decimal Benefits { get; set;}
        public decimal PensionContribution { get; set; }

        public PermanentEmployee(string name,string email,decimal baseSalary,decimal benefits): base(name, email, baseSalary)
        {
            Benefits=benefits;
            PensionContribution=baseSalary*0.1m;
        }

        public override void GetDetails()
        {
            System.Console.WriteLine("Employee Type:Permanent");
            base.GetDetails();
            System.Console.WriteLine($"Benifits:{Benefits:C}");
            System.Console.WriteLine($"Pension Contribution:{PensionContribution:C}");

        }
    }

    public class ContractEmployee : Employee
    {
        public decimal HourlyRate {get;set;}
        public int HoursWorked {get;set;}

        public ContractEmployee(string name,string email,decimal hourlyRate,int hoursWorked) : base(name, email, 0)
        {
            HourlyRate=hourlyRate;
            HoursWorked=hoursWorked;

        }

        public override decimal CalculateSalary()
        {
            return HourlyRate*HoursWorked;
        }

        public override void GetDetails()
        {
            System.Console.WriteLine("Employee Type:Contract");
            System.Console.WriteLine($"Employee:{Name}");
            System.Console.WriteLine($"Emai:{Email}");
            System.Console.WriteLine($"Hourly Rate:{HourlyRate:C}");
            System.Console.WriteLine($"Hourly Worked': {HoursWorked}");
            System.Console.WriteLine($"Salary:{CalculateSalary():C}");
        }
    }

    public class Manager : PermanentEmployee
    {
        public int TeamSize { get; set;}
        public decimal Bonus { get; set;}

        public Manager(string name,string email,decimal baseSalary,decimal benefits,int teamSize,decimal bonus) : base(name, email, baseSalary, benefits)
        {
            TeamSize=teamSize;
            Bonus=bonus;
        }

        public override decimal CalculateSalary()
        {
            return base.CalculateSalary() + Bonus;
        }

        public override void GetDetails()
        {
            System.Console.WriteLine("Employee Type:Manager");
            base.GetDetails();
            System.Console.WriteLine($"Team Size:{TeamSize}");
            System.Console.WriteLine($"Bonus:{Bonus:C}");
        }
    }
    class Program
    {
        static void Main()
        {
             List<Employee> employees = new()
             {
                new PermanentEmployee("Divya","divya@gmail.com",500,1000),
                new ContractEmployee("AISHU","aishu@gmail.com",50,140),
                new Manager("dii","dii@gmail.com",10,200,100,200)
             };

             foreach(var employee in employees)
            {
                employee.GetDetails();
                System.Console.WriteLine();
            }

            decimal totalPayroll = employees.Sum(e=>e.CalculateSalary());
            System.Console.WriteLine($"Total Payroll:{totalPayroll:C}");
        }
    }
}