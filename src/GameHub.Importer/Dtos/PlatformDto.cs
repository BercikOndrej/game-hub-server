namespace GameHub.Importer.Dtos;

public record ParentPlatformDto(
    int Id,
    string Name,
    string Slug,
    ICollection<PlatformDto> Platforms
);

public record PlatformDto(int Id, string Name, string Slug);
