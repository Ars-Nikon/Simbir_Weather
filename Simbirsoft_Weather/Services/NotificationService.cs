using System;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationSender _notificationSender;

        public NotificationService(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public async Task EchoAsync(int notificationId)
        {
            // get notification by id from database
            string email = "";
            string subject = "Weather notification";
            string message = $"{DateTime.Now} cool weather :)";

            await _notificationSender.SendNotificationAsync(email, subject, message);
        }
    }
}
