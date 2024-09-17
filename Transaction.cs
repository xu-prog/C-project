using System;

namespace BankSystemApp
{
    public abstract class Transaction
    {
        public decimal Amount { get; }
        public DateTime Timestamp { get; private set; }
        public bool Executed { get; protected set; }
        public bool Success { get; protected set; }
        public bool Reversed { get; protected set; }

        public Transaction(decimal amount)
        {
            Amount = amount;
            Timestamp = DateTime.Now;
            Executed = false;
            Success = false;
            Reversed = false;
        }

        public virtual void Execute()
        {
            if (Executed)
            {
                throw new InvalidOperationException("Transaction has already been executed.");
            }
            Timestamp = DateTime.Now;
            Executed = true;
        }

        public virtual void Rollback()
        {
            if (!Executed)
            {
                throw new InvalidOperationException("Transaction has not been executed.");
            }
            if (Reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }
            Reversed = true;
        }

        public abstract void Print();
    }
}
