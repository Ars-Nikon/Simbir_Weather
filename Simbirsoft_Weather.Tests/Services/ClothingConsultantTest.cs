using Moq;
using Xunit;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using Simbirsoft_Weather.Services;
using System.Collections.Generic;

namespace Simbirsoft_Weather.Tests.Services
{
    public class ClothingConsultantTest
    {
        private readonly IClothingConsultant _clothingConsultant;
        private readonly List<Clothes> _clothes;

        public ClothingConsultantTest()
        {
            _clothes = new List<Clothes>()
            {
                new Clothes() { ClothesType = ClothesType.Head, Name = "Кепка", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.Head, Name = "Шапка", MinTemperature = -100, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Head, Name = "Шляпа", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Куртка", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Пальто", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Футболка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Ветровка", MinTemperature = 5, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Свитер", MinTemperature = 10, MaxTemperature = 19, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Блузка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Джинсы", MinTemperature = 5, MaxTemperature = 25, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Шорты", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Юбка", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Теплые штаны", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Кроссовки", MinTemperature = 15, MaxTemperature = 30, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Тапочки", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Ботинки", MinTemperature = -100, MaxTemperature = 15, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Other, Name = "Зонт", MinTemperature = -100, MaxTemperature = 1000, ForWhom = ForWhom.ForAll },

                new Clothes() { ClothesType = ClothesType.Head, Name = "Кепка", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.Head, Name = "Шапка", MinTemperature = -100, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Head, Name = "Шляпа", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Куртка", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Пальто", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Футболка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Ветровка", MinTemperature = 5, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Свитер", MinTemperature = 10, MaxTemperature = 19, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Блузка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Джинсы", MinTemperature = 5, MaxTemperature = 25, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Шорты", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Man },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Юбка", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Woman },
                new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Теплые штаны", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Кроссовки", MinTemperature = 15, MaxTemperature = 30, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Тапочки", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Legs, Name = "Ботинки", MinTemperature = -100, MaxTemperature = 15, ForWhom = ForWhom.ForAll },
                new Clothes() { ClothesType = ClothesType.Other, Name = "Зонт", MinTemperature = -100, MaxTemperature = 1000, ForWhom = ForWhom.ForAll },
            };
         
            var clothesRepositry = new Mock<IClothesRepository>();
            clothesRepositry.Setup(repo => repo.GetClothes())
                .Returns(_clothes);

            _clothingConsultant = new ClothingConsultant(clothesRepositry.Object);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public void GetRecommendation_ActionExecutes_ReturnsNotNullRecommendation(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = _clothingConsultant.GetRecommendation(forecast);

            Assert.NotNull(result);
            Assert.IsType<Recommendation>(result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public void GetRecommendation_ActionExecutes_ReturnsNotNullPersonsInRecommendation(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Recommendation>(_clothingConsultant.GetRecommendation(forecast));

            Assert.NotNull(result.Man);
            Assert.IsType<Person>(result.Man);
            Assert.NotNull(result.Woman);
            Assert.IsType<Person>(result.Woman);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public void GetRecommendation_ActionExecutes_ReturnsNotNullListsOfClothesInPersonsInRecommendation(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Recommendation>(_clothingConsultant.GetRecommendation(forecast));
            var man = Assert.IsType<Person>(result.Man);
            var woman = Assert.IsType<Person>(result.Woman);

            Assert.NotNull(man.Head);
            Assert.NotNull(man.BodyTop);
            Assert.NotNull(man.BodyBottom);
            Assert.NotNull(man.Legs);
            Assert.NotNull(man.Other);
            Assert.NotNull(woman.Head);
            Assert.NotNull(woman.BodyTop);
            Assert.NotNull(woman.BodyBottom);
            Assert.NotNull(woman.Legs);
            Assert.NotNull(woman.Other);
            Assert.IsType<List<Clothes>>(man.Head);
            Assert.IsType<List<Clothes>>(man.BodyTop);
            Assert.IsType<List<Clothes>>(man.BodyBottom);
            Assert.IsType<List<Clothes>>(man.Legs);
            Assert.IsType<List<Clothes>>(man.Other);
            Assert.IsType<List<Clothes>>(woman.Head);
            Assert.IsType<List<Clothes>>(woman.BodyTop);
            Assert.IsType<List<Clothes>>(woman.BodyBottom);
            Assert.IsType<List<Clothes>>(woman.Legs);
            Assert.IsType<List<Clothes>>(woman.Other);
        }

        [Theory]
        [InlineData(double.MinValue, double.MinValue, 0)]
        public void GetRecommendation_ActionExecutes_ReturnsNotEmptyListsOfClothesInPersonsInRecommendation(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Recommendation>(_clothingConsultant.GetRecommendation(forecast));
            var man = Assert.IsType<Person>(result.Man);
            var woman = Assert.IsType<Person>(result.Woman);

            Assert.NotEmpty(man.Head);
            Assert.NotEmpty(man.BodyTop);
            Assert.NotEmpty(man.BodyBottom);
            Assert.NotEmpty(man.Legs);
            Assert.NotEmpty(man.Other);
            Assert.NotEmpty(woman.Head);
            Assert.NotEmpty(woman.BodyTop);
            Assert.NotEmpty(woman.BodyBottom);
            Assert.NotEmpty(woman.Legs);
            Assert.NotEmpty(woman.Other);
        }



        [Theory]
        [InlineData(0, 0, 0)]
        public void GetManRecommendation_ActionExecutes_ReturnsNotNullPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = _clothingConsultant.GetManRecommendation(forecast);

            Assert.NotNull(result);
            Assert.IsType<Person>(result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public void GetManRecommendation_ActionExecutes_ReturnsNotNullListsOfClothesInPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Person>(_clothingConsultant.GetManRecommendation(forecast));

            Assert.NotNull(result.Head);
            Assert.NotNull(result.BodyTop);
            Assert.NotNull(result.BodyBottom);
            Assert.NotNull(result.Legs);
            Assert.NotNull(result.Other);
            Assert.IsType<List<Clothes>>(result.Head);
            Assert.IsType<List<Clothes>>(result.BodyTop);
            Assert.IsType<List<Clothes>>(result.BodyBottom);
            Assert.IsType<List<Clothes>>(result.Legs);
            Assert.IsType<List<Clothes>>(result.Other);
        }

        [Theory]
        [InlineData(double.MinValue, double.MinValue, 0)]
        public void GetManRecommendation_ActionExecutes_ReturnsNotEmptyListsOfClothesInPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Person>(_clothingConsultant.GetManRecommendation(forecast));

            Assert.NotEmpty(result.Head);
            Assert.NotEmpty(result.BodyTop);
            Assert.NotEmpty(result.BodyBottom);
            Assert.NotEmpty(result.Legs);
            Assert.NotEmpty(result.Other);
        }



        [Theory]
        [InlineData(0, 0, 0)]
        public void GetWomanRecommendation_ActionExecutes_ReturnsNotNullPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = _clothingConsultant.GetWomanRecommendation(forecast);

            Assert.NotNull(result);
            Assert.IsType<Person>(result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public void GetWomanRecommendation_ActionExecutes_ReturnsNotNullListsOfClothesInPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Person>(_clothingConsultant.GetWomanRecommendation(forecast));

            Assert.NotNull(result.Head);
            Assert.NotNull(result.BodyTop);
            Assert.NotNull(result.BodyBottom);
            Assert.NotNull(result.Legs);
            Assert.NotNull(result.Other);
            Assert.IsType<List<Clothes>>(result.Head);
            Assert.IsType<List<Clothes>>(result.BodyTop);
            Assert.IsType<List<Clothes>>(result.BodyBottom);
            Assert.IsType<List<Clothes>>(result.Legs);
            Assert.IsType<List<Clothes>>(result.Other);
        }

        [Theory]
        [InlineData(double.MinValue, double.MinValue, 0)]
        public void GetWomanRecommendation_ActionExecutes_ReturnsNotEmptyListsOfClothesInPerson(double minTemp, double maxTemp, double chanceOfRain)
        {
            var forecast = new WeatherApi.ForecastView()
            {
                Mintemp = minTemp,
                Maxtemp = maxTemp,
                ProbabilityRain = chanceOfRain
            };

            var result = Assert.IsType<Person>(_clothingConsultant.GetWomanRecommendation(forecast));

            Assert.NotEmpty(result.Head);
            Assert.NotEmpty(result.BodyTop);
            Assert.NotEmpty(result.BodyBottom);
            Assert.NotEmpty(result.Legs);
            Assert.NotEmpty(result.Other);
        }
    }
}
