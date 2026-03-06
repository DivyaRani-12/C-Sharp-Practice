using System;

namespace Exercise4
{
    public class BankAccount
    {
        public string AccountNumber {get;set;}=string.Empty;
        public string HolderName {get;set;}=string.Empty;
        protected decimal balance;

        public decimal Balance=>balance;

        public BankAccount(string accountNumber,string holderName,decimal initialBalance)
        {
            AccountNumber=accountNumber;
            HolderName=holderName;
            balance=initialBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <=0)
            {
                System.Console.WriteLine("Deposit must be positive");
                return;
            }
            balance+=amount;
            System.Console.WriteLine($"Deposited {amount:C}. New balance:{balance:C}");
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                System.Console.WriteLine("withdreal amount must be positive");
                return false;
            }
            if (amount > balance)
            {
                System.Console.WriteLine("Insufficient funds");
                return false;
            }

            balance-=amount;
            System.Console.WriteLine($"Withdraw{amount:C}. New Balance:{balance:C}");
            return true;
        }

        public virtual void GetAccountInfo()
        {
            System.Console.WriteLine($"Account:{AccountNumber}");
            System.Console.WriteLine($"Holder:{HolderName}");
            System.Console.WriteLine($"Balance:{balance:C}");
        }
    }

    public class SavingsAccount : BankAccount
    {
        public decimal InterestRate {get; set;}

        public SavingsAccount(string accountNumber,string holderName,decimal initialBalance,decimal interestRate):base(accountNumber,holderName,initialBalance)
        {
            InterestRate=interestRate;
        }

        public override bool Withdraw(decimal amount)
        {
            if (balance - amount < 100)
            {
                System.Console.WriteLine("Cannot withdraw. minimum amount balance  of 100 required");
                return false;
            }
            return base.Withdraw(amount);
        }

        public void CalculateInterest()
        {
            decimal interest = balance*InterestRate;
            System.Console.WriteLine($"Interest earned:{interest:C}");
        }
    }

    public class CheckingAccount : BankAccount
    {
        public decimal OverdraftLimit {get;set;}

        public decimal AvailableBalance=>balance+OverdraftLimit;

        public CheckingAccount(string accountNumber,string holderName,decimal initialBalance,decimal overdraftLimit) : base(accountNumber, holderName, initialBalance)
        {
            OverdraftLimit=overdraftLimit;
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount > AvailableBalance)
            {
                System.Console.WriteLine("Withdrwal exceeds overdraft limit.");
                return false;
            }
            balance-=amount;
            System.Console.WriteLine($"Withdrawn {amount:C}. New balance:{balance:C}");
            return true;
        }
    }

    public class FixedDepositAccount : SavingsAccount
    {
        public DateTime MaturityDate {get;set;}
        public decimal PenaltyRate {get;set;}

        public FixedDepositAccount(string accountNumber,string holderName,decimal initialBalance,decimal interestRate,DateTime maturityDate) : base(accountNumber, holderName, initialBalance, interestRate)
        {
            MaturityDate=maturityDate;
            PenaltyRate=0.02m;
        }

        public override bool Withdraw(decimal amount)
        {
            if (DateTime.Now < MaturityDate)
            {
                decimal penalty = amount * PenaltyRate;
                System.Console.WriteLine($"Early withdraw penalty:{penalty:C}");
                amount+=penalty;
            }
            return base.Withdraw(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount savings = new SavingsAccount("SAV001","Divya",1000,0.3m);
            savings.Withdraw(950);
            savings.CalculateInterest();

            System.Console.WriteLine();

            CheckingAccount checking = new CheckingAccount("CH001","Aishu",500,200);
            checking.Withdraw(600);
            System.Console.WriteLine(($"Available: {checking.AvailableBalance:C}"));

            System.Console.WriteLine();

            FixedDepositAccount fd = new FixedDepositAccount("FD001","DIII",1000,0.05m,DateTime.Now.AddMonths(6));
            fd.Withdraw(1000);
        }
    }
}