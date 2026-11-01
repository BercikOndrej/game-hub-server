using System.Net.Http.Json;
using System.Text.Json;
using GameHub.Domain.Entities;
using GameHub.Importer.Dtos;
using GameHub.Infrastructure.Configuration;
using GameHub.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

const string BASE_URL = "https://api.rawg.io/api/";
const string GENRES_URL = "genres";
const string GAMES_URL = "games";
const string PARENT_PLATFORMS_URL = "platforms/lists/parents";
const int TARGET_GAMES_COUNT = 1_000;
const int PAGE_SIZE = 40;

JsonSerializerOptions jsonOptions = new()
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
};

HttpClient client = new() { BaseAddress = new Uri(BASE_URL) };

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

string? apiKey =
    configuration["ApiKeys:Rawg"]
    ?? throw new InvalidOperationException("Secret for 'ApiKeys:Rawg' is not set.");

// Database
builder.Services.AddInfrastructure(configuration);

using var host = builder.Build();
using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

// Main
if (db.Games.Any())
{
    return 0;
}

var parentPlatformDtos = await GetItems<ParentPlatformDto>(PARENT_PLATFORMS_URL);
var genres = await GetItems<Genre>(GENRES_URL);
var gameDtos = await GetItems<GameDto>(GAMES_URL);

var platformDictionary = new Dictionary<int, Platform>();
var genresDictionary = new Dictionary<int, GameHub.Domain.Entities.Genre>();

var games = gameDtos
    .DistinctBy(g => g.Id)
    .Select(g =>
    {
        var genres = g
            .Genres.Select(genre =>
            {
                if (genresDictionary.TryGetValue(genre.Id, out Genre? value))
                {
                    return value;
                }
                var newGenre = new Genre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Slug = genre.Slug,
                    ImageBackground = genre.ImageBackground,
                };
                genresDictionary[newGenre.Id] = newGenre;
                return newGenre;
            })
            .ToList();

        var platforms = g
            .Platforms.Select(p =>
            {
                ParentPlatformDto? parent = FindParentPlatform(parentPlatformDtos, p.Platform);
                if (parent is null)
                {
                    return null;
                }
                if (platformDictionary.TryGetValue(parent.Id, out Platform? plat))
                {
                    return plat;
                }
                var newPlat = new Platform
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Slug = parent.Slug,
                };
                platformDictionary[newPlat.Id] = newPlat;
                return newPlat;
            })
            .OfType<Platform>()
            .DistinctBy(p => p.Id)
            .ToList();

        return new Game
        {
            Id = g.Id,
            Name = g.Name,
            Slug = g.Slug,
            Genres = genres,
            Platforms = platforms,
        };
    })
    .ToList();

await db.Games.AddRangeAsync(games);
await db.SaveChangesAsync();

// Save Rest
var platformsRest = parentPlatformDtos
    .Select(dto => new Platform
    {
        Id = dto.Id,
        Name = dto.Name,
        Slug = dto.Slug,
    })
    .Where(p => !platformDictionary.ContainsKey(p.Id))
    .ToList();

var genresRest = genres.Where(g => !genresDictionary.ContainsKey(g.Id)).ToList();

await db.Platforms.AddRangeAsync(platformsRest);
await db.Genres.AddRangeAsync(genresRest);
await db.SaveChangesAsync();

return 0;

// Functions
async Task<List<T>> GetItems<T>(string url)
    where T : class
{
    List<T> items = [];
    int page = 1;
    string paramUrl = url + $"?key={apiKey}&page_size={PAGE_SIZE}&page={page}";

    while (items.Count() < TARGET_GAMES_COUNT)
    {
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<T>>(paramUrl, jsonOptions);
            if (response is null)
            {
                break;
            }

            items.AddRange(response.Results);

            if (response.Next is null)
            {
                break;
            }
            page++;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Chyba při získávání dat requestu: url: {paramUrl}, page: {page}. Message {ex.Message}"
            );
            break;
        }
    }

    return items;
}

ParentPlatformDto? FindParentPlatform(
    ICollection<ParentPlatformDto> parentPlatforms,
    PlatformDto platform
)
{
    foreach (var parent in parentPlatformDtos)
    {
        if (parent.Platforms.Contains(platform))
        {
            return parent;
        }
    }

    return null;
}
