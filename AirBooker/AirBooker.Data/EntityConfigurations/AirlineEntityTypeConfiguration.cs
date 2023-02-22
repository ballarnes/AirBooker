using AirBooker.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBooker.Data.EntityConfigurations
{
    public class AirlineEntityTypeConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Rating)
                .IsRequired(false);

            builder.Property(x => x.Founded)
                .IsRequired(false);

            builder.Property(x => x.LogoFileName)
                .IsRequired(false);
        }
    }
}
