using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.Implements.Services;

namespace organizer_backend_NET.Inits
{
    public static class ServicesInit
    {
        public static void Init(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<ICalendarService, CalendarService>();
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
