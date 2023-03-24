using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL;

namespace organizer_backend_NET.Inits
{
    public static class DatabasesInit
    {
        public static void Init(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppContextDb>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("AppContextDb"))); //Integrated Security=true;Pooling=true;
        }
    }
}
