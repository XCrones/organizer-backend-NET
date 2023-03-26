using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.DAL.Repository;

namespace organizer_backend_NET.Services
{
    public static class RepositoriesInit
    {
        public static void Init(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoRespository, TodoRepository>();
            builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            builder.Services.AddScoped<IWeatherUserRepository, WeatherUserRepository>();
        }
    }
}
