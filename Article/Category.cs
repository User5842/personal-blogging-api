namespace BloggingAPI.Article;

public sealed record Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
