using organizer_backend_NET.DAL.Interfaces;

namespace organizer_backend_NET.DAL.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppContextDb _db;

        public CalendarRepository(AppContextDb db)
        {
            _db = db;
        }

        public async Task<bool> Create(Domain.Entity.Calendar entity)
        {
            await _db.CalendarDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Domain.Entity.Calendar entity)
        {
            _db.CalendarDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<Domain.Entity.Calendar> Read() => _db.CalendarDB;

        public async Task<Domain.Entity.Calendar> Update(Domain.Entity.Calendar entity)
        {
            _db.CalendarDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
