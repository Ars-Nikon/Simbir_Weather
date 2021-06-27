using Microsoft.AspNetCore.Identity;
using Simbirsoft_Weather.Models;
using System;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationSender _notificationSender;
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<User> _userManager;
        private readonly INotificationWritter _notificationWritter;
        private readonly IClothingConsultant _clothingConsultant;

        public NotificationService(INotificationSender notificationSender,
            IEventRepository eventRepository,
            UserManager<User> userManager,
            INotificationWritter notificationWritter,
            IClothingConsultant clothingConsultant)
        {
            _notificationSender = notificationSender;
            _eventRepository = eventRepository;
            _userManager = userManager;
            _notificationWritter = notificationWritter;
            _clothingConsultant = clothingConsultant;
        }

        public async Task EchoAsync(int notificationId)
        {
            var eventInfo = _eventRepository.GetEventById(notificationId);
            if (eventInfo == null) return;
            var user = await _userManager.FindByIdAsync(eventInfo.Id_User);
            if (user == null) return;

            WeatherApi api = new WeatherApi(eventInfo.Region);
            int days = (eventInfo.DateEvent - DateTime.Now).Value.Days;
            var forecast = api.WheatherFor5Day()[days];
            var forecastForTime = api.WheatherForTime(forecast.Date.ToString());
            var recommendation = (bool)user.Gender ? _clothingConsultant.GetWomanRecommendation(forecast) : _clothingConsultant.GetManRecommendation(forecast);
            string title = eventInfo.NameEvent;
            string description = eventInfo.Description;

            string email = user.Email;
            string subject = "Weather";
            string message = (bool)user.Gender
                ? _notificationWritter.WriteNotificationPageForWoman(forecast, forecastForTime, recommendation, title, description)
                : _notificationWritter.WriteNotificationPageForMan(forecast, forecastForTime, recommendation, title, description);

            await _notificationSender.SendNotificationAsync(email, subject, message);

            _eventRepository.DeleteEventById(notificationId);
        }
    }
}
