using System;
using System.IO;

namespace Exercise5
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public class Logger
    {
        protected LogLevel minLevel;
        protected string outputPath;

        public Logger(LogLevel minLevel)
        {
            this.minLevel=minLevel;
            outputPath="logs";
        }

        protected string FormatMessage(string message, LogLevel level)
        {
             return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
        }
        public virtual void Log(string message, LogLevel level)
        {
            if (level < minLevel)
                return;
            
            Console.WriteLine(FormatMessage(message, level));
        }
    }

    public class FileLogger : Logger
    {
        public FileLogger(LogLevel minLevel, string filePath) : base(minLevel)
        {
            outputPath=filePath;
        }

        public override void Log(string message,LogLevel level)
        {
            if(level<minLevel)
                return;

            string formatMessage = FormatMessage(message,level);
            File.AppendAllText(outputPath,  formatMessage+Environment.NewLine);
        }
    }

    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LogLevel minLevel):base(minLevel)
        {
            
        }

        public override void Log(string message,LogLevel level)
        {
            if(level<minLevel)
                return;

            string formatMessage = FormatMessage(message,level);

            switch (level)
            {
                case LogLevel.Debug:
                    Console.ForegroundColor=ConsoleColor.Gray;
                    break;

                case LogLevel.Info:
                    Console.ForegroundColor=ConsoleColor.Green;
                    break;

                case LogLevel.Warning:
                    Console.ForegroundColor=ConsoleColor.Yellow;
                    break;

                case LogLevel.Error:
                    Console.ForegroundColor=ConsoleColor.Red;
                    break;
            }
            System.Console.WriteLine(formatMessage);
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger consoleLogger=new ConsoleLogger(LogLevel.Debug);
            FileLogger fileLogger= new FileLogger(LogLevel.Info,"log.txt");

            consoleLogger.Log("Application started",LogLevel.Info);
            consoleLogger.Log("Debugging message",LogLevel.Debug);
            consoleLogger.Log("Something went wrong",LogLevel.Error);

            fileLogger.Log("File logger warning",LogLevel.Warning);
            fileLogger.Log("File logger Error",LogLevel.Error);
        }
    }

}