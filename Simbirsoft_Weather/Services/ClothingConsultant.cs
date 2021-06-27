using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
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
            var _clothes = _clothesRepository.GetClothes();
            person.Head = _clothes.Where(c => c.ClothesType == ClothesType.Head)
                .SingleOrDefault(c => IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            person.BodyTop = _clothes.Where(c => c.ClothesType == ClothesType.BodyTop)
                .SingleOrDefault(c => IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            person.BodyBottom = _clothes.Where(c => c.ClothesType == ClothesType.BodyBottom)
                .SingleOrDefault(c => IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            person.Legs = _clothes.Where(c => c.ClothesType == ClothesType.Legs)
                .SingleOrDefault(c => IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            person.Other = _clothes.Where(c => c.ClothesType == ClothesType.Other)
                .SingleOrDefault(c => IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0 && forecast.СhanceOfRain >= 0.5);

            person.Head = EmptyClothes(person.Head, ClothesType.Head);
            person.BodyTop = EmptyClothes(person.BodyTop, ClothesType.BodyTop);
            person.BodyBottom = EmptyClothes(person.BodyBottom, ClothesType.BodyBottom);
            person.Legs = EmptyClothes(person.Legs, ClothesType.Legs);
            person.Other = EmptyClothes(person.Other, ClothesType.Other);
        }

        private bool IsGoodClothes(Forecast forecast, Clothes clothes)
        {
            var averageTemperature = (forecast.Mintemp + forecast.Maxtemp) / 2;
            return clothes.MinTemperature <= averageTemperature && averageTemperature < clothes.MaxTemperature;  
        }

        private Clothes EmptyClothes(Clothes clothes, ClothesType clothesType)
        {
            Clothes empty = new Clothes() { ClothesType = clothesType, Name = "-----" };
            return clothes == null ? empty : clothes;
        }
    }
}
