using System;

namespace Exercise4_ISP
{
    public interface IPrinter
    {
        void Print(string document);
    }

    public interface IScanner
    {
        void Scan(string document);
    }

    public interface IFax
    {
        void Fax(string document);
    }

    public interface ICopier
    {
        void Copy(string document);
    }

    public interface IEmailSender
    {
        void SendEmail(string email);
    }
    public class MultiFunctionPrinter : IPrinter, IScanner, IFax, ICopier, IEmailSender
    {
        public void Print(string document)
        {
            Console.WriteLine($"Printing: {document}");
        }

        public void Scan(string document)
        {
            Console.WriteLine($"Scanning: {document}");
        }

        public void Fax(string document)
        {
            Console.WriteLine($"Faxing: {document}");
        }

        public void Copy(string document)
        {
            Console.WriteLine($"Copying: {document}");
        }

        public void SendEmail(string email)
        {
            Console.WriteLine($"Sending Email: {email}");
        }
    }
    public class BasicPrinter : IPrinter
    {
        public void Print(string document)
        {
            Console.WriteLine($"Printing: {document}");
        }
    }
    public class Scanner : IScanner
    {
        public void Scan(string document)
        {
            Console.WriteLine($"Scanning: {document}");
        }
    }
    public class SmartPrinter : IPrinter, IScanner, ICopier, IEmailSender
    {
        public void Print(string document)
        {
            Console.WriteLine($"Printing: {document}");
        }

        public void Scan(string document)
        {
            Console.WriteLine($"Scanning: {document}");
        }

        public void Copy(string document)
        {
            Console.WriteLine($"Copying: {document}");
        }

        public void SendEmail(string email)
        {
            Console.WriteLine($"Sending Email: {email}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IPrinter basicPrinter = new BasicPrinter();
            basicPrinter.Print("Basic Document");

            Console.WriteLine();

            MultiFunctionPrinter mfp = new MultiFunctionPrinter();
            mfp.Print("Report");
            mfp.Scan("Photo");
            mfp.Fax("Contract");
            mfp.Copy("Invoice");
            mfp.SendEmail("report@example.com");

            Console.WriteLine();

            IScanner scanner = new Scanner();
            scanner.Scan("Scanned Document");

            Console.WriteLine();

            SmartPrinter smartPrinter = new SmartPrinter();
            smartPrinter.Print("Smart Print");
            smartPrinter.Scan("Smart Scan");
            smartPrinter.Copy("Smart Copy");
            smartPrinter.SendEmail("smart@example.com");
        }
    }
}