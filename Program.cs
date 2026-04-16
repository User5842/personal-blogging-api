using BloggingAPI.Article;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArticleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ArticleDatabase")));

var app = builder.Build();

app.MapGet("/articles", async (ArticleContext db) =>
    await db.Articles.ToListAsync());

app.MapPost("/articles", async (Article article, ArticleContext db) =>
{
    var createdArticle = db.Add(article);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/articles/{createdArticle.Entity.Id}");
});

app.Run();