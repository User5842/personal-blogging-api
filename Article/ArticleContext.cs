using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Article;

public class ArticleContext(DbContextOptions<ArticleContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }
}