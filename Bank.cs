using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystemApp
{
    public class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account FindAccount(string name)
        {
            return _accounts.FirstOrDefault(account => account.Name == name);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactions.Add(transaction);
        }

        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
        }

        public void PrintTransactionHistory()
        {
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Transaction:");
                _transactions[i].Print();
            }
        }

        public int TransactionsCount => _transactions.Count;

        public Transaction GetTransactionByIndex(int index)
        {
            if (index < 0 || index >= _transactions.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid transaction index.");
            }
            return _transactions[index];
        }
    }
}
