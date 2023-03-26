using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Entity;

namespace organizer_backend_NET.DAL.Repository
{
    public class WeatherUserRepository : IWeatherUserRepository
    {
        private readonly AppContextDb _db;

        public WeatherUserRepository(AppContextDb db)
        {
            _db = db;
        }

        public async Task<bool> Create(UserWeather entity)
        {
            await _db.WeatherUserDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserWeather entity)
        {
            _db.WeatherUserDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<UserWeather> Read() => _db.WeatherUserDB;

        public async Task<UserWeather> Update(UserWeather entity)
        {
            _db.WeatherUserDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
