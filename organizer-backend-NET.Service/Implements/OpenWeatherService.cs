﻿using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Service.Interfaces;
using System.Net;

namespace organizer_backend_NET.Service.Implements
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientService _httpClientService;

        private readonly string _apiKey = "YOR_API_KEY";
        private readonly string _urlApi = "https://api.openweathermap.org/data/2.5";

        public OpenWeatherService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        private async Task<WeatherForecastViewModel?> FetchForecast(string queries)
        {
            var result = await _httpClientService.Get<WeatherForecastViewModel?>($"{_urlApi}/forecast/?{queries}&units=metric&appid={_apiKey}");
            return result;
        }

        public async Task<IBaseResponse<WeatherForecastViewModel>> FetchByGeo(SearchCityByGeoViewModel model)
        {
            try
            {
                var response = await FetchForecast($"lat={model.Lat}&lon={model.Lon}");

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
