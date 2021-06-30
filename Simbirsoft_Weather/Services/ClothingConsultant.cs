using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Simbirsoft_Weather.Services
{
    public class ClothingConsultant : IClothingConsultant
    {
        private const int CLOTHES_PER_BODY_PART = 3; 
        private readonly IClothesRepository _clothesRepository;

        public ClothingConsultant(IClothesRepository clothesRepository)
        {
            _clothesRepository = clothesRepository;
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for men and women.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Recommendation"/> which contains a man and a woman with dress recommendations.</returns>
        public Recommendation GetRecommendation(Forecast forecast)
        {
            var recomendation = new Recommendation();

            DressPerson(recomendation.Man, ForWhom.Man, forecast);
            DressPerson(recomendation.Woman, ForWhom.Woman, forecast);

            return recomendation;
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for men and women.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Recommendation"/> which contains a man and a woman with dress recommendations.</returns>
        public Recommendation GetRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetRecommendation(new Forecast(forecast));
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for men.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Person"/> (man) with recommendations for clothes.</returns>
        public Person GetManRecommendation(Forecast forecast)
        {
            return GetRecommendationForManOrWoman(forecast, ForWhom.Man);
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for men.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Person"/> (man) with recommendations for clothes.</returns>
        public Person GetManRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetManRecommendation(new Forecast(forecast));
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for women.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Person"/> (woman) with recommendations for clothes.</returns>
        public Person GetWomanRecommendation(Forecast forecast)
        {
            return GetRecommendationForManOrWoman(forecast, ForWhom.Woman);
        }

        /// <summary>
        /// Get a recommendation of clothes for the weather for women.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Person"/> (woman) with recommendations for clothes.</returns>
        public Person GetWomanRecommendation(WeatherApi.ForecastView forecast)
        {
            return GetWomanRecommendation(new Forecast(forecast));
        }



        /// <summary>
        /// Get a recommendation of clothes for the weather for men or woman.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns><see cref="Person"/> (man or woman) with recommendations for clothes.</returns>
        private Person GetRecommendationForManOrWoman(Forecast forecast, ForWhom forWhom)
        {
            var person = new Person();

            DressPerson(person, forWhom, forecast);

            return person;
        }

        private void DressPerson(Person person, ForWhom forWhom, Forecast forecast)
        {
            person.Head = GetClothes(forecast, ClothesType.Head, forWhom);
            person.BodyTop = GetClothes(forecast, ClothesType.BodyTop, forWhom);
            person.BodyBottom = GetClothes(forecast, ClothesType.BodyBottom, forWhom);
            person.Legs = GetClothes(forecast, ClothesType.Legs, forWhom);
            person.Other = GetClothes(forecast, ClothesType.Other, forWhom);

            AddsEmptyClothes(person.Head, ClothesType.Head);
            AddsEmptyClothes(person.BodyTop, ClothesType.BodyTop);
            AddsEmptyClothes(person.BodyBottom, ClothesType.BodyBottom);
            AddsEmptyClothes(person.Legs, ClothesType.Legs);
            AddsEmptyClothes(person.Other, ClothesType.Other);
        }

        private bool IsGoodClothes(Forecast forecast, Clothes clothes)
        {
            var averageTemperature = (forecast.Mintemp + forecast.Maxtemp) / 2;
            return clothes.MinTemperature <= averageTemperature && averageTemperature < clothes.MaxTemperature;  
        }

        /// <summary>
        /// Adds empty clothes if the clothes list is empty.
        /// </summary>
        /// <param name="clothes">Clothing list.</param>
        /// <param name="clothesType">Clothing type.</param>
        private void AddsEmptyClothes(List<Clothes> clothes, ClothesType clothesType)
        {
            Clothes empty = new Clothes() { ClothesType = clothesType, Name = "-----" };
            
            if (clothes.Count == 0)
            {
                clothes.Add(empty);
            }
        }

        /// <summary>
        /// Receives clothes for the weather.
        /// </summary>
        /// <param name="forecast"></param>
        /// <param name="clothesType"></param>
        /// <param name="forWhom"></param>
        /// <returns>List of clothes.</returns>
        private List<Clothes> GetClothes(Forecast forecast, ClothesType clothesType, ForWhom forWhom)
        {
            var result = _clothesRepository.GetClothes()
                .Where(c => c.ClothesType == clothesType && IsGoodClothes(forecast, c) && (c.ForWhom & forWhom) > 0);
            
            int countResult = result.Count();
            if (countResult == 0)
                return new List<Clothes>(); 

            return result.Take(countResult >= CLOTHES_PER_BODY_PART ? CLOTHES_PER_BODY_PART : countResult).ToList();
        }
    }
}
