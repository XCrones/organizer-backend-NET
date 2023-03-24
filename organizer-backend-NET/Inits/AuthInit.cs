using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using organizer_backend_NET.Settings;
using System.Text;

namespace organizer_backend_NET.Inits
{
    public static class AuthInit
    {
        public static void Init(this WebApplicationBuilder builder)
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
