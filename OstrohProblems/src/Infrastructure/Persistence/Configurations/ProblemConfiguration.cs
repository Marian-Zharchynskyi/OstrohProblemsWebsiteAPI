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

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(p => p.Location)
            .WithMany()
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ProblemStatus)
            .WithMany()
            .HasForeignKey(p => p.ProblemStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Problem)
            .HasForeignKey(c => c.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Problems)
            .UsingEntity(j => j.ToTable("fk_problem_categories"));
    }
}