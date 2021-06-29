using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Simbirsoft_Weather.Services
{
    public class ClothingConsultant : IClothingConsultant
    {
        private readonly IClothesRepository _clothesRepository;

        public ClothingConsultant(IClothesRepository clothesRepository)
        {
            _clothesRepository = clothesRepository;
        }

        public Recommendation GetRecommendation(Forecast forecast)
        {
            var recomendation = new Recommendation();

            DressPerson(recomendation.Man, ForWhom.Man, forecast);
            DressPerson(recomendation.Woman, ForWhom.Woman, forecast);

            return recomendation;
        }

        public Recommendation GetRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetRecommendation(new Forecast(forecast));
        }

        public Person GetManRecommendation(Forecast forecast)
        {
            return GetRecommendationForWhom(forecast, ForWhom.Man);
        }

        public Person GetManRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetManRecommendation(new Forecast(forecast));
        }

        public Person GetWomanRecommendation(Forecast forecast)
        {
            return GetRecommendationForWhom(forecast, ForWhom.Woman);
        }

        public Person GetWomanRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetWomanRecommendation(new Forecast(forecast));
        }



        private Person GetRecommendationForWhom(Forecast forecast, ForWhom forWhom)
        {
            var person = new Person();

            DressPerson(person, forWhom, forecast);

            return person;
        }

        private void DressPerson(Person person, ForWhom forWhom, Forecast forecast)
        {
            int count = 3;

            person.Head = GetClothes(forecast, ClothesType.Head, forWhom, count);
            person.BodyTop = GetClothes(forecast, ClothesType.BodyTop, forWhom, count);
            person.BodyBottom = GetClothes(forecast, ClothesType.BodyBottom, forWhom, count);
            person.Legs = GetClothes(forecast, ClothesType.Legs, forWhom, count);
            person.Other = GetClothes(forecast, ClothesType.Other, forWhom, count);

            EmptyClothes(person.Head, ClothesType.Head);
            EmptyClothes(person.BodyTop, ClothesType.BodyTop);
            EmptyClothes(person.BodyBottom, ClothesType.BodyBottom);
            EmptyClothes(person.Legs, ClothesType.Legs);
            EmptyClothes(person.Other, ClothesType.Other);
        }

        private bool IsGoodClothes(Forecast forecast, Clothes clothes)
        {
            var averageTemperature = (forecast.Mintemp + forecast.Maxtemp) / 2;
            return clothes.MinTemperature <= averageTemperature && averageTemperature < clothes.MaxTemperature;  
        }

        private void EmptyClothes(List<Clothes> clothes, ClothesType clothesType)
        {
            Clothes empty = new Clothes() { ClothesType = clothesType, Name = "-----" };
            
            if (clothes.Count == 0)
            {
                clothes.Add(empty);
            }
        }

        private List<Clothes> GetClothes(Forecast forecast, ClothesType clothesType, ForWhom forWhom, int count)
        {
            var result = _clothesRepository.GetClothes()
                .Where(c => c.ClothesType == clothesType && IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            
            int countResult = result.Count();

            if (countResult == 0) return new List<Clothes>(); 

            return result.Take(countResult >= count ? count : countResult).ToList();
        }
    }
}
