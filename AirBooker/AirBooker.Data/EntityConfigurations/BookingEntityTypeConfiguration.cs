using AirBooker.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBooker.Data.EntityConfigurations
{
    public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.isCanceled)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.FlightId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Flight)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.FlightId);
        }
    }
}
