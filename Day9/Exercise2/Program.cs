using System;
using System.Collections.Generic;

namespace Exercise2
{
    public abstract class EventArgs
    {
        public DateTime Timestamp {get;}=DateTime.Now;
        public string EventId{get;}=Guid.NewGuid().ToString();
    }

    public class UserLoggedInEventArgs : EventArgs
    {
        public required string Username {get;init;}
        public string IpAddress {get;init;}=string.Empty;

    }

    public class OrderPlacedEventArgs : EventArgs
    {
        public required int OrderId{get;init;}
        public required decimal Total {get;init;}
        public required string CustomerId {get;init;}
    }

    public class EmailSentEventArgs : EventArgs
    {
        public required string To {get; init;}
        public required string Subject {get;init;}

        public required bool Success {get;init;}

    }

    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> subscribers = new();
        
        public void Subscribe<TEventArgs>(Action<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            Type eventType = typeof(TEventArgs);

            if (!subscribers.ContainsKey(eventType))
            {
                subscribers[eventType]=new List<Delegate>();
            }
            subscribers[eventType].Add(handler);
            System.Console.WriteLine($"Subscribed to {eventType.Name}");
        }

        public void Unsubscribe<TEventArgs>(Action<TEventArgs> handler) 
            where TEventArgs : EventArgs
        {
            Type eventType = typeof(TEventArgs);

            if (subscribers.ContainsKey(eventType))
            {
                subscribers[eventType].Remove(handler);
            }
        }

        public void Publish<TEventArgs>(TEventArgs eventArgs)
            where TEventArgs:EventArgs
        {
            Type eventType = typeof(TEventArgs);

            if (!subscribers.ContainsKey(eventType))
            {
                System.Console.WriteLine($"No subsribers for {eventType.Name}");
                return;
            }

            System.Console.WriteLine($"\nPublishing {eventType.Name}");

            foreach(Delegate handler in subscribers[eventType])
            {
                handler.DynamicInvoke(eventArgs);
            }
            
        }
    }

    public class Logger
    {
        public void OnUserLoggedIn(UserLoggedInEventArgs e)
        {
            System.Console.WriteLine($"[LOG] User {e.Username} logged in at {e.Timestamp}");
        }

        public void OnOrderPlaced(OrderPlacedEventArgs e)
        {
            System.Console.WriteLine($"[LOG] Order {e.OrderId} placed: {e.Total:C}");
        }

        public void OnEmailSent(EmailSentEventArgs e)
        {
            System.Console.WriteLine($"[LOG] Email sent to {e.To} | Success : {e.Success}");
        }
    }

    public class EmailService
    {
        public void OnUserLoggedIn(UserLoggedInEventArgs e)
        {
            System.Console.WriteLine($"[EMAIL] Sending welcome email to {e.Username}");
        }

        public void OnOrderPlaced(OrderPlacedEventArgs e)
        {
            System.Console.WriteLine($"[EMAIL] Sending order confirmation for #{e.OrderId}");
        }
    }
    public class NotificationService
    {
        public void OnEmailSent(EmailSentEventArgs e)
        {
            System.Console.WriteLine($"[NOTIFICATION] Email '{e.Subject}' delivered to {e.To}");
        }
    }

    class Program
    {
        static void Main()
        {
            EventBus bus = new();
            Logger logger=new();
            EmailService emailService=new();
            NotificationService notificationService=new();

            bus.Subscribe<UserLoggedInEventArgs>(logger.OnUserLoggedIn);
            bus.Subscribe<UserLoggedInEventArgs>(emailService.OnUserLoggedIn);

            bus.Subscribe<OrderPlacedEventArgs>(logger.OnOrderPlaced);
            bus.Subscribe<OrderPlacedEventArgs>(emailService.OnOrderPlaced);
            
            bus.Subscribe<EmailSentEventArgs>(logger.OnEmailSent);
            bus.Subscribe<EmailSentEventArgs>(notificationService.OnEmailSent);

            bus.Publish(new UserLoggedInEventArgs
            {
                Username="Divya",
                IpAddress="192.168.11"
            });

            bus.Publish(new OrderPlacedEventArgs
            {
                OrderId=123,
                Total=99.99m,
                CustomerId="C001"
            });

            bus.Publish(new EmailSentEventArgs
            {
                To="divya@gmail.com",
                Subject="Welcome!",
                Success=true
            });
        }
    }
}