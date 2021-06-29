using Microsoft.EntityFrameworkCore;
using Simbirsoft_Weather.Models.Enums;
using System.Linq;

namespace Simbirsoft_Weather.Models
{
    public class ClothesContext : DbContext
    {
        public DbSet<Clothes> Clothes { get; set; }

        public ClothesContext(DbContextOptions<ClothesContext> options)
                 : base(options)
        {
            Database.EnsureCreated();
            if (Clothes.Count() == 0)
            {
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Head, Name = "Кепка", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Man });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Head, Name = "Шапка", MinTemperature = -100, MaxTemperature = 10, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Head, Name = "Шляпа", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.Woman });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Куртка", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Man });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Пальто", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.Woman });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Футболка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Man });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Ветровка", MinTemperature = 5, MaxTemperature = 10, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Свитер", MinTemperature = 10, MaxTemperature = 19, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyTop, Name = "Блузка", MinTemperature = 19, MaxTemperature = 1000, ForWhom = ForWhom.Woman });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Джинсы", MinTemperature = 5, MaxTemperature = 25, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Шорты", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Man });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Юбка", MinTemperature = 25, MaxTemperature = 1000, ForWhom = ForWhom.Woman });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.BodyBottom, Name = "Теплые штаны", MinTemperature = -100, MaxTemperature = 5, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Legs, Name = "Кроссовки", MinTemperature = 15, MaxTemperature = 30, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Legs, Name = "Тапочки", MinTemperature = 30, MaxTemperature = 1000, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Legs, Name = "Ботинки", MinTemperature = -100, MaxTemperature = 15, ForWhom = ForWhom.ForAll });
                Clothes.Add(new Clothes() { ClothesType = ClothesType.Other, Name = "Зонт", MinTemperature = -100, MaxTemperature = 1000, ForWhom = ForWhom.ForAll });
                SaveChanges();
            }
        }
    }
}
