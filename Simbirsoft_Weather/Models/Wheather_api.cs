using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class Wheather_api
    {
        private root Root { get; set; }

        public Wheather_api(string city)
        {
            Root = json_to_list(Get_wheather_5d_json(city));
        }


        private int _count = 3;

        public int retrycount
        {
            get { return _count; }
            set { _count = value; }
        }
        public class main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }
        public class weather
        {
            public string description { get; set; }
        }
        public class wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }

        public class root2
        {
            public main main { get; set; }
            public wind wind { get; set; }
            public List<weather> weather { get; set; }
            public string dt_txt { get; set; }

        }

        public class root
        {
            public IList<root2> list { get; set; }
        }

        private string Get_wheather_5d_json(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid=922b05ce49a15397ded5a54a17cad16d&cnt=50&lang=ru&units=metric";
            HttpClient client = new HttpClient();
            string json = "";
            for (int tries = 0; tries < retrycount; tries++)
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    HttpContent content = response.Content;
                    json = content.ReadAsStringAsync().Result;
                    var success = response.IsSuccessStatusCode;
                    if (success)
                        break;

                }
                catch (HttpRequestException e)
                {
                    throw new Exception(e.Message);
                }
            }

            return json;
        }

        private root json_to_list(string json)
        {
            var forecast_list = JsonSerializer.Deserialize<root>(json);
            return forecast_list;
        }

        public Dictionary<string, root2> WheatherForTime(string date)
        {
            var test = Root.list.Where(root2 => Convert.ToDateTime(root2.dt_txt).Day == Convert.ToDateTime(date).Day).ToDictionary(x => x.dt_txt, x => x);
            return test;
        }

        public class Wheather
        {
            public double SpeedWind { get; set; }      
            public double Mintemp { get; set; }
            public double Maxtemp { get; set; }
            public string main { get; set; }
            public DateTime Date { get; set; }
        }
        public List<Wheather> WheatherFor5Day()
        {
            DateTime day = DateTime.Today;
            Dictionary<int, Dictionary<string, root2>> test_dict = new Dictionary<int, Dictionary<string, root2>>();
            for (int i = 0; i <= 4; i++)
            {
                test_dict.Add(i, WheatherForTime(day.ToString()));
                day = day.AddDays(1);
            }

            List<Wheather> result = new List<Wheather>();
            foreach (var k in test_dict)
            {
                var inner_dict_key_dates = k.Value.Keys.ToList();
                var inner_dict_key_data = k.Value.Values.ToList();

                Wheather day_wheather = new Wheather();
                day_wheather.Date = Convert.ToDateTime(inner_dict_key_dates[0]).Date;
                day_wheather.Mintemp = inner_dict_key_data.Select(root2 => root2.main.temp_min).Min();
                day_wheather.Maxtemp = inner_dict_key_data.Select(root2 => root2.main.temp_min).Max();
                day_wheather.SpeedWind = inner_dict_key_data.Select(root2 => root2.wind.speed).Average();
                day_wheather.main = inner_dict_key_data[0].weather[0].description;
                result.Add(day_wheather);

            }
            return result;
        }
    }
}
