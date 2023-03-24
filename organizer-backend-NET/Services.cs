using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL;
using organizer_backend_NET.Implements.Services;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.DAL.Repository;

namespace organizer_backend_NET
{
    public static class Services
    {
        public static void InitRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoRespository, TodoRepository>();
            builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InitServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<ICalendarService, CalendarService>();
            builder.Services.AddScoped<IUserService, UserService>();
        }

        public static void InitDataBase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppContextDb>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("AppContextDb"))); //Integrated Security=true;Pooling=true;
        }
    }
}
