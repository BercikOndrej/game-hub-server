using GameHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Infrastructure.Data.Configuration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasIndex(g => g.Id).IsUnique();

        builder.HasMany(g => g.Platforms).WithMany();
        builder.HasMany(g => g.Genres).WithMany();
    }
}
