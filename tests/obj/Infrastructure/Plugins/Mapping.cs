using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Plugins;

public class Mapping : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<UserRole>();
    }
}