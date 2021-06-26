using Simbirsoft_Weather.Models;

namespace Simbirsoft_Weather.Services
{
    public interface IClothingConsultant
    {
        Recommendation GetRecommendation(Forecast forecast);
    }
}
