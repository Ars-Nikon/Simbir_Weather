﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simbirsoft_Weather.Models.WeatherApi;

namespace Simbirsoft_Weather.Models
{
    public class IndexModel
    {
        public string Region { get; set; }

        public string ErrorMessege { get; set; }

        public string Result { get; set; }

        public List<ForecastView> Weathers { get; set; }

        public Dictionary<string, ForecastData> WeatherForTime { get; set; }

        public Person ClothesMan { get; set; }

        public Person ClothesWoman { get; set; }

        public readonly Dictionary<int, string> DayOfWeek = new Dictionary<int, string>()
        {{1, "Понедельник"},
           {2, "Вторник"},
           {3, "Среда"},
           {4, "Четверг"},
           {5, "Пятница"},
           {6, "Суббота"},
           {0, "Воскресенье"}
         };





    }
}
