using System;

namespace BankSystemApp
{
    class BankSystem
    {
        private readonly Bank _bank;

        // Changed enum to public
        public enum MenuOption
        {
            Withdraw = 1,
            Deposit,
            Transfer,
            PrintTransactionHistory,
            AddAccount,
            Quit
        }

        public BankSystem()
        {
            _bank = new Bank();
        }

        static void Main(string[] args)
        {
            BankSystem bankSystem = new BankSystem();
            bankSystem._bank.AddAccount(new Account("Ann", 2000));
            bankSystem._bank.AddAccount(new Account("Tony", 3000));

            MenuOption userChoice;
            do
            {
                userChoice = ReadUserOption();
                switch (userChoice)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bankSystem._bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bankSystem._bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(bankSystem._bank);
                        break;
                    case MenuOption.PrintTransactionHistory:
                        DoPrintTransactionHistory(bankSystem);
                        break;
                    case MenuOption.AddAccount:
                        DoAddAccount(bankSystem._bank);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Goodbye!");
                        break;
                }
            } while (userChoice != MenuOption.Quit);
        }

        public static MenuOption ReadUserOption()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Print Transaction History");
            Console.WriteLine("5. Add Account");
            Console.WriteLine("6. Quit");

            if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(MenuOption), choice))
            {
                return (MenuOption)choice;
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose a number between 1 and 6.");
                return ReadUserOption();
            }
        }

        // Helper method for account lookup
        private static Account GetAccount(Bank bank, string prompt)
        {
            Console.Write(prompt);
            string accountName = Console.ReadLine();
            Account account = bank.FindAccount(accountName);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }

            return account;
        }

        public static void DoWithdraw(Bank bank)
        {
            Account account = GetAccount(bank, "Enter account name: ");
            if (account == null) return;

            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                WithdrawTransaction withdrawTransaction = new WithdrawTransaction(bank, account.Name, amount);
                try
                {
                    bank.ExecuteTransaction(withdrawTransaction);
                    Console.WriteLine("Withdrawal successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to withdraw: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public static void DoDeposit(Bank bank)
        {
            Account account = GetAccount(bank, "Enter account name: ");
            if (account == null) return;

            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                DepositTransaction depositTransaction = new DepositTransaction(bank, account.Name, amount);
                try
                {
                    bank.ExecuteTransaction(depositTransaction);
                    Console.WriteLine("Deposit successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to deposit: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public static void DoTransfer(Bank bank)
        {
            Account fromAccount = GetAccount(bank, "Enter from account name: ");
            if (fromAccount == null) return;

            Account toAccount = GetAccount(bank, "Enter to account name: ");
            if (toAccount == null) return;

            Console.Write("Enter amount to transfer: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                TransferTransaction transferTransaction = new TransferTransaction(bank, fromAccount.Name, toAccount.Name, amount);
                try
                {
                    bank.ExecuteTransaction(transferTransaction);
                    Console.WriteLine("Transfer successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to transfer: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public static void DoPrintTransactionHistory(BankSystem bankSystem)
        {
            bankSystem._bank.PrintTransactionHistory();
            Console.WriteLine("Do you want to rollback a transaction? (Y/N)");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                DoRollback(bankSystem);
            }
        }

        public static void DoRollback(BankSystem bankSystem)
        {
            Console.Write("Enter the transaction number to rollback: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= bankSystem._bank.TransactionsCount)
            {
                try
                {
                    bankSystem._bank.RollbackTransaction(bankSystem._bank.GetTransactionByIndex(index - 1));
                    Console.WriteLine("Transaction rolled back successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to rollback transaction: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid transaction number.");
            }
        }

        public static void DoAddAccount(Bank bank)
        {
            Console.Write("Enter new account name: ");
            string accountName = Console.ReadLine();

            Console.Write("Enter starting balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                bank.AddAccount(new Account(accountName, balance));
                Console.WriteLine("Account created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid balance.");
            }
        }
    }
}
