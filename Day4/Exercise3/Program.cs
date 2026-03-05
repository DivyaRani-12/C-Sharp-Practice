using System;

namespace Exercise3
{
    public class Temprature
    {
        private const double AbsoluteZeroCelsius = -273.15;
        private double celsius;

        public double Celsius
        {
            get => celsius;
            set
            {
                if(value<AbsoluteZeroCelsius)
                    throw new ArgumentException($"Temprature cannot be below absolute zero ({AbsoluteZeroCelsius}°C)");
                celsius = value;
            }
        }

        public double Fahrenheit
        {
            get => (Celsius * 9 / 5) + 32;
            set => Celsius = (value - 32) * 5 / 9;
        }

        public double Kelvin
        {
            get => Celsius + 273.15;
            set => celsius = value -273.15;
        }

        public Temprature(double value, char unit= 'C')
        {
            switch (char.ToUpper(unit))
            {
                case 'C':
                    Celsius = value;
                    break;
                case 'F':
                    Fahrenheit = value;
                    break;
                case 'K':
                    Kelvin = value;
                    break;
                default:
                    throw new ArgumentException("Unit must be C,F or K");
            }
        }
        public static Temprature FromCelsius(double celsius)
        {
            return new Temprature(celsius,'C');
        }

        public static Temprature FromFahrenheit(double fahrenheit)
        {
            return new Temprature(fahrenheit,'F');
        }

        public static Temprature FromKelvin(double kelvin)
        {
            return new Temprature(kelvin,'K');
        }

        public override string ToString()
        {
            return $"{Celsius:F2}°C = {Fahrenheit:F2}°F = {Kelvin:F2}K";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Temprature temp1 = new Temprature(25,'C');
            System.Console.WriteLine(temp1);

            Temprature temp2 = Temprature.FromFahrenheit(98.6);
            System.Console.WriteLine($"Body temprature:{temp2}");

            Temprature temp3 = Temprature.FromKelvin(300);
            System.Console.WriteLine(temp3);

            try
            {
                Temprature invalid = new Temprature(-300,'C');

            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"Error:{ex.Message}");
            }
            
        }
    }
}