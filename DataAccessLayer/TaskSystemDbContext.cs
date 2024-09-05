using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Task = DataAccessLayer.Entities.Task;

namespace DataAccessLayer;
public class TaskSystemDbContext : DbContext
{
    public TaskSystemDbContext(DbContextOptions<TaskSystemDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Username)
            .IsUnique();

            entity.HasIndex(u => u.Email)
            .IsUnique();
        });

    }

}
