using System;

namespace Exercise04
{
    public record DatabaseConfig
    {
        public required string ConnectionString { get; init; }

        private int _maxConnections;
        public required int MaxConnections
        {
            get => _maxConnections;
            init
            {
                if (value <= 0 || value > 1000)
                    throw new ArgumentException("MaxConnections must be between 1 and 1000");
                _maxConnections = value;
            }
        }

        private TimeSpan _timeout = TimeSpan.FromSeconds(30);
        public TimeSpan Timeout
        {
            get => _timeout;
            init
            {
                if (value.TotalSeconds <= 0)
                    throw new ArgumentException("Timeout must be greater than 0 seconds");
                _timeout = value;
            }
        }
    }

    public record ApiConfig
    {
        public required string BaseUrl
        {
            get;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("BaseUrl cannot be empty");
                field = value;
            }
        }

        public required string ApiKey
        {
            get;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ApiKey cannot be empty");
                field = value;
            }
        }

        private int _rateLimitPerMinute = 60;
        public int RateLimitPerMinute
        {
            get => _rateLimitPerMinute;
            init
            {
                if (value <= 0 || value > 10000)
                    throw new ArgumentException("RateLimitPerMinute must be between 1 and 10000");
                _rateLimitPerMinute = value;
            }
        }
    }

    public class AppConfig
    {
        public required DatabaseConfig Database { get; init; }
        public required ApiConfig Api { get; init; }
        public required string AppName { get; init; }
        public string Version { get; init; } = "1.0.0";

        public bool ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(AppName))
                return false;

            if (Database == null || Api == null)
                return false;

            if (string.IsNullOrWhiteSpace(Database.ConnectionString))
                return false;

            if (Database.MaxConnections <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(Api.ApiKey))
                return false;

            if (string.IsNullOrWhiteSpace(Api.BaseUrl))
                return false;

            if (Api.RateLimitPerMinute <= 0)
                return false;

            return true;
        }

        public void DisplayConfiguration()
        {
            Console.WriteLine("=== Application Configuration ===");
            Console.WriteLine($"App: {AppName} v{Version}");

            Console.WriteLine("\nDatabase:");
            Console.WriteLine($"  Connection: {Database.ConnectionString}");
            Console.WriteLine($"  Max Connections: {Database.MaxConnections}");
            Console.WriteLine($"  Timeout: {Database.Timeout}");

            Console.WriteLine("\nAPI:");
            Console.WriteLine($"  Base URL: {Api.BaseUrl}");
            Console.WriteLine($"  Rate Limit: {Api.RateLimitPerMinute}/min");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppConfig config = new AppConfig
                {
                    AppName = "MyApplication",
                    Version = "1.0.0",

                    Database = new DatabaseConfig
                    {
                        ConnectionString = "Server=localhost;Database=MyDb;",
                        MaxConnections = 100,
                        Timeout = TimeSpan.FromSeconds(45)
                    },

                    Api = new ApiConfig
                    {
                        BaseUrl = "https://api.example.com",
                        ApiKey = "abc123xyz",
                        RateLimitPerMinute = 100
                    }
                };

                if (config.ValidateConfiguration())
                {
                    config.DisplayConfiguration();
                }
                else
                {
                    Console.WriteLine("Configuration validation failed");
                }

                // config.AppName = "NewName"; // Compile-time error (init only)

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}