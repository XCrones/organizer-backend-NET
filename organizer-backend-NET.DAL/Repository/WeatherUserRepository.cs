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

        public async Task<bool> Create(WeatherUsers entity)
        {
            await _db.WeatherUserDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(WeatherUsers entity)
        {
            _db.WeatherUserDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<WeatherUsers> Read() => _db.WeatherUserDB;

        public async Task<WeatherUsers> Update(WeatherUsers entity)
        {
            _db.WeatherUserDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
