
using organizer_backend_NET.Domain.Entity;

namespace organizer_backend_NET.Domain.ViewModel
{
    public class ForecastViewModel
    {
        public string cod { get; set; }

        public int message { get; set; }

        public int cnt { get; set; }

        public List<WeatherItem> list { get; set; }

        public CityForecast city { get; set; }
    }
}
