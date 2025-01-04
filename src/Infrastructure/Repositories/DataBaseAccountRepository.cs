using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class DataBaseAccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _conntectionProvider;

    public DataBaseAccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _conntectionProvider = connectionProvider;
    }

    public async Task<Account?> GetAccountByIdAsync(long accountId)
    {
        const string sql = """
                           select account_id, account_name, account_balance, account_pin, user_id
                           from accounts_data
                           where account_id = :accountId;
                           """;
        using NpgsqlConnection connection = await _conntectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
        if (!await reader.ReadAsync().ConfigureAwait(false))
        {
            return null;
        }

        return new Account(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetInt32(2),
            reader.GetInt32(3),
            reader.GetInt64(4));
    }

    public async Task UpdateAccountMoneyAsync(Account account, int amount)
    {
        const string sql = """
                           update accounts_data
                           set account_balance = account_balance + :amount
                           where account_id = :accountId;
                           """;
        using NpgsqlConnection connection = await _conntectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("amount", amount)
            .AddParameter("accountId", account.Id);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task AddAccountAsync(Account account)
    {
        const string sql = """
                           INSERT INTO accounts_data (account_id, account_name, account_balance, account_pin, user_id)
                           values (:accountId, :accountName, :accountBalance, :accountPin, :user_id);
                           """;
        using NpgsqlConnection connection = await _conntectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", account.Id)
            .AddParameter("accountName", account.Name)
            .AddParameter("accountBalance", account.Balance)
            .AddParameter("accountPin", account.PinCode)
            .AddParameter("user_id", account.UserId);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}