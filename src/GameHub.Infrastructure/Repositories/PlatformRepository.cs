using GameHub.Domain.Entities;
using GameHub.Domain.Repositories;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repositories;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _db;

    public PlatformRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Platform?> GetPlatformAsync(int id) =>
        await _db.Platforms.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<ICollection<Platform>> GetPlatformsAsync() =>
        await _db.Platforms.ToListAsync();
}
