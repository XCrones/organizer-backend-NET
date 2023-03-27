using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Response;
using organizer_backend_NET.Service.Interfaces;
using System.Net;

namespace organizer_backend_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherUserService _userService;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherController(IWeatherUserService userService, IWeatherForecastService weatherForecastService)
        {
            _userService = userService;
            _weatherForecastService = weatherForecastService;
        }

        private int GetUId()
        {
            try
            {
                var UId = User.Claims.Where(a => a.Type == "UId").FirstOrDefault().Value;

                if (UId == null || string.IsNullOrWhiteSpace(UId))
                {
                    return -1;
                }

                return Int32.Parse(UId);

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [Authorize]
        [HttpPost("city-name")]
        public async Task<IActionResult> GetForecastByCityName(SearchCityyNameViewModel model) 
        {
            int UId = GetUId();


            if (UId != -1)
            {
                var result = await _userService.SearchByCityName(UId, model.CityName);

                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return Created("", new ActionResponse<WeatherForecast> {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<WeatherForecast>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                return BadRequest(new ActionResponse<WeatherForecast>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                    Data = result.Data,
                });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("city-geo")]
        public async Task<IActionResult> GetForecastByCityGeo(SearchCityByGeoViewModel model)
        {
            int UId = GetUId();


            if (UId != -1)
            {
                var result = await _userService.SearchByCityGeo(UId, model);

                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return Created("", new ActionResponse<WeatherForecast>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<WeatherForecast>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                return BadRequest(new ActionResponse<WeatherForecast>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                    Data = result.Data,
                });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            int UId = GetUId();


            if (UId != -1)
            {
                var result = await _userService.GetCities(UId);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<List<CityWeather>>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                return BadRequest(new ActionResponse<List<CityWeather>>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                    Data = result.Data,
                });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetForecastByCityId(int cityId)
        {
            int UId = GetUId();


            if (UId != -1)
            {
                var result = await _weatherForecastService.GetByCityId(cityId);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<WeatherForecast>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new ActionResponse<WeatherForecast>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                return BadRequest(new ActionResponse<WeatherForecast>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                    Data = result.Data,
                });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> RemoveCity(int cityId)
        {
            int UId = GetUId();


            if (UId != -1)
            {
                var result = await _userService.RemoveItem(UId, cityId);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<bool>
                    {
                        Code = result.StatusCode,
                        Message = result.Description,
                        Data = result.Data
                    });
                }

                return BadRequest(new ActionResponse<bool>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                    Data = result.Data,
                });
            }

            return Unauthorized();
        }
    }
}
