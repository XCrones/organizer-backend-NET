using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL;
using organizer_backend_NET.Implements.Services;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static void InitJWTAuth(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

            var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
            var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
            var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
            var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = signinkey,
                    ValidateIssuerSigningKey = true,
                };
            });
        }
    }
}
