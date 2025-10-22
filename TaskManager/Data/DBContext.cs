using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks {get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Category> Categories { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>().
                HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskItem>()
               .HasOne(t => t.Category)
               .WithMany(c => c.TaskItems)
               .HasForeignKey(t => t.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.ID)
                .IsUnique();

        }


    }
}
