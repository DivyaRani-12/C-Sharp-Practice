using System;
using System.Collections.Generic;

namespace NotificationSystem
{
    public enum Priority { Low, Medium, High }
    public enum DeliveryStatus { Pending, Sent, Failed }

    public abstract class Notification
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public Priority Priority { get; set; }

        public Notification(string message, Priority priority)
        {
            Message = message;
            Priority = priority;
            CreatedAt = DateTime.Now;
        }

        public abstract void Send();

        public virtual bool Validate()
        {
            if (string.IsNullOrEmpty(Message))
            {
                Console.WriteLine("Invalid message!");
                return false;
            }
            return true;
        }
    }

    public interface ISchedulable
    {
        void ScheduleFor(DateTime dateTime);
        void CancelSchedule();
    }

    public interface IRetryable
    {
        int RetryCount { get; set; }
        int MaxRetries { get; set; }
        void Retry();
    }

    public interface ITrackable
    {
        string TrackingId { get; set; }
        DeliveryStatus Status { get; set; }
        string GetDeliveryStatus();
    }
    public class EmailNotification : Notification, ISchedulable, IRetryable, ITrackable
    {
        public int RetryCount { get; set; }
        public int MaxRetries { get; set; } = 3;

        public string TrackingId { get; set; }
        public DeliveryStatus Status { get; set; }

        private DateTime scheduledTime;

        public EmailNotification(string message, Priority priority)
            : base(message, priority)
        {
            TrackingId = Guid.NewGuid().ToString();
            Status = DeliveryStatus.Pending;
        }

        public override void Send()
        {
            Console.WriteLine($"Sending EMAIL: {Message}");
            Status = DeliveryStatus.Sent;
        }

        public void ScheduleFor(DateTime dateTime)
        {
            scheduledTime = dateTime;
            Console.WriteLine($"Email scheduled for {scheduledTime}");
        }

        public void CancelSchedule()
        {
            Console.WriteLine("Email schedule cancelled");
        }

        public void Retry()
        {
            if (RetryCount < MaxRetries)
            {
                RetryCount++;
                Console.WriteLine($"Retrying Email... Attempt {RetryCount}");
                Send();
            }
        }

        public string GetDeliveryStatus()
        {
            return $"Email Status: {Status} | TrackingId: {TrackingId}";
        }
    }
    public class SMSNotification : Notification, IRetryable, ITrackable
    {
        public int RetryCount { get; set; }
        public int MaxRetries { get; set; } = 2;

        public string TrackingId { get; set; }
        public DeliveryStatus Status { get; set; }

        public SMSNotification(string message, Priority priority)
            : base(message, priority)
        {
            TrackingId = Guid.NewGuid().ToString();
            Status = DeliveryStatus.Pending;
        }

        public override void Send()
        {
            Console.WriteLine($"Sending SMS: {Message}");
            Status = DeliveryStatus.Sent;
        }

        public void Retry()
        {
            if (RetryCount < MaxRetries)
            {
                RetryCount++;
                Console.WriteLine($"Retrying SMS... Attempt {RetryCount}");
                Send();
            }
        }

        public string GetDeliveryStatus()
        {
            return $"SMS Status: {Status} | TrackingId: {TrackingId}";
        }
    }

    public class PushNotification : Notification, ITrackable
    {
        public string TrackingId { get; set; }
        public DeliveryStatus Status { get; set; }

        public PushNotification(string message, Priority priority)
            : base(message, priority)
        {
            TrackingId = Guid.NewGuid().ToString();
            Status = DeliveryStatus.Pending;
        }

        public override void Send()
        {
            Console.WriteLine($"Sending PUSH notification: {Message}");
            Status = DeliveryStatus.Sent;
        }

        public string GetDeliveryStatus()
        {
            return $"Push Status: {Status} | TrackingId: {TrackingId}";
        }
    }

    public class SlackNotification : Notification, ISchedulable
    {
        private DateTime scheduledTime;

        public SlackNotification(string message, Priority priority)
            : base(message, priority)
        {
        }

        public override void Send()
        {
            Console.WriteLine($"Sending Slack message: {Message}");
        }

        public void ScheduleFor(DateTime dateTime)
        {
            scheduledTime = dateTime;
            Console.WriteLine($"Slack message scheduled for {scheduledTime}");
        }

        public void CancelSchedule()
        {
            Console.WriteLine("Slack schedule cancelled");
        }
    }

    public class NotificationService
    {
        private List<Notification> notifications = new();

        public void RegisterNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public void SendAll()
        {
            Console.WriteLine("\n--- Sending Notifications ---");

            foreach (var n in notifications)
            {
                if (n.Validate())
                    n.Send();
            }
        }

        public void RetryFailed()
        {
            Console.WriteLine("\n--- Retrying Failed Notifications ---");

            foreach (var n in notifications)
            {
                if (n is IRetryable retryable)
                {
                    retryable.Retry();
                }
            }
        }

        public void TrackAll()
        {
            Console.WriteLine("\n--- Tracking Notifications ---");

            foreach (var n in notifications)
            {
                if (n is ITrackable trackable)
                {
                    Console.WriteLine(trackable.GetDeliveryStatus());
                }
            }
        }
    }

   

    class Program
    {
        static void Main()
        {
            NotificationService service = new NotificationService();

            var email = new EmailNotification("Welcome Email!", Priority.High);
            var sms = new SMSNotification("OTP Code: 1234", Priority.High);
            var push = new PushNotification("New Offer Available!", Priority.Medium);
            var slack = new SlackNotification("Server Alert!", Priority.High);

            email.ScheduleFor(DateTime.Now.AddMinutes(5));
            slack.ScheduleFor(DateTime.Now.AddMinutes(10));

            service.RegisterNotification(email);
            service.RegisterNotification(sms);
            service.RegisterNotification(push);
            service.RegisterNotification(slack);

            service.SendAll();

            service.RetryFailed();

            service.TrackAll();
        }
    }
}