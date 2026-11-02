using GameHub.Application.Interfaces;
using GameHub.Domain.Entities;
using GameHub.Domain.Repositories;
using Microsoft.VisualBasic;

namespace GameHub.Application.Services;

public class PlatformService : IPlatformService
{
    private readonly IPlatformRepository _repository;

    public PlatformService(IPlatformRepository repository)
    {
        _repository = repository;
    }

    public async Task<Platform?> GetPlatformAsync(int id) => await _repository.GetPlatformAsync(id);

    public async Task<ICollection<Platform>> GetPlatformsAsync() =>
        await _repository.GetPlatformsAsync();
}
