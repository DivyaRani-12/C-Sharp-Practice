using System;
using System.Collections.Generic;

namespace Exercise5
{
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }

    public class EmailNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Sending EMAIL to {recipient}: {message}");
        }
    }

    public class SmsNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Sending SMS to {recipient}: {message}");
        }
    }

    public class PushNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Sending PUSH notification to {recipient}: {message}");
        }
    }
    public class CompositeNotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationService> services;

        public CompositeNotificationService(IEnumerable<INotificationService> services)
        {
            this.services = services;
        }

        public void Send(string recipient, string message)
        {
            foreach (var service in services)
            {
                service.Send(recipient, message);
            }
        }
    }
    public class UserService
    {
        private readonly INotificationService notificationService;
        public UserService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void NotifyUser(string userId, string message)
        {
            string recipient = $"{userId}@example.com";
            notificationService.Send(recipient, message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            INotificationService emailService = new EmailNotificationService();
            UserService service1 = new UserService(emailService);
            service1.NotifyUser("user123", "Hello via email");

            Console.WriteLine();

            INotificationService smsService = new SmsNotificationService();
            UserService service2 = new UserService(smsService);
            service2.NotifyUser("user123", "Hello via SMS");

            Console.WriteLine();

            INotificationService pushService = new PushNotificationService();

            INotificationService composite = new CompositeNotificationService(
                new INotificationService[] { emailService, smsService, pushService }
            );

            UserService service3 = new UserService(composite);
            service3.NotifyUser("user123", "Hello via all channels");
        }
    }
}
