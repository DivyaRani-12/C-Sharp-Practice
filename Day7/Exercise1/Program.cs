using System;
using System.Collections.Generic;

namespace Exercise1
{
    public interface IPaymentMethod
    {
        string PaymentType {get;}
        bool ProcessPayment(decimal amount);

        bool ValidatePayment();
        decimal GetTransactionFee(decimal amount);
    }

    public interface IRefundable
    {
        bool ProcessRefund(decimal amount);
        decimal GetRefundFee(decimal amount);
    }

    public class CreditCardPayment : IPaymentMethod, IRefundable
    {
        public string CardNumber{get;set;}=string.Empty;
        public string CVV {get;set;}=string.Empty;
        public DateTime ExpiryDate {get;set;}

        public string PaymentType=>"Credit Card";

        public bool ValidatePayment()
        {
            if (CardNumber.Length < 16)
            {
                System.Console.WriteLine("Invalid Card Number");
                return false;
            }

            if (CVV.Length != 3)
            {
                System.Console.WriteLine("Invalid CVV");
                return false;
            }

            if (ExpiryDate < DateTime.Now)
            {
                System.Console.WriteLine("Card Expired");
                return false;
            }
            return true;
        }

        public decimal GetTransactionFee(decimal amount)
        {
            return amount*0.025m;
        }

        public bool ProcessPayment(decimal amount)
        {
            if(!ValidatePayment())
                return false;

            decimal fee=GetTransactionFee(amount);
            decimal total=amount+fee;

            System.Console.WriteLine($"Processing Credit card Payment:{total:C} (Fee:{fee:C})");
            return true;
        }

        public decimal GetRefundFee(decimal amount)
        {
            return amount*0.01m;
        }

        public bool ProcessRefund(decimal amount)
        {
            decimal fee=GetRefundFee(amount);
            decimal refundAmount=amount-fee;

            System.Console.WriteLine($"Refund Processed:{refundAmount:C} (Fee:{fee:C})");
            return true;
        }
    }

    public class PayPalPayment : IPaymentMethod, IRefundable
    {
        public string Email {get;set;}=string.Empty;
        public string Password {get;set;}=string.Empty;
        public string PaymentType=>"PayPal";

        public bool ValidatePayment()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                System.Console.WriteLine("Invalid PayPal credentials");
                return false;
            }
            return true;
        }

        public decimal GetTransactionFee(decimal amount)
        {
            return amount*0.03m;
        }

        public bool ProcessPayment(decimal amount)
        {
            if(!ValidatePayment())
                return false;

            decimal fee = GetTransactionFee(amount);
            decimal total=amount+fee;

            System.Console.WriteLine($"Processing Paypal Paymnet: {total:C} (Fee:{fee:C})");
            return true;
        }

        public decimal GetRefundFee(decimal amount)
        {
            return 0m;
        }

        public bool ProcessRefund(decimal amount)
        {
            System.Console.WriteLine($"Full refund processed:{amount:C}");
            return true;
        }
    }

    public class CryptoPayment : IPaymentMethod
    {
        public string WalletAdress {get;set;}=string.Empty;
        public string PrivateKey {get;set;}=string.Empty;

        public string PaymentType=>"Crypto";

        public bool ValidatePayment()
        {
            if (string.IsNullOrEmpty(WalletAdress))
            {
                System.Console.WriteLine("Invalid wallet adress");
                return false;
            }
            return true;
        }

        public decimal GetTransactionFee(decimal amount)
        {
            return amount*0.01m;
        }

        public bool ProcessPayment(decimal amount)
        {
            if(!ValidatePayment())
                return false;

            decimal fee=GetTransactionFee(amount);
            decimal total=amount+fee;

            System.Console.WriteLine($"Processing Crypto Payment:{total:C} (Fee:{fee:C})");
            System.Console.WriteLine("Crypto transactions are irreversible (no refunds).");

            return true;
        }
    }

    public class BankTransferPayment : IPaymentMethod
    {
        public string AccountNumber{get;set;}=string.Empty;
        public string RoutingNumber {get;set;}=string.Empty;

        public string PaymentType=>"Bank Transfer";

        public bool ValidatePayment()
        {
            if(string.IsNullOrEmpty(AccountNumber) || string.IsNullOrEmpty(RoutingNumber))
            {
                System.Console.WriteLine("Invalid bank details");
                return false;
            }
            return true;
        }

        public decimal GetTransactionFee(decimal amount)
        {
            return 5m;
        }

        public bool ProcessPayment(decimal amount)
        {
            if(!ValidatePayment())
                return false;

            decimal fee = GetTransactionFee(amount);
            decimal total=amount+fee;

            System.Console.WriteLine($"Processing Bank Transfer:{total:C} (Fee:{fee:C})");
            System.Console.WriteLine("Processing time:2-3 business days");

            return true;
        }
    }

    class PaymentProcessor
    {
        public static void ProcessOrder(IPaymentMethod payment,decimal amount)
        {
            System.Console.WriteLine($"\n---- Processing {payment.PaymentType} ----");

            if (payment.ProcessPayment(amount))
            {
                System.Console.WriteLine("Payment sucessfull!");

                if(payment is IRefundable refundable)
                {
                    System.Console.WriteLine("This payment method suports refunds");
                    decimal refundFee = refundable.GetRefundFee(amount);
                    System.Console.WriteLine($"Refund fee would be:{refundFee:C}");
                }
                else
                {
                    System.Console.WriteLine("This payment method does not support refunds");
                }
            }
        }

        static void Main()
        {
            List<IPaymentMethod> payments = new()
            {
                new CreditCardPayment
                {
                    CardNumber="1253482586235945",
                    CVV="123",
                    ExpiryDate=DateTime.Now.AddYears(2)
                },
                new PayPalPayment
                {
                    Email="divya@gmail.com",
                    Password="password123"
                },
                new CryptoPayment
                {
                    WalletAdress="1w45fwhbefq156fw456"
                },
                new BankTransferPayment
                {
                    AccountNumber="2563285285",
                    RoutingNumber="58548562"
                }
            };

            decimal orderAmount=100m;

            foreach(var payment in payments)
            {
                ProcessOrder(payment,orderAmount);
            }
        }
    }
}