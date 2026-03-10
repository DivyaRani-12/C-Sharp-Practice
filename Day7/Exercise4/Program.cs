using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Exercise4
{
    public interface IPlugin
    {
        string Name{get;}
        string Version {get;}
        string Author {get;}

        void Initialize();
        void Execute();
        void Cleanup();
    }

    public interface IConfigurable
    {
        void LoadConfig(Dictionary<string, string> settings);
        Dictionary<string,string> SaveConfig();
    }

    public interface ILoggable
    {
        void LogInfo(string message);
        void LogError(string message);
    }

    public class DataExportPlugin : IPlugin, IConfigurable
    {
        public string Name=>"Data Exporter";
        public string Version=>"1.0.0";

        public string Author=>"Your Company";

        public string exportFormat="CSV";

        public void Initialize()
        {
            System.Console.WriteLine($"{Name} v{Version} initialized");
        }

        public void Execute()
        {
            System.Console.WriteLine($"Exporting data as {exportFormat}");
        }

        public void Cleanup()
        {
            System.Console.WriteLine($"{Name} cleaned up");
        }

        public void LoadConfig(Dictionary<string,string> settings)
        {
            if (settings.ContainsKey("format"))
            {
                exportFormat=settings["format"];
            }
        }

        public Dictionary<string, string> SaveConfig()
        {
            return new Dictionary<string, string>
            {
                ["format"]=exportFormat
            };
        }
    }

    public class EmailPlugin : IPlugin, IConfigurable, ILoggable
    {
        public string Name=>"Email sender";
        public string Version=>"1.0.0";
        public string Author=>"Your Company";

        public string smtpServer="smtp.gmail.com";

        public void Initialize()
        {
            LogInfo($"{Name} initialized");
        }

        public void Execute()
        {
            LogInfo($"Sending email using server {smtpServer}");
        }

        public void Cleanup()
        {
            LogInfo($"{Name} cleaned up");
        }

        public void LoadConfig(Dictionary<string, string> settings)
        {
            if (settings.ContainsKey("smtp"))
            {
                smtpServer=settings["smtp"];
            }
        }

        public Dictionary<string,string> SaveConfig()
        {
            return new Dictionary<string, string>
            {
                ["smtp"]=smtpServer
            };
        }

        public void LogInfo(string message)
        {
            System.Console.WriteLine($"INFO:{message}");
        }

        public void LogError(string message)
        {
            System.Console.WriteLine($"ERROR:{message}");
        }
    }

    public class ReportGeneratorPlugin : IPlugin, ILoggable
    {
        public string Name=>"Report Genertor";
        public string Version=>"1.0.0";
        public string Author=>"Your Company";

        public void Initialize()
        {
            LogInfo($"{Name} initialized");
        }

        public void Execute()
        {
            LogInfo("Generating report....");
        }

        public void Cleanup()
        {
            LogInfo($"{Name} cleaned up");
        }

        public void LogInfo(string message)
        {
            System.Console.WriteLine($"INFO:{message}");
        }

        public void LogError(string message)
        {
            System.Console.WriteLine($"ERROR:{message}");
        }
    }

    public class PluginManager
    {
        private List<IPlugin> plugins = new();

        public void LoadPlugin(IPlugin plugin)
        {
            plugins.Add(plugin);

            plugin.Initialize();

            if(plugin is IConfigurable configurable)
            {
                var config = new Dictionary<string, string>
                {
                    ["format"]="JSON"
                };
                configurable.LoadConfig(config);
            }
        }

        public void ExecuteAll()
        {
            foreach(var plugin in plugins)
            {
                plugin.Execute();
            }
        }

        public void UnloadAll()
        {
            foreach(var plugin in plugins)
            {
                plugin.Cleanup();
            }
            plugins.Clear();
        }

        public T? GetPlugin<T>() where T : IPlugin
        {
            return plugins.OfType<T>().FirstOrDefault();
        }
    }

    class Program
    {
        static void Main()
        {
            PluginManager manager=new();

            manager.LoadPlugin(new DataExportPlugin());
            manager.LoadPlugin(new EmailPlugin());
            manager.LoadPlugin(new ReportGeneratorPlugin());

            System.Console.WriteLine("\n --Executing Plugins ---\n");

            manager.ExecuteAll();

            System.Console.WriteLine("\n --- Unloading Plugins ---\n");

            manager.UnloadAll();
        }
    }
}