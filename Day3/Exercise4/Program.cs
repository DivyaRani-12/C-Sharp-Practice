using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.IO;

namespace Exercise4
{
    
        class Contact
        {
            public string Name {get; set;}
            public string Phone {get; set;}
            public string Email {get; set;}

            public string Category{get; set;}

            public override string ToString()
            {
                return $"{Name} | {Email} | {Phone} | {Category}";
            }
        }

        class ContactManager
        {
            private Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();

            public bool AddContact(Contact contact)
            {
                if(contacts.ContainsKey(contact.Email))
                    return false;

                contacts[contact.Email] = contact;
                return true;
            }

            public bool RemoveContact(string email)
            {
                return contacts.Remove(email);
            }

            public Contact? FindByEmail(string email)
            {
                contacts.TryGetValue(email, out Contact? contact);
                return contact;
            }

            public List<Contact> FindByName(string partialName)
            {
                return contacts.Values
                    .Where(c=>c.Name.Contains(partialName,StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            public List<Contact> FindByCategory(string category)
            {
                return contacts.Values
                    .Where(c=>c.Category.Equals(category,StringComparison.OrdinalIgnoreCase))
                    .ToList();
                
            }

            public List<Contact> GetAllContacts()
            {
                return contacts.Values.OrderBy(c=>c.Name).ToList();
            }

            public bool UpdateContact(string email, string newName, string newPhone, string newCategory)
            {
                if(!contacts.ContainsKey(email))
                    return false;

                contacts[email].Name = newName;
                contacts[email].Phone = newPhone;
                contacts[email].Category = newCategory;
                return true;
            }

            public void ExportToCsv(string filePath)
            {
                List<string> lines = new List<string>();
                foreach(var contact in contacts.Values)
                {
                    lines.Add($"{contact.Name},{contact.Email},{contact.Phone},{contact.Category}");
                }
                File.WriteAllLines(filePath,lines);
            }

            public void ImportFromCsv(string filePath)
            {
                if(!File.Exists(filePath))
                    return;

                var lines = File.ReadAllLines(filePath);

                foreach(var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        Contact contact = new Contact
                        {
                            Name = parts[0],
                            Email = parts[1],
                            Phone = parts[2],
                            Category = parts[3]
                        };
                        contacts[contact.Email] = contact;
                    }
                }
            }
        
        class Program
        {
            static void Main(string[] args)
            {
                ContactManager manager = new ContactManager();
                bool running = true;

                while (running)
                {
                    System.Console.WriteLine("\n=== Contact Manager ===");
                    System.Console.WriteLine("1. Add Contact");
                    System.Console.WriteLine("2. Remove Contact");
                    System.Console.WriteLine("3. Find my Name");
                    System.Console.WriteLine("4. Find my Category");
                    System.Console.WriteLine("5. List All");
                    System.Console.WriteLine("0. Exit");
                    Console.Write("Choice:");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddContactMenu(manager);
                            break;

                        case "2":
                            Console.Write("Enter Email to remove");
                            string remEmail = Console.ReadLine() ?? "";
                            if (manager.RemoveContact(remEmail))
                                System.Console.WriteLine("Removed");
                            else
                                System.Console.WriteLine("Not Found");
                            break;
                        case "3":
                            Console.Write("Enter name to search");
                            string nameSearch = Console.ReadLine() ?? "";
                            var nameResults = manager.FindByName(nameSearch);
                            nameResults.ForEach(c=>Console.WriteLine(c));
                            break;

                        case "4":
                            Console.Write("Enter category:");
                            string cat = Console.ReadLine() ?? "";
                            var catResults = manager.FindByCategory(cat);
                            catResults.ForEach(c=>Console.WriteLine(c));
                            break;

                        case "5":
                            var all = manager.GetAllContacts();
                            all.ForEach(c=>Console.WriteLine(c));
                            break;

                        case "0":
                            running = false;
                            break;
                    }
                }
            
            }
            static void AddContactMenu(ContactManager manager)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Phone: ");
                string Phone = Console.ReadLine() ?? "";

                Console.Write("Category: ");
                string category = Console.ReadLine() ?? "";

                Contact contact = new Contact
                {
                    Name = name,
                    Email = email,
                    Phone = Phone,
                    Category = category
                };

                if(manager.AddContact(contact))
                    System.Console.WriteLine("Contact added");
                else
                    System.Console.WriteLine("Contact already exists");

            }
        }
    }
}