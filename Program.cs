using BloggingAPI.Article;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArticleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ArticleDatabase")));

var app = builder.Build();

app.MapGet("/articles", async (ArticleContext db) =>
{
    var articles = await db.Articles.ToListAsync();
    return TypedResults.Ok(articles);
});

app.MapGet("/articles/{articleId}", async Task<Results<NotFound, Ok<Article>>> (int articleId, ArticleContext db) =>
{
    var article = await db.Articles.FindAsync(articleId);

    return article is null
        ? TypedResults.NotFound()
        : TypedResults.Ok<Article>(article);
});

app.MapDelete("/articles/{articleId}", async Task<Results<NotFound, Ok>> (int articleId, ArticleContext db) =>
{
    var article = await db.Articles.FindAsync(articleId);

    if (article is null)
    {
        return TypedResults.NotFound();
    }

    db.Articles.Remove(article);
    await db.SaveChangesAsync();

    return TypedResults.Ok();
});

app.MapPost("/articles", async (Article article, ArticleContext db) =>
{
    var createdArticle = db.Add(article);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/articles/{createdArticle.Entity.Id}");
});

app.MapPut("/articles/{articleId}", async Task<Results<Created, Ok<Article>>> (int articleId, Article newArticle, ArticleContext db) =>
{
    var article = await db.Articles.FindAsync(articleId);

    if (article is null)
    {
        var createdArticle = db.Add(newArticle);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/articles/{createdArticle.Entity.Id}");
    }

    article.Categories = newArticle.Categories;
    article.Content = newArticle.Content;
    article.Description = newArticle.Description;
    article.PublishedAt = newArticle.PublishedAt;
    article.Title = newArticle.Title;

    await db.SaveChangesAsync();
    return TypedResults.Ok(article);
});

app.Run();