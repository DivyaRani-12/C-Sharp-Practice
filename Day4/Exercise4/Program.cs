using System;
using System.Collections.Generic;

namespace Exercise4
{
    public class User
    {
        private static int totalUsers = 0;
        private static int totalLogins = 0;
        private static List<string> allUsernames = new List<string>();

        public string Username {get;set;}
        public string UserId {get;set;}
        private int loginCount = 0;
        private DateTime createdAt;

        public User(string username)
        {
            if(allUsernames.Contains(username))
                throw new ArgumentException("username already exists");
            
            Username=username;
            UserId=$"U{++totalUsers:D4}";
            createdAt=DateTime.Now;
            allUsernames.Add(username); 
        }

        public void Login()
        {
            loginCount++;
            totalLogins++;
            System.Console.WriteLine($"{Username} logged in (login # {loginCount})");
        }

        public static int GetTotalUsers()=>totalUsers;
        public static int GetTotalLogins()=>totalLogins;

        public static bool IsUsernameTaken(string username)
        {
            return allUsernames.Contains(username);
        }

        public override string ToString()
        {
            return $"User:{Username} (ID:{UserId}, Created:{createdAt:yyyy-mm-dd}, Logins:{loginCount})";
        }
        
    }
    public static class IdGenerator
    {
        private static int userIdCounter = 0;
        private static int productIdCounter = 0;
        private static int orderIdCounter = 0;

        public static string GenerateUserId()
        {
            return $"USR{++userIdCounter:D6}";
        }
        public static string GenerateProductId()
        {
            return $"PRD{++productIdCounter:D6}";
        }
        public static string GenerateOrderId()
        {
            return $"ORD{++orderIdCounter:D6}";
        }

        public static void ResetCounter()
        {
            userIdCounter = 0;
            productIdCounter=0;
            orderIdCounter=0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"Total users: {User.GetTotalUsers()}");

            User user1 = new User("divya");
            User user2 = new User("aishu");
            User user3 = new User("dii");

            System.Console.WriteLine($"\nTotal users: {User.GetTotalUsers()}");

            user1.Login();
            user1.Login();
            user2.Login();
            user3.Login();
            user1.Login();

            System.Console.WriteLine($"\nTotal login across all users:{User.GetTotalLogins()}");

            System.Console.WriteLine($"\nUser detials:");
            System.Console.WriteLine(user1);
            System.Console.WriteLine(user2);
            System.Console.WriteLine(user3);

            System.Console.WriteLine($"\n === ID Generator ===");
            System.Console.WriteLine(IdGenerator.GenerateUserId());
            System.Console.WriteLine(IdGenerator.GenerateUserId());
            System.Console.WriteLine(IdGenerator.GenerateProductId());
            System.Console.WriteLine(IdGenerator.GenerateOrderId());
            
        }
    }
}
