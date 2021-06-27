using Simbirsoft_Weather.Models;

namespace Simbirsoft_Weather.Services
{
    public interface IClothingConsultant
    {
        Recommendation GetRecommendation(Forecast forecast);
        Recommendation GetRecommendation(WeatherApi.ForecastView forecast);
        Person GetManRecommendation(Forecast forecast);
        Person GetManRecommendation(WeatherApi.ForecastView forecast);
        Person GetWomanRecommendation(Forecast forecast);
        Person GetWomanRecommendation(WeatherApi.ForecastView forecast);
    }
}
