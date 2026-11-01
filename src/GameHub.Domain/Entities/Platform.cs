namespace GameHub.Domain.Entities;

public class Platform
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
}
