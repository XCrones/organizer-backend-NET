using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Messages;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Service.Interfaces;
using System.Net;

namespace organizer_backend_NET.Service.Implements
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastService(IOpenWeatherService openWeatherService, IWeatherForecastRepository weatherForecastRepository)
        {
            _openWeatherService = openWeatherService;
            _repository = weatherForecastRepository;
        }

        private async Task<WeatherForecast?> SearchByGeoCity(SearchCityByGeoViewModel model)
        {
            return await _repository.Read().FirstOrDefaultAsync(item => item.city.coord.lat == model.Lat && item.city.coord.lon == model.Lon && item.DeleteAt == null);
        }

        private async Task<WeatherForecast?> SearchByNameCity(string cityName)
        {
            var subName = cityName.Substring(1).ToLower();
            var nameCapitalize = $"{char.ToUpper(cityName[0])}{subName}";
            return await _repository.Read().FirstOrDefaultAsync(item => item.city.name == nameCapitalize.Trim() && item.DeleteAt == null);
        }

        private async Task<IBaseResponse<WeatherForecast>> CreateItem (WeatherForecastViewModel model)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                WeatherForecast newItem = new WeatherForecast()
                {
                    cnt = model.cnt,
                    list = model.list,
                    city = model.city,
                    CreatedAt = timeStamp,
                    UpdatedAt = timeStamp,
                };

                await _repository.Create(newItem);

                return new BaseResponse<WeatherForecast>() { 
                    Data = newItem,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[CreateItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        private async Task<IBaseResponse<WeatherForecast>> UpdateItem(WeatherForecast target , WeatherForecastViewModel model) {
            try
            {
                target.list = model.list;
                target.cnt = model.cnt;
                target.city = model.city;
                target.UpdatedAt = DateTime.UtcNow;

                var response = await _repository.Update(target);

                return new BaseResponse<WeatherForecast>()
                {
                    Description = AppMessages.UpdateSucces,
                    StatusCode = HttpStatusCode.OK,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[UpdateItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecast>> SearchByGeo(SearchCityByGeoViewModel model)
        {
            try
            {
                var uniqCityGeo = await SearchByGeoCity(model);

                if (uniqCityGeo != null)
                {
                    var lastUpdate = uniqCityGeo.UpdatedAt.AddHours(1);

                    if (lastUpdate > DateTime.UtcNow)
                    {
                        return new BaseResponse<WeatherForecast>()
                        {
                            Description = AppMessages.NoNeedToUpdate,
                            StatusCode = HttpStatusCode.OK,
                            Data = uniqCityGeo
                        };
                    }
                }

                var response = await _openWeatherService.FetchByGeo(model);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new BaseResponse<WeatherForecast>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                var uniqCity = await SearchByNameCity(response.Data.city.name);

                if (uniqCity == null)
                {
                    var resultCreate = await CreateItem(response.Data);

                    if (resultCreate.StatusCode == HttpStatusCode.OK)
                    {
                        return new BaseResponse<WeatherForecast>() { 
                            StatusCode = HttpStatusCode.OK,
                            Data = resultCreate.Data,
                        };
                    }

                    return new BaseResponse<WeatherForecast>()
                    {
                        Description = AppMessages.ErrorSave,
                        StatusCode = HttpStatusCode.InternalServerError,
                    };
                }

                var resultUpdate = await UpdateItem(uniqCity, response.Data);

                if (resultUpdate.StatusCode == HttpStatusCode.OK)
                {
                    return new BaseResponse<WeatherForecast>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = resultUpdate.Data,
                    };
                }

                return new BaseResponse<WeatherForecast>()
                {
                    Description = AppMessages.ErrorSave,
                    StatusCode = HttpStatusCode.InsufficientStorage,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[SearchByGeo] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecast>> SearchByName(string cityName)
        {
            try
            {
                var uniqCity = await SearchByNameCity(cityName);

                if (uniqCity != null)
                {
                    var lastUpdate = uniqCity.UpdatedAt.AddHours(1);

                    if (lastUpdate > DateTime.UtcNow)
                    {
                        return new BaseResponse<WeatherForecast>()
                        {
                            Description = AppMessages.NoNeedToUpdate,
                            StatusCode = HttpStatusCode.OK,
                            Data = uniqCity
                        };
                    }
                }

                var response = await _openWeatherService.FetchByName(cityName);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new BaseResponse<WeatherForecast>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                if (uniqCity == null)
                {
                    var resultCreate = await CreateItem(response.Data);

                    if (resultCreate.StatusCode == HttpStatusCode.OK)
                    {
                        return new BaseResponse<WeatherForecast>()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Data = resultCreate.Data,
                        };
                    }

                    return new BaseResponse<WeatherForecast>()
                    {
                        Description = AppMessages.ErrorSave,
                        StatusCode = HttpStatusCode.InternalServerError,
                    };
                }


                var resultUpdate = await UpdateItem(uniqCity, response.Data);

                if (resultUpdate.StatusCode == HttpStatusCode.OK)
                {
                    return new BaseResponse<WeatherForecast>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = resultUpdate.Data,
                    };
                }

                return new BaseResponse<WeatherForecast>()
                {
                    Description = AppMessages.ErrorSave,
                    StatusCode = HttpStatusCode.InsufficientStorage,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[SearchByName] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecast>> GetByCityId(int cityId)
        {
            try
            {
                Console.WriteLine(cityId + " cityId");

                var response = await _repository.Read().FirstOrDefaultAsync(item => item.city.id == cityId && item.DeleteAt == null);

                if (response != null)
                {
                    return new BaseResponse<WeatherForecast>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = response,
                    };
                }

                return new BaseResponse<WeatherForecast>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = AppMessages.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[GetByCityId] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
