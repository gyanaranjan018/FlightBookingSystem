using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>().HasData(
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Biju Patnaik International Airport", City = "Bhubaneswar" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Chhatrapati Shivaji Maharaj International Airport", City = "Mumbai" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Kempegowda International Airport", City = "Bengaluru" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Chennai International Airport", City = "Chennai" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Rajiv Gandhi International Airport", City = "Hyderabad" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Netaji Subhas Chandra Bose International Airport", City = "Kolkata" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Indira Gandhi International Airport", City = "Delhi" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Cochin International Airport", City = "Kochi" },
                new Airport { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Sardar Vallabhbhai Patel International Airport", City = "Ahmedabad" }
                );

            foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<Airline> Airlines { get; set; }

        public DbSet<AirlineInventory> AirlineInventories { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }
    }
}
