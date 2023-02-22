using AirBooker.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBooker.Data.EntityConfigurations
{
    public class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.FreeSeatsCount)
                .IsRequired();

            builder.Property(x => x.DepartureDateTime)
                .IsRequired();

            builder.Property(x => x.ArrivalDateTime)
                .IsRequired();

            builder.Property(x => x.TravelTime)
                .IsRequired();

            builder.HasOne(x => x.Airline)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.AirlineId);

            builder.HasOne(x => x.DepartureAirport)
                .WithMany(x => x.DepartureFlights)
                .HasForeignKey(x => x.DepartureAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.ArrivalAirport)
                .WithMany(x => x.ArrivalFlights)
                .HasForeignKey(x => x.ArrivalAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
