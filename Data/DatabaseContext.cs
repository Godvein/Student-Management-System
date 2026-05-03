using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentMS.Models;

namespace StudentMS.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between User and Role
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "RoleId");
                    j.ToTable("UserRoles");
                }
                );

            // predefine some roles
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "User" }
                );

            // seed some users with roles
            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserId = 1, Username = "admin", Password = "$2a$11$xbic0fNNc1WZnUFzE0S8k.9E81QD6tqGwGPss3IVSphhU83hy1YvG"}
                );

            // seed the user-role relationship
            modelBuilder.Entity("UserRoles").HasData(
                new { UserId = 1, RoleId = 1 }
            );

        }
    }
}
