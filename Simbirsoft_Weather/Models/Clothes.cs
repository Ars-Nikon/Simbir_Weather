using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Models
{
    public class Clothes
    {
        public ClothesType ClothesType { get; set; }
        public string Name { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public ForWhom ForWhom { get; set; }
    }
}
