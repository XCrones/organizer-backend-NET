using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;

namespace organizer_backend_NET.Service.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<IBaseResponse<WeatherForecast>> SearchByName(string cityName);

        public Task<IBaseResponse<WeatherForecast>> SearchByGeo(SearchCityByGeoViewModel model);

        public Task<IBaseResponse<WeatherForecast>> GetByCityId(int cityId);
    }
}
