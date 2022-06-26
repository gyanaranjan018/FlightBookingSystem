using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<BookingDetail> BookingDetails { get; set; }

        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}
