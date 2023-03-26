using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;

namespace organizer_backend_NET.Service.Interfaces
{
    public interface IOpenWeatherService
    {
        public Task<IBaseResponse<ForecastViewModel>> FetchByName(string nameCity);

        public Task<IBaseResponse<ForecastViewModel>> FetchByGeo(int lat, int lon);
    }
}
