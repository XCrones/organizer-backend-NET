using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL;
using organizer_backend_NET.DAL.Repository.Todo;
using organizer_backend_NET.DAL.Repository.Calendar;
using organizer_backend_NET.Service.Services;
using organizer_backend_NET.Service.Interfaces;
using organizer_backend_NET.DAL.Interfaces;

namespace organizer_backend_NET
{
    public static class Services
    {
        public static void InitRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoRespository, TodoRepository>();
            builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
        }

        public static void InitServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<ICalendarService, CalendarService>();
        }

        public static void InitDataBase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppContextDb>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("AppContextDb"))); //Integrated Security=true;Pooling=true;
        }
    }
}
