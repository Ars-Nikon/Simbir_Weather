﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simbirsoft_Weather.Models.Weather_api;

namespace Simbirsoft_Weather.Models
{
    public class IndexModel
    {
        public string Region { get; set; }

        public List<Weather_api.Weather> Weathers { get; set; }

        public Dictionary<string, root2> WeatherForTime { get; set; }


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
