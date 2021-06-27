namespace Simbirsoft_Weather.Models
{
    public class Forecast
    {
        public double Mintemp { get; set; }
        public double Maxtemp { get; set; }
        public double СhanceOfRain { get; set; }

        public Forecast(WeatherApi.ForecastView forecast)
        {
            Mintemp = forecast.Mintemp;
            Maxtemp = forecast.Maxtemp;
            СhanceOfRain = forecast.ProbabilityRain;
        }
    }
}
