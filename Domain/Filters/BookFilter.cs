using Domain.Entites;
using Domain.Paginations;

namespace Domain.Filters;

public class BookFilter : ValidFilter
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public DateTime? FromPublishedDate { get; set; }
    public DateTime? ToPublishedDate { get; set; }
}
