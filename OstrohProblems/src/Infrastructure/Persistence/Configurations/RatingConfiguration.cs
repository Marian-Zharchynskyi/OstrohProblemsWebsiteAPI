using Domain.Ratings;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => new RatingId(value))
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Points)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        
        builder.HasOne(r => r.Problem)
            .WithMany(p => p.Ratings) 
            .HasForeignKey(r => r.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.ProblemId, r.UserId }).IsUnique();
    }
}