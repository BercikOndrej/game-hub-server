using GameHub.Application.Interfaces;
using GameHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameHub.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPlatformService, PlatformService>();
        return services;
    }
}
