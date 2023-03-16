using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Entity.Todo;

namespace organizer_backend_NET.DAL
{
    public class AppContextDb : DbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Todo> TodoDB { get; set; }
    }
}
