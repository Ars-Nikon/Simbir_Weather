using Dadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public static class GetCityByIp
    {
        public static async Task<string> CityByIP(string ip)
        {
            var api = new SuggestClientAsync("08249550af359364c5c05a6a1b7dc19837bc7078");
            var response = await api.Iplocate(ip);
            if (response.location == null)
            {
                return "Хабаровск";
            }
            var address = response.location.data.city;
            return address;
        }
    }
}
