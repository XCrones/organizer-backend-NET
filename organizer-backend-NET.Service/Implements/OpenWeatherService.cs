using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Service.Interfaces;
using System.Net;

namespace organizer_backend_NET.Service.Implements
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientService _httpClientService;

        private readonly string _token = "c42021649078332395ebebbbd1302a0d";
        private readonly string _urlApi = "https://api.openweathermap.org/data/2.5";
        // private readonly string _urlIcon = "http://openweathermap.org/img/wn";
        // private readonly string _prefixIcon = "@2x.png";

        public OpenWeatherService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        private async Task<WeatherForecastViewModel?> FetchForecast(string queries)
        {
            var result = await _httpClientService.Get<WeatherForecastViewModel?>($"{_urlApi}/forecast/?{queries}&units=metric&appid={_token}");
            return result;
        }

        public async Task<IBaseResponse<WeatherForecastViewModel>> FetchByGeo(int lat, int lon)
        {
            try
            {
                var response = await FetchForecast($"lat={lat}&lon={lon}");

                if (response != null && response.cod == "200")
                {
                    return new BaseResponse<WeatherForecastViewModel>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = response
                    };
                }

                return new BaseResponse<WeatherForecastViewModel>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecastViewModel>()
                {
                    Description = $"[FetchByGeo] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecastViewModel>> FetchByName(string nameCity)
        {
            try
            {
                var response = await FetchForecast($"q={nameCity}");

                if (response != null && response.cod == "200")
                {
                    return new BaseResponse<WeatherForecastViewModel>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = response
                    };
                }

                return new BaseResponse<WeatherForecastViewModel>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecastViewModel>()
                {
                    Description = $"[FetchByName] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
