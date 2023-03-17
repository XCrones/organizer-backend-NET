namespace organizer_backend_NET.Domain.Entity.Weather
{
    public class WeatherItem
    {
        public int dt { get; set; }

        public Main main { get; set; }

        public List<Weather> weather { get; set; }

        public Clouds clouds { get; set; }

        public Wind wind { get; set; }

        public int visibility { get; set; }

        public int pop { get; set; }

        public Sys sys { get; set; }

        public string dt_txt { get; set; }
    }

    public class Main
    {
        public int temp { get; set; }

        public int humidity { get; set; }

        public int pressure { get; set; }

        public int temp_max { get; set; }

        public int temp_min { get; set; }

        public int sea_level { get; set; }

        public int feels_like { get; set; }

        public int grnd_level { get; set; }

        public int temp_kf { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }

        public string main { get; set; }

        public string description { get; set; }

        public string icon { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind 
    {
        public int speed { get; set; }

        public int deg { get; set; }

        public int gust { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }
}