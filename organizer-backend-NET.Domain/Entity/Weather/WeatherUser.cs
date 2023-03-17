using organizer_backend_NET.Domain.Interfaces.IBaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity.Weather
{
    public class WeatherUser : IBaseEntity
    {
        public int Id { get; set; }

        public int Uid { get; set; }

        [Column(TypeName = "jsonb")]
        public List<CityWeather> cities { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }

    public class CityWeather
    {
        public int id { get; set; }

        public string name { get; set; }

        public string country { get; set; }
    }
}
