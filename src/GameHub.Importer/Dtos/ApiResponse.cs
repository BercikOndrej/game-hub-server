namespace GameHub.Importer.Dtos;

public record ApiResponse<T>(int Count, string? Next, string? Previous, List<T> Results);
