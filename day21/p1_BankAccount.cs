using System;

namespace BankSystem
{
    class BankAccount
    {
        // private fields (data hiding)
        private int accountNumber;
        private double balance;

        // property for account number
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        // property for balance (read only outside)
        public double Balance
        {
            get { return balance; }
        }

        // Deposit Method
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }

            balance += amount;
            Console.WriteLine("Amount Deposited: " + amount);
            Console.WriteLine("Current Balance = " + balance);
        }

        // Withdraw Method
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return;
            }

            if (amount > balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            balance -= amount;
            Console.WriteLine("Amount Withdrawn: " + amount);
            Console.WriteLine("Current Balance = " + balance);
        }
    }

    class Program
    {
        static void Main()
        {

            Console.Write("Enter account number: ");
            int accNo = int.Parse(Console.ReadLine());
            Console.Write("Enter Deposit money: ");
            double deposit = double.Parse(Console.ReadLine());
            Console.Write("Enter Withdraw money: ");
            double withdraw = double.Parse(Console.ReadLine());

            BankAccount acc = new BankAccount();
            acc.AccountNumber = accNo;

            acc.Deposit(deposit);
            acc.Withdraw(withdraw);

            Console.ReadLine();
        }
    }
}