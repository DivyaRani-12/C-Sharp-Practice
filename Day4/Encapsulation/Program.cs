using System;

namespace Encapsulation
{
    public class BankAccount
    {
        private decimal balance;
        public decimal Balance => balance;

        public void Deposit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Deposit cannot be negative");
            balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Withdrawal must be positive");

            if (amount > balance)
                return false;

            balance -= amount;
            return true;
        }
    }

    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            ConnectToServer();
            AuthenticateUser();
            SendMessage(to, subject, body);
            DisconnectFromServer();
        }

        private void ConnectToServer()
        {
            Console.WriteLine("Connecting to SMTP Server...");
        }

        private void AuthenticateUser()
        {
            Console.WriteLine("Authenticating user...");
        }

        private void SendMessage(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
        }

        private void DisconnectFromServer()
        {
            Console.WriteLine("Disconnected from server.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount();
            account.Deposit(1000);
            account.Withdraw(500);
            Console.WriteLine("Balance: " + account.Balance);

            EmailService email = new EmailService();
            email.SendEmail("user@example.com", "Hello", "Test message");
        }
    }
}