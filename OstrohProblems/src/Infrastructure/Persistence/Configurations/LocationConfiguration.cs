using Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasConversion(
                    id => id.Value,
                    value => new LocationId(value))
                .IsRequired();

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(l => l.Latitude)
                .IsRequired();

            builder.Property(l => l.Longitude)
                .IsRequired();
        }
    }
}