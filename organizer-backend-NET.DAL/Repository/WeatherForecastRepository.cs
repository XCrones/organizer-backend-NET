using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Entity;

namespace organizer_backend_NET.DAL.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly AppContextDb _db;

        public WeatherForecastRepository(AppContextDb db)
        {
            _db = db;
        }

        public async Task<bool> Create(WeatherForecast entity)
        {
            await _db.ForecastDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(WeatherForecast entity)
        {
            _db.ForecastDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<WeatherForecast> Read() => _db.ForecastDB;

        public async Task<WeatherForecast> Update(WeatherForecast entity)
        {
            _db.ForecastDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
