using System.ComponentModel.DataAnnotations;

namespace BloggingAPI.Article;

public sealed record ArticleRequest
{
    public required IList<CategoryRequest> Categories { get; set; }

    [MinLength(100)]
    public required string Content { get; set; }

    [StringLength(100, MinimumLength = 20)]
    public required string Description { get; set; }

    [StringLength(100, MinimumLength = 5)]
    public required string Title { get; set; }
}