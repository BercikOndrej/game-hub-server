using GameHub.Domain.Entities;

namespace GameHub.Domain.Repositories;

public interface IPlatformRepository
{
    public Task<Platform?> GetPlatformAsync(int id);
    public Task<ICollection<Platform>> GetPlatformsAsync();
}
