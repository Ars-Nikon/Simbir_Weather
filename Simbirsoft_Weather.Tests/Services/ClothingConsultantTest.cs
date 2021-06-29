using Moq;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using Simbirsoft_Weather.Services;
using System.Collections.Generic;
using Xunit;

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
        [InlineData(30, 50, 0.3)]
        public void Test1(double minTemp, double maxTemp, double chanceOfRain)
        {
            Forecast forecast = new Forecast(minTemp, maxTemp, chanceOfRain);

            var result = _clothingConsultant.GetRecommendation(forecast);

            Assert.NotNull(result.Man.Head);
            Assert.NotNull(result.Man.BodyTop);
            Assert.NotNull(result.Man.BodyBottom);
            Assert.NotNull(result.Man.Legs);
            Assert.NotNull(result.Woman.Head);
            Assert.NotNull(result.Woman.BodyTop);
            Assert.NotNull(result.Woman.BodyBottom);
            Assert.NotNull(result.Woman.Legs);
        }
    }
}
