using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL;
using organizer_backend_NET.DAL.Interfaces.ITodo;
using organizer_backend_NET.DAL.Repository.Todo;
using organizer_backend_NET.Service.Services.TodoService;
using organizer_backend_NET.Service.Interfaces.ITodo;
using organizer_backend_NET.DAL.Interfaces.ICalendar;
using organizer_backend_NET.DAL.Repository.Calendar;
using organizer_backend_NET.Service.Interfaces.ICalendarService;
using organizer_backend_NET.Service.Services.CalendarService;

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
