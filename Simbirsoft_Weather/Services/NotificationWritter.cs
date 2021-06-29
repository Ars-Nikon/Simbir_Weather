using Simbirsoft_Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Services
{
    public class NotificationWritter : INotificationWritter
    {
        private readonly Dictionary<ClothesType, string> clothesName;

        public NotificationWritter()
        {
            clothesName = new Dictionary<ClothesType, string>();
            clothesName.Add(ClothesType.Head, "Головной убор");
            clothesName.Add(ClothesType.BodyTop, "Верхняя одежда");
            clothesName.Add(ClothesType.BodyBottom, "Нижняя одежда");
            clothesName.Add(ClothesType.Legs, "Обувь");
            clothesName.Add(ClothesType.Other, "Другое");
        }

        public string WriteNotificationPageForAll(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Recommendation recommendation,
            string title,
            string description)
        {
            return ForAll(
                title,
                description,
                forecast.Date.DayOfWeek,
                forecast.Date.ToString("M"),
                forecast.City.Name,
                Math.Round((forecast.Maxtemp + forecast.Mintemp) / 2, 1),
                forecast.Icon,
                Math.Round(forecast.SpeedWind,1),
                forecast.Main,
                weatherForTime[weatherForTime.Keys.Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].WeatherDescriptions[0].Description,
                clothesName[ClothesType.Head],
                string.Join(", ", recommendation.Woman.Head.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Head.Select(c => c.Name)),
                clothesName[ClothesType.BodyTop],
                string.Join(", ", recommendation.Woman.BodyTop.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.BodyTop.Select(c => c.Name)),
                clothesName[ClothesType.BodyBottom],
                string.Join(", ", recommendation.Woman.BodyBottom.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.BodyBottom.Select(c => c.Name)),
                clothesName[ClothesType.Legs],
                string.Join(", ", recommendation.Woman.Legs.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Legs.Select(c => c.Name)),
                clothesName[ClothesType.Other],
                string.Join(", ", recommendation.Woman.Other.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Other.Select(c => c.Name))
                );
        }

        public string WriteNotificationPageForMan(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person man,
            string title,
            string description,
            string userName)
        {
            return ForWhom(Models.Enums.ForWhom.Man,
                title,
                description,
                forecast.Date.DayOfWeek,
                forecast.Date.ToString("M"),
                forecast.City.Name,
                Math.Round((forecast.Maxtemp + forecast.Mintemp) / 2, 1),
                forecast.Icon,
                Math.Round(forecast.SpeedWind, 1),
                forecast.Main,
                weatherForTime[weatherForTime.Keys.Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].WeatherDescriptions[0].Description,
                "Мужчина",
                clothesName[ClothesType.Head],
                string.Join(", ", man.Head.Select(c => c.Name)),
                clothesName[ClothesType.BodyTop],
                string.Join(", ", man.BodyTop.Select(c => c.Name)),
                clothesName[ClothesType.BodyBottom],
                string.Join(", ", man.BodyBottom.Select(c => c.Name)),
                clothesName[ClothesType.Legs],
                string.Join(", ", man.Legs.Select(c => c.Name)),
                clothesName[ClothesType.Other],
                string.Join(", ", man.Other.Select(c => c.Name)),
                userName,
                forecast.ProbabilityRain
                );
        }

        public string WriteNotificationPageForWoman(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person woman,
            string title,
            string description,
            string userName)
        {
            return ForWhom(Models.Enums.ForWhom.Man,
                title,
                description,
                forecast.Date.DayOfWeek,
                forecast.Date.ToString("M"),
                forecast.City.Name,
                Math.Round((forecast.Maxtemp + forecast.Mintemp) / 2, 1),
                forecast.Icon,
                Math.Round(forecast.SpeedWind, 1),
                forecast.Main,
                weatherForTime[weatherForTime.Keys.Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(1).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(2).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(3).First()].WeatherDescriptions[0].Description,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].Wind.Deg / 10d,
                weatherForTime[weatherForTime.Keys.Skip(1).Take(4).First()].WeatherDescriptions[0].Description,
                "Женщина",
                clothesName[ClothesType.Head],
                string.Join(", ", woman.Head.Select(c => c.Name)),
                clothesName[ClothesType.BodyTop],
                string.Join(", ", woman.BodyTop.Select(c => c.Name)),
                clothesName[ClothesType.BodyBottom],
                string.Join(", ", woman.BodyBottom.Select(c => c.Name)),
                clothesName[ClothesType.Legs],
                string.Join(", ", woman.Legs.Select(c => c.Name)),
                clothesName[ClothesType.Other],
                string.Join(", ", woman.Other.Select(c => c.Name)),
                userName,
                forecast.ProbabilityRain * 100
                );
        }

        private string ForAll(params object[] args)
        {
            return string.Format(File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/notification.html"), args);
        }
        private string ForWhom(ForWhom forWhom, params object[] args)
        {
            return string.Format(File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/notificationForWhom.html"), args);
        }
    }
}
