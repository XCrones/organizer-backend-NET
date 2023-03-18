using organizer_backend_NET.DAL.Interfaces.ITodo;

namespace organizer_backend_NET.DAL.Repository.Todo
{
    public class TodoRepository : ITodoRespository
    {
        private readonly AppContextDb _db;

        public TodoRepository(AppContextDb db)
        {
            _db = db;
        }

        public async Task<bool> Create(Domain.Entity.Todo entity)
        {
            await _db.TodoDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<Domain.Entity.Todo> Read() => _db.TodoDB;

        public async Task<Domain.Entity.Todo> Update(Domain.Entity.Todo entity)
        {
            _db.TodoDB.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Domain.Entity.Todo entity)
        {
            _db.TodoDB.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
