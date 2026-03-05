using System;
using System.Collections.Generic;
using System.Linq;

// Base class
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; }
    public decimal BaseSalary { get; set; }

    public Employee(string name, string email, decimal baseSalary)
    {
        Name = name;
        Email = email;
        BaseSalary = baseSalary;
        JoinDate = DateTime.Now;
    }

    public virtual decimal CalculateSalary()
    {
        return BaseSalary;
    }

    public virtual void GetDetails()
    {
        Console.WriteLine($"Employee: {Name}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Join Date: {JoinDate:d}");
        Console.WriteLine($"Salary: {CalculateSalary():C}");
    }
}

// PermanentEmployee
public class PermanentEmployee : Employee
{
    public decimal Benefits { get; set; }
    public decimal PensionContribution { get; set; }

    public PermanentEmployee(string name, string email, decimal baseSalary, decimal benefits)
        : base(name, email, baseSalary)
    {
        Benefits = benefits;
        PensionContribution = baseSalary * 0.1m;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + Benefits;
    }

    public override void GetDetails()
    {
        Console.WriteLine("Employee Type: Permanent");
        base.GetDetails();
        Console.WriteLine($"Benefits: {Benefits:C}");
        Console.WriteLine($"Pension Contribution: {PensionContribution:C}");
    }
}

// ContractEmployee
public class ContractEmployee : Employee
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public ContractEmployee(string name, string email, decimal hourlyRate, int hoursWorked)
        : base(name, email, 0)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public override decimal CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }

    public override void GetDetails()
    {
        Console.WriteLine("Employee Type: Contract");
        Console.WriteLine($"Employee: {Name}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Hourly Rate: {HourlyRate:C}");
        Console.WriteLine($"Hours Worked: {HoursWorked}");
        Console.WriteLine($"Salary: {CalculateSalary():C}");
    }
}

// Manager
public class Manager : PermanentEmployee
{
    public int TeamSize { get; set; }
    public decimal Bonus { get; set; }

    public Manager(string name, string email, decimal baseSalary, decimal benefits, int teamSize, decimal bonus)
        : base(name, email, baseSalary, benefits)
    {
        TeamSize = teamSize;
        Bonus = bonus;
    }

    public override decimal CalculateSalary()
    {
        return base.CalculateSalary() + Bonus;
    }

    public override void GetDetails()
    {
        Console.WriteLine("Employee Type: Manager");
        base.GetDetails();
        Console.WriteLine($"Team Size: {TeamSize}");
        Console.WriteLine($"Bonus: {Bonus:C}");
    }
}

// Test code
class Program
{
    static void Main()
    {
        List<Employee> employees = new()
        {
            new PermanentEmployee("Alice", "alice@company.com", 5000, 1000),
            new ContractEmployee("Bob", "bob@contractor.com", 50, 160),
            new Manager("Carol", "carol@company.com", 6000, 1200, 10, 2000)
        };

        foreach (var employee in employees)
        {
            employee.GetDetails();
            Console.WriteLine();
        }

        decimal totalPayroll = employees.Sum(e => e.CalculateSalary());
        Console.WriteLine($"Total Payroll: {totalPayroll:C}");
    }
}