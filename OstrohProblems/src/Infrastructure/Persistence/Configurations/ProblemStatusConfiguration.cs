using Domain.ProblemStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProblemStatusConfiguration : IEntityTypeConfiguration<ProblemStatus>
    {
        public void Configure(EntityTypeBuilder<ProblemStatus> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProblemStatusId(value))
                .IsRequired();

            builder.Property(ps => ps.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}