using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new ProblemId(value))
            .IsRequired();

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Longitude)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.Latitude)
            .IsRequired()
            .HasMaxLength(50);
    }
}