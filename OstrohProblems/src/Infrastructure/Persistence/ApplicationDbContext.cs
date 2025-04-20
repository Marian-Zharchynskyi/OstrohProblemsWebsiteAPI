using System.Reflection;
using Domain.Comments;
using Domain.ProblemCategories;
using Domain.ProblemRatings;
using Domain.Problems;
using Domain.ProblemStatuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ProblemCategory> ProblemCategories { get; set; }
        public DbSet<ProblemStatus> ProblemStatuses { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProblemRating> ProblemRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}