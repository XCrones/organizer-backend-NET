using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;

namespace organizer_backend_NET.Service.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<IBaseResponse<WeatherForecast>> SearchByName(string cityName);

        public Task<IBaseResponse<WeatherForecast>> SearchByGeo(int lat, int lon);
    }
}
