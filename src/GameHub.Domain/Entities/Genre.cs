namespace GameHub.Domain.Entities;

public class Genre
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? ImageBackground { get; set; }
}
