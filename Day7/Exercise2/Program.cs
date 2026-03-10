using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise2
{
    public abstract class FileSystemItem
    {
        public string Name{get;set;}=string.Empty;
        public string Path {get;set;}=string.Empty;
        public DateTime CreatedDate {get;set;}=DateTime.Now;

        public abstract long Size { get; }
        public abstract void Delete();

        public virtual void Move(string newPath)
        {
            System.Console.WriteLine($"Moving {Name} from {Path} to {newPath}");
            Path=newPath;
        }

        public virtual void GenInfo()
        {
            System.Console.WriteLine($"Name:{Name}");
            System.Console.WriteLine($"Path:{Path}");
            System.Console.WriteLine($"Created:{CreatedDate:yyyy-MM-dd}");
            System.Console.WriteLine($"Size:{Size} bytes");
        }

    }

    public class File : FileSystemItem
    {
        public string Extension {get;set;}=string.Empty;
        public string Content{get;set;}=string.Empty;

        public override long Size => Content.Length;

        public override void Delete()
        {
           System.Console.WriteLine($"File '{Name}.{Extension}' deleted from {Path}");
        }

        public override void GenInfo()
        {
            base.GenInfo();
            System.Console.WriteLine($"Type: File(.{Extension})");
        }
    }

    public class Directory : FileSystemItem
    {
        private List<FileSystemItem> children = new();
        public IReadOnlyList<FileSystemItem> Children => children.AsReadOnly();

        public override long Size
        {
            get
            {
                long total=0;
                foreach(var item in children)
                {
                    total+=item.Size;
                }
                return total;
            }
        }

        public void AddItem(FileSystemItem item)
        {
            children.Add(item);
            System.Console.WriteLine($"{item.Name} added to directory {Name}");
        }

        public void RemoveItem(string name)
        {
            var item=children.FirstOrDefault(x=>x.Name == name);

            if (item != null)
            {
                children.Remove(item);
                System.Console.WriteLine($"{name} removed from {Name}");
            }
            else
            {
                System.Console.WriteLine($"Item {name} not found");
            }
        }

        public override void Delete()
        {
           System.Console.WriteLine($"Deleting directory {Name}");

           foreach(var item in children)
            {
                item.Delete();
            }

            children.Clear();
            System.Console.WriteLine($"Directory {Name} deleted");
        }

        public void ListContents()
        {
            System.Console.WriteLine($"\nContents of {Name}:");

            foreach(var item in children)
            {
                System.Console.WriteLine($"-{item.Name} ({item.Size} bytes)");
            }
        }

        public override void GenInfo()
        {
            base.GenInfo();
            System.Console.WriteLine($"Type:Directory");
        }
    }

    public class Shortcut : FileSystemItem
    {
        public string TargetPath {get;set;}=string.Empty;

        public override long Size=>0;

        public override void Delete()
        {
           System.Console.WriteLine($"Shortcut '{Name}' deleted.Target was {TargetPath}");
        }

        public override void GenInfo()
        {
            base.GenInfo();
            System.Console.WriteLine($"Type:Shortcut");
            System.Console.WriteLine($"Target: {TargetPath}");
        }
    }
    class Program
    {
        static void Main()
        {
            Directory root = new() {Name = "Root",Path="C:\\"};

            Directory documents = new()
            {
                Name="Documents",
                Path="C:\\Documents"
            };

            File file1 = new()
            {
                Name="readme",
                Extension="txt",
                Content="Hello world",
                Path="C:\\Documents\\readme.txt"
            };

            File file2 = new()
            {
                Name="data",
                Extension="json",
                Content="{\"key\":\"value\"}",
                Path="C:\\Documents\\data.json"
            };

            Shortcut shortcut = new()
            {
                Name="DocsShortcut",
                Path="C:\\DocsShortcut",
                TargetPath="C:\\Documents"
            };

            documents.AddItem(file1);
            documents.AddItem(file2);

            root.AddItem(documents);
            root.AddItem(shortcut);

            System.Console.WriteLine("\n Root info:");
            root.GenInfo();

            documents.ListContents();

            System.Console.WriteLine($"\nTotal Size: {root.Size} bytes");
        }
    }
}