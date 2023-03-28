using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;

namespace organizer_backend_NET.Service.Interfaces
{
    public interface IWeatherUserService
    {
        public Task<IBaseResponse<List<CityWeather>>> GetCities(int UId);

        public Task<IBaseResponse<bool>> RemoveItem(int UId, int cityId);

        public Task<IBaseResponse<CityWeather>> SearchByCityName(int UId, string name);

        public Task<IBaseResponse<CityWeather>> SearchByCityGeo(int UId, SearchCityByGeoViewModel model);
    }
}
