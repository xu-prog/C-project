using System;

namespace BankSystemApp
{
    public class DepositTransaction : Transaction
    {
        private Bank _bank;
        private string _accountName;

        // Public property to expose account name
        public string AccountName => _accountName;

        // Constructor initializing the bank and account information
        public DepositTransaction(Bank bank, string accountName, decimal amount) : base(amount)
        {
            _bank = bank ?? throw new ArgumentNullException(nameof(bank)); // Ensure bank is not null
            _accountName = accountName ?? throw new ArgumentNullException(nameof(accountName)); // Ensure accountName is not null
        }

        // Execute method for depositing money into an account
        public override void Execute()
        {
            base.Execute(); // Call base class Execute logic

            Account account = _bank.FindAccount(_accountName); // Find the account

            // Check if account exists and deposit the amount
            if (account != null)
            {
                account.Deposit(Amount); // Perform deposit
                Success = true; // Mark the transaction as successful
            }
            else
            {
                throw new InvalidOperationException("Deposit failed: Account not found.");
            }
        }

        // Rollback method to undo the deposit
        public override void Rollback()
        {
            base.Rollback(); // Call base class Rollback logic

            Account account = _bank.FindAccount(_accountName); // Find the account

            // Check if account exists and withdraw the amount to rollback the deposit
            if (account != null)
            {
                if (!account.Withdraw(Amount)) // Try to withdraw the deposited amount
                {
                    throw new InvalidOperationException("Rollback failed: Insufficient funds to reverse the deposit.");
                }
            }
            else
            {
                throw new InvalidOperationException("Rollback failed: Account not found.");
            }
        }

        // Print method to display the deposit transaction details
        public override void Print()
        {
            // Display the deposit transaction details
            Console.WriteLine($"Deposit to {AccountName}: {Amount:C} on {Timestamp}");
        }
    }
}
