using Domain.Comments;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value,value => new CommentId(value))
            .IsRequired();

        builder.Property(p => p.ProblemId)
            .HasConversion(id => id.Value, value => new ProblemId(value))
            .IsRequired();

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasOne(c => c.Problem)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}