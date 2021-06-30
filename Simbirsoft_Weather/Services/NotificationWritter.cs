using System;
using System.IO;
using System.Linq;
using Simbirsoft_Weather.Models;
using System.Collections.Generic;
using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Services
{
    public class NotificationWritter : INotificationWritter
    {
        private readonly Dictionary<ClothesType, string> _clothesName;
        private readonly IRecPatternWritter _recPatternWritter;

        public NotificationWritter(IRecPatternWritter recPatternWritter)
        {
            _recPatternWritter = recPatternWritter;
            _clothesName = new Dictionary<ClothesType, string>()
            {
                { ClothesType.Head, "Головной убор" },
                { ClothesType.BodyTop, "Верхняя одежда"},
                { ClothesType.BodyBottom, "Нижняя одежда"},
                { ClothesType.Legs, "Обувь"},
                { ClothesType.Other, "Другое"}
            };
        }

        public string WriteNotificationPageForAll(
            WeatherApi.ForecastView forecast,
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
                _clothesName[ClothesType.Head],
                string.Join(", ", recommendation.Woman.Head.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Head.Select(c => c.Name)),
                _clothesName[ClothesType.BodyTop],
                string.Join(", ", recommendation.Woman.BodyTop.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.BodyTop.Select(c => c.Name)),
                _clothesName[ClothesType.BodyBottom],
                string.Join(", ", recommendation.Woman.BodyBottom.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.BodyBottom.Select(c => c.Name)),
                _clothesName[ClothesType.Legs],
                string.Join(", ", recommendation.Woman.Legs.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Legs.Select(c => c.Name)),
                _clothesName[ClothesType.Other],
                string.Join(", ", recommendation.Woman.Other.Select(c => c.Name)),
                string.Join(", ", recommendation.Man.Other.Select(c => c.Name))
                );
        }

        public string WriteNotificationPageForMan(
            WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person man,
            string title,
            string description,
            string userName)
        {
            return ForManOrWoman(
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
                _clothesName[ClothesType.Head],
                string.Join("/", man.Head.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Man))),
                _clothesName[ClothesType.BodyTop],
                string.Join("/", man.BodyTop.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Man))),
                _clothesName[ClothesType.BodyBottom],
                string.Join("/", man.BodyBottom.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Man))),
                _clothesName[ClothesType.Legs],
                string.Join("/", man.Legs.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Man))),
                _clothesName[ClothesType.Other],
                string.Join("/", man.Other.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Man))),
                userName,
                forecast.ProbabilityRain
                );
        }

        public string WriteNotificationPageForWoman(
            WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person woman,
            string title,
            string description,
            string userName)
        {
            return ForManOrWoman(
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
                _clothesName[ClothesType.Head],
                string.Join("/", woman.Head.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Woman))),
                _clothesName[ClothesType.BodyTop],
                string.Join("/", woman.BodyTop.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Woman))),
                _clothesName[ClothesType.BodyBottom],
                string.Join("/", woman.BodyBottom.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Woman))),
                _clothesName[ClothesType.Legs],
                string.Join("/", woman.Legs.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Woman))),
                _clothesName[ClothesType.Other],
                string.Join("/", woman.Other.Select(c => _recPatternWritter.WriteRec(c, ForWhom.Woman))),
                userName,
                forecast.ProbabilityRain * 100
                );
        }

        private string ForAll(params object[] args)
        {
            return string.Format(File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/notification.html"), args);
        }
        private string ForManOrWoman(params object[] args)
        {
            return string.Format(File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/notificationForWhom.html"), args);
        }
    }
}
