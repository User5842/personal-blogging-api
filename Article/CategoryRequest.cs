namespace BloggingAPI.Article;

public sealed record CategoryRequest
{
    public required string Name { get; set; }
}