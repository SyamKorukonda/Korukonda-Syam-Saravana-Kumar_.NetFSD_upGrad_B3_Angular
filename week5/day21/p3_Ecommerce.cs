using System;

namespace EcommerceCart
{
    // Base Class
    class Product
    {
        private double price;

        public string Name { get; set; }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Price cannot be negative.");
                }
                else
                {
                    price = value;
                }
            }
        }

        // Virtual method
        public virtual double CalculateDiscount()
        {
            return Price;
        }
    }

    // Derived Class Electronics
    class Electronics : Product
    {
        public override double CalculateDiscount() => Price - (Price * 0.05);
        
    }

    // Derived Class Clothing
    class Clothing : Product
    {
        public override double CalculateDiscount()=> Price - (Price * 0.15);
        
    }

    class Program
    {
        static void Main(string[] args)
        {

            Product product;

            Console.WriteLine("Enter Product Type (Electronics / Clothing):");
            string type = Console.ReadLine();

            Console.WriteLine("Enter Product Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Product Price:");
            double price = Convert.ToDouble(Console.ReadLine());

            if (type.ToLower() == "electronics")
            {
                product = new Electronics();
            }
            else
            {
                product = new Clothing();
            }

            product.Name = name;
            product.Price = price;

            double finalPrice = product.CalculateDiscount();

            Console.WriteLine($"Product Name: {product.Name}");
            Console.WriteLine($"Final Price after Discount = {finalPrice}");

            Console.ReadLine();
        }
    }
}