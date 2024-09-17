using System;

namespace BankSystemApp
{
    public class TransferTransaction : Transaction
    {
        private WithdrawTransaction _withdraw;
        private DepositTransaction _deposit;

        public TransferTransaction(Bank bank, string fromAccountName, string toAccountName, decimal amount)
            : base(amount)
        {
            _withdraw = new WithdrawTransaction(bank, fromAccountName, amount);
            _deposit = new DepositTransaction(bank, toAccountName, amount);
        }

        public override void Execute()
        {
            base.Execute();
            try
            {
                _withdraw.Execute();
                _deposit.Execute();
                Success = true;
            }
            catch
            {
                if (_withdraw.Executed)
                {
                    _withdraw.Rollback();
                }
                throw new InvalidOperationException("Transfer failed.");
            }
        }

        public override void Rollback()
        {
            base.Rollback();
            try
            {
                _deposit.Rollback();
                _withdraw.Rollback();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error during transfer rollback: " + ex.Message);
            }
        }

        public override void Print()
        {
            // Using public properties to access the account names
            Console.WriteLine($"Transfer: {_withdraw.Amount:C} from {_withdraw.AccountName} to {_deposit.AccountName} on {Timestamp}");

        }
    }
}
