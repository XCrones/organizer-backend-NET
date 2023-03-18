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

        public DbSet<UserWeather> WeatherUserDB { get; set; }

        public DbSet<User> UserDB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => {

                entity.Property(e => e.UId);

                entity.Property(e => e.Name);
                entity.Property(e => e.Email);
                entity.Property(e => e.Password);
                entity.Property(e => e.UrlAvatar);

                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.DeleteAt);
            });

            modelBuilder.Entity<Calendar>(entity => {

                entity.Property(e => e.Id);

                entity.Property(e => e.UId);
                entity.Property(e => e.Name);
                entity.Property(e => e.EventStart);
                entity.Property(e => e.EventEnd);
                entity.Property(e => e.Description);
                entity.Property(e => e.Background);

                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.DeleteAt);
            });

            modelBuilder.Entity<Todo>(entity => {

                entity.Property(e => e.Id);

                entity.Property(e => e.UId);
                entity.Property(e => e.Name);
                entity.Property(e => e.Category);
                entity.Property(e => e.Priority);
                entity.Property(e => e.DeadLine);
                entity.Property(e => e.Status);
                entity.Property(e => e.Background);

                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.DeleteAt);
            });

            modelBuilder.Entity<UserWeather>(entity => {

                entity.Property(e => e.Id);

                entity.Property(e => e.UId);
                entity.Property(e => e.Cities);

                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.DeleteAt);
            });

            modelBuilder.Entity<WeatherForecast>(entity => {

                entity.Property(e => e.Id);

                entity.Property(e => e.cod);
                entity.Property(e => e.cnt);
                entity.Property(e => e.message);
                entity.Property(e => e.weather);
                entity.Property(e => e.city);

                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.DeleteAt);
            });
        }

    }
}
