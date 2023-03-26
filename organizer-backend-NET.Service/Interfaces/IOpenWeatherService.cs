using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;

namespace organizer_backend_NET.Service.Interfaces
{
    public interface IOpenWeatherService
    {
        public Task<IBaseResponse<WeatherForecastViewModel>> FetchByName(string nameCity);

        public Task<IBaseResponse<WeatherForecastViewModel>> FetchByGeo(int lat, int lon);
    }
}
