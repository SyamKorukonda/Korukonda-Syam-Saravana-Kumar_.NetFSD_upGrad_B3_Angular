

/*
 Write  a  C# program to process product details using object oriented programming.
 
•	Class should contain private variables:  productId, productName, unitPrice, qty.
•	Constructor should allow productId as parameter
•	 Create properties for all private variables. Property Names :   ProductId, ProductName, UnitPrice, Quantity
•	ProductId – should be readonly property
•	ShowDetails()  method to display all the details along with total amount.
 */


using System.Security.Claims;

namespace ConsoleApp14
{
    internal class Program
    {
        //Class should contain private variables:  productId, productName, unitPrice, qty
        class Product
        {
            private int _productId;
            private string _productName;
            private Double _unitPrice;
            private int _qnt;

            //costructor productId as parameter
            public Product(int productId)
            {
                this._productId = productId;
            }

            //properties for all private variables

            public int ProductId
            {
                get { return _productId; }
            }

            public string ProductName
            {
                get { return _productName; }
                set { _productName = value; }
            }
            public Double UnitPrice
            {
                get { return _unitPrice; }
                set { _unitPrice = value; }
            }
            public int Qnt
            {
                get { return _qnt; }
                set { _qnt = value; }
            }

            //ShowDetails()

            public void ShowDetails()
            {
                double total = UnitPrice * Qnt;
                Console.WriteLine("Product Id: " + ProductId);
                Console.WriteLine("Product Name: " + ProductName);
                Console.WriteLine("Unit Price: " + UnitPrice);
                Console.WriteLine("Quantity: " + Qnt);
                Console.WriteLine("Total Amount: " + total);
            }

        }
        
        static void Main(string[] args)
        {
            Product p = new Product(101);
            p.ProductName = "Iphone";
            p.UnitPrice = 55000;
            p.Qnt = 1;
            p.ShowDetails();    

            
            Console.ReadLine(); 
        }
    }
}
