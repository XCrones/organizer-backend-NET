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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>(entity => {
                entity.ToTable("Todo");

                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50);
                entity.Property(e => e.Category).HasColumnName("Category").HasMaxLength(50);
                entity.Property(e => e.Priority).HasColumnName("Priority");
                entity.Property(e => e.DeadLine).HasColumnName("DeadLine");
                entity.Property(e => e.Status).HasColumnName("Status");
                entity.Property(e => e.Background).HasColumnName("Background").HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
                entity.Property(e => e.DeleteAt).HasColumnName("DeleteAt");
            });

        }

    }
}
