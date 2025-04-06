using Domain.ProblemCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProblemCategoryConfiguration : IEntityTypeConfiguration<ProblemCategory>
    {
        public void Configure(EntityTypeBuilder<ProblemCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(id => id.Value, value => new ProblemCategoryId(value))
                .IsRequired();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}