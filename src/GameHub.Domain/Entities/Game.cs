namespace GameHub.Domain.Entities;

public class Game
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? Released { get; set; }
    public string? BackgroundImage { get; set; }
    public float Rating { get; set; }
    public ICollection<Platform> Platforms { get; set; } = [];
    public ICollection<Genre> Genres { get; set; } = [];
}
