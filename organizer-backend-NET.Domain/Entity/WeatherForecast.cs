using organizer_backend_NET.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity
{
    [Table("WeatherForecasts")]
    public class WeatherForecast : IBaseEntity
    {
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("cod")]
        public string cod { get; set; }

        [Column("message")]
        public int message { get; set; }

        [Column("cnt")]
        public int cnt { get; set; }

        [Column("weather", TypeName = "jsonb")]
        public List<WeatherItem> weather { get; set; }

        [Column("city", TypeName = "jsonb")]
        public CityForecast city { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [Column("DeleteAt")]
        public DateTime? DeleteAt { get; set; }
    }


    public class CityForecast
    {
        public int id { get; set; }

        public string name { get; set; }

        public Coord coord { get; set; }

        public string country { get; set; }

        public int population { get; set; }

        public int timezone { get; set; }

        public int sunrise { get; set; }

        public int sunset { get; set; }
    }


    public class Coord
    {
        public int lat { get; set; }

        public int lon { get; set; }
    }

    public class WeatherItem
    {
        public int dt { get; set; }

        public Main main { get; set; }

        public List<Weather> weather { get; set; }

        public Clouds clouds { get; set; }

        public Wind wind { get; set; }

        public Rain rain { get; set; }

        public Snow show { get; set; }

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

    public class Snow
    {
        public int h1 { get; set; }

        public int h3 { get; set; }
    }

    public class Rain
    {
        public int h1 { get; set; }

        public int h3 { get; set; }
    }
}

