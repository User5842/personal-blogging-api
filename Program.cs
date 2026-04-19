using BloggingAPI.Article;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArticleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ArticleDatabase")));

builder.Services.AddValidation();

var app = builder.Build();

app.MapArticleRoutes();

app.Run();