using System.Linq;
using System.Threading.Tasks;
using Simbirsoft_Weather.Models;
using Microsoft.AspNetCore.Identity;

namespace Simbirsoft_Weather.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationSender _notificationSender;
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<User> _userManager;
        private readonly INotificationWritter _notificationWritter;
        private readonly IClothingConsultant _clothingConsultant;

        public NotificationService(
            INotificationSender notificationSender,
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

        /// <summary>
        /// Collects information about the user, weather, clothing, event, generates a notification and sends it.
        /// </summary>
        /// <param name="notificationId">Unique event identifier.</param>
        /// <returns></returns>
        public async Task EchoAsync(int notificationId)
        {
            var eventInfo = _eventRepository.GetEventById(notificationId);
            if (eventInfo == null) return;
            var user = await _userManager.FindByIdAsync(eventInfo.Id_User);
            if (user == null) return;

            WeatherApi api = new WeatherApi(eventInfo.Region);
            var forecast = api.WheatherFor5Day().SingleOrDefault(f => f.Date.Day == eventInfo.DateEvent.Value.Day);
            var forecastForTime = api.WheatherForTime(forecast.Date.ToString());
            var recommendation = !(bool)user.Gender ? _clothingConsultant.GetWomanRecommendation(forecast) : _clothingConsultant.GetManRecommendation(forecast);
            string title = eventInfo.NameEvent;
            string description = eventInfo.Description;
            string email = user.Email;
            string subject = "Weather";
            string message = !(bool)user.Gender
                ? _notificationWritter.WriteNotificationPageForWoman(
                    forecast, forecastForTime, recommendation, title, description, user.Name)
                : _notificationWritter.WriteNotificationPageForMan(
                    forecast, forecastForTime, recommendation, title, description, user.Name);

            await _notificationSender.SendNotificationAsync(email, subject, message);
        }
    }
}
