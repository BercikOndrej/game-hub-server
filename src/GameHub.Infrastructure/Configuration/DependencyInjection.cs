using GameHub.Domain.Repositories;
using GameHub.Infrastructure.Data;
using GameHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameHub.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDbContext<AppDbContext>(o =>
            o.UseNpgsql(config.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IPlatformRepository, PlatformRepository>();
        return services;
    }
}
