using GameHub.Domain.Entities;

namespace GameHub.Importer.Dtos;

public record GameDto(
    int Id,
    string Slug,
    string Name,
    DateOnly Released,
    string BackgroundImage,
    float Rating,
    List<PlatformEntry> Platforms,
    List<Genre> Genres
);
