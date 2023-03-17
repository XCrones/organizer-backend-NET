using organizer_backend_NET.Domain.Interfaces.IBaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity.Weather
{
    public class Forecast : IBaseEntity
    {
        public int Id { get; set; }

        public string cod { get; set; }

        public int message { get; set; }

        public int cnt { get; set; }

        [Column(TypeName = "jsonb")]
        public List<WeatherItem> weather { get; set; }

        [Column(TypeName = "jsonb")]
        public City city { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }


    public class City 
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
}

