using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Services;
using System;
using Xunit;

namespace Simbirsoft_Weather.Tests.Services
{
    public class ClothingConsultantTest
    {
        private readonly IClothingConsultant _clothingConsultant;

        public ClothingConsultantTest()
        {
            _clothingConsultant = new ClothingConsultant();
        }

        [Theory]
        [InlineData(30, 50, 0.3)]
        public void Test1(double minTemp, double maxTemp, double chanceOfRain)
        {
            Forecast forecast = new Forecast() { Mintemp = minTemp, Maxtemp = maxTemp, СhanceOfRain = chanceOfRain };

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
