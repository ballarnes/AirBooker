using AirBooker.Data.Entities;
using AirBooker.Data.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AirBooker.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Airline> Airlines { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<AirlineAirport> AirlineAirport { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AirlineEntityTypeConfiguration());
            builder.ApplyConfiguration(new AirportEntityTypeConfiguration());
            builder.ApplyConfiguration(new BookingEntityTypeConfiguration());
            builder.ApplyConfiguration(new FlightEntityTypeConfiguration());

            builder.Entity<AirlineAirport>()
                .HasKey(x => new { x.AirlineId, x.AirportId });
        }
    }
}
