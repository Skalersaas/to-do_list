using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Models.Task> Tasks { get; set; } 
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Group> Groups { get; set; }
        public void Restart()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Models.User>(user =>
            {
                user.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                user.HasMany(p => p.Tasks)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(p => p.Groups)
                    .WithOne(g => g.User)
                    .HasForeignKey(g => g.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Models.Group>(group =>
            {
                group.Property(g => g.Id)
                     .ValueGeneratedOnAdd();

                group.HasMany(g => g.Tasks)
                     .WithOne(t => t.Group)
                     .HasForeignKey(t => t.GroupId)
                     .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Models.Task>(task =>
            {
                task.Property(t => t.Id)
                    .ValueGeneratedOnAdd();
            });
        }
        public bool UserExists(string Login)
            => GetUser(Login) != null;

        public User? GetUser(string Login)
            => Users.FirstOrDefault(user => user.Login == Login);
    }
}
