using System;

namespace BankSystemApp
{
    public class Account
    {
        private string _name;
        private decimal _balance;

        public string Name
        {
            get { return _name; }
        }

        public decimal Balance
        {
            get { return _balance; }
        }

        public Account(string name, decimal initialBalance)
        {
            _name = name;
            _balance = initialBalance;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Withdrawal amount must be positive.");
            }

            if (amount > _balance)
            {
                return false;
            }

            _balance -= amount;
            return true;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Deposit amount must be positive.");
            }

            _balance += amount;
        }

        public void Print()
        {
            Console.WriteLine($"Account: {_name}");
            Console.WriteLine($"Balance: {_balance:C}");
        }
    }
}
