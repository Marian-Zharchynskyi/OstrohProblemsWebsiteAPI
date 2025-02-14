using System.Reflection;
using Domain.Comments;
using Domain.Locations;
using Domain.ProblemCategories;
using Domain.Problems;
using Domain.ProblemStatuses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ProblemCategory> ProblemCategories { get; set; }
        public DbSet<ProblemStatus> ProblemStatuses { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}