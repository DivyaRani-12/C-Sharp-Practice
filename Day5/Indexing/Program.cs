using System;
using System.Collections.Generic;
using System.Linq;

namespace Indexing
{
    public class StudentGrades
    {
        private Dictionary<string, int> grades = new();

        // Indexer using subject name
        public int this[string subject]
        {
            get
            {
                if (grades.TryGetValue(subject, out int grade))
                    return grade;

                return 0;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Grade must be 0-100");

                grades[subject] = value;
            }
        }

        // Indexer using numeric index
        public int this[int index]
        {
            get => grades.Values.ElementAt(index);
        }

        // Matrix class with 2D indexer
        public class Matrix
        {
            private int[,] data;

            public Matrix(int rows, int cols)
            {
                data = new int[rows, cols];
            }

            public int this[int row, int col]
            {
                get => data[row, col];
                set => data[row, col] = value;
            }
        }
    }

    public class WeekSchedule
    {
        private Dictionary<DayOfWeek, string> schedule = new();

        public string this[DayOfWeek day]
        {
            get=>schedule.TryGetValue(day, out string? activity) ? activity:"Free";
            set=>schedule[day]=value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentGrades grades = new StudentGrades();

            grades["Math"] = 85;
            grades["Science"] = 97;

            int mathGrade = grades["Math"];

            Console.WriteLine(mathGrade);

            // Access using index
            Console.WriteLine(grades[0]);

            // Matrix example
            StudentGrades.Matrix matrix = new StudentGrades.Matrix(2, 2);

            matrix[0, 0] = 5;
            matrix[0, 1] = 10;

            Console.WriteLine(matrix[0, 1]);

            WeekSchedule myWeek = new();
            myWeek[DayOfWeek.Monday]="Team Meeting";
            myWeek[DayOfWeek.Wednesday]="Client Presentation";
            myWeek[DayOfWeek.Friday]="Code Review";

            System.Console.WriteLine(myWeek[DayOfWeek.Monday]);
            System.Console.WriteLine(myWeek[DayOfWeek.Tuesday]);
            System.Console.WriteLine(myWeek[DayOfWeek.Wednesday]);
            
        }
    }
}