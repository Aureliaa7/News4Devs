using Microsoft.EntityFrameworkCore;
using News4Devs.Shared.Entities;
using News4Devs.Infrastructure.ModelConfigurations;

namespace News4Devs.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<SavedArticle> SavedArticles { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new ArticleConfiguration().Configure(modelBuilder.Entity<Article>());
            new ChatMessageConfiguration().Configure(modelBuilder.Entity<ChatMessage>());
        }
    }
}
