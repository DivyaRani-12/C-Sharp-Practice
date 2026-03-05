using System;
using System.Collections.Generic;

namespace Exercise1
{
    public class BankAccount
    {
        private readonly string accountNumber;
        private decimal balance;
        private string ownerName;
        private List<string> transactionHistory;
        
        public string AccountNumber=>accountNumber;
        public decimal Balance=>balance;

        public string OwnerName
        {
            get=>ownerName;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("owner name canot be empty");

                ownerName=value;
            }
        }

        public BankAccount(string owner,decimal initialBalance = 0)
        {
            if(initialBalance<0)
                throw new ArgumentException("Initial balance canot be negative");
            
            accountNumber=$"ACC{Random.Shared.Next(100000,999999)}";
            OwnerName=owner;
            balance=initialBalance;
            transactionHistory=new List<string>();

            if(initialBalance>0)
                AddTransaction($"Initial deposit:{initialBalance:C}");
        }

        public void Deposit(decimal amount)
        {
            if(amount<=0)
                throw new ArgumentException("Deposit amount must be postive");

            balance+=amount;
            AddTransaction($"Deposit:{amount:C}");
            System.Console.WriteLine($"Deposited {amount:C}.New Balance:{balance:C}");
        }

        public bool Withdraw(decimal amount)
        {
            if(amount<=0)
                throw new ArgumentException("withdrawal amount must be postive");
            
            if (amount > balance)
            {
                System.Console.WriteLine($"Insufficient funds. Balance:{balance:C}");
                return false;
            }

            balance-=amount;
            AddTransaction($"Withdrawal:{amount:C}");
            System.Console.WriteLine($"Withdrew:{amount:C}.New Balance:{balance:C}");
            return true;
        }

        public bool Transfer(decimal amount,BankAccount targetAccount)
        {
            if(amount<=0)
                throw new ArgumentException("Transfer amount must be postive");

            if(targetAccount==null)
                throw new ArgumentNullException(nameof(targetAccount));

            if(!Withdraw(amount))
                return false;

            targetAccount.Deposit(amount);

            AddTransaction($"Transfered {amount:C} to {targetAccount.AccountNumber}");
            targetAccount.AddTransaction($"Recieved {amount:c} from {AccountNumber}");

            System.Console.WriteLine($"Transfered {amount:c} to {targetAccount.ownerName}");
            return true;
        }

        private void AddTransaction(string transaction)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            transactionHistory.Add($"[{timestamp}] {transaction}");
        }

        public List<string> GetTransactionHistory()
        {
            return new List<string>(transactionHistory);
            
        }

        public void PrintStatement()
        {
            System.Console.WriteLine($"\n ==== Account Statement ====");
            System.Console.WriteLine($"Account:{AccountNumber}");
            System.Console.WriteLine($"Owner:{ownerName}");
            System.Console.WriteLine($"Balance:{Balance:C}");
            System.Console.WriteLine("\nTransaction History");
            foreach(var transaction in transactionHistory)
            {
                System.Console.WriteLine(transaction);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1=new BankAccount("Divya",1000);
            BankAccount account2=new BankAccount("Aishu",800);

            account1.Deposit(200);
            account1.Withdraw(300);
            account1.Transfer(200,account2);

            account1.PrintStatement();
            account2.PrintStatement();
            
        }
    }
}