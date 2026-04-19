using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Article;

public class ArticleContext(DbContextOptions<ArticleContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(article =>
        {
            article.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            article.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(100);

            article.Property(a => a.Content)
                .IsRequired();
        });
    }
}
