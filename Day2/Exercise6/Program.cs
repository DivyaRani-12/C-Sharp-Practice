using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise6
{ 
    class Student
    {
        public string Name { get; set; }
        public string ID { get; set; }
        private List<double> grades = new List<double>();

        public Student(string name, string id)
        {
            Name = name;
            ID = id;
        }

        public void AddGrade(double grade)
        {
            if (grade >= 0 && grade <= 100)
                grades.Add(grade);
            else
                throw new ArgumentException("Grade must be 0-100");
        }

        public void AddGrades(params double[] newGrades)
        {
            foreach (var grade in newGrades)
                AddGrade(grade);
        }

        public double GetAverage()
        {
            return grades.Count > 0 ? grades.Average() : 0;
        }

        public string GetLetterGrade()
        {
            double avg = GetAverage();
            return avg switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }

        public void PrintReport()
        {
            Console.WriteLine("\n=== Report Card ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"ID: {ID}");

            if (grades.Count > 0)
                Console.WriteLine($"Grades: {string.Join(", ", grades)}");
            else
                Console.WriteLine("Grades: No grades");

            Console.WriteLine($"Average: {GetAverage():F2}");
            Console.WriteLine($"Letter Grade: {GetLetterGrade()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            Student student = new Student("Divya", "S001");

            student.AddGrade(95);
            student.AddGrade(85);
            student.AddGrade(72);
            student.AddGrades(10, 85, 23);

            student.PrintReport();

            // ===== Menu for interactive grade entry =====
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Grade");
                Console.WriteLine("2. View Report");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");
                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter Grade: ");
                    string? input = Console.ReadLine();

                    if (double.TryParse(input, out double grade))
                    {
                        try
                        {
                            student.AddGrade(grade);
                            Console.WriteLine("Grade added successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number");
                    }
                }
                else if (choice == "2")
                {
                    student.PrintReport();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            // ===== Multiple students & comparison =====
            students.Add(student);
            students.Add(new Student("Aishu", "S002"));
            students.Add(new Student("Bhaghu", "S003"));

            students[1].AddGrades(85, 96, 75, 85, 25);
            students[2].AddGrades(85, 96, 25, 36);

            var topStudent = students
                .OrderByDescending(s => s.GetAverage())
                .First();

            Console.WriteLine("\n=== Top Student ===");
            Console.WriteLine($"Name: {topStudent.Name}");
            Console.WriteLine($"Average: {topStudent.GetAverage():F2}");
            Console.WriteLine($"Grade: {topStudent.GetLetterGrade()}");
        }
    }
}