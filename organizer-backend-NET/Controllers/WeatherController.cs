using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Service.Interfaces;

namespace organizer_backend_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IActionResult> GetForecast(string name) {
            var result = await _weatherForecastService.SearchByName(name);

            return Ok(result);
        }
    }
}
