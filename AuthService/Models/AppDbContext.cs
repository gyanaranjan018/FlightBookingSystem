using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace AuthService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    Id= Guid.Parse("42c3d7b2-7ca6-44fb-a240-828a78ff01f6"),
                    Username = "admin",
                    Password = "admin",
                    Role = UserRole.Admin,
                }
            });
        }
        public DbSet<User> Users { get; set; }
    }
}
