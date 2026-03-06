using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise3
{
    public class Vehicle
    {
        public int Id {get;set;}
        public string Brand {get;set;}=string.Empty;
        public string Model {get;set;}=string.Empty;

        public int Year {get; set;}

        public decimal DailyRate {get;set;}
        public bool IsRented {get;set;}

        public virtual bool Rent()
        {
            if (IsRented)
            {
                System.Console.WriteLine($"{Brand} {Model} is already rented");
                return false;
            }
            IsRented=true;
            System.Console.WriteLine($"{Brand} {Model} rented sucessfully");
            return true;
        }

        public virtual void Return()
        {
            IsRented=false;
            System.Console.WriteLine($"{Brand} {Model} returned");
        }

        public virtual decimal GetRentalCost(int days)
        {
            return DailyRate*days;
        }

        public virtual void DisplayInfo()
        {
            System.Console.WriteLine($"{Brand} {Model} ({Year})");
            System.Console.WriteLine($"Daily Rate:{DailyRate:C}");
            System.Console.WriteLine($"Status: {(IsRented ? "Rented":"Available")}");
        }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors {get;set;}
        public string FuelType {get;set;}

        public override decimal GetRentalCost(int days)
        {
            decimal cost = DailyRate*days;

            if(days>7)
                cost*=0.90m;

            return cost;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            System.Console.WriteLine($"Doors:{NumberOfDoors}");
            System.Console.WriteLine($"Fuel Type:{FuelType}");
        }
    }

    public class Motorcycle : Vehicle
    {
        public int EngineCC { get; set;}

        public override decimal GetRentalCost(int days)
        {
            decimal cost = DailyRate*days;

            if(days>3)
                cost*=0.85m;

            return cost;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            System.Console.WriteLine($"Engine:{EngineCC} CC");
        }
    }

    public class Truck : Vehicle
    {
        public double LoadCapicity {get;set;}

        public override decimal GetRentalCost(int days)
        {
            decimal baseCost = DailyRate*days;
            decimal extra = (decimal)(LoadCapicity*0.1*days);

            return baseCost+extra;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            System.Console.WriteLine($"Load Capicity:{LoadCapicity}kg");
        }
    }

    public class ElectricCar : Car
    {
        public int BatteryCapacity {get;set;}
        public int Range {get; set;}

        public override decimal GetRentalCost(int days)
        {
            decimal baseCost=base.GetRentalCost(days);
            return baseCost*0.8m;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            System.Console.WriteLine($"Battery:{BatteryCapacity} kWh");
            System.Console.WriteLine($"Range:{Range} km");
        }
    }

    class RentalSystem
    {
        static void Main()
        {
            List<Vehicle> fleet = new()
            {
                new Car{Brand="Toyato",Model="Camry",Year=2022,DailyRate=50,NumberOfDoors=4,FuelType="Petrol"},
                new Motorcycle{Brand="Honda",Model="CBR",Year=2021,DailyRate=30,EngineCC=600},
                new Truck{Brand="Ford",Model="F-150",Year=2020,DailyRate=80,LoadCapicity=1000},
                new ElectricCar{Brand="Tesla",Model="Model 3",Year=2023,DailyRate=70,Range=350,BatteryCapacity=75}
            };

            System.Console.WriteLine("Available vehicles:");

            foreach(var vehicle in fleet.Where(v => !v.IsRented))
            {
                vehicle.DisplayInfo();
                System.Console.WriteLine($"cost for 10 days: {vehicle.GetRentalCost(10):C}");
                System.Console.WriteLine();
            }

            fleet[0].Rent();

            decimal revenue = fleet.Sum(v=>v.GetRentalCost(10));
            System.Console.WriteLine($"Total potential revenue (10 days): {revenue:C}");
        }
    }
   
}