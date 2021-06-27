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
                recommendation.Woman.Head.ClothesType,
                recommendation.Woman.Head.Name,
                recommendation.Man.Head.Name,
                recommendation.Woman.BodyTop.ClothesType,
                recommendation.Woman.BodyTop.Name,
                recommendation.Man.BodyTop.Name,
                recommendation.Woman.BodyBottom.ClothesType,
                recommendation.Woman.BodyBottom.Name,
                recommendation.Man.BodyBottom.Name,
                recommendation.Woman.Legs.ClothesType,
                recommendation.Woman.Legs.Name,
                recommendation.Man.Legs.Name,
                recommendation.Woman.Other.ClothesType,
                recommendation.Woman.Other.Name,
                recommendation.Man.Other.Name
                );
        }

        public string WriteNotificationPageForMan(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person man,
            string title,
            string description)
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
                man.Head.ClothesType,
                man.Head.Name,
                man.BodyTop.ClothesType,
                man.BodyTop.Name,
                man.BodyBottom.ClothesType,
                man.BodyBottom.Name,
                man.Legs.ClothesType,
                man.Legs.Name,
                man.Other.ClothesType,
                man.Other.Name
                );
        }

        public string WriteNotificationPageForWoman(WeatherApi.ForecastView forecast,
            Dictionary<string, WeatherApi.ForecastData> weatherForTime,
            Person woman,
            string title,
            string description)
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
                woman.Head.ClothesType,
                woman.Head.Name,
                woman.BodyTop.ClothesType,
                woman.BodyTop.Name,
                woman.BodyBottom.ClothesType,
                woman.BodyBottom.Name,
                woman.Legs.ClothesType,
                woman.Legs.Name,
                woman.Other.ClothesType,
                woman.Other.Name
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
