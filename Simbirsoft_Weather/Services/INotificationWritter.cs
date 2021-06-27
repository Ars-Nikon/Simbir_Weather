using Simbirsoft_Weather.Models;
using System.Collections.Generic;

namespace Simbirsoft_Weather.Services
{
    public interface INotificationWritter
    {
        string WriteNotificationPageForAll(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Recommendation recommendation,
            string title,
            string description);
        string WriteNotificationPageForMan(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person man,
            string title,
            string description);
        string WriteNotificationPageForWoman(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person woman,
            string title,
            string description);
    }
}
