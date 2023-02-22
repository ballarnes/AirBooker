using AirBooker.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBooker.Data.EntityConfigurations
{
    public class AirportEntityTypeConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.CityPhotoFileName)
                .IsRequired(false);

            builder.Property(x => x.AirportPhotoFileName)
                .IsRequired(false);

            builder.Property(x => x.Place)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
