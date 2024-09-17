using System;

namespace BankSystemApp
{
    public class WithdrawTransaction : Transaction
    {
        private Bank _bank;
        private string _accountName;

        // Public property to access the account name outside the class
        public string AccountName => _accountName;

        // Constructor initializing the bank and account information
        public WithdrawTransaction(Bank bank, string accountName, decimal amount) : base(amount)
        {
            _bank = bank ?? throw new ArgumentNullException(nameof(bank));
            _accountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
        }

        // Execute method for the withdrawal operation
        public override void Execute()
        {
            base.Execute(); // Call base class Execute logic

            Account account = _bank.FindAccount(_accountName); // Find the account

            // Check if account exists and if withdrawal is successful
            if (account != null && account.Withdraw(Amount))
            {
                Success = true; // Mark transaction as successful
            }
            else
            {
                throw new InvalidOperationException("Withdrawal failed: Insufficient funds or account not found.");
            }
        }

        // Rollback method to undo the withdrawal if needed
        public override void Rollback()
        {
            base.Rollback(); // Call base class Rollback logic

            Account account = _bank.FindAccount(_accountName); // Find the account

            // Check if account exists and deposit the amount back
            if (account != null)
            {
                account.Deposit(Amount); // Undo the withdrawal by depositing back the amount
            }
            else
            {
                throw new InvalidOperationException("Rollback failed: Account not found.");
            }
        }

        // Print method to display the withdrawal transaction details
        public override void Print()
        {
            // Display the withdrawal transaction details with formatted output
            Console.WriteLine($"Withdraw from {AccountName}: {Amount:C} on {Timestamp}");
        }
    }
}
