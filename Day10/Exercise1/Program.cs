using System;

namespace SRPExample
{
    public class Employee
    {
        public int Id {get;set;}
        public string Name {get;set;}=string.Empty;
        public string Email {get;set;} =string.Empty;

        public decimal Salary {get;set;}

        public string Department {get;set;}=string.Empty;

    }

    public class EmployeeValidator
    {
        public bool Validate(Employee employee)
        {
            return !string.IsNullOrEmpty(employee.Name) &&
                    employee.Email.Contains('@') &&
                    employee.Salary>0;
        }       
    }

    public class EmployeeRepository
    {
        public void Save(Employee employee)
        {
            System.Console.WriteLine($"Saving employee to database: {employee.Name}");
        }


        public Employee? LoadById(int id)
        {
            System.Console.WriteLine($"Loading employee with Id: {id}");

            return new Employee
            {
                Id=id,
                Name="Divya",
                Email="divya@gmail.com",
                Salary=40000,
                Department="HR"
            };
        }
    }

    public class SalaryCalculator
    {
        public decimal CalculateAnnualSalary(Employee employee)
        {
            return employee.Salary*12;
        }

        public decimal CalculateTax(Employee employee)
        {
            return employee.Salary*0.2m;
        }
    }

    public class EmailSevice
    {
        public void SendWelcomeEmail(Employee employee)
        {
            System.Console.WriteLine($"Sending welcome email to {employee.Email}");
        }

        public void SendPayslip(Employee employee)
        {
            System.Console.WriteLine($"Sending payslip to {employee.Email}");
        }
    }

    public class EmployeeReportGenerator
    {
        public string GenerateEmployeeReport(Employee employee)
        {
            return $"Employee:{employee.Name}, Department:{employee.Department},Salary:{employee.Salary:C}";
        }

        public string ExportToJson(Employee employee)
        {
            return $"{{\"name\":\"{employee.Name}\",\"salary\":{employee.Salary}}}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee
            {
                Id=1,
                Name="Aishu",
                Email="aishu@gmail.com",
                Salary=5000,
                Department="IT"
            };

            EmployeeValidator validator = new EmployeeValidator();

            if (validator.Validate(emp))
            {
                EmployeeRepository repo=new EmployeeRepository();
                repo.Save(emp);

                SalaryCalculator calculator=new SalaryCalculator();
                decimal annualSalary = calculator.CalculateAnnualSalary(emp);
                decimal tax = calculator.CalculateTax(emp);

                EmailSevice emailSevice=new EmailSevice();
                emailSevice.SendWelcomeEmail(emp);
                emailSevice.SendPayslip(emp);

                EmployeeReportGenerator report = new EmployeeReportGenerator();
                string employeeReport = report.GenerateEmployeeReport(emp);
                string json = report.ExportToJson(emp);

                System.Console.WriteLine();
                System.Console.WriteLine($"Annual Salary: {annualSalary}");
                System.Console.WriteLine($"Tax: {tax}");
                System.Console.WriteLine(employeeReport);
                System.Console.WriteLine($"JSON:{json}");
            }
            else
            {
                System.Console.WriteLine("Employee Validation Failed.");
            }
        }
    }
}
