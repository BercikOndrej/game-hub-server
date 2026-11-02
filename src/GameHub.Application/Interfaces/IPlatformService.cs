using GameHub.Domain.Entities;

namespace GameHub.Application.Interfaces;

public interface IPlatformService
{
    public Task<Platform?> GetPlatformAsync(int id);

    public Task<ICollection<Platform>> GetPlatformsAsync();
}
