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
    public class WeatherUserService : IWeatherUserService
    {
        private readonly IWeatherUserRepository _repository;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherUserService(IWeatherUserRepository repository, IWeatherForecastService weatherForecastService)
        {
            _repository = repository;
            _weatherForecastService = weatherForecastService;
        }

        private async Task<IBaseResponse<WeatherUsers>> SeacrhCitiesList(int UId)
        {
            try
            {
                var searchUser = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt == null);

                if (searchUser == null)
                {
                    return new BaseResponse<WeatherUsers>()
                    {
                        Description = AppMessages.UserNotFound,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }

                return new BaseResponse<WeatherUsers>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = searchUser,
                };
            } catch(Exception ex)
            {
                return new BaseResponse<WeatherUsers>() {
                    Description = $"[SeacrhCitiesList] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<List<CityWeather>>> GetCities(int UId)
        {
            try
            {
                var itemsResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt == null);

                if (itemsResponse == null)
                {
                    return new BaseResponse<List<CityWeather>>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                return new BaseResponse<List<CityWeather>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = itemsResponse.Cities,
                };
            } catch (Exception ex)
            {
                return new BaseResponse<List<CityWeather>>()
                {
                    Description = $"[GetAll] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveItem(int UId, int cityId)
        {
            try
            {
                var weatherUser = await SeacrhCitiesList(UId);

                if (weatherUser.Data != null)
                {

                    var seacrhCity = weatherUser.Data.Cities.FirstOrDefault(item => item.id == cityId);

                    if (seacrhCity != null)
                    {
                        weatherUser.Data.Cities = weatherUser.Data.Cities.Where(item => item.id != cityId).ToList();
                        weatherUser.Data.UpdatedAt = DateTime.UtcNow;

                        await _repository.Update(weatherUser.Data);

                        return new BaseResponse<bool>()
                        {
                            Description = AppMessages.RemoveSucces,
                            StatusCode = HttpStatusCode.OK,
                            Data = true,
                        };
                    }

                    return new BaseResponse<bool>()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Description = AppMessages.CityNotFound,
                    };
                }

                return new BaseResponse<bool>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = AppMessages.UserNotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[RemoveItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecast>> SearchByCityName(int UId, string name)
        {
            try
            {
                var forecastSeacrh = await _weatherForecastService.SearchByName(name);

                if (forecastSeacrh.StatusCode == HttpStatusCode.OK)
                {
                    DateTime timeStamp = DateTime.UtcNow;
                    var city = new CityWeather()
                    {
                        id = forecastSeacrh.Data.city.id,
                        name = forecastSeacrh.Data.city.name,
                        country = forecastSeacrh.Data.city.country
                    };

                    var weatherUser = await SeacrhCitiesList(UId);

                    if (weatherUser.Data != null)
                    {
                        var searchCity = weatherUser.Data.Cities.FirstOrDefault(city => city.id == forecastSeacrh.Data.city.id);

                        if (searchCity != null)
                        {
                            return new BaseResponse<WeatherForecast>()
                            {
                                StatusCode = HttpStatusCode.OK,
                                Description = AppMessages.NoNeedToUpdate,
                                Data = forecastSeacrh.Data
                            };
                        }

                        weatherUser.Data.Cities.Add(city);
                        weatherUser.Data.UpdatedAt = timeStamp;

                        var response = await _repository.Update(weatherUser.Data);

                        return new BaseResponse<WeatherForecast>()
                        {
                            Description = AppMessages.UpdateSucces,
                            StatusCode = HttpStatusCode.OK,
                            Data = forecastSeacrh.Data,
                        };
                    }

                    var newItem = new WeatherUsers()
                    {
                        UId = UId,
                        Cities = new List<CityWeather>(),
                        CreatedAt = timeStamp,
                        UpdatedAt = timeStamp,
                    };

                    newItem.Cities.Add(city);

                    await _repository.Create(newItem);

                    return new BaseResponse<WeatherForecast>()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Description = AppMessages.CreateSucces,
                        Data = forecastSeacrh.Data
                    };
                }

                return new BaseResponse<WeatherForecast>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = forecastSeacrh.Description,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[SearchByCityName] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<WeatherForecast>> SearchByCityGeo(int UId, SearchCityByGeoViewModel model)
        {
            try
            {
                var forecastSeacrh = await _weatherForecastService.SearchByGeo(model);

                if (forecastSeacrh.StatusCode == HttpStatusCode.OK)
                {
                    DateTime timeStamp = DateTime.UtcNow;
                    var city = new CityWeather()
                    {
                        id = forecastSeacrh.Data.city.id,
                        name = forecastSeacrh.Data.city.name,
                        country = forecastSeacrh.Data.city.country
                    };

                    var weatherUser = await SeacrhCitiesList(UId);

                    if (weatherUser.Data != null)
                    {
                        var searchCity = weatherUser.Data.Cities.FirstOrDefault(city => city.id == forecastSeacrh.Data.city.id);

                        if (searchCity != null)
                        {
                            return new BaseResponse<WeatherForecast>()
                            {
                                StatusCode = HttpStatusCode.OK,
                                Description = AppMessages.NoNeedToUpdate,
                                Data = forecastSeacrh.Data
                            };
                        }

                        weatherUser.Data.Cities.Add(city);
                        weatherUser.Data.UpdatedAt = timeStamp;

                        var response = await _repository.Update(weatherUser.Data);

                        return new BaseResponse<WeatherForecast>()
                        {
                            Description = AppMessages.UpdateSucces,
                            StatusCode = HttpStatusCode.OK,
                            Data = forecastSeacrh.Data,
                        };
                    }

                    var newItem = new WeatherUsers()
                    {
                        UId = UId,
                        Cities = new List<CityWeather>(),
                        CreatedAt = timeStamp,
                        UpdatedAt = timeStamp,
                    };

                    newItem.Cities.Add(city);

                    await _repository.Create(newItem);

                    return new BaseResponse<WeatherForecast>()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Description = AppMessages.CreateSucces,
                        Data = forecastSeacrh.Data
                    };
                }

                return new BaseResponse<WeatherForecast>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = forecastSeacrh.Description,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeatherForecast>()
                {
                    Description = $"[SearchByCityName] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
