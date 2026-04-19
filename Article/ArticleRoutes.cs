using System.Collections.Immutable;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Article;

public static class ArticleRoutes
{
    public static void MapArticleRoutes(this IEndpointRouteBuilder app)
    {
        var articleGroup = app.MapGroup("/articles");

        articleGroup.MapGet("/", GetAllArticles);
        articleGroup.MapGet("/{articleId}", GetArticleById);

        articleGroup.MapPost("/", CreateArticle);

        articleGroup.MapPut("/{articleId}", UpdateArticle);

        articleGroup.MapDelete("/{articleId}", DeleteArticle);
    }

    private static async Task<Created> CreateArticle(ArticleRequest article, ArticleContext db)
    {
        var articleToCreate = new Article
        {
            Categories = [.. article.Categories.Select(c => new Category
            {
                Name = c.Name
            })],
            Content = article.Content,
            Description = article.Description,
            PublishedAt = DateTime.Now,
            Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec placerat nunc quis tempor molestie. Quisque mollis risus ut arcu elementum mollis ut sed dolor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Proin risus dolor, venenatis ut odio vel, lobortis accumsan lorem. Maecenas ex elit, feugiat eu dolor eu, ullamcorper volutpat augue. Integer euismod ut justo in blandit. Quisque sed velit gravida, porta sapien id, fermentum nisl. Donec vel augue elit. Nam euismod risus lorem, a vehicula urna scelerisque eget. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis at."
        };

        var createdArticle = db.Add(articleToCreate);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/articles/{createdArticle.Entity.Id}");
    }

    private static async Task<Results<NotFound, NoContent>> DeleteArticle(int articleId, ArticleContext db)
    {
        var article = await db.Articles.FindAsync(articleId);

        if (article is null)
        {
            return TypedResults.NotFound();
        }

        db.Articles.Remove(article);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<Results<NotFound, Ok<Article>>> GetArticleById(int articleId, ArticleContext db)
    {
        var article = await db.Articles.FindAsync(articleId);

        return article is null
            ? TypedResults.NotFound()
            : TypedResults.Ok<Article>(article);
    }

    private static async Task<Ok<List<Article>>> GetAllArticles(ArticleContext db)
    {
        var articles = await db.Articles.ToListAsync();
        return TypedResults.Ok(articles);
    }

    private static async Task<Results<NotFound, Ok<Article>>> UpdateArticle(int articleId, ArticleRequest newArticle, ArticleContext db)
    {
        var article = await db.Articles.FindAsync(articleId);

        if (article is null)
        {
            return TypedResults.NotFound();
        }

        article.Categories = [.. newArticle.Categories.Select(c => new Category
        {
            Name = c.Name
        })];
        article.Content = newArticle.Content;
        article.Description = newArticle.Description;
        article.Title = newArticle.Title;

        await db.SaveChangesAsync();
        return TypedResults.Ok(article);
    }
}
