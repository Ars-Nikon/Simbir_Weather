using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Simbirsoft_Weather.Services
{
    public class ClothingConsultant : IClothingConsultant
    {
        private List<Clothes> _clothes = new List<Clothes>()
        {
            new Clothes() { ClothesType = ClothesType.Head, Name = "Кепка", MinTemperature = 30, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Man },
            new Clothes() { ClothesType = ClothesType.Head, Name = "Шапка", MinTemperature = double.MinValue, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.Head, Name = "Шляпа", MinTemperature = 30, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Woman },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Куртка", MinTemperature = double.MinValue, MaxTemperature = 5, ForWhom = ForWhom.Man },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Пальто", MinTemperature = double.MinValue, MaxTemperature = 5, ForWhom = ForWhom.Woman },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Футболка", MinTemperature = 19, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Man },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Ветровка", MinTemperature = 5, MaxTemperature = 10, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Свитер", MinTemperature = 10, MaxTemperature = 19, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Блузка", MinTemperature = 19, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Woman },
            new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Джинсы", MinTemperature = 5, MaxTemperature = 25, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Шорты", MinTemperature = 25, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Man },
            new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Юбка", MinTemperature = 25, MaxTemperature = double.MaxValue, ForWhom = ForWhom.Woman },
            new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Теплые штаны", MinTemperature = double.MinValue, MaxTemperature = 5, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.Legs, Name = "Кроссовки", MinTemperature = 15, MaxTemperature = 30, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.Legs, Name = "Тапочки", MinTemperature = 30, MaxTemperature = double.MaxValue, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.Legs, Name = "Ботинки", MinTemperature = double.MinValue, MaxTemperature = 15, ForWhom = ForWhom.ForAll },
            new Clothes() { ClothesType = ClothesType.Other, Name = "Зонт", MinTemperature = double.MinValue, MaxTemperature = double.MaxValue, ForWhom = ForWhom.ForAll },
        };

        public Recommendation GetRecommendation(Forecast forecast)
        {
            var recomendation = new Recommendation();

            DressPerson(recomendation.Man, ForWhom.Man, forecast);
            DressPerson(recomendation.Woman, ForWhom.Woman, forecast);

            return recomendation;
        }

        private void DressPerson(Person person, ForWhom forWhom, Forecast forecast)
        {
            person.Head = _clothes.FirstOrDefault
                (c => IsGoodClothes(forecast, c) && c.ClothesType == ClothesType.Head && (c.ForWhom & forWhom) > 0);
            person.BodyTop = _clothes.FirstOrDefault
                (c => IsGoodClothes(forecast, c) && c.ClothesType == ClothesType.BodyTop && (c.ForWhom & forWhom) > 0);
            person.BodyBottom = _clothes.FirstOrDefault
                (c => IsGoodClothes(forecast, c) && c.ClothesType == ClothesType.BodyBottom && (c.ForWhom & forWhom) > 0);
            person.Legs = _clothes.FirstOrDefault
                (c => IsGoodClothes(forecast, c) && c.ClothesType == ClothesType.Legs && (c.ForWhom & forWhom) > 0);
            person.Other = _clothes.FirstOrDefault
                (c => IsGoodClothes(forecast, c) && c.ClothesType == ClothesType.Other && (c.ForWhom & forWhom) > 0);
        }

        private bool IsGoodClothes(Forecast forecast, Clothes clothes)
        {
            var averageTemperature = (forecast.Mintemp + forecast.Maxtemp) / 2;
            return clothes.MinTemperature <= averageTemperature && averageTemperature < clothes.MaxTemperature;  
        }
    }
}
