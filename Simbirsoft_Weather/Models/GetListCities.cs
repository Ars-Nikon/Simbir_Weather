using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class GetListCities
    {
        private CityContext CityContext;

        public GetListCities(CityContext cityContext)
        {
            CityContext = cityContext;
        }

        public List<City> Cities()
        {
            return CityContext.Cities.ToList();
        }
    }
}
