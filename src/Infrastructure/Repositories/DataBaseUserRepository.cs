using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class DataBaseUserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public DataBaseUserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<IUser?> GetUserByIdAsync(long id)
    {
        const string sql = """
                           select user_id, user_role, user_name, user_password
                           from users
                           where user_id = :id;
                           """;
        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", id);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
        if (!await reader.ReadAsync().ConfigureAwait(false))
        {
            return null;
        }

        return new AtmUser(
            reader.GetInt64(0),
            reader.GetFieldValue<UserRole>(1),
            reader.GetString(2),
            reader.GetString(3));
    }

    public async Task CreateUserAsync(AtmUser user)
    {
        Console.WriteLine("Добавление пользователя...");
        const string sql = """
                           INSERT INTO users (user_id, user_role, user_name, user_password)
                           VALUES (:id, :role, :name, :password);
                           """;
        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", user.Id)
            .AddParameter("role", user.Role)
            .AddParameter("name", user.Name)
            .AddParameter("password", user.Password);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<IUser?> GetUserByNameAsync(string name)
    {
        const string sql = """
                           select user_id, user_role, user_name, user_password
                           from users
                           where user_name = :name;
                           """;
        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("name", name);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
        if (!await reader.ReadAsync().ConfigureAwait(false))
        {
            return null;
        }

        return new AtmUser(
            reader.GetInt64(0),
            reader.GetFieldValue<UserRole>(1),
            reader.GetString(2),
            reader.GetString(3));
    }

    public async Task<IEnumerable<IUser>> GetAllUsersAsync()
    {
        const string sql = """
                           select user_id, user_role, user_name, user_password
                           from users;
                           """;

        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var users = new List<IUser>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            users.Add(new AtmUser(
                reader.GetInt64(0),
                reader.GetFieldValue<UserRole>(1),
                reader.GetString(2),
                reader.GetString(3)));
        }

        return users;
    }
}