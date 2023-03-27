using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Entity;

namespace organizer_backend_NET.DAL
{
    public class AppContextDb : DbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }

        public DbSet<Todo> TodoDB { get; set; }

        public DbSet<Calendar> CalendarDB { get; set; }

        public DbSet<WeatherForecast> ForecastDB { get; set; }

        public DbSet<WeatherUsers> WeatherUserDB { get; set; }

        public DbSet<User> UserDB { get; set; }
    }
}
