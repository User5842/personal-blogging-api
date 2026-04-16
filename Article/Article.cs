namespace BloggingAPI.Article;

public sealed record Article
{
    public required IList<string> Categories { get; set; }
    public required string Content { get; set; }
    public required int Id { get; set; }
    public required string Description { get; set; }
    public required DateTime PublishedAt { get; set; }
    public required string Title { get; set; }
}