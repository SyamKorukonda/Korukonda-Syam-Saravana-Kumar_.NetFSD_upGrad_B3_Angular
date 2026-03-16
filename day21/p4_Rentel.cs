using System;

namespace VehicleRental
{
    class Vehicle
    {
        private double rentalRatePerDay;

        public string Brand { get; set; }

        public double RentalRatePerDay
        {
            get { return rentalRatePerDay; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Rental rate cannot be negative.");
                }
                else
                {
                    rentalRatePerDay = value;
                }
            }
        }

        public virtual double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("Invalid number of days.");
                return 0;
            }

            return RentalRatePerDay * days;
        }
    }

    class Car : Vehicle
    {
        public override double CalculateRental(int days)
        {
            double total = base.CalculateRental(days);
            return total + 500;   // insurance
        }
    }

    class Bike : Vehicle
    {
        public override double CalculateRental(int days)
        {
            double total = base.CalculateRental(days);
            return total - (total * 0.05);   // 5% discount
        }
    }

    class Program
    {
        static void Main()
        {
            Vehicle vehicle;

            Console.WriteLine("Enter Vehicle Type (Car/Bike): ");
            string type = Console.ReadLine();

            Console.WriteLine("Enter Brand: ");
            string brand = Console.ReadLine();

            Console.WriteLine("Enter Rental Rate Per Day: ");
            double rate = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Number of Days: ");
            int days = Convert.ToInt32(Console.ReadLine());

            if (type.ToLower() == "car")
            {
                vehicle = new Car();
            }
            else
            {
                vehicle = new Bike();
            }

            vehicle.Brand = brand;
            vehicle.RentalRatePerDay = rate;


            Console.WriteLine($"Brand : {vehicle.Brand} , Total Rental = {vehicle.CalculateRental(days)} ");

            Console.ReadLine();
        }
    }
}