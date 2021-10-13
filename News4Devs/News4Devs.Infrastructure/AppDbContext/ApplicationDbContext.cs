using Microsoft.EntityFrameworkCore;
using News4Devs.Core.Entities;
using News4Devs.Infrastructure.ModelConfigurations;

namespace News4Devs.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
