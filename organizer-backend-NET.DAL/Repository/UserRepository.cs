using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Entity;

namespace organizer_backend_NET.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppContextDb _db;

        public UserRepository(AppContextDb db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            await _db.UserDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(User entity)
        {
            _db.UserDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<User> Read() => _db.UserDB;

        public async Task<User> Update(User entity)
        {
            _db.UserDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
